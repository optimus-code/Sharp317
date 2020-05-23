using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharp317
{
	public class ObjectHandler
	{
		/* FIREMAKING */
		public static int FireDelay = 80; // 80 * 500 = 40000 / 1000 = 40s
		public static int FireGianDelay = 10; // 10 * 500
											  // = 5000 /
											  // 1000 = 5s

		public static int MaxObjects = 100000;
		// process() is called evry 500 ms
		public static int MaxOpenDelay = 120; // 120 * 500 = 60000 / 1000 = 60s
		public static int[] ObjectDelay = new int[MaxObjects];
		public static int[] ObjectFace = new int[MaxObjects];
		public static int[] ObjectFireDelay = new int[MaxObjects];
		public static int[] ObjectFireDeletecount = new int[MaxObjects];
		public static int[] ObjectFireH = new int[MaxObjects];
		public static int[] ObjectFireID = new int[MaxObjects];
		public static int[] ObjectFireMaxDelay = new int[MaxObjects];
		public static int[] ObjectFireX = new int[MaxObjects];
		public static int[] ObjectFireY = new int[MaxObjects];
		public static int[] ObjectH = new int[MaxObjects];
		public static int[] ObjectID = new int[MaxObjects];
		public static Boolean[] ObjectOpen = new Boolean[MaxObjects];
		public static int[] ObjectOriFace = new int[MaxObjects];
		public static int[] ObjectOriID = new int[MaxObjects];
		public static Boolean[] ObjectOriOpen = new Boolean[MaxObjects];
		public static int[] ObjectOriType = new int[MaxObjects];
		public static int[] ObjectType = new int[MaxObjects];
		public static int[] ObjectX = new int[MaxObjects];
		public static int[] ObjectY = new int[MaxObjects];

		public ObjectHandler( )
		{
			for ( int i = 0; i < MaxObjects; i++ )
			{
				ObjectID[i] = -1;
				ObjectX[i] = -1;
				ObjectY[i] = -1;
				ObjectH[i] = -1;
				ObjectDelay[i] = 0;
				ObjectOriType[i] = 1;
				ObjectType[i] = 1;
				ObjectOriFace[i] = 0;
				ObjectFace[i] = 0;
				ObjectOriOpen[i] = false;
				ObjectOpen[i] = false;
				ResetFire( i );
			}
			loadObjects( "objects.cfg" );
			// loadCustomObjects("preload-objects.cfg");
		}

		/* FIREMAKING */
		public void firemaking_process( )
		{
			for ( int i = 0; i < MaxObjects; i++ )
			{
				if ( ObjectFireID[i] > -1 )
				{
					if ( ObjectFireDelay[i] < ObjectFireMaxDelay[i] )
					{
						ObjectFireDelay[i]++;
					}
					else
					{
						for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
						{
							if ( PlayerHandler.players[j] != null )
							{
								PlayerHandler.players[j].FireDelete[i] = true;
							}
						}
					}
				}
			}
		}

		public Boolean loadCustomObjects( String FileName )
		{
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[10];
			Boolean EndOfFile = false;
			int ReadMode = 0;
			TextReader characterfile = null;
			try
			{
				characterfile = File.OpenText( "config\\" + FileName );
			}
			catch ( FileNotFoundException fileex )
			{
				misc.println( FileName + ": file not found." );
				return false;
			}
			try
			{
				line = characterfile.ReadLine();
			}
			catch ( IOException ioexception )
			{
				misc.println( FileName + ": error loading file." );
				return false;
			}
			while ( ( EndOfFile == false ) && ( line != null ) )
			{
				line = line.Trim();
				int spot = line.IndexOf( "=" );
				if ( spot > -1 )
				{
					token = line.Substring( 0, spot );
					token = token.Trim();
					token2 = line.Substring( spot + 1 );
					token2 = token2.Trim();
					token2_2 = token2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token3 = token2_2.Split( "\t" );
					if ( token.Equals( "object" ) )
					{
						for ( int i = 0; i < MaxObjects; i++ )
						{
							if ( ObjectID[i] == -1 )
							{
								ObjectOriID[i] = Int32.Parse( token3[0] );
								ObjectID[i] = Int32.Parse( token3[0] );
								ObjectX[i] = Int32.Parse( token3[1] );
								ObjectY[i] = Int32.Parse( token3[2] );
								ObjectH[i] = Int32.Parse( token3[3] );
								ObjectOriFace[i] = Int32.Parse( token3[4] );
								ObjectFace[i] = Int32.Parse( token3[4] );
								ObjectOriType[i] = Int32.Parse( token3[5] );
								ObjectType[i] = Int32.Parse( token3[5] );
								if ( token3[6].Equals( "true" ) )
								{
									ObjectOriOpen[i] = true;
									ObjectOpen[i] = true;
								}
								break;
							}
						}
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFOBJECTLIST]" ) )
					{
						try
						{
							characterfile.Close();
						}
						catch ( IOException ioexception )
						{
						}
						return true;
					}
				}
				try
				{
					line = characterfile.ReadLine();
				}
				catch ( IOException ioexception1 )
				{
					EndOfFile = true;
				}
			}
			try
			{
				characterfile.Close();
			}
			catch ( IOException ioexception )
			{
			}
			return false;
		}

		public Boolean loadObjects( String FileName )
		{
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[10];
			Boolean EndOfFile = false;
			int ReadMode = 0;
			TextReader characterfile = null;
			try
			{
				characterfile = File.OpenText( "config\\" + FileName );
			}
			catch ( FileNotFoundException fileex )
			{
				misc.println( FileName + ": file not found." );
				return false;
			}
			try
			{
				line = characterfile.ReadLine();
			}
			catch ( IOException ioexception )
			{
				misc.println( FileName + ": error loading file." );
				return false;
			}
			while ( ( EndOfFile == false ) && ( line != null ) )
			{
				line = line.Trim();
				int spot = line.IndexOf( "=" );
				if ( spot > -1 )
				{
					token = line.Substring( 0, spot );
					token = token.Trim();
					token2 = line.Substring( spot + 1 );
					token2 = token2.Trim();
					token2_2 = token2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token3 = token2_2.Split( "\t" );
					if ( token.Equals( "object" ) )
					{
						server.objects.Add( new Object( Int32.Parse( token3[0] ),
								Int32.Parse( token3[1] ), Int32.Parse( token3[2] ), Int32.Parse( token3[3] ) ) );
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFOBJECTLIST]" ) )
					{
						try
						{
							characterfile.Close();
						}
						catch ( IOException ioexception )
						{
						}
						return true;
					}
				}
				try
				{
					line = characterfile.ReadLine();
				}
				catch ( IOException ioexception1 )
				{
					EndOfFile = true;
				}
			}
			try
			{
				characterfile.Close();
			}
			catch ( IOException ioexception )
			{
			}
			return false;
		}

		public static void process( )
		{
			for ( int i = 0; i < MaxObjects; i++ )
			{
				if ( ObjectID[i] > -1 )
				{
					if ( ObjectDelay[i] > 0 )
					{
						ObjectDelay[i]--;
					}
					if ( ObjectDelay[i] == 0 )
					{
						if ( ObjectOpen[i] != ObjectOriOpen[i] )
						{
							for ( int j = 0; j < PlayerHandler.maxPlayers; j++ )
							{
								if ( PlayerHandler.players[j] != null )
								{
									PlayerHandler.players[j].ChangeDoor[i] = true;
								}
							}
							ObjectOpen[i] = ObjectOriOpen[i];
						}
					}
				}
			}
		}

		public void ResetFire( int ArrayID )
		{
			ObjectFireID[ArrayID] = -1;
			ObjectFireX[ArrayID] = -1;
			ObjectFireY[ArrayID] = -1;
			ObjectFireH[ArrayID] = -1;
			ObjectFireDelay[ArrayID] = 0;
			ObjectFireMaxDelay[ArrayID] = 0;
		}
	}

}
