using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharp317
{
	public class DoorHandler
	{
		public static int[] doorFace = new int[100];
		public static int[] doorFaceClosed = new int[100];
		public static int[] doorFaceOpen = new int[100];
		public static int[] doorHeight = new int[100];
		public static int[] doorId = new int[100];
		public static int[] doorState = new int[100];
		public static int[] doorX = new int[100];
		public static int[] doorY = new int[100];

		public DoorHandler( )
		{
			try
			{
				loadDoors( "doors.cfg" );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public Boolean loadDoors( String FileName )
		{
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[10];
			Boolean EndOfFile = false;
			int doorCount = 0;
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
					if ( token.Equals( "door" ) )
					{
						doorX[doorCount] = Int32.Parse( token3[0] );
						doorY[doorCount] = Int32.Parse( token3[1] );
						doorId[doorCount] = Int32.Parse( token3[2] );
						;
						doorFaceOpen[doorCount] = Int32.Parse( token3[3] );
						doorFaceClosed[doorCount] = Int32.Parse( token3[4] );
						doorFace[doorCount] = Int32.Parse( token3[5] );
						doorState[doorCount] = Int32.Parse( token3[6] );
						doorHeight[doorCount] = Int32.Parse( token3[7] );
						doorCount++;
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFDOORLIST]" ) )
					{
						try
						{
							characterfile.Dispose();
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
				characterfile.Dispose();
			}
			catch ( IOException ioexception )
			{
			}
			return false;
		}
	}
}
