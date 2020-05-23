using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Sharp317
{
	public class server
	{
		public static int uptime;
		public static NpcAnimHandler NpcAnimHandler = new NpcAnimHandler();
		public static List<String> banned = new List<String>();
		public static WeaponHandler WeaponHandler = null;
		public static List<Int32> bannedUid = new List<Int32>();
		public static server clientHandler = null; // handles all the clients
		public static Socket clientListener = null;
		public static int MaxConnections = 1000;
		public static int[] ConnectionCount = new int[MaxConnections];
		public static List<String> connections = new List<String>();
		public static String[] Connections = new String[MaxConnections];
		public static int cycleTime = 500;
		public static int delay = 500;
		public static long delayUpdate = 0, lastRunite = 0;
		public static DoorHandler doorHandler;
		public static int EnergyRegian = 60;
		public static Boolean enforceClient = false;
		public static GraphicsHandler GraphicsHandler = null;
		public static ItemHandler itemHandler = null;
		public static Boolean loginServerConnected = true;
		public static NPCHandler npcHandler = null;
		public static ObjectHandler objectHandler = null;
		public static List<Object> objects = new List<Object>();
		public static lvlHandler lvlHandler = new lvlHandler();
		public static PlayerHandler playerHandler = null;
		public static special special = null;
		public static int[][] runesRequired = new int[24][]; // 9
		public static int serverlistenerPort = 43594;
		public static ShopHandler shopHandler = null;
		public static Boolean ShutDown = false;
		public static Boolean shutdownClientHandler;
		public static int ShutDownCounter = 0;
		public static Boolean shutdownServer = false;
		public static long startTime;
		public static Boolean trading = true, dueling = true, pking = true;
		public static int updateSeconds = 180;
		public static Boolean updateServer = false;
		public static int world = 1;

		public static void calcTime( )
		{
			long curTime = TimeHelper.CurrentTimeMillis();
			updateSeconds = 180 - ( ( int ) ( curTime - startTime ) / 1000 );
			if ( updateSeconds == 0 )
			{
				shutdownServer = true;
			}
		}

		public static void logError( String message )
		{
			misc.println( message );
		}

		public static void main( params String[] args )
		{
			GraphicsHandler = new GraphicsHandler( );
			try 
			{
				var serverIni = "server.json";

				if ( !File.Exists( serverIni ) ) 
				{
					misc.println( "server.json doesn't exist!" );
				}

				var settings = JsonSerializer.Deserialize<Settings>( File.ReadAllText( serverIni ) );
				
				int client = settings.ClientRequired;
				world = settings.WorldId;
				serverlistenerPort = settings.ServerPort;

				if (client > 0) 
				{
					misc.println( "Enforcing Sharp317 client requirement" );
					enforceClient = true;
				}
			} 
			catch (Exception e) 
			{
				misc.println("Error loading settings");
				//e.printStackTrace();
			}
			WeaponHandler = new WeaponHandler( );	
			(new Thread( new ThreadStart( RunServer ) ) ).Start( ); // launch server listener
			playerHandler = new PlayerHandler( );
			npcHandler = new NPCHandler( );
			itemHandler = new ItemHandler( );
			special = new special( );
			doorHandler = new DoorHandler( );
			if (itemHandler == null) {
				misc.println("ERROR NULL");
			}
			shopHandler = new ShopHandler( );
			objectHandler = new ObjectHandler( );
			GraphicsHandler = new GraphicsHandler( );

			new Thread( Program.Process ).Start();
			//process proc = new process();
			//new Thread( proc).start( );		
		}

		private static void RunServer()
		{
			clientHandler = new server();
			clientHandler.run();
		}

		//public static void openPage( String pageName )
		//{
		//	try
		//	{
		//		URL page = new URL( pageName );
		//		URLConnection conn = page.openConnection();
		//		DataInputStream in = new DataInputStream( conn.getInputStream() );
		//		String source, pageSource = "";
		//		while ( ( source = in.readLine()) != null) {
		//			pageSource += source;
		//		}
		//	}
		//	catch ( Exception e )
		//	{
		//		//e.printStackTrace();
		//		return;
		//	}
		//}

		public int[] ips = new int[1000];

		public long[] lastConnect = new long[1000];

		public server( )
		{
			// the current way of controlling the server at runtime and a great
			// debugging/testing tool
			// jserv js = new jserv(this);
			// js.start();

		}

		public void banHost( String host, int num )
		{
			if ( false )
			{
				banned.Add( host );
			}
			else
			{
				try
				{
					misc.println( "BANNING HOST " + host + " (flooding)" );
					banned.Add( host );
					delay = 2000;
					delayUpdate = TimeHelper.CurrentTimeMillis() + 60000;
				}
				catch ( Exception e )
				{
					//e.printStackTrace();
				}
			}

		}

		public Boolean checkHost( String host )
		{
			foreach ( String h in banned )
			{
				if ( h.Equals( host ) )
					return false;
			}
			int num = 0;
				foreach ( String h in connections )
			{
				if ( host.Equals( h ) )
				{
					num++;
				}
			}
			if ( num > 5 )
			{
				//banHost( host, num );
				//return false;
			}

			if ( checkLog( "ipbans", host ) )
			{
				//Console.WriteLine("They are in ip ban list!");
				return false; // ip ban added by bakatool
			}
			return true;
		}

		public Boolean checkLog( String file, String playerName )
		{
			// check ipbans -bakatool
			try
			{
				var textReader = File.OpenText( "config//" + file + ".txt" );
				String data = null;
				while ( ( data = textReader.ReadLine()) != null)
				{
					if ( playerName.ToLower() == data.ToLower() )
					{
						return true;
					}
				}
			}
			catch ( IOException e )
			{
				Console.WriteLine( "Critical error while checking for data!" );
				Console.WriteLine( file + ":" + playerName );
				//e.printStackTrace();
			}
			return false;
		}

		public int getConnections( String host )
		{
			int count = 0;
			foreach ( Player p in PlayerHandler.players )
			{
				if ( ( p != null ) && !p.disconnected
						&& p.connectedFrom.ToLower() == host.ToLower() )
				{
					count++;
				}
			}
			return count;
		}

		public void killServer( )
		{
			try
			{
				shutdownClientHandler = true;
				if ( clientListener != null )
					clientListener.Close();
				clientListener = null;
			}
			catch ( Exception __ex )
			{
				//__ex.printStackTrace();
			}
		}

		public void run( )
		{
			// setup the listener
			try
			{
				shutdownClientHandler = false;

				var ipAddress = IPAddress.Parse( "127.0.0.1" );
				var localEndPoint = new IPEndPoint( ipAddress, serverlistenerPort );

				clientListener = new Socket( ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp );
				clientListener.Bind( localEndPoint );
				clientListener.Listen( 100 );

				while ( true )
				{
					try
					{
						var s = clientListener.Accept();
						s.NoDelay = true;

						IPEndPoint remoteIpEndPoint = s.RemoteEndPoint as IPEndPoint;

						IPHostEntry hostEntry = Dns.GetHostEntry( remoteIpEndPoint.Address );

						String connectingHost = hostEntry.HostName;
						if ( /*
							 * connectingHost.StartsWith("localhost") ||
							 * connectingHost.equals("127.0.0.1")
							 */true )
						{
							if ( connectingHost.StartsWith( "izar.lunarpages.com" )
									|| connectingHost.StartsWith( "server2" )
									|| connectingHost.StartsWith( "dodian.com" )
									|| connectingHost
											.StartsWith( "newgamersworld.com" )
									|| connectingHost.StartsWith( "sputnik" )
									|| connectingHost.StartsWith( "sugardaddy" ) )
							{
								misc.println( "Checking Server Status..." );
								s.Close();
							}
							else
							{
								connections.Add( connectingHost );
								if ( checkHost( connectingHost ) )
								{
									//misc.println("Connection from "
									//		+ connectingHost + ":" + s.getPort());
									playerHandler
											.newPlayerClient( clientListener, s, connectingHost );
								}
								else
								{
									//misc.println("ClientHandler: Rejected "
									//		+ connectingHost + ":" + s.getPort());
									s.Close();
								}
							}
						}
						else
						{
							//misc.println("ClientHandler: Rejected "
							//		+ connectingHost + ":" + s.getPort());
							//s.close();
						}
						if ( ( delayUpdate > 0 )
								&& ( TimeHelper.CurrentTimeMillis() > delayUpdate ) )
						{
							delay = 500;
							delayUpdate = 0;
						}
						Thread.Sleep( delay );
					}
					catch ( Exception e )
					{
						logError( e.ToString() );
					}
				}
			}
			catch ( Exception ioe )
			{
				if ( !shutdownClientHandler )
				{
					misc.println( "Server is already in use." );
				}
				else
				{
					misc.println( "ClientHandler was shut down." );
				}
			}
		}
	}
}
