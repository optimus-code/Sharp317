using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharp317
{
	public class ShopHandler
	{
		public static int MaxInShopItems = 80;
		public static int MaxShopItems = 101; // 1 more because we don't use [0] !
		public static int MaxShops = 101; // 1 more because we don't use [0] !
		public static int MaxShowDelay = 90;
		public static int[] ShopBModifier = new int[MaxShops];
		public static int[][] ShopItems = new int[MaxShops][];
		public static int[][] ShopItemsDelay = new int[MaxShops][];
		public static int[][] ShopItemsN = new int[MaxShops][];
		public static int[][] ShopItemsSN = new int[MaxShops][];
		public static int[] ShopItemsStandard = new int[MaxShops];
		public static String[] ShopName = new String[MaxShops];
		public static int[] ShopSModifier = new int[MaxShops];
		public static int TotalShops = 0;

		public ShopHandler( )
		{
			for ( int i = 0; i < MaxShops; i++ )
			{
				ShopItems[i] = new int[MaxShopItems];
				ShopItemsDelay[i] = new int[MaxShopItems];
				ShopItemsN[i] = new int[MaxShopItems];
				ShopItemsSN[i] = new int[MaxShopItems];

				for ( int j = 0; j < MaxShopItems; j++ )
				{
					ResetItem( i, j );
					ShopItemsSN[i][j] = 0; // Special resetting, only at begin !
				}
				ShopItemsStandard[i] = 0; // Special resetting, only at begin !
				ShopSModifier[i] = 0;
				ShopBModifier[i] = 0;
				ShopName[i] = "";
			}
			TotalShops = 0;
			loadShops( "shops.cfg" );
		}

		public void DiscountItem( int ShopID, int ArrayID )
		{
			ShopItemsN[ShopID][ArrayID] -= 1;
			if ( ShopItemsN[ShopID][ArrayID] <= 0 )
			{
				ShopItemsN[ShopID][ArrayID] = 0;
				ResetItem( ShopID, ArrayID );
			}
		}

		public Boolean loadShops( String FileName )
		{
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[( MaxShopItems * 2 )];
			Boolean EndOfFile = false;
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
			catch ( Exception ioexception )
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
					if ( token.Equals( "shop" ) )
					{
						int ShopID = Int32.Parse( token3[0] );
						ShopName[ShopID] = token3[1].Replace( "_", " " );
						ShopSModifier[ShopID] = Int32.Parse( token3[2] );
						ShopBModifier[ShopID] = Int32.Parse( token3[3] );
						for ( int i = 0; i < ( ( token3.Length - 4 ) / 2 ); i++ )
						{
							if ( token3[( 4 + ( i * 2 ) )] != null )
							{
								ShopItems[ShopID][i] = ( Int32.Parse( token3[( 4 + ( i * 2 ) )] ) + 1 );
								ShopItemsN[ShopID][i] = Int32.Parse( token3[( 5 + ( i * 2 ) )] );
								ShopItemsSN[ShopID][i] = Int32.Parse( token3[( 5 + ( i * 2 ) )] );
								ShopItemsStandard[ShopID]++;
							}
							else
							{
								break;
							}
						}
						TotalShops++;
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFSHOPLIST]" ) )
					{
						try
						{
							characterfile.Dispose();
						}
						catch ( Exception ioexception )
						{
						}
						return true;
					}
				}
				try
				{
					line = characterfile.ReadLine();
				}
				catch ( Exception ioexception1 )
				{
					EndOfFile = true;
				}
			}
			try
			{
				characterfile.Dispose();
			}
			catch ( Exception ioexception )
			{
			}
			return false;
		}

		public void process( )
		{
			Boolean DidUpdate = false;
			for ( int i = 1; i <= TotalShops; i++ )
			{
				for ( int j = 0; j < MaxShopItems; j++ )
				{
					if ( ShopItems[i][j] > 0 )
					{
						if ( ShopItemsDelay[i][j] >= MaxShowDelay )
						{
							if ( ( j <= ShopItemsStandard[i] )
									&& ( ShopItemsN[i][j] <= ShopItemsSN[i][j] ) )
							{
								if ( ShopItemsN[i][j] < ShopItemsSN[i][j] )
								{
									ShopItemsN[i][j] += 1; // if amount lower then
														   // standard, increase it
														   // !
								}
							}
							else
							{
								DiscountItem( i, j );
							}
							ShopItemsDelay[i][j] = 0;
							DidUpdate = true;
						}
						ShopItemsDelay[i][j]++;
					}
				}
				if ( DidUpdate == true )
				{
					for ( int k = 1; k < PlayerHandler.maxPlayers; k++ )
					{
						if ( PlayerHandler.players[k] != null )
						{
							if ( ( PlayerHandler.players[k].IsShopping == true )
									&& ( PlayerHandler.players[k].MyShopID == i ) )
							{
								PlayerHandler.players[k].UpdateShop = true;
							}
						}
					}
					DidUpdate = false;
				}
			}
		}

		public void ResetItem( int ShopID, int ArrayID )
		{
			ShopItems[ShopID][ArrayID] = 0;
			ShopItemsN[ShopID][ArrayID] = 0;
			ShopItemsDelay[ShopID][ArrayID] = 0;
		}
	}

}
