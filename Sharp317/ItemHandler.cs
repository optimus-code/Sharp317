using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharp317
{
	public class ItemHandler
	{
		public static int DropItemCount = 0;
		public static int MaxDropShowDelay = 120; // 120 * 500 = 60000 / 1000 =
												  // 60s
		public static int MaxListedItems = 10000;
		public static int hideItemTimer = 60;
		public static int MaxDropItems = 100000;

		// SDID = Standard Drop Items Delay
		public static int SDID = 90; // 90 * 500 = 45000 / 1000 = 45s
									 // Phate: Setting VARS
		public static int showItemTimer = 60;

		public static Boolean[] DroppedItemsAlwaysDrop = new Boolean[MaxDropItems];
		public static int[] DroppedItemsDDelay = new int[MaxDropItems];
		public static int[] DroppedItemsDeletecount = new int[MaxDropItems];
		public static int[] DroppedItemsDropper = new int[MaxDropItems];
		public static int[] DroppedItemsH = new int[MaxDropItems];
		public static int[] DroppedItemsID = new int[MaxDropItems];
		public static int[] DroppedItemsN = new int[MaxDropItems];
		public static int[] DroppedItemsSDelay = new int[MaxDropItems];
		public static int[] DroppedItemsX = new int[MaxDropItems];
		public static int[] DroppedItemsY = new int[MaxDropItems];

		// Phate: Global Item VARS
		public static int[] globalItemAmount = new int[8001];
		public static int[] globalItemController = new int[8001];
		public static int[] globalItemID = new int[8001];
		public static Boolean[] globalItemStatic = new Boolean[8001];
		public static int[] globalItemTicks = new int[8001];
		public static int[] globalItemX = new int[8001];
		public static int[] globalItemY = new int[8001];

		public static ItemList[] ItemList = new ItemList[MaxListedItems];
		// process() is called evry 500 ms

		public static void addItem( int itemID, int itemX, int itemY,
				int itemAmount, int itemController, Boolean itemStatic )
		{
			for ( int i = 0; i <= 8000; i++ )
			{ // Phate: Loop through all item
			  // spots
				if ( globalItemID[i] == 0 )
				{ // Phate: Found blank item spot
					globalItemController[i] = itemController;
					globalItemID[i] = itemID;
					globalItemX[i] = itemX;
					globalItemY[i] = itemY;
					globalItemAmount[i] = itemAmount;
					globalItemStatic[i] = itemStatic;
					globalItemTicks[i] = 0;
					if ( ( globalItemController[i] != -1 )
							&& ( globalItemController[i] != 0 ) )
						spawnItem( globalItemID[i], globalItemX[i], globalItemY[i],
								globalItemAmount[i], globalItemController[i] );
					break;
				}
			}
		}

		public static void createItemAll( int itemID, int itemX, int itemY,
				int itemAmount, int itemController )
		{
			// for (Player p : server.playerHandler.players) {
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;
					if ( ( person.playerName != null )
							&& !( person.playerId == itemController ) )
					{
						// misc.println("distance to create
						// "+person.distanceToPoint(itemX, itemY));
						if ( person.distanceToPoint( itemX, itemY ) <= 60 )
						{
							person.createGroundItem( itemID, itemX, itemY,
									itemAmount );
						}
					}
				}
			}
		}

		public static int itemAmount( int itemID, int itemX, int itemY )
		{
			for ( int i = 0; i <= 8000; i++ )
			{ // Phate: Loop through all item
			  // spots
				if ( ( globalItemID[i] == itemID ) && ( globalItemX[i] == itemX )
						&& ( globalItemY[i] == itemY ) ) // Phate:
														 // Found
														 // item
					return globalItemAmount[i];
			}
			return 0; // Phate: Item doesnt exist
		}

		public static Boolean itemExists( int itemID, int itemX, int itemY )
		{
			for ( int i = 0; i <= 8000; i++ )
			{ // Phate: Loop through all item
			  // spots
				if ( ( globalItemID[i] == itemID ) && ( globalItemX[i] == itemX )
						&& ( globalItemY[i] == itemY ) ) // Phate:
														 // Found
														 // item
					return true;
			}
			return false; // Phate: Item doesn't exist
		}

		public static void removeItem( int itemID, int itemX, int itemY,
				int itemAmount )
		{
			for ( int i = 0; i <= 8000; i++ )
			{ // Phate: Loop through all item
			  // spots
				if ( ( globalItemID[i] == itemID ) && ( globalItemX[i] == itemX )
						&& ( globalItemY[i] == itemY )
						&& ( globalItemAmount[i] == itemAmount ) )
				{
					removeItemAll( globalItemID[i], globalItemX[i], globalItemY[i] );
					globalItemController[i] = 0;
					globalItemID[i] = 0;
					globalItemX[i] = 0;
					globalItemY[i] = 0;
					globalItemAmount[i] = 0;
					globalItemTicks[i] = 0;
					globalItemStatic[i] = false;
					break;
				}
			}
		}

		public static void removeItemAll( int itemID, int itemX, int itemY )
		{
			// for (Player p : server.playerHandler.players) {
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;
					if ( person.playerName != null )
					{
						// misc.println("distance to remove
						// "+person.distanceToPoint(itemX, itemY));
						if ( person.distanceToPoint( itemX, itemY ) <= 60 )
						{
							person.removeGroundItem( itemX, itemY, itemID );
						}
					}
				}
			}
		}

		public static void spawnItem( int itemID, int itemX, int itemY,
				int itemAmount, int playerFor )
		{
			client person = ( client ) PlayerHandler.players[playerFor];
			person.createGroundItem( itemID, itemX, itemY, itemAmount );
		}

		public ItemHandler( )
		{
			for ( int i = 0; i <= 8000; i++ )
			{
				globalItemController[i] = 0;
				globalItemID[i] = 0;
				globalItemX[i] = 0;
				globalItemY[i] = 0;
				globalItemAmount[i] = 0;
				globalItemTicks[i] = 0;
				globalItemStatic[i] = false;
			}
			for ( int i = 0; i < MaxDropItems; i++ )
			{
				ResetItem( i );
			}
			for ( int i = 0; i < MaxListedItems; i++ )
			{
				ItemList[i] = null;
			}
			loadItemList( "item.cfg" );
			loadDrops( "globaldrops.cfg" );
		}

		public Boolean loadDrops( String FileName )
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
					if ( token.Equals( "drop" ) )
					{
						int id = Int32.Parse( token3[0] );
						int x = Int32.Parse( token3[1] );
						int y = Int32.Parse( token3[2] );
						int amount = Int32.Parse( token3[3] );
						int height = Int32.Parse( token3[4] );
						for ( int i = 0; i < 5000; i++ )
						{
							createItemAll( id, x, y, amount, globalItemController[i] );
						}
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFDROPLIST]" ) )
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

		public Boolean loadItemList( String FileName )
		{
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[10];
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
					if ( token.Equals( "item" ) )
					{
						int[] Bonuses = new int[12];
						for ( int i = 0; i < 12; i++ )
						{
							if ( token3[( 6 + i )] != null )
							{
								Bonuses[i] = Int32.Parse( token3[( 6 + i )] );
							}
							else
							{
								break;
							}
						}
						newItemList( Int32.Parse( token3[0] ), token3[1]
								.Replace( "_", " " ), token3[2].Replace( "_",
								" " ), Double.Parse( token3[4] ), Double.Parse( token3[4] ), Double.Parse( token3[6] ), Bonuses );
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFITEMLIST]" ) )
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

		public void newItemList( int ItemId, String ItemName,
				String ItemDescription, double ShopValue, double LowAlch,
				double HighAlch, int[] Bonuses )
		{
			// first, search for a free slot
			int slot = -1;
			for ( int i = 0; i < 9999; i++ )
			{
				if ( ItemList[i] == null )
				{
					slot = i;
					break;
				}
			}

			if ( slot == -1 )
				return; // no free slot found
			ItemName = ItemName.Replace( "'", "" );
			ItemDescription = ItemDescription.Replace( "'", "" );
			ItemList newItemList = new ItemList( ItemId );
			newItemList.itemName = ItemName;
			newItemList.itemDescription = ItemDescription;
			newItemList.ShopValue = ShopValue;
			newItemList.LowAlch = LowAlch;
			newItemList.HighAlch = HighAlch;
			newItemList.Bonuses = Bonuses;
			ItemList[slot] = newItemList;
		}

		public static void process( )
		{
			for ( int i = 0; i <= 8000; i++ )
			{
				if ( globalItemID[i] != 0 )
					globalItemTicks[i]++;

				if ( ( hideItemTimer + showItemTimer ) == globalItemTicks[i] )
				{
					// misc.println("Item Removed ["+i+"] id is ["+globalItemID[i]+"]");
					if ( !globalItemStatic[i] )
					{
						removeItemAll( globalItemID[i], globalItemX[i],
								globalItemY[i] );
					}
					else
					{
						misc.println( "Item is static" );
					}
				}

				if ( showItemTimer == globalItemTicks[i] )
				{ // Phate: Item has
				  // expired, show to all
					if ( !globalItemStatic[i] )
					{
						createItemAll( globalItemID[i], globalItemX[i],
								globalItemY[i], globalItemAmount[i],
								globalItemController[i] );
					}
					else
						misc.println( "Item is static" );
				}

			}
		}

		public void ResetItem( int ArrayID )
		{
			DroppedItemsID[ArrayID] = -1;
			DroppedItemsX[ArrayID] = -1;
			DroppedItemsY[ArrayID] = -1;
			DroppedItemsN[ArrayID] = -1;
			DroppedItemsH[ArrayID] = -1;
			DroppedItemsDDelay[ArrayID] = -1;
			DroppedItemsSDelay[ArrayID] = 0;
			DroppedItemsDropper[ArrayID] = -1;
			DroppedItemsDeletecount[ArrayID] = 0;
			DroppedItemsAlwaysDrop[ArrayID] = false;
		}
	}
}
