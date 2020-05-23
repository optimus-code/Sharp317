using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharp317
{
	public class NpcAnimHandler
	{
		public int[] block = new int[3851], atk = new int[3851], die = new int[3851]; 

		public void println( String str )
		{
			Console.WriteLine( str ); 
		}

		public void loadanim( )
		{
			String line = ""; 
			String token = ""; 
			String token2 = ""; 
			String token2_2 = ""; 
			String[] token3 = new String[5];
			Boolean EndOfFile = false; 
			int ReadMode = 0; 
			TextReader characterfile = null; 
			try 
			{ 
				characterfile = File.OpenText( "config\\NPCEmotes.cfg" ); 
			} 
			catch ( FileNotFoundException fileex ) 
			{ 
				println( "NPCEmotes.cfg was not found." ); 
			}

			try { 
				line = characterfile.ReadLine();
			} 
			catch ( IOException ioexception ) 
			{ 
				println( "NPCEmotes.cfg: error loading file." ); 
			} 
			while ( EndOfFile == false && line != null )
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
					if ( token.Equals( "npcID" ) )
					{
						atk[Int32.Parse( token3[0] )] = Int32.Parse( token3[1] ); 
						block[Int32.Parse( token3[0] )] = Int32.Parse( token3[2] );
						die[Int32.Parse( token3[0] )] = Int32.Parse( token3[3] );
					}
				}
				else
				{ 
					if ( line.Equals( "[ENDOFNPCEMOTES]" ) ) 
					{ 
						try 
						{ 
							characterfile.Dispose(); 
						} 
						catch ( Exception ioexception ) 
						{
						} 
					} 
				}
				try
				{
					line = characterfile.ReadLine();
				}
				catch ( Exception ioexception1 ) 
				{ 
					EndOfFile = true;
					characterfile.Dispose();
				}
			}
			try 
			{ 
				characterfile.Dispose(); 
			} 
			catch ( Exception ioexception ) 
			{ 
			}
		}
	}
}
