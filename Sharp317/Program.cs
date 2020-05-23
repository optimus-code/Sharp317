using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sharp317
{
	class Program
	{
		public static int cycleTime = 500;

		[STAThread]
		static void Main()
		{
			server.main();		
		}

		public static void Process()
		{
			int waitFails = 0;
			long lastTicks = TimeHelper.CurrentTimeMillis();
			long totalTimeSpentProcessing = 0;
			int cycle = 0;
			while ( !server.shutdownServer )
			{
				try
				{
					if ( server.updateServer )
						server.calcTime();
					// could do game updating stuff in here...
					// maybe do all the major stuff here in a big loop and just do
					// the packet
					// sending/receiving in the client sub-threads. The actual packet
					// forming code
					// will reside within here and all created packets are then
					// relayed by the sub-threads.
					// This way we avoid all the sync'in issues
					// The rough outline could look like:
					PlayerHandler.process(); // updates all player related
					server.uptime++;                            // stuff
					server.npcHandler.process();
					ItemHandler.process();
					server.shopHandler.process();
					ObjectHandler.process();
					// misc.println("objectHandler(f) process...");
					// server.objectHandler.firemaking_process();
					// doNpcs() // all npc related stuff
					// doObjects()
					// doWhatever()

					// taking into account the time spend in the processing code for
					// more accurate timing
					long timeSpent = TimeHelper.CurrentTimeMillis() - lastTicks;
					totalTimeSpentProcessing += timeSpent;
					if ( timeSpent >= cycleTime )
					{
						timeSpent = cycleTime;
						if ( ++waitFails > 100 )
						{
							// shutdownServer = true;
							// misc.println("[KERNEL]: machine is too slow to run
							// this server!");
						}
					}
					try
					{
						Thread.Sleep( ( Int32 ) ( cycleTime - timeSpent ) );
					}
					catch ( Exception _ex )
					{
					}
					lastTicks = TimeHelper.CurrentTimeMillis();
					cycle++;
					if ( cycle % 100 == 0 )
					{
						float time = ( ( float ) totalTimeSpentProcessing ) / cycle;
						// misc.println_debug("[KERNEL]: "+(time*100/cycleTime)+"%
						// processing time");
					}
					if ( server.ShutDown == true )
					{
						if ( server.ShutDownCounter >= 100 )
						{
							server.shutdownServer = true;
						}
						server.ShutDownCounter++;
					}
				}
				catch ( Exception e )
				{
					//e.printStackTrace();
				}
			}

			// shut down the server
			server.playerHandler.destruct();
			server.clientHandler.killServer();
			server.clientHandler = null;
		}
	}
}
