using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Sharp317
{
	public class client : Player
	{
		protected ISAACRandomGenerator inStreamDecryption { get; private set; }
		protected ISAACRandomGenerator outStreamDecryption { get; private set; }

		public void followPlayer( int playerId )
		{
			Boolean UseBow = false;
			client AttackingOn2 = ( client ) PlayerHandler.players[AttackingOn];
			int EnemyX = PlayerHandler.players[AttackingOn].absX;
			int EnemyY = PlayerHandler.players[AttackingOn].absY;
			TurnPlayerTo( EnemyX, EnemyY );

			for ( int i = 0; i < Constants.shortbow.Length; i++ )
			{
				if ( ( playerEquipment[playerWeapon] == Constants.shortbow[i] ) || ( playerEquipment[playerWeapon] == Constants.longbow[i] ) )
				{
					UseBow = true;
					break;
				}
			}
			int k = 0;
			for ( int i = 0; i < k; i++ )
			{
				if ( ( playerEquipment[playerWeapon] != Constants.shortbow[i] ) || ( playerEquipment[playerWeapon] != Constants.longbow[i] ) )
				{
					UseBow = false;
					break;
				}
			}
			if ( absY == EnemyY && absX == EnemyX )
			{
				WalkTo( -1, 0 );

			}
			if ( PlayerHandler.players[AttackingOn] != null )
			{
				if ( GoodDistance( EnemyX, EnemyY, absX, absY, 15 ) == true )
				{
					if ( UseBow && GoodDistance( EnemyX, EnemyY, absX, absY, 6 ) != true )
					{

						if ( absY < EnemyY )
						{
							WalkTo( 0, +1 );
						}
						else if ( absY > EnemyY )
						{
							WalkTo( 0, -1 );
						}
						else if ( absX < EnemyX )
						{
							WalkTo( +1, 0 );
						}
						else if ( absX > EnemyX )
						{
							WalkTo( -1, 0 );
						}
						updateRequired = true;
						appearanceUpdateRequired = true;
					}
					if ( !UseBow && GoodDistance( EnemyX, EnemyY, absX, absY, 1 ) != true )
					{
						if ( absY < EnemyY )
						{
							WalkTo( 0, +1 );
						}
						else if ( absY > EnemyY )
						{
							WalkTo( 0, -1 );
						}
						else if ( absX < EnemyX )
						{
							WalkTo( +1, 0 );
						}
						else if ( absX > EnemyX )
						{
							WalkTo( -1, 0 );
						}
						updateRequired = true;
						appearanceUpdateRequired = true;

					}
				}
			}
		}

		public int GetMove( int Place1, int Place2 )
		{

			if ( ( Place1 - Place2 ) == 0 )
				return 0;
			else if ( ( Place1 - Place2 ) < 0 )
				return 1;
			else if ( ( Place1 - Place2 ) > 0 )
				return -1;
			return 0;
		}

		public void specAttack( )
		{
			server.special.specialAttacks( playerId );
			server.special.specialAttacks2( playerId );
			server.special.specialAttacks3( playerId );
			server.special.specialAttacks4( playerId );
			server.special.specialAttacks5( playerId );
			server.special.specialAttacks6( playerId );
		}

		public void loadSpecialInformationPvp( int wep, int amount, int gfx, int ani, int delay )
		{
			wep = playerEquipment[playerWeapon];
			amount = specialAmount -= amount;
			gfx100( gfx );
			startAnimation( ani );

		}
		public int specialAmount = 100;
		public Boolean specOn;
		public int EntangleDelay = 0;
		public Boolean hasWildySign = false;
		public int wildyLevel = 0;
		public Boolean isMassClicking = false;
		public int deathTimer2 = 0;
		public Boolean isdieing = false;
		public int launcharrowtimer = 0;
		public Boolean readytolaunch = false;
		public Boolean startedrange = false;
		public int rangedelaytimer = 0;
		public Boolean shootarrow = true;
		public int shootarrowtimer = 0;
		public Boolean hasteled = false;
		public int playerItemAmountCount = 0;
		public int teleporter = 0;
		public Boolean pullarrow;
		public Boolean firedarrow;
		public Boolean droppedarrow;
		public int HasLearnedCombat = 0;
		public int killcounts = 0;
		public Boolean killcounter;
		public double AttackHitTimer3 = 0;
		public Boolean AttackHit3 = false;
		public double AttackHitTimer2 = 0;
		public Boolean AttackHit2 = false;
		public int starterItems = 0;
		public int weapontimer = 0;
		public int weapontimer2 = 0;
		public int isinjaillolz = 0;
		public static Boolean AutoSave = false;
		public Boolean ssemallikybedamsawsiht = true;
		public static int bufferSize = 1000000;
		public static Boolean comeback = false;
		public void removeSpec( int id )
		{
			outStream.createFrame( 171 );
			outStream.writeByte( 1 );
			outStream.writeWord( id );
			flushOutStream();
		}
		public void l33thax( int id )
		{
			outStream.createFrame( 171 );
			outStream.writeByte( 0 );
			outStream.writeWord( id );
			flushOutStream();
		}
		public void GotOwned( )
		{
			if ( deathStage == 1 )
			{
				if ( attacknpc > 0 )
				{ // was killed by a npc -bakatool
					server.npcHandler.ResetAttackPlayer( attacknpc );
				}
				client p = getClient( duel_with );
				if ( ( duel_with > 0 ) && ValidClient( duel_with ) && inDuel
						&& duelFight )
				{
					// p.killedPlayer(p.playerName, p.combatLevel);
					// p.ResetAttack();
					p.DuelVictory();
					DuelTele = true;
					// duelStatus = 4;
					// p.duelStatus = 4;
					sendMessage( "You have lost the duel!" );
				}
				else if ( isInWilderness( absX, absY, 1 ) )
				{
					client killerz = ( client ) PlayerHandler.players[KillerId];
					youdied();
					killerz.sendMessage( "You have defeated " + playerName + "." );
				}
				ResetAttack();
				ResetAttackNPC();
				deathStage = 2;
				fighting = false;
				hits = 0;
				EntangleDelay = 0;
				startAnimation( 0x900 );
				updateRequired = true;
				appearanceUpdateRequired = true;
				deathTimer = TimeHelper.CurrentTimeMillis();
				currentHealth = playerLevel[playerHitpoints];
				playerLevel[0] = getLevelForXP( playerXP[0] );
				playerLevel[1] = getLevelForXP( playerXP[1] );
				playerLevel[2] = getLevelForXP( playerXP[2] );
				playerLevel[4] = getLevelForXP( playerXP[4] );
				playerLevel[5] = getLevelForXP( playerXP[5] );
				playerLevel[6] = getLevelForXP( playerXP[6] );
			}
			if ( ( deathStage == 2 )
					&& ( TimeHelper.CurrentTimeMillis() - deathTimer >= 2500 ) )
			{
				if ( DuelTele == true )
				{
					teleportToX = 3377 + misc.random( 1 );
					teleportToY = 3273 + misc.random( 4 );
				}
				else
				{
					sendMessage( "Oh dear you have died!" );
					teleportToX = 2635;
					teleportToY = 3324;
				}
				heightLevel = 0;
				deathStage = 0;
				resetAnimation();
				EntangleDelay = 0;
				savegame( false );
			}

		}

		public static Boolean threats;
		public int aaa;
		public int abc;
		public int abc2;
		public int actionButtonId = 0;
		public Boolean adding = false, emoting = false;
		/* ALLIGNMENT(Good - Evil - Creds to Runite) */
		public int allignment = 0;
		public Boolean alreadyChose = false;
		public int[] ancientButton = { 51133, 51185, -1, 51091, 24018, -1, 51159,
				51211, -1, 51111, 51069, -1, 51146, 51198, -1, 51102, 51058, -1,
				51172, 51224, -1, 51122, 51080, -1 };
		public int[] ancientId = { 12939, 12987, 0, 12901, 12861, 0, 12963, 13011,
				0, 12919, 12881, 0, 12951, 12999, 0, 12911, 12871, 0, 12975, 13023,
				0, 12929, 12891, 0 };
		public int ancients = 1;
		public Boolean ancientstele = false;
		public int[] ancientType = { 0, 0, 1, 2, 3, 1, 0, 0, 1, 2, 3, 1, 0, 0, 1,
				2, 3, 1, 0, 0, 1, 2, 3, 1 };
		public int angle = 250;
		long animationReset = 0, lastButton = 0;
		public Boolean AnimationReset; // Resets Animations With The Use Of The
		public int AntiTeleDelay;
		public int arenaSpellTimer;
		public int attackedNpcId = -1;
		public int AttackingOn = 0;
		public int autocast_spellIndex = -1;

		public int barTimer = 0, saveTimer = 0;
		public int[] baseDamage = { 1, 2, 0, 3, 4, 0, 5, 6, 0, 7, 8, 0, 9, 10, 0,
				11, 12, 0, 13, 14, 0, 15, 16, 0 };
		public String[] BonusMySqlName = { "attack_stab", "attack_slash",
				"attack_crush", "attack_magic", "attack_range", "defence_stab",
				"defence_slash", "defence_crush", "defence_magic", "defence_range",
				"other_strength", "other_prayer" };

		public String[] BonusName = { "Stab", "Slash", "Crush", "Magic", "Range",
				"Stab", "Slash", "Crush", "Magic", "Range", "Str", "Spell Dmg" };
		public byte[] buffer = null;
		int cAmount = 0;
		public int[] casketItems = { 4724, 4726, 4728, 4730, 1050, 1053, 1055,
				1057, 1037, 3107, 4708, 4710, 4712, 4714, 2904, 777, 2414 };
		public Boolean cast = false; // Part Of The Create Spell Code
		public Boolean castleWarsOn = false;

		public Boolean castSpell, isStillSpell;
		public int cba;
		int cExp = 0;
		int cItem = -1;
		int cLevel = 1;
		public int clickIndex = 0;
		public int[] clicks = new int[50];
		public int CombatExpRate = 1;
		public String converse;
		private int[] cooking = { 0, 0, 0, 1, -1, -1, -1 };
		public long[] coolDown = { 5000, 5000, 2500, 5000 };
		public int[] coolDownGroup = { 2, 2, 1, 2, 3, 1, 2, 2, 1, 2, 3, 1, 2, 2, 1,
				2, 3, 1, 2, 2, 1, 2, 3, 1 };
		// Sharp317: crafting
		Boolean crafting = false;
		int cSelected = -1, cIndex = -1;
		public int currentButton = 0, currentStatus = 0;
		public int cwAmount = 0;
		public int cwTimer = 0;
		// Sharp317: smelting
		Boolean dialog = true, spinning = false;
		int dialogInterface = 2459, dialogId = 1;
		public String dsendMessage = "";
		public int doors = -1;

		public int duel_with = 0;

		public Boolean duelAccept1 = false;
		public Boolean duelAccept2 = false;
		public int[] duelButtons = { 26069, 26070, 26071, 30136, 2158, 26065,
				26072, 26073, 26074, 26066, 26076 };
		public int[] duelItems = new int[28];
		public int[] duelItemsN = new int[28];
		public Boolean[] duelItemsNoted = new Boolean[28];
		public int[] duelLine = { 6698, 6699, 6697, 7817, 669, 6696, 6701, 6702,
				6703, 6704, 6731 };
		public String[] duelNames = { "No Ranged", "No Melee", "No Magic",
				"No Gear Change", "Fun Weapons", "No Retreat", "No Drinks",
				"No Food", "No prayer", "No Movement", "Obstacles" };
		public int duelpartner = 0;
		public Boolean duelReq = false;
		public Boolean duelRequested = false, inDuel = false,
				duelConfirmed = false, duelConfirmed2 = false,
				duelResetNeeded = false, duelFight = false;
		public Boolean[] duelRule = { false, false, false, false, false, true,
				false, true, false, true, false };
		public int duelWho = 0;
		public int[] effects = new int[10];
		private int emotes = 0;
		public int enemyId = -1, enemyX = -1, enemyY = -1, attackTimer = 0;
		/*
		 * [0] North West [1] North East [2] Center [3] South East [4] South West
		 */
		public int[] EssenceMineRX = { 3253, 3105, 2681, 2591 };
		public int[] EssenceMineRY = { 3401, 9571, 3325, 3086 };
		public int[] EssenceMineX = { 2893, 2921, 2911, 2926, 2899 };
		public int[] EssenceMineY = { 4846, 4846, 4832, 4817, 4817 };

		public int fangle = 0;
		public int fcasterX = 0;
		public int fcasterY = 0;
		// ActionTimer
		public int fcastid = 0;
		public int feh = 0;
		public int fenemyX = 0;

		public int fenemyY = 0;
		public int ffinishid = 0;
		public Boolean fired = false; // Part Of The Create Spell Code
		private int[] firemaking = new int[] { 0, 0, 0, 1, -1 };
		public Boolean firingspell = false; // Part Of The Create Spell Code

		Boolean fishing = false;

		// Sharp317: fishing
		int fishTries, fishId;

		public int fletchId = -1, fletchAmount = -1, fletchLog = -1,
				originalW = -1, originalS = -1, fletchExp = 0;

		public Boolean fletching = false;

		// These are temp data
		public int fletchTime;

		public int fmgfxid = 0;

		public int foffsetX = 0;

		public int foffsetY = 0;

		public Boolean friendUpdate = false, lookUpdate = false;

		public int fsh = 0;

		public int fspeed = 0;

		public long[] globalCooldown = new long[10];

		/* RANGE */
		public Boolean HasArrows = false;

		/* MISC */
		public int hasset = 0;

		private int[] healing = new int[] { 0, 0, 0, -1, -1 };

		public int i;

		public Boolean iceBarrage = false;

		/* WALKING TO OBJECT BEFORE DOING ACTION */

		public int iceTimer = 0;

		/* DUELING */

		private NetworkStream inputStream;

		public stream inStream = null, outStream = null;

		//public Cryption inStreamDecryption = null, outStreamDecryption = null;

		public Boolean isSpellNPC; // added check weather magic attack player or

		public int KillerId = 0;// playerId;

		public long lastAttack = 0;

		String[] lastMessage = new String[3];

		public long lastMouse = 0;
		// Sharp317: fletching

		private long lastPickup = 0;

		public long lastProcess = 0;
		public int loginDelay = 1;
		public Boolean lookNeeded = false;

		/* SUMMONING */

		public int lowMemoryVersion = 0;
		public int MageAttackIndex = -1; // -1
		public String MBBC;
		public String MBHT;
		public String MBID;

		/*
		 * --------MOD BOT----------- // MB = MOD BOT // -TC = Text Censor // -BC =
		 * Bad Command // -HT = Help Text // -ID = Item Duping
		 */
		public String MBTC;

		public Boolean member = false;
		private int[] mining = new int[] { 0, 0, 0, 1, -1 };
		private Socket mySock;

		public int newheightLevel = 0;

		public int[] noTrade = { 1543, 1544 };

		public int NPCID; // GLOBALLY NOW last clicked npcID -bakatool

		public int NPCSlot; // GLOBALLY NOW last clicked npc slot -bakatool

		public Boolean oddDeath = false;

		public SynchronizedCollection<GameItem> offeredItems = new SynchronizedCollection<GameItem>();

		public Boolean officialClient = false;
		public int oldclick = 0;
		public int OriginalShield = -1;
		public int OriginalWeapon = -1;
		public SynchronizedCollection<GameItem> otherOfferedItems = new SynchronizedCollection<GameItem>();
		private NetworkStream outputStream;
		public int packetSize = 0, packetType = -1;

		public int pCArms;

		public int pCBeard;
		public int pCFeet;
		public int pCHands;
		public int pCHead;

		public int pCLegs;
		public int pColor;

		public int pCTorso;

		public int PickUpAmount = 0;

		public int PickUpDelete = 0;

		public int PickUpID = 0;

		public int playerClass = 0; // 0 = undecided, 1 = warrior, 2 = mage, 3 =

		public Boolean playerIsSaradomin = false;

		public Boolean playerIsZamorak = false;

		public String playerstatus = "";
		private int[] prayer = new int[] { 0, 1, 0, 1, -1, -1 };
		// public int[] killers = new int[server.playerHandler.maxPlayers];
		public String properName = "";

		public int Publicchat = 0;
		public int[] QuestInterface = { 8145, 8147, 8148, 8149, 8150, 8151, 8152,
				8153, 8154, 8155, 8156, 8157, 8158, 8159, 8160, 8161, 8162, 8163,
				8164, 8165, 8166, 8167, 8168, 8169, 8170, 8171, 8172, 8173, 8174,
				8175, 8176, 8177, 8178, 8179, 8180, 8181, 8182, 8183, 8184, 8185,
				8186, 8187, 8188, 8189, 8190, 8191, 8192, 8193, 8194, 8195, 12174,
				12175, 12176, 12177, 12178, 12179, 12180, 12181, 12182, 12183,
				12184, 12185, 12186, 12187, 12188, 12189, 12190, 12191, 12192,
				12193, 12194, 12195, 12196, 12197, 12198, 12199, 12200, 12201,
				12202, 12203, 12204, 12205, 12206, 12207, 12208, 12209, 12210,
				12211, 12212, 12213, 12214, 12215, 12216, 12217, 12218, 12219,
				12220, 12221, 12222, 12223 };
		public int random_skill = -1, npcId = -1;
		public int readPtr, writePtr;
		public int[] requiredLevel = { 1, 10, 25, 38, 50, 60, 62, 64, 66, 68, 70,
				72, 74, 76, 78, 80, 82, 84, 86, 88, 90, 92, 94, 96 };

		public int resetanim = 8;

		public int restart = 0;

		/* PRAYER */
		public Boolean Retribution = false;

		public int returnCode = 2; // Tells the client if the login was successfull

		public int rune1, rune1Am, rune2, rune2Am, rune3, rune3Am, rune4, rune4Am,
				spellXP;

		public int sameclick = 0;

		public Boolean SaradominStrike, GuthixClaws, ZamorakFlames;

		public int saraScore;

		public int savecounter = 0;

		public Boolean saveNeeded = true;

		public Boolean shafting = false;

		public int SkillID = 0;

		public int skillX = -1;

		public int skillY = -1;

		public int smelt_id, smeltCount;

		public Boolean smelting = false;

		private int[] smithing = new int[] { 0, 0, 0, 1, -1, 0 };

		private int somejunk;

		public Boolean spamButton = false;

		// -bakatool
		public int spellHit; // also same added so it won't be static ROFL
							 // -bakatool

		public int spellHitTimer;
		// npc. -bakatool
		public int spellNpcIndex = -1; // added so it won't be static ROFL..
									   // -bakatool
		public int spellPlayerIndex = -1; // same added so it won't be static ROFL
		public Boolean spellSet = false;
		public int stairDistance = 1;
		public int stairDistanceAdd = 0;
		public int stairs = 0;
		public int[] statId = { 10252, 11000, 10253, 11001, 10254, 11002, 10255,
				11011, 11013, 11014, 11010, 11012, 11006, 11009, 11008, 11004,
				11003, 11005, 47002, 54090, 11007 };
		public String[] statName = new String[]{ "attack", "defence", "strength", "hitpoints",
				"range", "prayer", "magic", "cooking", "woodcutting", "fletching",
				"fishing", "firemaking", "crafting", "smithing", "mining",
				"herblore", "agility", "thieving", "slayer", "farming",
				"runecrafting" };
		public int stealtimer;
		public int stillSpellGFX;
		public int summonLevel = 250;
		public int summonXP = 9999999;
		public String teleLoc = "";
		public Boolean teleOtherScreen = false;
		public Boolean teleport = false;
		public int teleReq = 0;
		public int teletimer = 0;

		public int teleX = 0;

		public int teleY = 0;

		public int timeOutCounter = 0; // to detect timeouts on the connection to
									   // the client

		public int trade_reqId = 0, trade_other;

		public int Tradecompete = 0;
		public Boolean tradeRequested = false, inTrade = false, canOffer = true,
				tradeConfirmed = false, tradeConfirmed2 = false,
				tradeResetNeeded = false;
		long tTime = 0;
		// Sharp317: teleports
		int tX = 0, tY = 0, tStage = 0, tH = 1;
		private int[] useitems = new int[] { -1, -1, -1, -1 };

		// ranger
		public String[] userGroup = { "Unknown group", "Guest", "Registered User",
				"Awaiting Confirmation", "COPPA User", "Super Moderator",
				"Administrator", "Moderator", "Banned User", "Unknown group",
				"Veteran" };

		public Boolean validClient = true, muted = false, attackedNpc = false;

		public Boolean validLogin = false;
		// public int[] restrictedItem = { 4716, 4718, 4720, 4722, 4724, 4726, 4728,
		// 4730};
		private int WanneBank = 0;
		private int WanneShop = 0;

		public Boolean wearing = false;

		public Boolean WildernessWarning = false;

		public String Winner = "Nobody";

		private int[] woodcutting = new int[] { 0, 0, 0, 1, -1, 2 };

		private int XinterfaceID = 0;

		private int XremoveID = 0;

		private int XremoveSlot = 0;

		public int zammyScore;

		public client( Socket serverSocket, Socket s, int _playerId ) : base (_playerId )
		{
			mySock = s;
			try
			{
				inputStream = new NetworkStream( s );// s.getInputStream();
				outputStream = new NetworkStream( s ); //s.getOutputStream();
			}
			catch ( IOException ioe )
			{
				misc.println( "Sharp317 Server (1): Exception!" );
				server.logError( ioe.ToString() );
			}

			outStream = new stream( new byte[bufferSize] );
			outStream.currentOffset = 0;
			inStream = new stream( new byte[bufferSize] );
			inStream.currentOffset = 0;

			readPtr = writePtr = 0;
			buffer = new byte[bufferSize];
		}

		public void actionReset( )
		{
			actionName = "";
		}

		public Boolean pickUpItem( int item, int amount )
		{

			if ( !Item.itemStackable[item] || amount < 1 )
			{
				amount = 1;
			}

			if ( freeSlots() >= 1 )
			{
				for ( int i = 0; i < playerItems.Length; i++ )
				{
					if ( playerItems[i] == ( item + 1 ) && Item.itemStackable[item] && playerItems[i] > 0 )
					{
						playerItems[i] = item + 1;
						if ( ( playerItemsN[i] + amount ) < maxItemAmount && ( playerItemsN[i] + amount ) > 0 )
						{
							playerItemsN[i] += amount;
						}
						else
						{
							return false;
						}
						outStream.createFrameVarSizeWord( 34 );
						outStream.writeWord( 3214 );
						outStream.writeByte( i );
						outStream.writeWord( playerItems[i] );
						if ( playerItemsN[i] > 254 )
						{
							outStream.writeByte( 255 );
							outStream.writeDWord( playerItemsN[i] );
						}
						else
						{
							outStream.writeByte( playerItemsN[i] ); //amount
						}
						outStream.endFrameVarSizeWord();
						i = 30;
						return true;
					}
				}
				for ( int i = 0; i < playerItems.Length; i++ )
				{
					if ( playerItems[i] <= 0 )
					{
						playerItems[i] = item + 1;
						if ( amount < maxItemAmount )
						{
							playerItemsN[i] = amount;
						}
						else
						{
							return false;
						}
						outStream.createFrameVarSizeWord( 34 );
						outStream.writeWord( 3214 );
						outStream.writeByte( i );
						outStream.writeWord( playerItems[i] );
						if ( playerItemsN[i] > 254 )
						{
							outStream.writeByte( 255 );
							outStream.writeDWord_v2( playerItemsN[i] );
						}
						else
						{
							outStream.writeByte( playerItemsN[i] ); //amount
						}
						outStream.endFrameVarSizeWord();
						i = 30;
						return true;
					}
				}
				return true;
			}
			else
			{
				sendMessage( "Your inventory is to full!" );
				return false;
			}
		}

		public void playersOnline_and_Uptime( )
		{
			sendFrame126( "@or1@Players online: @gre@" + PlayerHandler.getPlayerCount() + " !", 7332 );
			sendFrame126( "@or1@Uptime: @gre@" + ( server.uptime / 2 / 60 ) + " Mins", 7333 );
			sendFrame126( "", 7334 );
		}
		public void TheifStall( String stallName, String message, int lvlReq, int XPamount, int item, int itemAmount, int emote )
		{
			if ( TimeHelper.CurrentTimeMillis() - lastAction < actionInterval ) return;
			if ( playerLevel[17] >= lvlReq )
			{
				if ( freeSlots() > 0 )
				{
					actionInterval = 4000;
					lastAction = TimeHelper.CurrentTimeMillis();
					setAnimation( emote );
					sendMessage( "You take from the stall.." );
					sendMessage( message );
					addItem( item, itemAmount );
					addSkillXP( XPamount, 17 );
				}
				else
				{
					sendMessage( "You don't have enough space in your inventory." );
				}
			}
			else if ( playerLevel[17] < lvlReq )
			{
				sendMessage( "You need a thieving level of " + lvlReq + " to thief from this stall." );
			}
		}

		public void lowGFX( int id, int delay )
		{
			mask100var1 = id;
			mask100var2 = delay;
			mask100update = true;
			updateRequired = true;
		}
		public void appendToAutoSpawn( int npcid, int absx, int absy )
		{
			try
			{
				File.AppendAllLines( "config//autospawncodes.txt", new String[] { "spawn = " + npcid + "	" + absx + "	" + absy + "	0	0	0	0	0	1" } );
			}
			catch ( IOException ioe )
			{
				////ioe.printStackTrace();
			}
		}

		public void appendToAutoSpawn2( int npcid, int absx, int absy, int absx2, int absy2, int absx3, int absy3 )
		{
			try
			{
				File.AppendAllLines( "config//autospawncodes.txt", new String[] { "spawn = " + npcid + "	" + absx + "	" + absy + "	0	" + absx2 + "	" + absy2 + "	" + absx3 + "	" + absy3 + "	1" } );
			}
			catch ( IOException ioe )
			{
				////ioe.printStackTrace();
			}
		}

		public static int[] packetSizes = { 0, 0, 0, 1, -1, 0, 0, 0, 0, 0, // 0
				0, 0, 0, 0, 8, 0, 6, 2, 2, 0, // 10
				0, 2, 0, 6, 0, 12, 0, 0, 0, 0, // 20
				0, 0, 0, 0, 0, 8, 4, 0, 0, 2, // 30
				2, 6, 0, 6, 0, -1, 0, 0, 0, 0, // 40
				0, 0, 0, 12, 0, 0, 0, 0, 8, 0, // 50
				0, 8, 0, 0, 0, 0, 0, 0, 0, 0, // 60
				6, 0, 2, 2, 8, 6, 0, -1, 0, 6, // 70
				0, 0, 0, 0, 0, 1, 4, 6, 0, 0, // 80
				0, 0, 0, 0, 0, 3, 0, 0, -1, 0, // 90
				0, 13, 0, -1, 0, 0, 0, 0, 0, 0, // 100
				0, 0, 0, 0, 0, 0, 0, 6, 0, 0, // 110
				1, 0, 6, 0, 0, 0, -1, 0, 2, 6, // 120
				0, 4, 6, 8, 0, 6, 0, 0, 0, 2, // 130
				0, 0, 0, 0, 0, 6, 0, 0, 0, 0, // 140
				0, 0, 1, 2, 0, 2, 6, 0, 0, 0, // 150
				0, 0, 0, 0, -1, -1, 0, 0, 0, 0, // 160
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0, // 170
				0, 8, 0, 3, 0, 2, 0, 0, 8, 1, // 180
				0, 0, 12, 0, 0, 0, 0, 0, 0, 0, // 190
				2, 0, 0, 0, 0, 0, 0, 0, 4, 0, // 200
				4, 0, 0, 0, 7, 8, 0, 0, 10, 0, // 210
				0, 0, 0, 0, 0, 0, -1, 0, 6, 0, // 220
				1, 0, 0, 0, 6, 0, 6, 8, 1, 0, // 230
				0, 4, 0, 0, 0, 0, -1, 0, -1, 4, // 240
				0, 0, 6, 6, 0, 0, 0 // 250
		};
		public Boolean multiCombat( )
		{
			if ( ( absX >= 3144 && absX <= 3184 && absY >= 3519 && absY <= 3656 ) || ( absX >= 3185 && absX <= 3350 && absY >= 3502 && absY <= 3900 ) || ( absX >= 2983 && absX <= 3007 && absY >= 3905 && absY <= 3917 ) || ( absX >= 3007 && absX <= 3075 && absY >= 3608 && absY <= 3713 ) || ( absX >= 2944 && absX <= 2963 && absY >= 3812 && absY <= 3827 ) || ( absX >= 3041 && absX <= 3057 && absY >= 3869 && absY <= 3883 ) || ( absX >= 3157 && absX <= 3181 && absY >= 3874 && absY <= 3895 ) || ( absX >= 2720 && absX <= 2760 && absY >= 5073 && absY <= 5114 ) || ( absX >= 2256 && absX <= 2287 && absY >= 4680 && absY <= 4711 ) || ( absX >= 2360 && absX <= 2445 && absY >= 5045 && absY <= 5125 ) || ( absX >= 2760 && absX <= 2780 && absY >= 2790 && absY <= 2810 ) || ( absX >= 2624 && absX <= 2690 && absY >= 2550 && absY <= 2619 ) || ( absX >= 3460 && absX <= 3520 && absY >= 9470 && absY <= 9530 ) || ( absX >= 2371 && absX <= 2424 && absY >= 5125 && absY <= 5167 ) || ( absX >= 2627 && absX <= 2677 && absY >= 4550 && absY <= 4602 ) || ( absX >= 3249 && absX <= 3307 && absY >= 3904 && absY <= 3952 ) || ( absX >= 2420 && absX <= 2520 && absY >= 10100 && absY <= 10200 ) || ( absX >= 2992 && absX <= 3090 && absY >= 4804 && absY <= 4872 ) )
				return true;
			else
				return false;
		}

		public void triggerTele2( int x, int y, int height )
		{
			if ( TimeHelper.CurrentTimeMillis() - lastAction > 5000 )
			{
				lastAction = TimeHelper.CurrentTimeMillis();
				resetWalkingQueue();
				if ( TimeHelper.CurrentTimeMillis() - lastTeleblock < 300000 )
				{
					sendMessage( "You are teleblocked!" );
					return;
				}
				tX = x;
				tY = y;
				tH = height;
				tStage = 1;
				tTime = 0;
				ResetAttack();
				ResetAttackNPC();
			}
		}

		public void sendFrame106( int i1 )
		{
			outStream.createFrame( 106 );
			outStream.writeByteC( i1 );
		}

		public void ReplaceServerObject( int x, int y, int obj, int face, int t )
		{
			for ( int i = 0; i < PlayerHandler.maxPlayers; i++ )
			{
				client c = ( client ) PlayerHandler.players[i];
				if ( c == null || c.disconnected )
					continue;
				c.ReplaceObject2( x, y, obj, face, t );
			}
		}


		public int maxRangeHit( )
		{
			int rangehit = 0;
			int weapon = playerEquipment[playerWeapon];
			int Arrows = playerEquipment[playerArrows];

			if ( Arrows == 4740 && weapon == 4734 )
			{//BoltRacks
				rangehit = 3;
				rangehit += ( Int32 ) ( playerLevel[4] / 5.5 );
			}
			else if ( Arrows == 892 )
			{//rune arrows
				rangehit = 3;
				rangehit += playerLevel[4] / 6;
			}
			else if ( Arrows == 890 )
			{//adamant arrows
				rangehit = 2;
				rangehit += playerLevel[4] / 7;
			}
			else if ( weapon == 4212 )
			{//crystalbow
				rangehit = 4;
				rangehit += playerLevel[4] / 5;
			}
			else if ( Arrows == 888 )
			{//mithril arrows
				rangehit = 2;
				rangehit += ( Int32 ) ( playerLevel[4] / 7.5 );
			}
			else if ( Arrows == 886 )
			{//steel arrows
				rangehit = 2;
				rangehit += playerLevel[4] / 8;
			}
			else if ( Arrows == 884 )
			{//Iron arrows
				rangehit = 2;
				rangehit -= 1 / 8;
				rangehit += playerLevel[4] / 9;
			}
			else if ( Arrows == 882 )
			{//Bronze arrows
				rangehit = 1;
				rangehit += playerLevel[4] / 9;
			}
			if ( FightType != 1 )
			{
				rangehit -= rangehit / 10;
			}
			return rangehit;
		}
		public void youdied( )
		{
			if ( playerHasItem( 5509 ) )
			{
				deleteItem( 5509, 1 );
			}
			if ( playerHasItem( 5510 ) )
			{
				deleteItem( 5510, 1 );
			}
			if ( playerHasItem( 5512 ) )
			{
				deleteItem( 5512, 1 );
			}
			if ( playerHasItem( 5514 ) )
			{
				deleteItem( 5514, 1 );
			}
			if ( playerHasItem( 6570 ) )
			{
				keep6570 = true;
				deleteItem( 6570, 1 );
			}
			if ( !isSkulled )
			{
				keepItemHandle();
			}
			for ( int rr = 0; rr < playerItems.Length; rr++ )
			{
				try
				{
					if ( playerItems[rr] > 0 && playerItems[rr] < 11999 )
					{       //createItem(currentX,currentY,playerItems[rr]-1);
						replaceBarrows();
						//server.checkPlayerCapes.checkDrop(this);
						ItemHandler.addItem( playerItems[rr] - 1, absX, absY, playerItemsN[rr], KillerId, false );
						//createGroundItem(playerItems[rr]-1, absX, absY, playerItemsN[i]);
						deleteItem( playerItems[rr] - 1, getItemSlot( playerItems[rr] - 1 ), playerItemsN[rr] );

					}
				}
				catch ( Exception e ) { }
			}
			for ( int r = 0; r < playerEquipment.Length; r++ )
			{
				try
				{
					int item = playerEquipment[r];
					if ( ( item > 0 ) && ( item < 11999 ) )
					{
						remove( item, r );
					}
				}
				catch ( Exception e ) { sendMessage( "ERROR: Removing Equipment" ); }
			}
			if ( playerHasItem( 5509 ) )
			{
				deleteItem( 5509, 1 );
			}
			if ( playerHasItem( 5510 ) )
			{
				deleteItem( 5510, 1 );
			}
			if ( playerHasItem( 5512 ) )
			{
				deleteItem( 5512, 1 );
			}
			if ( playerHasItem( 5514 ) )
			{
				deleteItem( 5514, 1 );
			}
			if ( playerHasItem( 6570 ) )
			{
				keep6570 = true;
				deleteItem( 6570, 1 );
			}
			for ( int rr = 0; rr < playerItems.Length; rr++ )
			{
				try
				{
					if ( playerItems[rr] > 0 && playerItems[rr] < 11999 )
					{
						//createItem(currentX,currentY,playerItems[rr]-1);
						replaceBarrows();
						//server.checkPlayerCapes.checkDrop(this);
						ItemHandler.addItem( playerItems[rr] - 1, absX, absY, playerItemsN[rr], KillerId, false );
						//createGroundItem(playerItems[rr]-1, absX, absY, playerItemsN[i]);
						deleteItem( playerItems[rr] - 1, getItemSlot( playerItems[rr] - 1 ), playerItemsN[rr] );
					}
				}
				catch ( Exception e ) { }
			}
			try
			{

			}
			catch ( Exception e ) { }
			ItemHandler.addItem( 526, absX, absY, 1, KillerId, false );
			try
			{
			}
			catch ( Exception e ) { }
			if ( itemKept1 > 0 )
				addItem( itemKept1, 1 );
			if ( itemKept2 > 0 )
				addItem( itemKept2, 1 );
			if ( itemKept3 > 0 )
				addItem( itemKept3, 1 );
			if ( itemKept4 > 0 )
				addItem( itemKept4, 1 );
			if ( keep6570 )
			{
				addItem( 6570, 1 );
				keep6570 = false;
			}

			resetKeepItem();
			hitDiff = 0;
			updateRequired = true; appearanceUpdateRequired = true;
		}
		public void keepItem1( )
		{
			int highest = 0;
			for ( int i = 0; i < playerItems.Length; i++ )
			{
				int value = ( int ) Math.Floor( GetItemValue( playerItems[i] - 1 ) );
				if ( value > highest && playerItems[i] - 1 != -1 )
				{
					highest = value;
					itemKept1 = playerItems[i] - 1;
					itemKept1Slot = i;
					itemSlot1 = true;
				}
			}
			for ( int i = 0; i < playerEquipment.Length; i++ )
			{
				int value = ( int ) Math.Floor( GetItemValue( playerEquipment[i] ) );
				if ( value > highest && playerEquipment[i] != -1 )
				{
					highest = value;
					itemKept1 = playerEquipment[i];
					itemKept1Slot = i;
					itemSlot1 = false;
				}
			}
		}

		public void keepItem2( )
		{
			int highest = 0;
			for ( int i = 0; i < playerItems.Length; i++ )
			{
				if ( itemKept1Slot != i )
				{
					int value = ( int ) Math.Floor( GetItemValue( playerItems[i] - 1 ) );
					if ( value > highest && playerItems[i] - 1 != -1 )
					{
						highest = value;
						itemKept2 = playerItems[i] - 1;
						itemKept2Slot = i;
						itemSlot2 = true;
					}
				}
			}
			for ( int i = 0; i < playerEquipment.Length; i++ )
			{
				if ( itemKept1Slot != i )
				{
					int value = ( int ) Math.Floor( GetItemValue( playerEquipment[i] ) );
					if ( value > highest && playerEquipment[i] != -1 )
					{
						highest = value;
						itemKept2 = playerEquipment[i];
						itemKept2Slot = i;
						itemSlot2 = false;
					}
				}
			}
		}

		public void keepItem3( )
		{
			int highest = 0;
			for ( int i = 0; i < playerItems.Length; i++ )
			{
				if ( itemKept1Slot != i && itemKept2Slot != i )
				{
					int value = ( int ) Math.Floor( GetItemValue( playerItems[i] - 1 ) );
					if ( value > highest && playerItems[i] - 1 != -1 )
					{
						highest = value;
						itemKept3 = playerItems[i] - 1;
						itemKept3Slot = i;
						itemSlot3 = true;
					}
				}
			}
			for ( int i = 0; i < playerEquipment.Length; i++ )
			{
				if ( itemKept1Slot != i && itemKept2Slot != i )
				{
					int value = ( int ) Math.Floor( GetItemValue( playerEquipment[i] ) );
					if ( value > highest && playerEquipment[i] != -1 )
					{
						highest = value;
						itemKept3 = playerEquipment[i];
						itemKept3Slot = i;
						itemSlot3 = false;
					}
				}
			}
		}

		public void resetKeepItem( )
		{
			itemKept1 = itemKept2 = itemKept3 = itemKept4 = -1;
			itemKept1Slot = itemKept2Slot = itemKept3Slot = itemKept4Slot = -1;
		}
		public void replaceBarrows( )
		{
			replaceitem( 4708, 4860 );
			replaceitem( 4710, 4866 );
			replaceitem( 4712, 4872 );
			replaceitem( 4714, 4878 );
			replaceitem( 4716, 4884 );
			replaceitem( 4718, 4890 );
			replaceitem( 4720, 4896 );
			replaceitem( 4722, 4902 );
			replaceitem( 4724, 4908 );
			replaceitem( 4726, 4914 );
			replaceitem( 4728, 4920 );
			replaceitem( 4730, 4926 );
			replaceitem( 4732, 4932 );
			replaceitem( 4734, 4938 );
			replaceitem( 4736, 4944 );
			replaceitem( 4738, 4950 );
			replaceitem( 4745, 4956 );
			replaceitem( 4747, 4962 );
			replaceitem( 4749, 4968 );
			replaceitem( 4751, 4974 );
			replaceitem( 4753, 4980 );
			replaceitem( 4755, 4986 );
			replaceitem( 4757, 4992 );
			replaceitem( 4759, 4998 );
		}
		public void replaceitem( int oldID, int newID )
		{

			for ( int i2 = 0; i2 < playerItems.Length; i2++ )
			{
				if ( playerItems[i2] == oldID + 1 )
				{
					int newamount = playerItemsN[i2];
					deleteItem( oldID, getItemSlot( oldID ), playerItemsN[i2] );
					addItem( newID, newamount );
				}
			}
		}
		public void replaceitem2( int oldID, int newID )
		{

			for ( int i2 = 0; i2 < playerItems.Length; i2++ )
			{
				if ( playerItems[i2] == oldID + 1 )
				{
					int newamount = playerItemsN[i2];
					deleteItem( oldID, getItemSlot( oldID ), playerItemsN[i2] );
					ItemHandler.addItem( newID, absX, absY, newamount, playerId, false );
				}
			}
		}

		public void keepItemHandle( )
		{
			keepItem1();
			keepItem2();
			keepItem3();
			if ( itemKept1 > 0 )
			{
				if ( itemSlot1 )
					deleteItem( itemKept1, itemKept1Slot, 1 );
				else if ( !itemSlot1 )
					deleteequiment( itemKept1, itemKept1Slot );
			}
			if ( itemKept2 > 0 )
			{
				if ( itemSlot2 )
					deleteItem( itemKept2, itemKept2Slot, 1 );
				else if ( !itemSlot2 )
					deleteequiment( itemKept2, itemKept2Slot );
			}
			if ( itemKept3 > 0 )
			{
				if ( itemSlot3 )
					deleteItem( itemKept3, itemKept3Slot, 1 );
				else if ( !itemSlot3 )
					deleteequiment( itemKept3, itemKept3Slot );
			}
		}



		public int getarrowgfxnow( )
		{
			if ( playerEquipment[playerWeapon] == 4212 )
			{
				return 249;
			}
			if ( playerEquipment[playerArrows] == 892 )
			{
				return 15;
			}
			if ( playerEquipment[playerArrows] == 4740 )
			{
				return 27;
			}
			if ( playerEquipment[playerArrows] == 890 )
			{
				return 13;
			}
			if ( playerEquipment[playerArrows] == 888 )
			{
				return 12;
			}
			if ( playerEquipment[playerArrows] == 886 )
			{
				return 11;
			}
			if ( playerEquipment[playerArrows] == 884 )
			{
				return 9;
			}
			if ( playerEquipment[playerArrows] == -1 )
			{
				return 249;
			}
			if ( playerEquipment[playerArrows] == 882 )
			{
				return 10;
			}
			else { return -1; }
		}



		public void refreshhps( )
		{
			sendQuest( "" + currentHealth + "", 4016 );
			sendQuest( "" + playerXP[3] + "", 4080 );
			sendQuest( "" + getLevelForXP( playerXP[3] ) + "", 4017 );
			sendQuest( "" + getXPForLevel( playerLevel[3] + 1 ) + "", 4081 );
		}

		public void rearrangeBank( )
		{ //this fills up the empty spots
			int totalItems = 0;
			int highestSlot = 0;
			for ( int i = 0; i < playerBankSize; i++ )
			{
				if ( bankItems[i] != 0 )
				{
					totalItems++;
					if ( highestSlot <= i ) highestSlot = i;
				}
			}

			for ( int i = 0; i <= highestSlot; i++ )
			{
				if ( bankItems[i] == 0 )
				{
					Boolean stop = false;

					for ( int k = i; k <= highestSlot; k++ )
					{
						if ( bankItems[k] != 0 && !stop )
						{
							int spots = k - i;
							for ( int j = k; j <= highestSlot; j++ )
							{
								bankItems[j - spots] = bankItems[j];
								bankItemsN[j - spots] = bankItemsN[j];
								stop = true;
								bankItems[j] = 0; bankItemsN[j] = 0;
							}
						}
					}
				}
			}

			int totalItemsAfter = 0;
			for ( int i = 0; i < playerBankSize; i++ )
			{
				if ( bankItems[i] != 0 ) { totalItemsAfter++; }
			}

			if ( totalItems != totalItemsAfter ) outStream.createFrame( 109 ); //disconnects when duping

		}
		public void resetPickup( )
		{
			apickupid = -1;
			apickupx = -1;
			apickupy = -1;
		}
		public int apickupid = -1;
		public int apickupx = -1;
		public int apickupy = -1;
		public void scanPickup( )
		{
			if ( absX == apickupx && absY == apickupy )
			{
				if ( ItemHandler.itemExists( apickupid, absX, absY ) )
				{
					int itemAmount = ItemHandler.itemAmount( apickupid, apickupx, apickupy );
					if ( addItem( apickupid, itemAmount ) )
					{
						ItemHandler.removeItem( apickupid, apickupx, apickupy, itemAmount );
					}
				}
				resetPickup();
			}
		}
		public String getItemName( int ItemID )
		{
			for ( int i = 0; i < ItemHandler.MaxListedItems; i++ )
			{
				if ( ItemHandler.ItemList[i] != null )
				{
					if ( ItemHandler.ItemList[i].itemId == ItemID )
					{
						return ItemHandler.ItemList[i].itemName;
					}
				}
			}
			return "!!! ITEM NOT FULLY ADDED !!! - ID:" + ItemID;
		}
		public String GetItemName( int ItemID )
		{
			for ( int i = 0; i < ItemHandler.MaxListedItems; i++ )
			{
				if ( ItemHandler.ItemList[i] != null )
				{
					if ( ItemHandler.ItemList[i].itemId == ItemID )
					{
						return ItemHandler.ItemList[i].itemName;
					}
					if ( ItemID == -1 )
					{
						return "Unarmed";
					}
				}
			}
			return "!!! ITEM NOT FULLY ADDED !!! - ID:" + ItemID;
		}

		public void addClick( int ms )
		{
			println( "adding click " + ms + " (clickindex = " + clickIndex + " )" );
			if ( clickIndex <= clicks.Length - 1 )
			{
				clicks[clickIndex] = ms;
				clickIndex++;
			}
			else
			{
				int[] precision = new int[50];
				int average = Average( clicks );
				for ( int i = 0; i < precision.Length; i++ )
				{
					precision[i] = Math.Abs( clicks[i] - average );
					println( "precision[" + i + "]=" + precision[i] );
				}
				resetArray( clicks );
				clickIndex = 0;
			}
		}

		public void AddDroppedItems( )
		{
			if ( IsDropping == false )
			{
				IsDropping = true;
				int tmpX = 0;
				int tmpY = 0;
				int calcX = 0;
				int calcY = 0;

				for ( int i = 0; i < ItemHandler.DropItemCount; i++ )
				{
					if ( ItemHandler.DroppedItemsID[i] > -1 )
					{
						tmpX = ItemHandler.DroppedItemsX[i];
						tmpY = ItemHandler.DroppedItemsY[i];
						calcX = tmpX - absX;
						calcY = tmpY - absY;
						if ( ( calcX >= -16 )
								&& ( calcX <= 15 )
								&& ( calcY >= -16 )
								&& ( calcY <= 15 )
								&& ( MustDelete[i] == false )
								&& ( ItemHandler.DroppedItemsH[i] == heightLevel ) )
						{
							if ( ( IsDropped[i] == false )
									&& ( ( ItemHandler.DroppedItemsDDelay[i] <= 0 ) || ( ItemHandler.DroppedItemsDropper[i] == playerId ) ) )
							{
								IsDropped[i] = true;
								outStream.createFrame( 85 );
								outStream
										.writeByteC( ( ItemHandler.DroppedItemsY[i] - 8 * mapRegionY ) );
								outStream
										.writeByteC( ( ItemHandler.DroppedItemsX[i] - 8 * mapRegionX ) );
								outStream.createFrame( 44 ); // create item frame
								outStream
										.writeWordBigEndianA( ItemHandler.DroppedItemsID[i] );
								outStream
										.writeWord( ItemHandler.DroppedItemsN[i] ); // amount
								outStream.writeByte( 0 ); // x(4 MSB) y(LSB) coords
							}
						}
						else if ( ( IsDropped[i] == true )
							  || ( MustDelete[i] == true ) )
						{
							outStream.createFrame( 85 );
							outStream
									.writeByteC( ( ItemHandler.DroppedItemsY[i] - 8 * mapRegionY ) );
							outStream
									.writeByteC( ( ItemHandler.DroppedItemsX[i] - 8 * mapRegionX ) );
							outStream.createFrame( 156 ); // remove item frame
							outStream.writeByteS( 0 ); // x(4 MSB) y(LSB) coords
							outStream
									.writeWord( ItemHandler.DroppedItemsID[i] );
							int LastPlayerInList = -1;
							int TotalPlayers = 0;

							for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
							{
								if ( PlayerHandler.players[j] != null )
								{
									LastPlayerInList = j;
									TotalPlayers++;
								}
							}
							if ( MustDelete[i] == true )
							{
								MustDelete[i] = false;
								ItemHandler.DroppedItemsDeletecount[i]++;
								if ( ( ( LastPlayerInList == playerId ) || ( LastPlayerInList == -1 ) )
										&& ( ItemHandler.DroppedItemsDeletecount[i] == TotalPlayers ) )
								{
									if ( ItemHandler.DroppedItemsAlwaysDrop[i] == true )
									{
										ItemHandler.DroppedItemsDropper[i] = -1;
										ItemHandler.DroppedItemsDDelay[i] = ItemHandler.SDID;
									}
									else
									{
										server.itemHandler.ResetItem( i );
									}
									for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
									{
										if ( PlayerHandler.players[j] != null )
										{
											PlayerHandler.players[j].IsDropped[i] = false;
										}
									}
								}
							}
							else
							{
								IsDropped[i] = false;
							}
						}
					}
				}
				IsDropping = false;
			}
		}

		public void AddGlobalObj( int objectX, int objectY, int NewObjectID,
				int Face, int ObjectType )
		{
			// foreach (Player p in PlayerHandler.players) {
			// Linux (java 1.4.2-compatible) change - Sharp317
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;

					if ( person.playerName != null )
					{
						if ( person.distanceToPoint( objectX, objectY ) <= 60 )
						{
							person.ReplaceObject2( objectX, objectY, NewObjectID,
									Face, ObjectType );
						}
					}
				}
			}
		}

		public Boolean addItem( int item, int amount )
		{
			if ( item >= Item.itemStackable.Length )
			{
				return false;
			}
			if ( ( item < 0 ) || ( amount < 1 ) )
			{
				return false;
			}
			if ( !Item.itemStackable[item] || ( amount < 1 ) )
			{
				amount = 1;
			}

			if ( ( ( freeSlots() >= amount ) && !Item.itemStackable[item] )
					|| ( freeSlots() > 0 ) )
			{
				for ( int i = 0; i < playerItems.Length; i++ )
				{
					if ( ( playerItems[i] == ( item + 1 ) ) && Item.itemStackable[item]
							&& ( playerItems[i] > 0 ) )
					{
						playerItems[i] = ( item + 1 );
						if ( ( ( playerItemsN[i] + amount ) < maxItemAmount )
								&& ( ( playerItemsN[i] + amount ) > -1 ) )
						{
							playerItemsN[i] += amount;
						}
						else
						{
							playerItemsN[i] = maxItemAmount;
						}
						outStream.createFrameVarSizeWord( 34 );
						outStream.writeWord( 3214 );
						outStream.writeByte( i );
						outStream.writeWord( playerItems[i] );
						if ( playerItemsN[i] > 254 )
						{
							outStream.writeByte( 255 );
							outStream.writeDWord( playerItemsN[i] );
						}
						else
						{
							outStream.writeByte( playerItemsN[i] ); // amount
						}
						outStream.endFrameVarSizeWord();
						i = 30;
						return true;
					}
				}
				for ( int i = 0; i < playerItems.Length; i++ )
				{
					if ( playerItems[i] <= 0 )
					{
						playerItems[i] = item + 1;
						if ( ( amount < maxItemAmount ) && ( amount > -1 ) )
						{
							playerItemsN[i] = amount;
						}
						else
						{
							playerItemsN[i] = maxItemAmount;
						}
						outStream.createFrameVarSizeWord( 34 );
						outStream.writeWord( 3214 );
						outStream.writeByte( i );
						outStream.writeWord( playerItems[i] );
						if ( playerItemsN[i] > 254 )
						{
							outStream.writeByte( 255 );
							outStream.writeDWord( playerItemsN[i] );
						}
						else
						{
							outStream.writeByte( playerItemsN[i] ); // amount
						}
						outStream.endFrameVarSizeWord();
						i = 30;
						return true;
					}
				}
				return false;
			}
			else
			{
				sendMessage( "Not enough space in your inventory." );
				return false;
			}
		}
		public Boolean playerHasItemAmount( int itemID, int itemAmount )
		{
			//if(itemID == 0 || itemAmount == 0) return true;
			playerItemAmountCount = 0;
			for ( int i = 0; i < playerItems.Length; i++ )
			{
				if ( playerItems[i] == itemID + 1 )
				{
					playerItemAmountCount = playerItemsN[i];
				}
				if ( playerItemAmountCount >= itemAmount )
				{
					return true;
				}
			}
			//return (itemAmount <= playerItemAmountCount);
			return false;
		}


		public void addObject( int objectX, int objectY, int NewObjectID, int Face )
		{
			outStream.createFrameVarSizeWord( 60 );
			outStream.writeByte( objectY - ( mapRegionY * 8 ) );
			outStream.writeByteC( objectX - ( mapRegionX * 8 ) );

			/* CREATE OBJECT */
			if ( NewObjectID > -1 )
			{
				outStream.writeByte( 151 );
				outStream.writeByteS( 0 );
				outStream.writeWordBigEndian( NewObjectID );
				outStream.writeByteA( Face ); // 0= WEST | -1 = NORTH | -2 = EAST | -3
											  // = SOUTH
			}
			outStream.endFrameVarSizeWord();
		}

		public void AddObjectFire( )
		{
			if ( IsFireing == false )
			{
				IsFireing = true;
				int tmpX = 0;
				int tmpY = 0;
				int calcX = 0;
				int calcY = 0;

				for ( int i = 0; i < ObjectHandler.MaxObjects; i++ )
				{
					if ( ItemHandler.DroppedItemsID[i] > -1 )
					{
						tmpX = ObjectHandler.ObjectFireX[i];
						tmpY = ObjectHandler.ObjectFireY[i];
						calcX = tmpX - absX;
						calcY = tmpY - absY;
						if ( ( calcX >= -16 )
								&& ( calcX <= 15 )
								&& ( calcY >= -16 )
								&& ( calcY <= 15 )
								&& ( FireDelete[i] == false )
								&& ( ObjectHandler.ObjectFireH[i] == heightLevel ) )
						{
							if ( IsFireShowed[i] == false )
							{
								IsFireShowed[i] = true;
								ReplaceObject( ObjectHandler.ObjectFireX[i],
										ObjectHandler.ObjectFireY[i],
										ObjectHandler.ObjectFireID[i], 0, 10 );
							}
						}
						else if ( ( IsFireShowed[i] == true )
							  || ( FireDelete[i] == true ) )
						{
							ReplaceObject( ObjectHandler.ObjectFireX[i],
									ObjectHandler.ObjectFireY[i], -1, 0, 10 );
							int LastPlayerInList = -1;
							int TotalPlayers = 0;

							for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
							{
								if ( PlayerHandler.players[j] != null )
								{
									LastPlayerInList = j;
									TotalPlayers++;
								}
							}
							if ( FireDelete[i] == true )
							{
								FireDelete[i] = false;
								ObjectHandler.ObjectFireDeletecount[i]++;
								if ( ( ( LastPlayerInList == playerId ) || ( LastPlayerInList == -1 ) )
										&& ( ObjectHandler.ObjectFireDeletecount[i] == TotalPlayers ) )
								{
									server.objectHandler.ResetFire( i );
									for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
									{
										if ( PlayerHandler.players[j] != null )
										{
											PlayerHandler.players[j].IsFireShowed[i] = false;
										}
									}
									if ( misc.random( 2 ) == 1 )
									{
										createItem( 592 );
									}
								}
							}
							else
							{
								IsFireShowed[i] = false;
							}
						}
					}
				}
				IsFireing = false;
			}
		}

		public Boolean addShopItem( int itemID, int amount )
		{
			Boolean Added = false;

			if ( amount <= 0 )
			{
				return false;
			}
			if ( Item.itemIsNote[itemID] == true )
			{
				itemID = GetUnnotedItem( itemID );
			}
			for ( int i = 0; i < ShopHandler.ShopItems.Length; i++ )
			{
				if ( ( ShopHandler.ShopItems[MyShopID][i] - 1 ) == itemID )
				{
					ShopHandler.ShopItemsN[MyShopID][i] += amount;
					Added = true;
				}
			}
			if ( Added == false )
			{
				for ( int i = 0; i < ShopHandler.ShopItems.Length; i++ )
				{
					if ( ShopHandler.ShopItems[MyShopID][i] == 0 )
					{
						ShopHandler.ShopItems[MyShopID][i] = ( itemID + 1 );
						ShopHandler.ShopItemsN[MyShopID][i] = amount;
						ShopHandler.ShopItemsDelay[MyShopID][i] = 0;
						break;
					}
				}
			}
			return true;
		}

		public Boolean addSkillXP( int amount, int skill )
		{
			if ( randomed )
			{
				sendMessage( "You must answer the genie before you can gain experience!" );
				return false;
			}

			int oldLevel = getLevelForXP( playerXP[skill] );
			// int[] statId = {4004, 4008, 4006, 4016, 4010, 4012, 4014, 4034, 4038,
			// 4026, 4032, 4036, 4024, 4030, 4028, 4020, 4018, 4022, 4152};
			playerXP[skill] += amount;
			if ( oldLevel < getLevelForXP( playerXP[skill] ) )
			{
				// if(oldLevel >= 85)
				animation( 199, absY, absX );
				playerLevel[skill] = getLevelForXP( playerXP[skill] );
				// stillgfx(623, absY, absX);
				// levelup(skill);
				updateRequired = true;
				appearanceUpdateRequired = true;
				sendMessage( "Congratulations, you just advanced a "
						+ statName[skill] + " level." );
				if ( playerLevel[skill] > 90 )
					yell( playerName + "'s " + statName[skill] + " level is now "
							+ playerLevel[skill] + "!" );
				// sendFrame126(playerName + " (" + combatLevel + ")", 6572);
				setSkillLevel( skill, playerLevel[skill], playerXP[skill] );
			}
			// setSkillLevel(skill, playerLevel[skill], playerXP[skill]);
			refreshSkills();
			if ( skill == 2 )
			{
				CalculateMaxHit();
			}
			return true;

		}

		public int amountOfItem( int itemID )
		{
			int i1 = 0;

			for ( int i = 0; i < 28; i++ )
			{
				if ( playerItems[i] == ( itemID + 1 ) )
				{
					i1++;
				}
			}
			return i1;
		}

		public void animation( int id, int Y, int X )
		{
			// ANIMATIONS AT GROUND HEIGHT
			// foreach (Player p in PlayerHandler.players) {
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;

					if ( person.playerName != null )
					{
						if ( person.distanceToPoint( X, Y ) <= 60 )
						{
							person.animation2( id, Y, X );
						}
					}
				}
			}
		}

		public void animation2( int id, int Y, int X )
		{
			// ANIMATIONS AT GROUND HEIGHT
			outStream.createFrame( 85 );
			outStream.writeByteC( Y - ( mapRegionY * 8 ) );
			outStream.writeByteC( X - ( mapRegionX * 8 ) );
			outStream.createFrame( 4 );
			outStream.writeByte( 0 ); // Tiles away (X >> 4 + Y & 7)
			outStream.writeWord( id ); // Graphic id
			outStream.writeByte( 0 ); // height of the spell above it's basic place, i
									  // think it's written in pixels 100 pixels high
			outStream.writeWord( 0 ); // Time before casting the graphic
		}

		public void animation3( int id, int Y, int X )
		{
			// ANIMATIONS AT MIDDLE HEIGHT
			// foreach (Player p in PlayerHandler.players) {
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;

					if ( person.playerName != null )
					{
						if ( person.distanceToPoint( X, Y ) <= 60 )
						{
							person.animation4( id, Y, X );
						}
					}
				}
			}
		}

		public void animation4( int id, int Y, int X )
		{
			// ANIMATIONS AT GROUND HEIGHT
			outStream.createFrame( 85 );
			outStream.writeByteC( Y - ( mapRegionY * 8 ) );
			outStream.writeByteC( X - ( mapRegionX * 8 ) );
			outStream.createFrame( 4 );
			outStream.writeByte( 0 ); // Tiles away (X >> 4 + Y & 7)
			outStream.writeWord( id ); // Graphic id
			outStream.writeByte( 0 ); // height of the spell above it's basic place, i
									  // think it's written in pixels 100 pixels high
			outStream.writeWord( 0 ); // Time before casting the graphic
		}

		public Boolean antiHax( )
		{
			if ( TimeHelper.CurrentTimeMillis() - lastMouse > 5000 )
			{
				println( "Suspicious activity!" );
				sendMessage( "Client hack detected!" );
				sendMessage( "The only supported clients are the Rs317.Sharp client, devolution client and moparscape" );
				return false;
			}
			return true;
		}



		public void getEnd( )
		{
			if ( Projectile( MagicHandler.spellID ) )
			{
				stillgfx( ffinishid, fenemyY, fenemyX );
			}
			if ( !Projectile( MagicHandler.spellID ) )
			{
				stillgfx( fcastid, fenemyY, fenemyX );
			}
		}
		public void stillgfx10( int id, int y, int x )
		{
			stillgfx10( id, y, x, 100, 0 );
		}

		public void stillgfx10( int id, int Y, int X, int height, int time )
		{
			// foreach (Player p in PlayerHandler.players) {
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;

					if ( person.playerName != null )
					{
						if ( person.distanceToPoint( X, Y ) <= 60 )
						{
							person.stillgfx2( id, Y, X, height, time );
						}
					}
				}
			}
		}

		public void appendHitToNpc( int index, int hitDiff, Boolean stillSpell )
		{
			Boolean splash = false;
			if ( !playerMage2( index ) )
			{
				splash = true;
			}
			if ( !splash )
			{
				getEnd();
			}
			if ( splash )
			{
				stillgfx10( 85, fenemyY, fenemyX );

			}
			if ( MagicHandler.npcHP - hitDiff < 0 && !splash )
			{
				hitDiff = MagicHandler.npcHP;
			}
			if ( MagicHandler.itHeals && !splash )
			{
				if ( misc.random( 2 ) == 1 )
				{
					currentHealth += hitDiff / 4;
					if ( currentHealth > playerLevel[playerHitpoints] )
						currentHealth = playerLevel[playerHitpoints];
					sendQuest( "" + currentHealth + "", 4016 );
					sendMessage( "You drain the enemies health." );
				}
			}
			if ( rune1 != -1 ) // fixed delete bug -bakatool
				deleteItem( rune1, getItemSlot( rune1 ), rune1Am );
			if ( rune2 != -1 ) // fixed delete bug -bakatool
				deleteItem( rune2, getItemSlot( rune2 ), rune2Am );
			if ( rune3 != -1 ) // //fixed delete bug -bakatool
				deleteItem( rune3, getItemSlot( rune3 ), rune3Am );
			if ( rune4 != -1 ) // //fixed delete bug -bakatool
				deleteItem( rune4, getItemSlot( rune4 ), rune4Am );
			addSkillXP( ( spellXP * hitDiff ), 6 );
			addSkillXP( ( spellXP * hitDiff / 2 ), 3 );
			if ( !splash )
			{
				server.npcHandler.npcs[index].StartKilling = playerId;
				server.npcHandler.npcs[index].RandomWalk = false;
				server.npcHandler.npcs[index].IsUnderAttack = true;
				server.npcHandler.npcs[index].hitDiff = hitDiff;
				server.npcHandler.npcs[index].Killing[playerId] += hitDiff;
				server.npcHandler.npcs[index].updateRequired = true;
				server.npcHandler.npcs[index].hitUpdateRequired = true;
				server.npcHandler.npcs[index].hit = true;
			}
		}




		public Boolean checkTeleblock( )
		{
			if ( MagicHandler.spellID == 12445 )
			{
				return true;
			}
			return false;
		}

		public void getEnd2( int index )
		{
			client mage = ( client ) PlayerHandler.players[index];
			if ( Projectile( MagicHandler.spellID ) )
			{
				mage.gfx0( fcastid, 0 );
			}
			if ( !Projectile( MagicHandler.spellID ) )
			{
				mage.gfx0( fcastid, 0 );
			}
		}
		public void appendHitToPlayer( int index, int hitDiff, Boolean stillSpell )
		{
			try
			{
				if ( PlayerHandler.players[index] != null )
				{
					client mage = ( client ) PlayerHandler.players[index];
					Boolean splash = false;
					if ( !playerMage( index ) )
					{
						splash = true;
					}
					if ( !splash )
					{
						getEnd2( index );
					}
					if ( splash )
					{
						mage.gfx100( 85 );
					}
					if ( MagicHandler.playerHP - hitDiff < 0 && !splash )
					{
						hitDiff = MagicHandler.playerHP;
					}
					if ( MagicHandler.itHeals && !splash )
					{
						if ( hitDiff > 0 )
						{
							currentHealth += hitDiff / 4;
							if ( currentHealth > playerLevel[playerHitpoints] )
								currentHealth = playerLevel[playerHitpoints];
							sendQuest( "" + currentHealth + "", 4016 );
							sendMessage( "You drain the enemies health." );
						}
					}
					if ( MagicHandler.itTeleblocks && !splash )
					{
						if ( TimeHelper.CurrentTimeMillis() - lastTeleblock >= 300000 )
						{
							mage.sendMessage( "You have been teleblocked!" );
							PlayerHandler.players[index].lastTeleblock = TimeHelper.CurrentTimeMillis();
						}
					}
					if ( MagicHandler.itFreezes && !splash )
					{
						if ( PlayerHandler.players[index].EntangleDelay <= 0 )
						{
							PlayerHandler.players[index].lastEntangle = TimeHelper.CurrentTimeMillis();
							PlayerHandler.players[index].entangleDelay = getFreezeTimer( MagicHandler.spellID );
							mage.sendMessage( "You have been frozen!" );
							mage.teleportToX = mage.absX;
							mage.teleportToY = mage.absY;
						}
					}
					client player = ( client ) PlayerHandler.players[playerId];
					if ( rune1 != -1 ) // fixed delete bug -bakatool
						deleteItem( rune1, getItemSlot( rune1 ), rune1Am );
					if ( rune2 != -1 ) // fixed delete bug -bakatool
						deleteItem( rune2, getItemSlot( rune2 ), rune2Am );
					if ( rune3 != -1 ) // //fixed delete bug -bakatool
						deleteItem( rune3, getItemSlot( rune3 ), rune3Am );
					if ( rune4 != -1 ) // //fixed delete bug -bakatool
						deleteItem( rune4, getItemSlot( rune4 ), rune4Am );
					addSkillXP( ( spellXP * hitDiff ), 6 );
					addSkillXP( ( spellXP * hitDiff / 2 ), 3 );
					if ( !MagicHandler.itTeleblocks && !splash )
					{
						PlayerHandler.players[index].dealDamage( hitDiff );
						PlayerHandler.players[index].hitDiff = hitDiff;
						PlayerHandler.players[index].updateRequired = true;
						PlayerHandler.players[index].hitUpdateRequired = true;
						PlayerHandler.players[index].KilledBy[playerId] += hitDiff;
						PlayerHandler.players[index].offTimer = TimeHelper.CurrentTimeMillis();
						PlayerHandler.players[index].hitID = playerId;
						mage.KillerId = playerId;
					}
				}
			}
			catch ( Exception e )
			{
			}
		}
		public Boolean TallSpell( int i )
		{
			if ( i == 12963 || i == 13011 || i == 12919 || i == 12881 || i == 12975 || i == 13023 || i == 12929 || i == 12891 )
			{
				return true;
			}
			return false;
		}

		public Boolean playerMage2( int indexx )
		{
			int magicBonus = playerBonus[3] * 5 + misc.random( 100 );
			int negative = CheckBestBonus2();
			int negativeBonus = playerBonus[negative];
			if ( misc.random( magicBonus ) > misc.random( negativeBonus ) )
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public Boolean firespell( int castID, int casterY, int casterX, int offsetY,
				int offsetX, int angle, int speed, int movegfxID, int startHeight,
				int endHeight, int MageAttackIndex, int finishID, int enemyY,
				int enemyX )
		{
			fcastid = castID;
			fcasterY = casterY;
			fcasterX = casterX;
			foffsetY = offsetY;
			foffsetX = offsetX;
			fangle = angle;
			fspeed = speed;
			fmgfxid = movegfxID;
			fsh = startHeight;
			feh = endHeight;
			ffinishid = finishID;
			fenemyY = enemyY;
			fenemyX = enemyX;
			MageAttackIndex = MageAttackIndex;

			// Casts Spell In Hands
			if ( ( cast == false ) && Projectile( MagicHandler.spellID ) && casterY == absY && casterX == absX )
			{
				specGFX( castID );
				cast = true;
				firingspell = true;
			}
			if ( ( cast == false ) && !Projectile( MagicHandler.spellID ) )
			{
				cast = true;
				firingspell = true;
			}
			// Fires Projectile
			if ( ( cast == true ) && ( fired == false ) && Projectile( MagicHandler.spellID ) )
			{
				createProjectile( casterY, casterX, offsetY, offsetX, angle, speed,
						movegfxID, startHeight, endHeight, MageAttackIndex );
				fired = true;
			}
			if ( ( cast == true ) && ( fired == false ) && !Projectile( MagicHandler.spellID ) )
			{
				fired = true;
			}
			// Finishes Spell
			if ( fired == true )
			{
				resetGFX( castID, enemyX, enemyY );
				return false;
			}
			return true;

		} // Resets Projectiles

		public void appendToBanned( String player )
		{
			try
			{
				File.AppendAllLines( "data//bannedusers.txt", new String[] 
				{
					player
				} );
			}
			catch ( IOException ioe )
			{
				//ioe.printStackTrace();
			}
		}

		public Boolean AreXItemsInBag( int ItemID, int Amount )
		{
			int ItemCount = 0;

			foreach ( int pItem in playerItems )
			{
				if ( ( pItem - 1 ) == ItemID )
				{
					ItemCount++;
				}
				if ( ItemCount == Amount )
				{
					return true;
				}
			}
			return false;
		}




		public int arrowPullBackPvp( )
		{

			int arrowgfx2 = getarrowgfxnow();
			damagedelay2 = 4;
			client A = ( client ) PlayerHandler.players[AttackingOn];
			long thisAttack = TimeHelper.CurrentTimeMillis();
			int[] arrowIds = { 882, 884, 886, 888, 890, 892 };
			int playerIndex = inStream.readSignedWordA();
			int EnemyX = PlayerHandler.players[AttackingOn].absX;
			int EnemyY = PlayerHandler.players[AttackingOn].absY;
			int offsetX = ( absY - EnemyY ) * -1;
			int offsetY = ( absX - EnemyX ) * -1;

			if ( thisAttack - lastAttack >= 20 )
			{
				if ( playerEquipment[playerWeapon] != 4212 && playerEquipment[playerWeapon] != 15156 )
				{
					Projectile2( absY, absX, offsetX, offsetY, 50, 78, arrowgfx2, 43, 31, A.playerId );

				}
				if ( playerEquipment[playerWeapon] == 4212 )
				{
					Projectile2( absY, absX, offsetX, offsetY, 50, 78, 249, 45, 30, A.playerId + 1 );

				}
				if ( playerEquipment[playerWeapon] == 15156 )
				{
					damagedelay3 = 6;
					Projectile2( absY, absX, offsetX, offsetY, 50, 115, 249, 45, 30, A.playerId + 1 );
					Projectile2( absY, absX, offsetX, offsetY, 50, 84, 249, 45, 35, A.playerId + 1 );
					//GraphicsHandler.createProjectile(arrowgfx2, absY, absX, offsetY, offsetX, 50, 115, arrowgfx2, 45, 30, EnemyIndexP + 1, 410, EnemyY, EnemyX, 20);	
					//GraphicsHandler.createProjectile(arrowgfx2, absY, absX, offsetY, offsetX, 50, 84, arrowgfx2, 45, 35, EnemyIndexP + 1, 410, EnemyY, EnemyX, 20);	
				}
			}

			if ( ( playerEquipment[playerArrows] == 892 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 24;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 631;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 890 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 22;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 888 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 21;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 886 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 20;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 884 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 18;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( playerEquipment[playerWeapon] == 4212 )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 882 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 19;
			}
			else
			{
				lastAttack = TimeHelper.CurrentTimeMillis();
				return -1;
			}
		}

		public Boolean isattackingaplayer = false;

		public Boolean Attack( )
		{
			isattackingaplayer = true;
			client AttackingOn2 = ( client ) PlayerHandler.players[AttackingOn];
			int arrowgfx = 10;
			int hitDiff = 0;
			int EnemyX = PlayerHandler.players[AttackingOn].absX;
			int EnemyY = PlayerHandler.players[AttackingOn].absY;
			int[] arrowIds = { 882, 884, 886, 888, 890, 892 };
			int[] arrowGfx = { 10, 9, 11, 12, 13, 15 };
			Boolean UseBow = false;
			Boolean canAttacK = true;
			int offsetX = ( absY - EnemyY ) * -1;
			int offsetY = ( absX - EnemyX ) * -1;
			int EnemyHP = PlayerHandler.players[AttackingOn].playerLevel[playerHitpoints];
			int EnemyHPExp = PlayerHandler.players[AttackingOn].playerXP[playerHitpoints];

			client other = getClient( duel_with );
			if ( hitID != AttackingOn2.playerId && hitID != 0 && !multiCombat() )
			{
				sendMessage( "I'm already under attack." );
				ResetAttack();
				canAttacK = false;
				return false;
			}
			followPlayer( AttackingOn2.playerId );
			if ( AttackingOn2.duelFight == false && duelFight == true && AttackingOn2 != other )
			{
				sendMessage( "you can only attack your enemy" );
				ResetAttack();
				canAttacK = false;
			}
			if ( ( isInWilderness( EnemyX, EnemyY, 1 ) == true ) && ( isInWilderness( absX, absY, 1 ) == false ) )
			{
				sendMessage( "Maybe i should move into the Wilderness." );
				ResetAttack();
				canAttacK = false;

			}
			if ( ( isInWilderness( EnemyX, EnemyY, 1 ) == false ) && ( isInWilderness( absX, absY, 1 ) == true ) )
			{
				sendMessage( "I can't hurt innocent players." );
				ResetAttack();
				canAttacK = false;
				WalkTo( 0, 0 );
			}




			TurnPlayerTo( EnemyX, EnemyY );
			updateRequired = true;
			appearanceUpdateRequired = true;
			for ( int i = 0; i < Constants.shortbow.Length; i++ )
			{
				if ( ( playerEquipment[playerWeapon] == Constants.shortbow[i] ) || ( playerEquipment[playerWeapon] == Constants.longbow[i] ) )
				{
					UseBow = true;
					break;
				}
			}
			for ( int i1 = 0; i1 < arrowIds.Length; i1++ )
			{
				if ( playerEquipment[playerArrows] == arrowIds[i1] )
				{
					arrowgfx = arrowGfx[i1];
				}
			}
			if ( ( duelFight && !( AttackingOn == duel_with ) ) || AttackingOn2.combatLevel + AttackingOn2.wildyLevel < combatLevel && duelFight == false || combatLevel + AttackingOn2.wildyLevel < AttackingOn2.combatLevel && duelFight == false )
			{
				sendMessage( "You need to move deeper into the Wilderness." );
				ResetAttack();
				canAttacK = false;
				WalkTo( 0, 0 );
			}
			if ( AttackingOn2.hitID != playerId && AttackingOn2.hitID != 0 && !multiCombat() )
			{
				sendMessage( "Someone else is already fighting your opponent." );
				ResetAttack();
				canAttacK = false;
				setFaceNPC( 32768 + AttackingOn );
				return false;
			}
			if ( playerEquipment[playerArrows] == -1 && playerEquipment[playerWeapon] == 4212 && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttack();
				canAttacK = false;
				sendMessage( "You are out of arrows!" );
			}
			if ( ( playerEquipment[playerWeapon] == 4734 ) && ( playerEquipment[playerArrows] != 4740 ) && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttackNPC();
				sendMessage( "you cannot use these arrows with this bow!" );
			}
			if ( playerEquipment[playerWeapon] == 841 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 843 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 849 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 853 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 857 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 861 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 839 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 845 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 847 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 851 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 855 && playerEquipment[playerArrows] == 4740 || playerEquipment[playerWeapon] == 859 && playerEquipment[playerArrows] == 4740 && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttack();
				canAttacK = false;
				sendMessage( "you cannot use this bow with these arrows!" );
			}
			if ( ( playerEquipment[playerWeapon] == 4212 ) && ( playerEquipment[playerArrows] != -1 ) && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttack();
				canAttacK = false;
				sendMessage( "you cannot use arrows with this bow!" );
			}
			if ( absX == EnemyX && absY == EnemyY )
			{
				WalkTo( 0, -1 );
			}

			if ( TimeHelper.CurrentTimeMillis() - lastAction > weaponDelay() )
			{

				long thisAttack = TimeHelper.CurrentTimeMillis();
				if ( ( !UseBow && GoodDistance( EnemyX, EnemyY, absX, absY, 1 ) == true ) && canAttacK == true )
				{
					DeleteArrow();
					damagedelay2 = 1;
					AttackingOn2.KillerId = playerId;
					setAnimation( GetWepAnim() );
					Usingbow = false;
					isFighting = true;
					lastAction = TimeHelper.CurrentTimeMillis();
					lastAttack = TimeHelper.CurrentTimeMillis();
				}
				if ( ( UseBow && GoodDistance( EnemyX, EnemyY, absX, absY, 9 ) == true ) && canAttacK == true )
				{
					DeleteArrow();
					WalkTo( 0, 0 );
					AttackingOn2.KillerId = playerId;
					lastAction = TimeHelper.CurrentTimeMillis();
					setAnimation( GetWepAnim() );
					lastAction = TimeHelper.CurrentTimeMillis();
					arrowPullBack( arrowPullBackPvp() );
				}

			}
			return false;
		}



		public void DropArrowsNPC( )
		{
			int EnemyX = server.npcHandler.npcs[attacknpc].absX;
			int EnemyY = server.npcHandler.npcs[attacknpc].absY;

			if ( playerEquipment[playerWeapon] != 4214
					&& playerEquipmentN[playerArrows] != 0 )
			{
				if ( ItemHandler.itemAmount( playerEquipment[playerArrows], EnemyX,
						EnemyY )
						== 0 )
				{
					ItemHandler.addItem( playerEquipment[playerArrows], EnemyX,
							EnemyY, 1, playerId, false );
				}
				else if ( ItemHandler.itemAmount( playerEquipment[playerArrows],
					  EnemyX, EnemyY )
					  != 0 )
				{
					int amount = ItemHandler.itemAmount(
							playerEquipment[playerArrows], EnemyX, EnemyY );

					ItemHandler.removeItem( playerEquipment[playerArrows], EnemyX,
							EnemyY, amount );
					ItemHandler.addItem( playerEquipment[playerArrows], EnemyX,
							EnemyY, amount + 1, playerId, false );
				}
			}
		}

		public void GetDistance( )
		{
			int EnemyX = server.npcHandler.npcs[attacknpc].absX;
			int EnemyY = server.npcHandler.npcs[attacknpc].absY;
			int EnemyHP = server.npcHandler.npcs[attacknpc].HP;
			setFaceNPC( attacknpc );

			if ( EnemyHP < 1 )
			{
				ResetAttackNPC();
				sendMessage( "You can't attack that monster!" );
			}
			if ( absX == EnemyX && absY == EnemyY )
			{
				WalkTo( 0, -1 );
			}
		}
		public int weaponDelay( )
		{
			if ( playerEquipment[playerWeapon] == 35 || playerEquipment[playerWeapon] == 667 || playerEquipment[playerWeapon] == 8100 || playerEquipment[playerWeapon] == 2402 || playerEquipment[playerWeapon] == 746 || playerEquipment[playerWeapon] == 6528 || playerEquipment[playerWeapon] == 4153 || playerEquipment[playerWeapon] == 4718 || playerEquipment[playerWeapon] == 1377 || playerEquipment[playerWeapon] == 3204 || playerEquipment[playerWeapon] == 4827 || playerEquipment[playerWeapon] == 7158 || playerEquipment[playerWeapon] == 1319 )
			{
				return 4500;
			}
			else
			{
				if ( playerEquipment[playerWeapon] == 837 || playerEquipment[playerWeapon] == 1305 )
				{
					return 3000;
				}
				if ( playerEquipment[playerWeapon] == 1434 || playerEquipment[playerWeapon] == 4151 )
				{
					return 2500;
				}
				if ( playerEquipment[playerWeapon] == 861 )
				{
					return 2000;
				}
				else
				{
					if ( playerEquipment[playerWeapon] == 839 || playerEquipment[playerWeapon] == 841 || playerEquipment[playerWeapon] == 843 || playerEquipment[playerWeapon] == 845 || playerEquipment[playerWeapon] == 847 || playerEquipment[playerWeapon] == 849 || playerEquipment[playerWeapon] == 851 || playerEquipment[playerWeapon] == 853 || playerEquipment[playerWeapon] == 855 || playerEquipment[playerWeapon] == 857 || playerEquipment[playerWeapon] == 862 || playerEquipment[playerWeapon] == 4734 || playerEquipment[playerWeapon] == 6522 )
					{
						return 2500;
					}
					else
					{
						return 3000;
					}
				}
			}
		}

		public Boolean AttackNPC( )
		{


			int arrowgfx = 10;
			int EnemyX = server.npcHandler.npcs[attacknpc].absX;
			int EnemyY = server.npcHandler.npcs[attacknpc].absY;
			int EnemyHP = server.npcHandler.npcs[attacknpc].HP;
			int hitDiff = 0;
			int type = server.npcHandler.npcs[attacknpc].npcType;
			int[] arrowIds = { 882, 884, 886, 888, 890, 892 };
			int[] arrowGfx = { 10, 9, 11, 12, 13, 15 };
			Boolean UseBow = false;
			int offsetX = ( absY - EnemyY ) * -1;
			int offsetY = ( absX - EnemyX ) * -1;
			Boolean canAttacK = true;
			GetDistance();
			for ( int i = 0; i < Constants.shortbow.Length; i++ )
			{
				if ( ( playerEquipment[playerWeapon] == Constants.shortbow[i] ) || ( playerEquipment[playerWeapon] == Constants.longbow[i] ) )
				{
					UseBow = true;
					break;
				}
			}
			for ( int i1 = 0; i1 < arrowIds.Length; i1++ )
			{
				if ( playerEquipment[playerArrows] == arrowIds[i1] )
				{
					arrowgfx = arrowGfx[i1];
				}
			}
			long thisAttack = TimeHelper.CurrentTimeMillis();

			if ( playerEquipment[playerArrows] == -1 && playerEquipment[playerWeapon] != 4212 && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttackNPC();
				canAttacK = false;
				sendMessage( "You are out of arrows!" );
			}
			if ( ( playerEquipment[playerWeapon] == 4734 ) && ( playerEquipment[playerArrows] != 4740 ) && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttackNPC();
				canAttacK = false;
				sendMessage( "you cannot use these arrows with this bow!" );
			}
			if ( playerEquipment[playerWeapon] != 4734 && playerEquipment[playerArrows] == 4740 && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttackNPC();
				canAttacK = false;
				sendMessage( "you cannot use this bow with these arrows!" );
			}
			if ( ( playerEquipment[playerWeapon] == 4212 ) && ( playerEquipment[playerArrows] != -1 ) && UseBow == true )
			{
				WalkTo( 0, 0 );
				ResetAttackNPC();
				canAttacK = false;
				sendMessage( "you cannot use arrows with this bow!" );
			}
			if ( absX == EnemyX && absY == EnemyY )
			{
				WalkTo( 0, -1 );
			}



			if ( TimeHelper.CurrentTimeMillis() - lastAction > weaponDelay() )
			{

				if ( !UseBow && GoodDistance( EnemyX, EnemyY, absX, absY, 2 ) == true )
				{
					damagedelay = 1;
					setAnimation( GetWepAnim() );
					Usingbow = false;
					isFighting = true;
					lastAction = TimeHelper.CurrentTimeMillis();
				}
				if ( ( UseBow && GoodDistance( EnemyX, EnemyY, absX, absY, 7 ) == true ) && canAttacK == true )
				{
					setAnimation( GetWepAnim() );
					arrowPullBack( arrowPullBack() );
					lastAction = TimeHelper.CurrentTimeMillis();
					DeleteArrow();
					WalkTo( 0, 0 );

				}
			}

			return false;

		}

		public int arrowPullBack( )
		{
			int arrowgfx = 249;
			long thisAttack = TimeHelper.CurrentTimeMillis();
			damagedelay = 4;
			int EnemyX = server.npcHandler.npcs[attacknpc].absX;
			int EnemyY = server.npcHandler.npcs[attacknpc].absY;
			int EnemyHP = server.npcHandler.npcs[attacknpc].HP;
			int hitDiff = 0;
			int type = server.npcHandler.npcs[attacknpc].npcType;
			int[] arrowIds = { 882, 884, 886, 888, 890, 892 };
			int[] arrowGfx = { 10, 9, 11, 12, 13, 15 };
			Boolean UseBow = false;
			int offsetX = ( absY - EnemyY ) * -1;
			int offsetY = ( absX - EnemyX ) * -1;
			if ( IsAttackingNPC )
			{
				if ( thisAttack - lastAttack >= 20 )
				{
					if ( playerEquipment[playerWeapon] != 4212 && playerEquipment[playerWeapon] != 15156 )
					{
						int arrowgfx2 = getarrowgfxnow(); Projectile2( absY, absX, offsetX, offsetY, 50, 78, arrowgfx2, 43, 31, attacknpc + 1 );
					}
					if ( playerEquipment[playerWeapon] == 4212 )
					{
						Projectile2( absY, absX, offsetX, offsetY, 50, 85, 249, 45, 30, attacknpc + 1 );
					}
					if ( playerEquipment[playerWeapon] == 15156 )
					{
						int arrowgfx2 = getarrowgfxnow();
						damagedelay0 = 6;
						Projectile2( absY, absX, offsetX, offsetY, 50, 115, arrowgfx2, 45, 30, attacknpc + 1 );
						Projectile2( absY, absX, offsetX, offsetY, 50, 84, arrowgfx2, 40, 35, attacknpc + 1 );
					}
				}

			}

			if ( ( playerEquipment[playerArrows] == 892 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 24;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 631;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 890 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 22;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 888 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 21;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 886 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 20;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 884 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 18;
			}
			else if ( ( playerEquipment[playerWeapon] == 598 ) && ( playerEquipment[playerWeapon] == 4212 ) )
			{
				return 250;
			}
			if ( playerEquipment[playerWeapon] == 4212 )
			{
				return 250;
			}
			if ( ( playerEquipment[playerArrows] == 882 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				return 19;
			}
			else
			{
				lastAttack = TimeHelper.CurrentTimeMillis();
				return -1;
			}
		}
		public int CheckBestBonus2( )
		{
			if ( playerBonus[6] > playerBonus[7] && playerBonus[6] > playerBonus[8] )
			{
				return 6;
			}
			if ( playerBonus[7] > playerBonus[6] && playerBonus[7] > playerBonus[8] )
			{
				return 7;
			}
			if ( playerBonus[8] > playerBonus[6] && playerBonus[8] > playerBonus[7] )
			{
				return 8;
			}
			else
			{
				return 0;
			}
		}

		public Boolean playerMage( int index )
		{
			int mystic = 0;
			if ( PlayerHandler.players[index] == null )
			{
				return false;
			}
			int enemyDef = PlayerHandler.players[index].playerBonus[8];
			int myBonus = playerBonus[3] + 30 + mystic;

			if ( misc.random( myBonus ) > misc.random( enemyDef ) )
			{
				return true;
			}
			return false;
		}
		public Boolean Projectile( int spell )
		{
			if ( spell == 12939 )
			{
				return false;
			}
			if ( spell == 12987 )
			{
				return false;
			}
			if ( spell == 12901 )
			{
				return false;
			}
			if ( spell == 12861 )
			{
				return false;
			}
			if ( spell == 12951 )
			{
				return false;
			}
			if ( spell == 12999 )
			{
				return false;
			}
			if ( spell == 12911 )
			{
				return false;
			}
			if ( spell == 12871 )
			{
				return false;
			}
			if ( spell == 12963 )
			{
				return false;
			}
			if ( spell == 13011 )
			{
				return false;
			}
			if ( spell == 12919 )
			{
				return false;
			}
			if ( spell == 12881 )
			{
				return false;
			}
			if ( spell == 12975 )
			{
				return false;
			}
			if ( spell == 13023 )
			{
				return false;
			}
			if ( spell == 12929 )
			{
				return false;
			}
			if ( spell == 12891 )
			{
				return false;
			}
			if ( spell == 1191 )
			{
				return false;
			}
			if ( spell == 1190 )
			{
				return false;
			}
			if ( spell == 1192 )
			{
				return false;
			}
			return true;
		}
		public void Projectile2( int casterY, int casterX, int offsetY, int offsetX, int angle, int speed, int gfxMoving, int startHeight, int endHeight, int MageAttackIndex )
		{
			foreach ( Player p in PlayerHandler.players )
			{
				client plr = ( client ) p;
				if ( p != null && !plr.disconnected )
				{
					try
					{
						plr.outStream.createFrame( 85 );
						plr.outStream.writeByteC( ( casterY - ( plr.mapRegionY * 8 ) ) - 2 );
						plr.outStream.writeByteC( ( casterX - ( plr.mapRegionX * 8 ) ) - 3 );
						plr.outStream.createFrame( 117 );
						plr.outStream.writeByte( angle );
						plr.outStream.writeByte( offsetY );
						plr.outStream.writeByte( offsetX );
						plr.outStream.writeWord( MageAttackIndex );
						plr.outStream.writeWord( gfxMoving );
						plr.outStream.writeByte( startHeight );
						plr.outStream.writeByte( endHeight );
						plr.outStream.writeWord( 51 );
						plr.outStream.writeWord( speed );
						plr.outStream.writeByte( 16 );
						plr.outStream.writeByte( 64 );
					}
					catch ( Exception e )
					{
					}
				}
			}
		}
		public void DAMAGEPVP( )
		{
			if ( isattackingaplayer )
			{
				int EnemyX = PlayerHandler.players[AttackingOn].absX;
				int EnemyY = PlayerHandler.players[AttackingOn].absY;
				int EnemyHP = PlayerHandler.players[AttackingOn].playerLevel[playerHitpoints];
				int EnemyHPExp = PlayerHandler.players[AttackingOn].playerXP[playerHitpoints];
				client AttackingOn2 = ( client ) PlayerHandler.players[AttackingOn];

				Boolean UseBow = false;
				for ( int i = 0; i < Constants.shortbow.Length; i++ )
				{
					if ( ( playerEquipment[playerWeapon] == Constants.shortbow[i] ) || ( playerEquipment[playerWeapon] == Constants.longbow[i] ) )
					{
						UseBow = true;
						break;
					}
				}
				if ( UseBow )
				{

					int MAXHIT = misc.random( maxRangeHit() );

					if ( MAXHIT > EnemyHP )
					{
						MAXHIT = EnemyHP;
					}
					if ( FightType == 3 )
					{
						addSkillXP( MAXHIT * 100, 4 );
						addSkillXP( MAXHIT * 100, 1 );
						addSkillXP( MAXHIT * 100, 3 );
					}
					else
					{
						addSkillXP( MAXHIT * 300, 4 );
						addSkillXP( MAXHIT * 125, 3 );
					}
					PlayerHandler.players[AttackingOn].hitUpdateRequired = true;
					PlayerHandler.players[AttackingOn].updateRequired = true;
					PlayerHandler.players[AttackingOn].appearanceUpdateRequired = true;
					PlayerHandler.players[AttackingOn].dealDamage( MAXHIT );
					PlayerHandler.players[AttackingOn].hitDiff = MAXHIT;
					PlayerHandler.players[AttackingOn].killers[localId] += MAXHIT;
					PlayerHandler.players[AttackingOn].hits++;
					PlayerHandler.players[AttackingOn].fighting = true;
					PlayerHandler.players[AttackingOn].fightId = localId;
					PlayerHandler.players[AttackingOn].IsAttacking = true;
				}
				else
				{
					int aBonus = 0;
					int rand_att = misc.random( playerLevel[0] );
					int rand_def = ( int ) ( 0.65 * misc.random( AttackingOn2.playerLevel[1] ) );
					int MAXHIT = 0;
					if ( FightType == 1 )
						aBonus += ( int ) ( playerBonus[1] / 20 );
					int random_u = misc.random( playerBonus[1] + aBonus ) * 2;
					int dBonus = 0;
					if ( AttackingOn2.FightType == 4 )
						dBonus += ( int ) ( AttackingOn2.playerBonus[6] / 20 );
					int random_def = misc.random( AttackingOn2.playerBonus[6] + dBonus );

					if ( ( random_u >= random_def ) && ( rand_att > rand_def ) )
					{
						MAXHIT = misc.random( playerMaxHit );
					}
					else
					{
						MAXHIT = 0;
					}
					if ( FightType == 2 )
						MAXHIT = ( int ) ( hitDiff * 1.20 );


					if ( MAXHIT > EnemyHP )
					{
						MAXHIT = EnemyHP;
					}
					double TotalExp = 0;
					TotalExp = ( double ) ( 300 * MAXHIT );
					TotalExp = ( double ) ( TotalExp * CombatExpRate );
					addSkillXP( ( int ) ( TotalExp ), SkillID );
					addSkillXP( MAXHIT * 100, playerHitpoints );
					PlayerHandler.players[AttackingOn].hitUpdateRequired = true;
					PlayerHandler.players[AttackingOn].updateRequired = true;
					PlayerHandler.players[AttackingOn].appearanceUpdateRequired = true;
					PlayerHandler.players[AttackingOn].dealDamage( MAXHIT );
					PlayerHandler.players[AttackingOn].hitDiff = MAXHIT;
					PlayerHandler.players[AttackingOn].killers[localId] += MAXHIT;
					PlayerHandler.players[AttackingOn].hits++;
					PlayerHandler.players[AttackingOn].fighting = true;
					PlayerHandler.players[AttackingOn].fightId = localId;
					PlayerHandler.players[AttackingOn].IsAttacking = true;
				}
			}
			else { resetWalkingQueue(); }


		}
		public void DAMAGENPC( )
		{
			if ( IsAttackingNPC )
			{
				int EnemyX = server.npcHandler.npcs[attacknpc].absX;
				int EnemyY = server.npcHandler.npcs[attacknpc].absY;
				int EnemyHP = server.npcHandler.npcs[attacknpc].HP;

				Boolean UseBow = false;
				for ( int i = 0; i < Constants.shortbow.Length; i++ )
				{
					if ( ( playerEquipment[playerWeapon] == Constants.shortbow[i] ) || ( playerEquipment[playerWeapon] == Constants.longbow[i] ) )
					{
						UseBow = true;
						break;
					}
				}
				if ( UseBow )
				{

					isFighting = false;
					updateRequired = true;
					appearanceUpdateRequired = true;
					int MAXHIT = misc.random( maxRangeHit() );
					if ( FightType == 3 )
					{
						addSkillXP( MAXHIT * 100, 4 );
						addSkillXP( MAXHIT * 100, 1 );
						addSkillXP( MAXHIT * 100, 3 );
						server.npcHandler.npcs[attacknpc].Killing[playerId] += MAXHIT;
						server.npcHandler.npcs[attacknpc].StartKilling = localId;
						server.npcHandler.npcs[attacknpc].IsUnderAttack = true;
						server.npcHandler.npcs[attacknpc].RandomWalk = false;
						server.npcHandler.npcs[attacknpc].hitDiff = MAXHIT;
						server.npcHandler.npcs[attacknpc].hit = true;
						server.npcHandler.npcs[attacknpc].updateRequired = true;
						server.npcHandler.npcs[attacknpc].hitUpdateRequired = true;
					}
					else
					{
						if ( MAXHIT > EnemyHP )
						{
							MAXHIT = EnemyHP;
						}
						if ( ( misc.random( 2 ) == 1 ) && ( playerEquipment[playerWeapon] != 4212 ) )
						{
							DropArrowsNPC();
						}
						addSkillXP( MAXHIT * 300, 4 );
						addSkillXP( MAXHIT * 100, 3 );
						server.npcHandler.npcs[attacknpc].Killing[playerId] += MAXHIT;
						server.npcHandler.npcs[attacknpc].StartKilling = localId;
						server.npcHandler.npcs[attacknpc].IsUnderAttack = true;
						server.npcHandler.npcs[attacknpc].RandomWalk = false;
						server.npcHandler.npcs[attacknpc].hitDiff = MAXHIT;
						server.npcHandler.npcs[attacknpc].hit = true;
						server.npcHandler.npcs[attacknpc].updateRequired = true;
						server.npcHandler.npcs[attacknpc].hitUpdateRequired = true;

					}
				}
				if ( !UseBow )
				{
					updateRequired = true;
					appearanceUpdateRequired = true;
					int MAXHIT = misc.random( playerMaxHit );

					if ( MAXHIT > EnemyHP )
					{
						MAXHIT = EnemyHP;
					}
					if ( EnemyHP > 0 )
					{
						server.npcHandler.npcs[attacknpc].hitDiff = MAXHIT;
					}
					if ( EnemyHP < 1 )
					{
						server.npcHandler.npcs[attacknpc].hitDiff = 0;
					}
					double TotalExp = 0;
					TotalExp = ( double ) ( 300 * MAXHIT );
					TotalExp = ( double ) ( TotalExp * CombatExpRate );
					addSkillXP( ( int ) ( TotalExp ), SkillID );
					addSkillXP( MAXHIT * 100, playerHitpoints );
					server.npcHandler.npcs[attacknpc].Killing[playerId] += MAXHIT;
					server.npcHandler.npcs[attacknpc].StartKilling = localId;
					server.npcHandler.npcs[attacknpc].IsUnderAttack = true;
					server.npcHandler.npcs[attacknpc].RandomWalk = false;
					server.npcHandler.npcs[attacknpc].hitDiff = MAXHIT;
					server.npcHandler.npcs[attacknpc].hit = true;
					server.npcHandler.npcs[attacknpc].updateRequired = true;
					server.npcHandler.npcs[attacknpc].hitUpdateRequired = true;
					Usingbow = false;
					isFighting = true;
					weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
				}
			}
		}
		public int damagedelay3 = 0;

		public int damagedelay0 = 0;
		public void attackNPCSWithin( int gfx, int maxDamage, int range )
		{
			for ( int i = 0; i <= NPCHandler.maxNPCSpawns; i++ )
			{
				if ( server.npcHandler.npcs[i] != null )
				{
					if ( ( distanceToPoint( server.npcHandler.npcs[i].absX,
							server.npcHandler.npcs[i].absY ) <= range )
							&& !server.npcHandler.npcs[i].IsDead )
					{
						int damage = misc.random( maxDamage );

						animation( gfx, server.npcHandler.npcs[i].absY,
								server.npcHandler.npcs[i].absX );
						if ( server.npcHandler.npcs[i].HP - damage < 0 )
						{
							damage = server.npcHandler.npcs[i].HP;
						}
						server.npcHandler.npcs[i].StartKilling = playerId;
						server.npcHandler.npcs[i].RandomWalk = false;
						server.npcHandler.npcs[i].hitDiff = damage;
						server.npcHandler.npcs[i].updateRequired = true;
						server.npcHandler.npcs[i].hitUpdateRequired = true;
						server.npcHandler.npcs[i].hit = true;
					}
				}
			}
		}

		public void attackPlayersPrayer( int maxDamage, int range )
		{
			// foreach (Player p in PlayerHandler.players) {
			// Linux (java 1.4.2-compatible) change - Sharp317
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;

					if ( person.playerName != null )
					{
						if ( ( person.distanceToPoint( absX, absY ) <= range )
								&& ( person.playerId != playerId ) )
						{
							int damage = misc.random( maxDamage );

							if ( person.playerLevel[3] - damage < 0 )
							{
								damage = person.playerLevel[3];
							}
							person.hitDiff = damage;
							person.KillerId = playerId;
							person.updateRequired = true;
							person.hitUpdateRequired = true;
						}
					}
				}
			}
		}

		public void attackPlayersWithin( int gfx, int maxDamage, int range )
		{
			// foreach (Player p in PlayerHandler.players) {
			// Linux (java 1.4.2-compatible) change - Sharp317
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;

					if ( person.playerName != null )
					{
						if ( ( person.distanceToPoint( absX, absY ) <= range )
								&& ( person.playerId != playerId ) )
						{
							int damage = misc.random( maxDamage );

							person.animation( gfx, person.absY, person.absX );
							if ( person.playerLevel[3] - damage < 0 )
							{
								damage = person.playerLevel[3];
							}
							person.hitDiff = damage;
							person.KillerId = playerId;
							person.updateRequired = true;
							person.hitUpdateRequired = true;
						}
					}
				}
			}
		}

		public Boolean Attackrange( )
		{
			client q = ( client ) PlayerHandler.players[AttackingOn];
			int EnemyX = PlayerHandler.players[AttackingOn].absX;
			int EnemyY = PlayerHandler.players[AttackingOn].absY;
			int EnemyHP = PlayerHandler.players[AttackingOn].playerLevel[playerHitpoints];
			int EnemyHPExp = PlayerHandler.players[AttackingOn].playerXP[playerHitpoints];
			Boolean RingOfLife = false;

			if ( PlayerHandler.players[AttackingOn].playerEquipment[playerRing] == 2570 )
			{
				RingOfLife = true;
			}
			int hitDiff = 0;

			hitDiff = misc.random( playerMaxHit );

			if ( GoodDistance( EnemyX, EnemyY, absX, absY, 20 ) == true )
			{
				WalkTo( 0, 0 );
				WalkTo( 0, 0 );
				if ( actionTimer == 0 )
				{
					if ( isInWilderness( EnemyX, EnemyY, 1 ) == true )
					{
						if ( false )
						{
						}
						else
						{
							if ( PlayerHandler.players[AttackingOn].deathStage > 0 )
							{
								ResetAttack();
							}
							else
							{
								actionAmount++;
								setAnimation( playerSEA );
								PlayerHandler.players[AttackingOn].hitUpdateRequired = true;
								PlayerHandler.players[AttackingOn].updateRequired = true;
								PlayerHandler.players[AttackingOn].appearanceUpdateRequired = true;
								hitDiff = EnemyHP;

								PlayerHandler.players[AttackingOn].hitDiff = hitDiff;
								actionTimer = 7;
							}
						}
						return true;
					}
					else
					{
						sendMessage( "This player is not in the Wilderness." );
						ResetAttack();
					}
				}
			}
			return false;
		}

		public int Average( int[] array )
		{
			int total = 0;
			foreach ( int element in array )
			{
				total += element;
			}
			int average = ( int ) ( total / array.Length );
			println( "total=" + total + ", average=" + average );
			return average;
		}

		public Boolean bankItem( int itemID, int fromSlot, int amount )
		{
			if ( !IsBanking )
				return false;
			if ( playerItemsN[fromSlot] <= 0 )
			{
				return false;
			}
			if ( !Item.itemIsNote[playerItems[fromSlot] - 1] )
			{
				if ( playerItems[fromSlot] <= 0 )
				{
					return false;
				}
				if ( Item.itemStackable[playerItems[fromSlot] - 1]
						|| ( playerItemsN[fromSlot] > 1 ) )
				{
					int toBankSlot = 0;
					Boolean alreadyInBank = false;

					for ( int i = 0; i < playerBankSize; i++ )
					{
						if ( bankItems[i] == playerItems[fromSlot] )
						{
							if ( playerItemsN[fromSlot] < amount )
							{
								amount = playerItemsN[fromSlot];
							}
							alreadyInBank = true;
							toBankSlot = i;
							i = playerBankSize + 1;
						}
					}

					if ( !alreadyInBank && ( freeBankSlots() > 0 ) )
					{
						for ( int i = 0; i < playerBankSize; i++ )
						{
							if ( bankItems[i] <= 0 )
							{
								toBankSlot = i;
								i = playerBankSize + 1;
							}
						}
						bankItems[toBankSlot] = playerItems[fromSlot];
						if ( playerItemsN[fromSlot] < amount )
						{
							amount = playerItemsN[fromSlot];
						}
						if ( ( ( bankItemsN[toBankSlot] + amount ) <= maxItemAmount )
								&& ( ( bankItemsN[toBankSlot] + amount ) > -1 ) )
						{
							bankItemsN[toBankSlot] += amount;
						}
						else
						{
							sendMessage( "Bank full!" );
							return false;
						}
						deleteItem( ( playerItems[fromSlot] - 1 ), fromSlot, amount );
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else if ( alreadyInBank )
					{
						if ( ( ( bankItemsN[toBankSlot] + amount ) <= maxItemAmount )
								&& ( ( bankItemsN[toBankSlot] + amount ) > -1 ) )
						{
							bankItemsN[toBankSlot] += amount;
						}
						else
						{
							sendMessage( "Bank full!" );
							return false;
						}
						deleteItem( ( playerItems[fromSlot] - 1 ), fromSlot, amount );
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else
					{
						sendMessage( "Bank full!" );
						return false;
					}
				}
				else
				{
					itemID = playerItems[fromSlot];
					int toBankSlot = 0;
					Boolean alreadyInBank = false;

					for ( int i = 0; i < playerBankSize; i++ )
					{
						if ( bankItems[i] == playerItems[fromSlot] )
						{
							alreadyInBank = true;
							toBankSlot = i;
							i = playerBankSize + 1;
						}
					}
					if ( !alreadyInBank && ( freeBankSlots() > 0 ) )
					{
						for ( int i = 0; i < playerBankSize; i++ )
						{
							if ( bankItems[i] <= 0 )
							{
								toBankSlot = i;
								i = playerBankSize + 1;
							}
						}
						int firstPossibleSlot = 0;
						Boolean itemExists = false;

						while ( amount > 0 )
						{
							itemExists = false;
							for ( int i = firstPossibleSlot; i < playerItems.Length; i++ )
							{
								if ( ( playerItems[i] ) == itemID )
								{
									firstPossibleSlot = i;
									itemExists = true;
									i = 30;
								}
							}
							if ( itemExists )
							{
								bankItems[toBankSlot] = playerItems[firstPossibleSlot];
								bankItemsN[toBankSlot] += 1;
								deleteItem( ( playerItems[firstPossibleSlot] - 1 ),
										firstPossibleSlot, 1 );
								amount--;
							}
							else
							{
								amount = 0;
							}
						}
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else if ( alreadyInBank )
					{
						int firstPossibleSlot = 0;
						Boolean itemExists = false;

						while ( amount > 0 )
						{
							itemExists = false;
							for ( int i = firstPossibleSlot; i < playerItems.Length; i++ )
							{
								if ( ( playerItems[i] ) == itemID )
								{
									firstPossibleSlot = i;
									itemExists = true;
									i = 30;
								}
							}
							if ( itemExists )
							{
								bankItemsN[toBankSlot] += 1;
								deleteItem( ( playerItems[firstPossibleSlot] - 1 ),
										firstPossibleSlot, 1 );
								amount--;
							}
							else
							{
								amount = 0;
							}
						}
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else
					{
						sendMessage( "Bank full!" );
						return false;
					}
				}
			}
			else if ( Item.itemIsNote[playerItems[fromSlot] - 1]
				  && !Item.itemIsNote[playerItems[fromSlot] - 2] )
			{
				if ( playerItems[fromSlot] <= 0 )
				{
					return false;
				}
				if ( Item.itemStackable[playerItems[fromSlot] - 1]
						|| ( playerItemsN[fromSlot] > 1 ) )
				{
					int toBankSlot = 0;
					Boolean alreadyInBank = false;

					for ( int i = 0; i < playerBankSize; i++ )
					{
						if ( bankItems[i] == ( playerItems[fromSlot] - 1 ) )
						{
							if ( playerItemsN[fromSlot] < amount )
							{
								amount = playerItemsN[fromSlot];
							}
							alreadyInBank = true;
							toBankSlot = i;
							i = playerBankSize + 1;
						}
					}

					if ( !alreadyInBank && ( freeBankSlots() > 0 ) )
					{
						for ( int i = 0; i < playerBankSize; i++ )
						{
							if ( bankItems[i] <= 0 )
							{
								toBankSlot = i;
								i = playerBankSize + 1;
							}
						}
						bankItems[toBankSlot] = ( playerItems[fromSlot] - 1 );
						if ( playerItemsN[fromSlot] < amount )
						{
							amount = playerItemsN[fromSlot];
						}
						if ( ( ( bankItemsN[toBankSlot] + amount ) <= maxItemAmount )
								&& ( ( bankItemsN[toBankSlot] + amount ) > -1 ) )
						{
							bankItemsN[toBankSlot] += amount;
						}
						else
						{
							return false;
						}
						deleteItem( ( playerItems[fromSlot] - 1 ), fromSlot, amount );
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else if ( alreadyInBank )
					{
						if ( ( ( bankItemsN[toBankSlot] + amount ) <= maxItemAmount )
								&& ( ( bankItemsN[toBankSlot] + amount ) > -1 ) )
						{
							bankItemsN[toBankSlot] += amount;
						}
						else
						{
							return false;
						}
						deleteItem( ( playerItems[fromSlot] - 1 ), fromSlot, amount );
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else
					{
						sendMessage( "Bank full!" );
						return false;
					}
				}
				else
				{
					itemID = playerItems[fromSlot];
					int toBankSlot = 0;
					Boolean alreadyInBank = false;

					for ( int i = 0; i < playerBankSize; i++ )
					{
						if ( bankItems[i] == ( playerItems[fromSlot] - 1 ) )
						{
							alreadyInBank = true;
							toBankSlot = i;
							i = playerBankSize + 1;
						}
					}
					if ( !alreadyInBank && ( freeBankSlots() > 0 ) )
					{
						for ( int i = 0; i < playerBankSize; i++ )
						{
							if ( bankItems[i] <= 0 )
							{
								toBankSlot = i;
								i = playerBankSize + 1;
							}
						}
						int firstPossibleSlot = 0;
						Boolean itemExists = false;

						while ( amount > 0 )
						{
							itemExists = false;
							for ( int i = firstPossibleSlot; i < playerItems.Length; i++ )
							{
								if ( ( playerItems[i] ) == itemID )
								{
									firstPossibleSlot = i;
									itemExists = true;
									i = 30;
								}
							}
							if ( itemExists )
							{
								bankItems[toBankSlot] = ( playerItems[firstPossibleSlot] - 1 );
								bankItemsN[toBankSlot] += 1;
								deleteItem( ( playerItems[firstPossibleSlot] - 1 ),
										firstPossibleSlot, 1 );
								amount--;
							}
							else
							{
								amount = 0;
							}
						}
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else if ( alreadyInBank )
					{
						int firstPossibleSlot = 0;
						Boolean itemExists = false;

						while ( amount > 0 )
						{
							itemExists = false;
							for ( int i = firstPossibleSlot; i < playerItems.Length; i++ )
							{
								if ( ( playerItems[i] ) == itemID )
								{
									firstPossibleSlot = i;
									itemExists = true;
									i = 30;
								}
							}
							if ( itemExists )
							{
								bankItemsN[toBankSlot] += 1;
								deleteItem( ( playerItems[firstPossibleSlot] - 1 ),
										firstPossibleSlot, 1 );
								amount--;
							}
							else
							{
								amount = 0;
							}
						}
						resetItems( 5064 );
						resetBank();
						return true;
					}
					else
					{
						sendMessage( "Bank full!" );
						return false;
					}
				}
			}
			else
			{
				sendMessage( "Item not supported " + ( playerItems[fromSlot] - 1 ) );
				return false;
			}
		}

		public Boolean banned( String host )
		{
			try
			{
				TextReader input = File.OpenText( "data/bannedusers.dat" );
				String data = null;

				while ( ( data = input.ReadLine() ) != null )
				{
					if ( host.ToLower() == data.ToLower() )
					{
						return true;
					}
				}
			}
			catch ( IOException e )
			{
				Console.WriteLine( "Unable to ban player" );
				server.logError( e.ToString() );
			}
			return false;
		}

		public void buryItem( int item, int slot )
		{
			if ( duelRule[7] && inDuel && duelFight )
			{
				sendMessage( "Food has been disabled for this duel" );
				return;
			}
			Boolean used = true;
			if ( playerHasItem( item ) )
			{
				switch ( item )
				{
					case 526:
						prayerMessage( 70 );
						break;
					case 532:
						prayerMessage( 115 );
						break;
					case 3125:
						prayerMessage( 300 );
						break;
					case 536:
						prayerMessage( 450 );
						break;
					case 315:
						setAnimation( 0x33D );
						animationReset = TimeHelper.CurrentTimeMillis() + 750;
						currentHealth += 5;
						if ( currentHealth > playerLevel[playerHitpoints] )
							currentHealth = playerLevel[playerHitpoints];
						refreshhps();
						weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
						sendMessage( "You eat the shrimps" );
						break;
					case 379:
						setAnimation( 0x33D );
						animationReset = TimeHelper.CurrentTimeMillis() + 750;
						currentHealth += 12;
						if ( currentHealth > playerLevel[playerHitpoints] )
							currentHealth = playerLevel[playerHitpoints];
						refreshhps();
						weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
						sendMessage( "You eat the lobster" );
						break;
					case 2140:
						setAnimation( 0x33D );
						animationReset = TimeHelper.CurrentTimeMillis() + 750;
						currentHealth += 3;
						if ( currentHealth > playerLevel[playerHitpoints] )
							currentHealth = playerLevel[playerHitpoints];
						refreshhps();
						weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
						sendMessage( "You eat the cooked chicken" );

						break;
					case 385:
						setAnimation( 0x33D );
						animationReset = TimeHelper.CurrentTimeMillis() + 750;
						currentHealth += 20;
						if ( currentHealth > playerLevel[playerHitpoints] )
							currentHealth = playerLevel[playerHitpoints];
						refreshhps();
						weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
						sendMessage( "You eat the Shark!" );

						break;

					case 391:
						setAnimation( 0x33D );
						animationReset = TimeHelper.CurrentTimeMillis() + 750;
						currentHealth += 30;
						if ( currentHealth > playerLevel[playerHitpoints] )
							currentHealth = playerLevel[playerHitpoints];
						refreshhps();
						weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
						sendMessage( "You eat the Manta Ray!" );
						break;
					default:
						used = false;
						break;
				}
			}
			if ( used )
				deleteItem( item, slot, 1 );
		}

		public Boolean buyItem( int itemID, int fromSlot, int amount )
		{
			if ( ( amount > 0 )
					&& ( itemID == ( ShopHandler.ShopItems[MyShopID][fromSlot] - 1 ) ) )
			{
				if ( amount > ShopHandler.ShopItemsN[MyShopID][fromSlot] )
				{
					amount = ShopHandler.ShopItemsN[MyShopID][fromSlot];
				}
				double ShopValue;
				double TotPrice;
				int TotPrice2;
				int Overstock;
				int Slot = 0;
				if ( !canUse( itemID ) )
				{
					sendMessage( "You must be a premium member to buy this item" );
					sendMessage( "Visit moparscape.org/smf to subscribe" );
					return false;
				}
				for ( int i = amount; i > 0; i-- )
				{
					TotPrice2 = ( int ) Math.Floor( GetItemShopValue( itemID, 0,
							fromSlot ) );
					Slot = GetItemSlot( 995 );
					if ( Slot == -1 )
					{
						sendMessage( "You don't have enough coins." );
						break;
					}
					if ( playerItemsN[Slot] >= TotPrice2 )
					{
						if ( freeSlots() > 0 )
						{
							deleteItem( 995, GetItemSlot( 995 ), TotPrice2 );
							addItem( itemID, 1 );
							ShopHandler.ShopItemsN[MyShopID][fromSlot] -= 1;
							ShopHandler.ShopItemsDelay[MyShopID][fromSlot] = 0;
							if ( ( fromSlot + 1 ) > ShopHandler.ShopItemsStandard[MyShopID] )
							{
								ShopHandler.ShopItems[MyShopID][fromSlot] = 0;
							}
						}
						else
						{
							sendMessage( "Not enough space in your inventory." );
							break;
						}
					}
					else
					{
						sendMessage( "You don't have enough coins." );
						break;
					}
				}
				resetItems( 3823 );
				resetShop( MyShopID );
				UpdatePlayerShop();
				return true;
			}
			return false;
		}

		public void CalculateMaxHit( )
		{
			double MaxHit = 0;
			int StrBonus = playerBonus[10]; // Strength Bonus
			int Strength = playerLevel[playerStrength]; // Strength
			int RngBonus = playerBonus[4]; // Ranged Bonus
			int Range = playerLevel[playerRanged]; // Ranged

			if ( ( FightType == 1 ) || ( FightType == 4 ) )
			{
				// Accurate & Defensive
				MaxHit += ( double ) ( 1.05 + ( double ) ( ( double ) ( StrBonus * Strength ) * 0.00175 ) );
			}
			else if ( FightType == 2 )
			{
				// Aggresive
				MaxHit += ( double ) ( 1.35 + ( double ) ( ( double ) ( StrBonus * Strength ) * 0.00175 ) );
			}
			else if ( FightType == 3 )
			{
				// Controlled
				MaxHit += ( double ) ( 1.15 + ( double ) ( ( double ) ( StrBonus * Strength ) * 0.00175 ) );
			}
			MaxHit += ( double ) ( Strength * 0.1 );
			/*
			 * if (StrPotion == 1) { // Strength Potion MaxHit += (double) (Strength *
			 * 0.0014); } else if (StrPotion == 2) { // Super Strength Potion MaxHit +=
			 * (double) (Strength * 0.0205); }
			 */
			if ( StrPrayer == 1 )
			{
				// Burst Of Strength
				MaxHit += ( double ) ( Strength * 0.005 );
			}
			else if ( StrPrayer == 2 )
			{
				// Super Human Strength
				MaxHit += ( double ) ( Strength * 0.01 );
			}
			else if ( StrPrayer == 3 )
			{
				// Ultimate Strength
				MaxHit += ( double ) ( Strength * 0.015 );
			}
			if ( ( FightType == 5 ) || ( FightType == 6 ) )
			{
				// Accurate and Longranged
				MaxHit += ( double ) ( 1.05 + ( double ) ( ( double ) ( RngBonus * Range ) * 0.00075 ) );
			}
			else if ( FightType == 7 )
			{
				// Rapid
				MaxHit += ( double ) ( 1.35 + ( double ) ( ( double ) ( RngBonus ) * 0.00025 ) );
			}


			playerMaxHit = ( int ) Math.Floor( MaxHit );
		}

		public void CalculateRange( )
		{
			double MaxHit = 0;
			int RangeBonus = playerBonus[3]; // Range Bonus
			int Range = playerLevel[4];
			{
				// Range
				MaxHit += ( double ) ( 1.05 + ( double ) ( ( double ) ( RangeBonus * Range ) * 0.00175 ) );
			}
			MaxHit += ( double ) ( Range * 0.2 );
			playerMaxHit = ( int ) Math.Floor( MaxHit );
		}

		public Boolean canUse( int id )
		{
			if ( !premium && premiumItem( id ) )
			{
				return false;
			}
			return true;
		}

		public void castleWarsScore( )
		{
			String zammyColor;
			String saraColor;

			if ( zammyScore > saraScore )
			{
				zammyColor = "@gre@";
				saraColor = "@red@";
			}
			else if ( zammyScore < saraScore )
			{
				zammyColor = "@red@";
				saraColor = "@gre@";
			}
			else
			{
				zammyColor = "@yel@";
				saraColor = "@yel@";
			}
			sendQuest( "@bla@.................Castle Wars Scores.............", 8144 );
			clearQuestInterface();
			sendQuest( "@or3@Zamorak: " + zammyColor + zammyScore, 8147 );
			sendQuest( "@or3@Saradomin: " + saraColor + saraScore, 8148 );
			sendQuestSomething( 8143 );
			showInterface( 8134 );
			flushOutStream();
		}

		public void ChangeDoor( int ArrayID )
		{
			int objectID = ObjectHandler.ObjectOriID[ArrayID];
			int objectX = ObjectHandler.ObjectX[ArrayID];
			int objectY = ObjectHandler.ObjectY[ArrayID];
			int Face = ObjectHandler.ObjectFace[ArrayID];
			int Type = ObjectHandler.ObjectType[ArrayID];

			ReplaceObject( objectX, objectY, -1, -1, 0 );
			switch ( Type )
			{
				case 1:
					ReplaceObject( objectX, ( objectY + 1 ), objectID, Face, 0 );
					ObjectHandler.ObjectType[ArrayID] = 2;
					break;

				case 2:
					ReplaceObject( objectX, ( objectY - 1 ), objectID, Face, 0 );
					ObjectHandler.ObjectType[ArrayID] = 1;
					break;

				case 3:
					ReplaceObject( ( objectX + 1 ), objectY, objectID, Face, 0 );
					ObjectHandler.ObjectType[ArrayID] = 4;
					break;

				case 4:
					ReplaceObject( ( objectX - 1 ), objectY, objectID, Face, 0 );
					ObjectHandler.ObjectType[ArrayID] = 3;
					break;
			}
		}

		public void CheckArrows( )
		{
			for ( int k = 880; k < 893; k++ )
			{

				if ( playerEquipment[playerArrows] == k )
				{
					HasArrows = true;
				}
				else if ( playerEquipment[playerWeapon] == 4214 )
				{
					HasArrows = true;
				}
				else
				{
					HasArrows = false;
				}

			}
		}

		public void checkCooking( int id )
		{
			long now = TimeHelper.CurrentTimeMillis();
			if ( !playerHasItem( id ) )
				return;
			if ( now - lastAction < 2000 )
				return;
			int[] fish = { 317, 377 };
			int[] cooked = { 315, 379 };
			int[] burned = { 323, 381 };
			int exp = 0, ran = 0, index = 0;
			for ( int i = 0; i < fish.Length; i++ )
			{
				if ( id == fish[i] )
				{
					index = i;
				}
			}

			switch ( id )
			{
				case 317:
					exp = 40;
					ran = 30 - playerLevel[playerCooking];
					break;
				case 377:
					exp = 100;
					ran = 70 - playerLevel[playerCooking];
					break;
			}
			if ( ran < 0 )
				ran = 0;
			Boolean success = true;
			if ( misc.random( 100 ) < ran )
			{
				success = false;
			}
			if ( exp > 0 )
			{
				deleteItem( id, 1 );
				if ( success )
				{
					addItem( cooked[index], 1 );
					sendMessage( "You cook the " + getItemName( id ) );
					addSkillXP( exp, playerCooking );
				}
				else
				{
					addItem( burned[index], 1 );
					sendMessage( "You burn the " + getItemName( id ) );
				}
			}
		}



		public Boolean CheckForSkillUse4( int Item )
		{
			Boolean GoOn = true;
			Boolean IsFiremaking = false;

			switch ( Item )
			{
				case 1511:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 1;
						firemaking[1] = 1;
						firemaking[2] = 40;
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				case 1513:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 8;
						firemaking[1] = 75;
						firemaking[2] = 303;
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				case 1515:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 7;
						firemaking[1] = 60;
						if ( misc.random2( 2 ) == 1 )
						{
							firemaking[2] = 202;
						}
						else
						{
							firemaking[2] = 203;
						}
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				case 1517:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 5;
						firemaking[1] = 45;
						firemaking[2] = 135;
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				case 1519:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 3;
						firemaking[1] = 30;
						firemaking[2] = 90;
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				case 1521:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 2;
						firemaking[1] = 15;
						firemaking[2] = 60;
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				case 6333:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 4;
						firemaking[1] = 35;
						firemaking[2] = 105;
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				case 6332:
					if ( IsItemInBag( 590 ) == true )
					{
						IsFiremaking = true;
						firemaking[0] = 6;
						firemaking[1] = 50;
						if ( misc.random2( 2 ) == 1 )
						{
							firemaking[2] = 157;
						}
						else
						{
							firemaking[2] = 158;
						}
					}
					else
					{
						sendMessage( "You need a " + getItemName( 591 )
								+ " to light a fire." );
					}
					break;

				default:
					sendMessage( "Nothing interesting happens." );
					println_debug( "Firemaking Usage - ItemID: " + Item );
					GoOn = false;
					break;
			}
			if ( GoOn == false )
			{
				return false;
			}
			if ( IsFiremaking == true )
			{
				firemaking[4] = GetGroundItemID( Item, skillX, skillY, heightLevel );
				if ( ( firemaking[4] == -1 ) && false )
				{
					sendMessage( "No logs on the ground." );
					resetFM();
					println_debug( "Firemaking bug: no logs on the ground." );
				}
			}
			if ( prayer[0] > 0 )
			{
				Pray();
			}
			return true;
		}

		public Boolean checkLog( String file, String playerName )
		{
			// check bans/mutes/chatlogs etc.. -bakatool
			try
			{
				TextReader input = File.OpenText( "config//" + file + ".txt" );
				String data = null;
				while ( ( data = input.ReadLine() ) != null )
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

		public Boolean CheckObjectSkill( int objectID )
		{
			Boolean GoFalse = false;

			switch ( objectID )
			{

				/*
				 * 
				 * WOODCUTTING
				 * 
				 */
				case 1276:
				case 1277:
				case 1278:
				case 1279:
				case 1280:
				case 1330:
				case 1332:
				case 2409:
				case 3033:
				case 3034:
				case 3035:
				case 3036:
				case 3879:
				case 3881:
				case 3882:
				case 3883:
				// Normal Tree
				case 1315:
				case 1316:
				case 1318:
				case 1319:
				// Evergreen
				case 1282:
				case 1283:
				case 1284:
				case 1285:
				case 1286:
				case 1287:
				case 1289:
				case 1290:
				case 1291:
				case 1365:
				case 1383:
				case 1384:
				case 5902:
				case 5903:
				case 5904:
					// Dead Tree
					woodcutting[0] = 1;
					woodcutting[1] = 1;
					woodcutting[2] = 25;
					woodcutting[4] = 1511;
					break;

				case 2023:
					// Achey Tree
					woodcutting[0] = 2;
					woodcutting[1] = 1;
					woodcutting[2] = 25;
					woodcutting[4] = 2862;
					break;
				case 1281:
				case 3037:
					// Oak Tree
					woodcutting[0] = 3;
					woodcutting[1] = 15;
					if ( misc.random( 2 ) == 1 )
					{
						woodcutting[2] = 37;
					}
					else
					{
						woodcutting[2] = 38;
					}
					woodcutting[4] = 1521;
					break;

				case 1308:
				case 5551:
				case 5552:
				case 5553:
					// Willow Tree
					woodcutting[0] = 4;
					woodcutting[1] = 30;
					if ( misc.random( 2 ) == 1 )
					{
						woodcutting[2] = 67;
					}
					else
					{
						woodcutting[2] = 68;
					}
					woodcutting[4] = 1519;
					break;

				case 9036:
					// Teak Tree
					woodcutting[0] = 5;
					woodcutting[1] = 35;
					woodcutting[2] = 85;
					woodcutting[4] = 6333;
					break;

				case 1292:
					// Dramen Tree
					woodcutting[0] = 5;
					woodcutting[1] = 36;
					woodcutting[2] = 0;
					woodcutting[4] = 771;
					break;

				case 1307:
				case 4674:
					// Maple Tree
					woodcutting[0] = 6;
					woodcutting[1] = 45;
					woodcutting[2] = 100;
					woodcutting[4] = 1517;
					break;

				case 2289:
				case 4060:
					// Hollow Tree
					woodcutting[0] = 7;
					woodcutting[1] = 45;
					if ( misc.random( 2 ) == 1 )
					{
						woodcutting[2] = 82;
					}
					else
					{
						woodcutting[2] = 83;
					}
					woodcutting[4] = 3239;
					break;

				case 9034:
					// Mahogany Tree
					woodcutting[0] = 8;
					woodcutting[1] = 50;
					woodcutting[2] = 125;
					woodcutting[4] = 4445;
					break;

				case 1309:
					// Yew Tree
					woodcutting[0] = 9;
					woodcutting[1] = 60;
					woodcutting[2] = 175;
					woodcutting[4] = 1515;
					woodcutting[5] = 3;
					break;

				case 1306:
					// Magic Tree
					woodcutting[0] = 10;
					woodcutting[1] = 75;
					woodcutting[2] = 250;
					woodcutting[4] = 1513;
					break;

				/*
				 * 
				 * MINING
				 * 
				 */
				case 2491:
					// rune essence
					mining[0] = 1;
					mining[1] = 1;
					mining[2] = 5;
					mining[4] = 1436;
					break;

				case 2108:
				case 2109:
					// clay rock
					mining[0] = 1;
					mining[1] = 1;
					mining[2] = 5;
					mining[4] = 434;
					break;

				case 2090:
				case 2091:
					// copper rock
					mining[0] = 1;
					mining[1] = 1;
					if ( misc.random( 2 ) == 1 )
					{
						mining[2] = 17;
					}
					else
					{
						mining[2] = 18;
					}
					mining[4] = 436;
					break;

				case 2094:
				case 2095:
					// tin rock
					mining[0] = 1;
					mining[1] = 1;
					if ( misc.random( 2 ) == 1 )
					{
						mining[2] = 17;
					}
					else
					{
						mining[2] = 18;
					}
					mining[4] = 438;
					break;

				case 2110:
					// blurite rock
					mining[0] = 2;
					mining[1] = 10;
					if ( misc.random( 2 ) == 1 )
					{
						mining[2] = 17;
					}
					else
					{
						mining[2] = 18;
					}
					mining[4] = 668;
					break;

				case 4028:
				case 4029:
				case 4030:
					// lime rock
					mining[0] = 1;
					mining[1] = 1;
					if ( misc.random( 2 ) == 1 )
					{
						mining[2] = 26;
					}
					else
					{
						mining[2] = 27;
					}
					mining[4] = 3211;
					break;

				case 2092:
				case 2093:
					// iron rock
					mining[0] = 3;
					mining[1] = 15;
					mining[2] = 35;
					mining[4] = 440;
					break;

				case 2100:
				case 2101:
					// silver rock
					mining[0] = 4;
					mining[1] = 20;
					mining[2] = 40;
					mining[4] = 442;
					break;

				case 3403:
					// elemental rock
					mining[0] = 4;
					mining[1] = 20;
					mining[2] = 20;
					mining[4] = 2892;
					break;

				case 2096:
				case 2097:
					// coal rock
					mining[0] = 5;
					mining[1] = 30;
					mining[2] = 50;
					mining[4] = 453;
					break;

				case 2098:
				case 2099:
					// gold rock
					mining[0] = 6;
					mining[1] = 40;
					mining[2] = 65;
					break;

				/*
				 * GEM ROCK case : case : //gem rock mining[0] = 6; mining[1] = 40;
				 * mining[2] = 65; mining[4] = Item.randosendMessageems(); break;
				 */
				case 2102:
				case 2103:
					// mithril rock
					mining[0] = 7;
					mining[1] = 55;
					mining[2] = 80;
					mining[4] = 447;
					break;

				case 2104:
				case 2105:
					// adamant rock
					if ( actionTimer == 0 )
					{
						mining[0] = 8;
						mining[1] = 70;
						mining[2] = 95;
						mining[4] = 449;
						actionTimer = 7;
					}
					break;

				case 2106:
				case 2107:
					// rune rock
					if ( actionTimer == 0 )
					{
						mining[0] = 9;
						mining[1] = 85;
						mining[2] = 125;
						mining[4] = 451;
						actionTimer = 15;
					}
					break;

				default:
					GoFalse = true;
					break;
			}
			if ( GoFalse == true )
			{
				return false;
			}
			return true;
		}

		public int CheckSmithing( int ItemID, int ItemSlot )
		{
			Boolean GoFalse = false;
			int Type = -1;

			if ( IsItemInBag( 2347 ) == false )
			{
				sendMessage( "You need a " + getItemName( 2347 ) + " to hammer bars." );
				return -1;
			}
			if ( playerEquipment[playerWeapon] > 0 )
			{
				sendMessage( "You must remove your weapon to smith" );
				return -1;
			}
			switch ( ItemID )
			{
				case 2349:
					// Bronze Bar
					Type = 1;
					break;

				case 2351:
					// Iron Bar
					Type = 2;
					break;

				case 2353:
					// Steel Bar
					Type = 3;
					break;

				case 2359:
					// Mithril Bar
					Type = 4;
					break;

				case 2361:
					// Adamantite Bar
					Type = 5;
					break;

				case 2363:
					// Runite Bar
					Type = 6;
					break;

				default:
					sendMessage( "You cannot smith this item." );
					GoFalse = true;
					break;
			}
			if ( GoFalse == true )
			{
				return -1;
			}
			return Type;
		}

		public void clearQuestInterface( )
		{
			foreach ( int element in QuestInterface )
			{
				sendFrame126( "", element );
			}
		}

		public void closeInterface( )
		{
			IsBanking = false;
			outStream.createFrame( 219 );
		}

		public void commitobj( )
		{
			createNewTileObject( GObjX, GObjY, GObjId );
			GObjChange = 0;
		}

		public void confirmDuel( )
		{
			secondDuelWindow = true;
			client other = getClient( duel_with );
			if ( !ValidClient( duel_with ) )
				declineDuel();
			String outString = "";
			foreach ( GameItem item in offeredItems )
			{
				if ( Item.itemStackable[item.id] || Item.itemIsNote[item.id] )
				{
					outString += getItemName( item.id ) + " x " + misc.format( item.amount )
							+ ", ";
				}
				else
				{
					outString += getItemName( item.id ) + ", ";
				}
			}
			sendQuest( outString, 6516 );
			outString = "";
			foreach ( GameItem item in other.offeredItems )
			{
				if ( Item.itemStackable[item.id] || Item.itemIsNote[item.id] )
				{
					outString += getItemName( item.id ) + " x " + misc.format( item.amount )
							+ ", ";
				}
				else
				{
					outString += getItemName( item.id ) + ", ";
				}
			}
			sendQuest( outString, 6517 );
			sendQuest( "Movement will be disabled", 8242 );
			for ( int i = 8243; i <= 8253; i++ )
			{
				sendQuest( "", i );
			}
			sendQuest( "Hitpoints will be restored", 8250 );
			sendQuest( "", 6571 );
			showInterface( 6412 );
		}

		public void confirmScreen( )
		{
			secondTradeWindow = true;
			canOffer = false;
			sendFrame248( 3443, 3213 ); // trade confirm + normal bag
			resetItems( 3214 );
			String SendTrade = "Absolutely nothing!";
			String SendAmount = "";
			int Count = 0;
			client other = getClient( trade_reqId );
			foreach ( GameItem item in offeredItems )
			{
				if ( item.id > 0 )
				{
					if ( ( item.amount >= 1000 ) && ( item.amount < 1000000 ) )
					{
						SendAmount = "@cya@" + ( item.amount / 1000 ) + "K @whi@("
								+ misc.format( item.amount ) + ")";
					}
					else if ( item.amount >= 1000000 )
					{
						SendAmount = "@gre@" + ( item.amount / 1000000 )
								+ " million @whi@(" + misc.format( item.amount )
								+ ")";
					}
					else
					{
						SendAmount = "" + misc.format( item.amount );
					}
					if ( Count == 0 )
					{
						SendTrade = getItemName( item.id );
					}
					else
					{
						SendTrade = SendTrade + "\\n" + getItemName( item.id );
					}
					if ( item.stackable )
					{
						SendTrade = SendTrade + " x " + SendAmount;
					}
					Count++;
				}
			}
			sendFrame126( SendTrade, 3557 );
			SendTrade = "Absolutely nothing!";
			SendAmount = "";
			Count = 0;
			foreach ( GameItem item in other.offeredItems )
			{
				if ( item.id > 0 )
				{
					if ( ( item.amount >= 1000 ) && ( item.amount < 1000000 ) )
					{
						SendAmount = "@cya@" + ( item.amount / 1000 ) + "K @whi@("
								+ misc.format( item.amount ) + ")";
					}
					else if ( item.amount >= 1000000 )
					{
						SendAmount = "@gre@" + ( item.amount / 1000000 )
								+ " million @whi@(" + misc.format( item.amount )
								+ ")";
					}
					else
					{
						SendAmount = "" + misc.format( item.amount );
					}
					if ( Count == 0 )
					{
						SendTrade = getItemName( item.id );
					}
					else
					{
						SendTrade = SendTrade + "\\n" + getItemName( item.id );
					}
					if ( item.stackable )
					{
						SendTrade = SendTrade + " x " + SendAmount;
					}
					Count++;
				}
			}
			sendFrame126( SendTrade, 3558 );
		}

		public Boolean Cook( )
		{
			/* COOKING */
			if ( playerLevel[playerCooking] >= cooking[1] )
			{
				if ( ( actionTimer == 0 ) && ( cooking[0] == 1 ) )
				{
					actionAmount++;
					actionTimer = 4;
					OriginalShield = playerEquipment[playerShield];
					OriginalWeapon = playerEquipment[playerWeapon];
					playerEquipment[playerShield] = -1;
					playerEquipment[playerWeapon] = -1;
					setAnimation( 0x380 );
					cooking[0] = 2;
				}
				if ( ( actionTimer == 0 ) && ( cooking[0] == 2 ) )
				{
					deleteItem( cooking[5], GetItemSlot( cooking[5] ), 1 );
					int Discount = 0;

					if ( playerEquipment[playerHands] == 775 )
					{
						// Cooking hauntlets
						Discount = 10;
					}
					int StopBurnLevel = ( ( cooking[1] + 35 ) - Discount );

					if ( ( StopBurnLevel > playerLevel[playerCooking] )
							&& ( misc.random2( StopBurnLevel ) <= misc
									.random2( StopBurnLevel ) ) )
					{
						addItem( cooking[6], 1 );
						sendMessage( "You burned the " + getItemName( cooking[5] )
								+ "." );
					}
					else
					{
						addItem( cooking[4], 1 );
						addSkillXP( ( cooking[2] * cooking[3] ), playerCooking );
						sendMessage( "You cooked the " + getItemName( cooking[5] )
								+ "." );
					}
					playerEquipment[playerWeapon] = OriginalWeapon;
					playerEquipment[playerShield] = OriginalShield;
					OriginalWeapon = -1;
					OriginalShield = -1;
					resetAnimation();
					resetCO();
				}
			}
			else
			{
				sendMessage( "You need " + cooking[1] + " "
						+ statName[playerCooking] + " to cook this "
						+ getItemName( cooking[5] ) + "." );
				resetCO();
				return false;
			}
			return true;
		}

		public Boolean crackCracker( )
		{
			sendMessage( "Somone used a crackers on you..." );
			CrackerMsg = false;
			if ( CrackerForMe == true )
			{
				if ( freeSlots() > 0 )
				{
					addItem( Item.randomPHat(), 1 );
					sendMessage( "And you get the crackers item." );
				}
				else
				{
					sendMessage( "but you don't have enough space in your inventory." );
				}
				CrackerForMe = false;
			}
			else
			{
				sendMessage( "but you didn't get the crackers item." );
			}
			return true;
		}

		/*
		 * WOODCUTTING [0] = woodcutting [1] = Level [2] = Exp [3] = Exp Rate [4] =
		 * Item [5] = Distance
		 * 
		 * FLETCHING [0] = fletching [1] = Level [2] = Exp [3] = Exp Rate [4] = Item
		 * [5] = Asking [6] = Make
		 * 
		 * MINING [0] = mining [1] = Level [2] = Exp [3] = Exp Rate [4] = Item
		 * 
		 * SMELTING [0] = smelting [1] = Level [2] = Exp [3] = Item [4] = What [5] =
		 * What Slot [6] = Del Coal
		 * 
		 * SMITHING [0] = smithing [1] = Level [2] = Smith Type [3] = Exp Rate [4] =
		 * Item [5] = smithing loop value
		 * 
		 * USEITEMS [0] = use id [1] = used on id [2] = used on slot [3] = use slot
		 * 
		 * CRAFTING [0] = crafting [1] = Level [2] = Exp [3] = Exp Rate [4] = Item
		 * 
		 * FISHING [0] = fishing [1] = Level [2] = Exp [3] = Exp Rate [4] = Item [5] =
		 * DelItem [6] = FishingEquip [7] = FishingEmotion [8] = FishingRandom
		 * 
		 * PRAYER [0] = prayer [1] = Level (always 1) [2] = Exp [3] = Exp Rate [4] =
		 * DelItem [5] = DelItemSlot
		 * 
		 * COOKING [0] = cooking [1] = Level [2] = Exp [3] = Exp Rate [4] = Item [5] =
		 * UsedItem [6] = BurnedItem
		 * 
		 * HEALING [0] = healing [1] = MinHealth [2] = MaxHealth [3] = Item [4] =
		 * UsedItem
		 * 
		 * FIREMAKING [0] = firemaking [1] = Level [2] = Exp [3] = Exp Rate [4] =
		 * Item
		 */

		public void craft( )
		{
			if ( playerLevel[playerCrafting] < cLevel )
			{
				sendMessage( "You need " + cLevel + " crafting to make a "
						+ getItemName( cItem ) );
				resetAction( true );
				return;
			}
			pEmote = 885;
			updateRequired = true;
			appearanceUpdateRequired = true;
			if ( playerHasItem( cSelected, 1 ) && playerHasItem( 1734 ) && ( cAmount > 0 ) )
			{
				deleteItem( cSelected, 1 );
				deleteItem( 1734, 1 );
				sendMessage( "You make some " + getItemName( cItem ) );
				addItem( cItem, 1 );
				addSkillXP( cExp, playerCrafting );
				cAmount--;
				if ( cAmount < 1 )
					resetAction( true );
			}
			else
			{
				resetAction( true );
			}
		}

		public void craftMenu( int i )
		{
			sendQuest( "What would you like to make?", 8898 );
			sendQuest( "Vambraces", 8889 );
			sendQuest( "Chaps", 8893 );
			sendQuest( "Body", 8897 );
			sendFrame246( 8883, 250, Constants.gloves[i] );
			sendFrame246( 8884, 250, Constants.legs[i] );
			sendFrame246( 8885, 250, Constants.chests[i] );
			sendFrame164( 8880 );
		}

		public void createEnemyItem( int newItemID )
		{
			int EnemyX = PlayerHandler.players[AttackingOn].absX;
			int EnemyY = PlayerHandler.players[AttackingOn].absY;
			int Maxi = ItemHandler.DropItemCount;

			for ( int i = 0; i <= Maxi; i++ )
			{
				if ( ItemHandler.DroppedItemsID[i] < 1 )
				{
					ItemHandler.DroppedItemsID[i] = newItemID;
					ItemHandler.DroppedItemsX[i] = ( EnemyX );
					ItemHandler.DroppedItemsY[i] = ( EnemyY );
					ItemHandler.DroppedItemsN[i] = 1;
					ItemHandler.DroppedItemsH[i] = heightLevel;
					ItemHandler.DroppedItemsDDelay[i] = ( ItemHandler.MaxDropShowDelay + 1 ); // this
																							  // way
																							  // the
																							  // item
																							  // can
																							  // NEVER
																							  // be
																							  // showed
																							  // to
																							  // another
																							  // client
					ItemHandler.DroppedItemsDropper[i] = playerId;
					if ( i == Maxi )
					{
						ItemHandler.DropItemCount++;
						if ( ItemHandler.DropItemCount >= ( ItemHandler.MaxDropItems + 1 ) )
						{
							ItemHandler.DropItemCount = 0;
							misc.println( "! Notify item resterting !" );
						}
					}
					break;
				}
			}
		}

		public void createGroundItem( int itemID, int itemX, int itemY,
				int itemAmount )
		{
			// Phate: Omg fucking sexy! creates item at absolute X and Y
			outStream.createFrame( 85 ); // Phate: Spawn ground item
			outStream.writeByteC( ( itemY - 8 * mapRegionY ) );
			outStream.writeByteC( ( itemX - 8 * mapRegionX ) );
			outStream.createFrame( 44 );
			outStream.writeWordBigEndianA( itemID );
			outStream.writeWord( itemAmount );
			outStream.writeByte( 0 ); // x(4 MSB) y(LSB) coords
									  // Console.WriteLine("CreateGroundItem "+itemID+" "+(itemX - 8 *
									  // mapRegionX)+","+(itemY - 8 * mapRegionY)+" "+itemAmount);
		}

		public void createItem( int newItemID )
		{
			int Maxi = ItemHandler.DropItemCount;

			for ( int i = 0; i <= Maxi; i++ )
			{
				if ( ItemHandler.DroppedItemsID[i] < 1 )
				{
					ItemHandler.DroppedItemsID[i] = newItemID;
					ItemHandler.DroppedItemsX[i] = ( absX );
					ItemHandler.DroppedItemsY[i] = ( absY );
					ItemHandler.DroppedItemsN[i] = 1;
					ItemHandler.DroppedItemsH[i] = heightLevel;
					ItemHandler.DroppedItemsDDelay[i] = ( ItemHandler.MaxDropShowDelay + 1 ); // this
																							  // way
																							  // the
																							  // item
																							  // can
																							  // NEVER
																							  // be
																							  // showed
																							  // to
																							  // another
																							  // client
					ItemHandler.DroppedItemsDropper[i] = playerId;
					if ( i == Maxi )
					{
						ItemHandler.DropItemCount++;
						if ( ItemHandler.DropItemCount >= ( ItemHandler.MaxDropItems + 1 ) )
						{
							ItemHandler.DropItemCount = 0;
							misc.println( "! Notify item resterting !" );
						}
					}
					break;
				}
			}
		}

		public void createItem( int newItemID, int amount )
		{
			int Maxi = ItemHandler.DropItemCount;

			for ( int i = 0; i <= Maxi; i++ )
			{
				if ( ItemHandler.DroppedItemsID[i] < 1 )
				{
					ItemHandler.DroppedItemsID[i] = newItemID;
					ItemHandler.DroppedItemsX[i] = ( absX );
					ItemHandler.DroppedItemsY[i] = ( absY );
					ItemHandler.DroppedItemsN[i] = amount;
					ItemHandler.DroppedItemsH[i] = heightLevel;
					ItemHandler.DroppedItemsDDelay[i] = ( ItemHandler.MaxDropShowDelay + 1 ); // this
																							  // way
																							  // the
																							  // item
																							  // can
																							  // NEVER
																							  // be
																							  // showed
																							  // to
																							  // another
																							  // client
					ItemHandler.DroppedItemsDropper[i] = playerId;
					if ( i == Maxi )
					{
						ItemHandler.DropItemCount++;
						if ( ItemHandler.DropItemCount >= ( ItemHandler.MaxDropItems + 1 ) )
						{
							ItemHandler.DropItemCount = 0;
							misc.println( "! Notify item resterting !" );
						}
					}
					break;
				}
			}
		}

		public void createItemForAll( int newItemID, int amount, int X, int Y )
		{
			int Maxi = ItemHandler.DropItemCount;

			for ( int i = 0; i <= Maxi; i++ )
			{
				if ( ItemHandler.DroppedItemsID[i] < 1 )
				{
					ItemHandler.DroppedItemsID[i] = newItemID;
					ItemHandler.DroppedItemsX[i] = X;
					ItemHandler.DroppedItemsY[i] = Y;
					ItemHandler.DroppedItemsN[i] = amount;
					ItemHandler.DroppedItemsH[i] = heightLevel;
					ItemHandler.DroppedItemsDDelay[i] = ( ItemHandler.MaxDropShowDelay + 1 ); // this
																							  // way
																							  // the
																							  // item
																							  // can
																							  // NEVER
																							  // be
																							  // showed
																							  // to
																							  // another
																							  // client
					ItemHandler.DroppedItemsDropper[i] = playerId;
					if ( i == Maxi )
					{
						ItemHandler.DropItemCount++;
						if ( ItemHandler.DropItemCount >= ( ItemHandler.MaxDropItems + 1 ) )
						{
							ItemHandler.DropItemCount = 0;
							misc.println( "! Notify item resterting !" );
						}
					}
					break;
				}
			}
		}

		public void CreateNewFire( )
		{
			for ( int i = 0; i < ObjectHandler.MaxObjects; i++ )
			{
				if ( ObjectHandler.ObjectFireID[i] == -1 )
				{
					ObjectHandler.ObjectFireID[i] = 2732;
					ObjectHandler.ObjectFireX[i] = skillX;
					ObjectHandler.ObjectFireY[i] = skillY;
					ObjectHandler.ObjectFireH[i] = heightLevel;
					ObjectHandler.ObjectFireMaxDelay[i] = ObjectHandler.FireDelay
							+ ( ObjectHandler.FireGianDelay * firemaking[0] );
					break;
				}
			}
		}

		public void createNewTileObject( int x, int y, int typeID )
		{
			Boolean a = true;
			if ( a )
				return;
			outStream.createFrame( 85 );
			outStream.writeByteC( y - ( mapRegionY * 8 ) );
			outStream.writeByteC( x - ( mapRegionX * 8 ) );
			outStream.createFrame( 151 );
			// outStream.writeByteA(((x&7) << 4) + (y&7));
			outStream.writeByteA( 0 );
			outStream.writeWordBigEndian( typeID );
		}

		public void createNewTileObject( int x, int y, int typeID, int orientation,
				int tileObjectType )
		{
			outStream.createFrame( 85 );
			outStream.writeByteC( y - ( mapRegionY * 8 ) );
			outStream.writeByteC( x - ( mapRegionX * 8 ) );

			outStream.createFrame( 151 );
			// outStream.writeByteA(((x&7) << 4) + (y&7));
			outStream.writeByteA( 0 );
			outStream.writeWordBigEndian( typeID );
			outStream.writeByteS( ( tileObjectType << 2 ) + ( orientation & 3 ) );
		}

		public void createProjectile( int casterY, int casterX, int offsetY,
				int offsetX, int angle, int speed, int gfxMoving, int startHeight,
				int endHeight, int MageAttackIndex )
		{
			try
			{
				outStream.createFrame( 85 );
				outStream.writeByteC( ( casterY - ( mapRegionY * 8 ) ) - 2 );
				outStream.writeByteC( ( casterX - ( mapRegionX * 8 ) ) - 3 );
				outStream.createFrame( 117 );
				outStream.writeByte( angle ); // Starting place of the projectile
				outStream.writeByte( offsetY ); // Distance between caster and enemy
												// Y
				outStream.writeByte( offsetX ); // Distance between caster and enemy
												// X
				outStream.writeWord( MageAttackIndex ); // The NPC the missle is
														// locked on to
				outStream.writeWord( gfxMoving ); // The moving graphic ID
				outStream.writeByte( startHeight ); // The starting height
				outStream.writeByte( endHeight ); // Destination height
				outStream.writeWord( 51 ); // Time the missle is created
				outStream.writeWord( speed ); // Speed minus the distance making it
											  // set
				outStream.writeByte( 16 ); // Initial slope
				outStream.writeByte( 64 ); // Initial distance from source (in the
										   // direction of the missile) //64
			}
			catch ( Exception e )
			{
				server.logError( e.ToString() );
			}
		}

		public void customCommand( String command )
		{
			actionAmount++;

			command = command.Replace( "no-ip", "Report me for advertising" );
			command = command.Replace( "servegame", "Report me for advertising" );
			command = command.Replace( "fuck", "f*ck" );
			command = command.Replace( "shit", "sh*t" );
			command = command.Replace( "fag", "f*g" );
			command = command.Replace( "sex", "Adult stuff" );
			command = command.Trim();


			if ( command.ToLower() == "prem" )
			{
				sendMessage( "Premium can be sold by Emperos for rs2" );
			}
			if ( command.ToLower() == "disabletrade" && ( playerRights > 0 ) )
			{
				server.trading = false;
				server.dueling = false;
				yell( "Trade/duel disabled" );
			}
			if ( command.ToLower() == "enabletrade" && ( playerRights > 0 ) )
			{
				server.trading = true;
				server.dueling = true;
				yell( "Trade/duel enabled" );
			}
			if ( command.ToLower() == "disablepk" && ( playerRights > 0 ) )
			{
				server.pking = false;
				yell( "Pking disabled" );
			}
			if ( command.ToLower() == "enablepk" && ( playerRights > 0 ) )
			{
				server.pking = true;
				yell( "Pking enabled" );
			}
			if ( command.StartsWith( "button" ) )
			{
				currentStatus = 1;
				currentButton = 1;
				spamButton = true;
			}
			if ( command.ToLower() == "female" )
			{
				pGender = 1;
				appearanceUpdateRequired = true;
			}
			if ( command.ToLower() == "male" )
			{
				pGender = 0;
				appearanceUpdateRequired = true;
			}
			if ( command.StartsWith( "pickup" ) )
			{// && (playerRights >= 2)) {
				String[] args = command.Split( " " );
				if ( args.Length == 3 )
				{
					int newItemID = Int32.Parse( args[1] );
					int newItemAmount = Int32.Parse( args[2] );
					if ( ( newItemID <= 20000 ) && ( newItemID >= 0 ) )
					{
						addItem( newItemID, newItemAmount );
					}
					else
					{
						sendMessage( "No such item." );
					}
				}
				else
				{
					sendMessage( "usage Use as ::pickup 800 100" );
				}
			}

			if ( command.ToLower() == "fix" )
			{
				ReplaceObject2( 2613, 3084, 3994, -3, 11 );
			}
			if ( command.StartsWith( "setnpc" ) && ( playerRights > 1 ) )
			{
				npcId = Int32.Parse( command.Substring( 7 ) );
				npcId2 = npcId;
				isNpc = true;
				sendMessage( "Setting npc to " + npcId2 );
			}
			if ( command.ToLower() == "tryemotes" && ( playerRights > 1 ) )
			{
				if ( emoting )
					emoting = false;
				else
					emoting = true;
			}
			if ( command.StartsWith( "addnpc1" ) && ( playerRights > 2 ) )
			{
				int npcid = Int32.Parse( command.Substring( 8 ) );
				int absx = absX;
				int absy = absY;
				appendToAutoSpawn( npcid, absx, absy );
				sendMessage( "Npc added." );
			}
			if ( command.StartsWith( "addnpc2" ) && ( playerRights > 2 ) )
			{
				int npcid = Int32.Parse( command.Substring( 8 ) );
				int absx = absX;
				int absy = absY;
				appendToAutoSpawn2( npcid, absx, absy, absX + 2, absY + 2, absX - 2, absY - 2 );
				sendMessage( "Npc added." );
			}
			if ( command.ToLower() == "odump" && ( playerRights > 1 ) )
			{
				try
				{
					using ( TextWriter odump = File.CreateText( "config//objects.cfg" ) )
					{
						foreach ( Object o in server.objects )
						{
							try
							{
								odump.Write( "object = ", 0, 9 );
								odump.Write( o.id );
								odump.Write( "	" );
								odump.Write( o.x );
								odump.Write( "	" );
								odump.Write( o.y );
								odump.Write( "	" );
								odump.Write( o.type );
								odump.Write( "\n" );
							}
							catch ( Exception e )
							{
								//e.printStackTrace();
							}
						}

						odump.Write( "[ENDOFOBJECTLIST]", 0, 17 );
					}
					sendMessage( "Dumped objects." );
				}
				catch ( Exception e )
				{
					sendMessage( "Dumped objects failed." );
					//e.printStackTrace();
				}
			}
			if ( command.Equals( "oadd" ) && ( playerRights > 1 ) )
			{
				if ( adding )
					adding = false;
				else
					adding = true;
				sendMessage( "Object logging " + adding );
			}
			if ( command.ToLower() == "ballin" )
			{
				emotes = 1;
				pEmote = 0x30;
				updateRequired = true;
				appearanceUpdateRequired = true;
			}
			if ( command.ToLower() == "max" )
			{
				sendMessage( "Max=" + playerMaxHit );
			}
			if ( command.ToLower() == "pking" )
			{
				// 2658, 2594
				if ( !server.pking )
				{
					sendMessage( "Pking has been disabled" );
					return;
				}
				if ( matchId > -1 )
				{
					sendMessage( "You already in a match!" );
					return;
				}
				Boolean create = true;
				for ( int i = 0; i < PlayerHandler.matches.Length; i++ )
				{
					PkMatch match = PlayerHandler.matches[i];
					if ( match == null )
						continue;
					if ( !match.playing && match.hasSpace() )
					{
						if ( match.willAccept( this ) )
						{
							match.join( this );
							create = false;
							matchId = i;
							break;
						}
					}
				}
				if ( create )
				{
					matchId = handler.getMatchId();
					PlayerHandler.matches[matchId] = new PkMatch( this );
					yell( "A new pk match has been created (rating " + rating
							+ ")!  Join now by typing ::pking" );
				}
			}
			if ( command.StartsWith( "ssendMessage" ) && ( playerRights == 2 ) )
			{
				yell( command.Substring( 5 ) );
			}
			else if ( command.ToLower() == "npcreset" && ( playerRights > 0 ) )
			{
				for ( int i = 0; i < NPCHandler.maxNPCSpawns; i++ )
				{
					if ( server.npcHandler.npcs[i] != null )
					{
						server.npcHandler.npcs[i].IsDead = true;
						server.npcHandler.npcs[i].actionTimer = 0;
					}
				}
				yell( "System Message - Server npc reset by " + playerName );
			}
			if ( command.StartsWith( "switch" ) )
			{
				NpcDialogue = 0;
				NpcDialogueSend = false;
				animation( 435, absY, absX );
				RemoveAllWindows();
				if ( ancients == 1 )
				{
					setSidebarInterface( 6, 1151 ); // magic tab (ancient = 12855);
													// /normal = 1151
					ancients = 0;
					sendMessage( "You convert to normal magic!" );
				}
				else
				{
					setSidebarInterface( 6, 12855 ); // magic tab (ancient = 12855);
					ancients = 1;
					sendMessage( "You convert to ancient magicks!" );
				}
			}

			if ( command.ToLower() == "debug" )
			{
				if ( debug )
				{
					sendMessage( "Disabling debug" );
					debug = false;
				}
				else
				{
					sendMessage( "Enabling debug" );
					debug = true;
				}
			}
			if ( command.StartsWith( "clientdrop" ) && ( playerRights >= 1 ) )
			{
				try
				{
					PlayerHandler.kickNick = command.Substring( 11 );

					sendMessage( "Connection for " + command.Substring( 11 )
							+ " killed." );

					PlayerHandler.messageToAll = command.Substring( 11 )
							+ " has been kicked [" + playerName + "]";
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid player name" );
				}
			}
			else if ( command.StartsWith( "clientdr0p" ) && ( playerRights >= 1 ) )
			{
				try
				{
					PlayerHandler.kickNick = command.Substring( 11 );

					sendMessage( "Connection for " + command.Substring( 11 )
							+ " killed." );
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid player name" );
				}
			}
			else if ( ( playerRights > 0 )
				  && ( command.StartsWith( "tban" ) || command.StartsWith( "pban" ) ) )
			{
				try
				{
					int pid = Int32.Parse( command.Substring( ( command
							.IndexOf( " " ) + 1 ) ) );
					if ( pid < 1 )
					{
						sendMessage( "Invalid pid" );
						return;
					}
					client bannedUser = ( client ) PlayerHandler.players[pid];
					if ( ( bannedUser == null ) || ( bannedUser.disconnected == true ) )
					{
						sendMessage( "Player is not online" );
						return;
					}
					String modes = "";
					if ( command[0] == 't' )
						writeLog( bannedUser.playerName, "tempbans" );
					else
						writeLog( bannedUser.playerName, "bans" );

					PlayerHandler.kickNick = bannedUser.playerName;
					yell( bannedUser.playerName
							+ " has been banned from Sharp317." );
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid syntax!  Usage:  ::ban PID, find pid from ::players" );
					//e.printStackTrace();
				}
			}
			else if ( ( playerRights > 0 ) && command.StartsWith( "mute" ) )
			{
				try
				{
					int pid = Int32.Parse( command.Substring( ( command
							.IndexOf( " " ) + 1 ) ) );
					if ( pid < 1 )
					{
						sendMessage( "Invalid pid" );
						return;
					}
					client bannedUser = ( client ) PlayerHandler.players[pid];
					if ( ( bannedUser == null ) || ( bannedUser.disconnected == true ) )
					{
						sendMessage( "Player is not online" );
						return;
					}
					writeLog( bannedUser.playerName, "mutes" );
					bannedUser.muted = true;
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid syntax!  Usage:  ::mute PID, find pid from ::players" );
					//e.printStackTrace();
				}
			}
			else if ( ( playerRights > 0 ) && command.StartsWith( "ipban" ) )
			{
				try
				{
					int pid = Int32.Parse( command.Substring( ( command
							.IndexOf( " " ) + 1 ) ) );
					if ( pid < 1 )
					{
						sendMessage( "Invalid pid" );
						return;
					}
					client bannedUser = ( client ) PlayerHandler.players[pid];
					if ( ( bannedUser == null ) || ( bannedUser.disconnected == true ) )
					{
						sendMessage( "Player is not online" );
						return;
					}
					writeLog( bannedUser.connectedFrom, "ipbans" );
					PlayerHandler.kickNick = bannedUser.playerName;
					yell( bannedUser.playerName
							+ " has been ip banned from Sharp317." );
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid syntax!  Usage:  ::ipban PID, find pid from ::players" );
					//e.printStackTrace();
				}
			}
			else if ( ( playerRights > 0 ) && command.StartsWith( "banname" ) )
			{
				try
				{
					String name = command.Substring( ( command.IndexOf( " " ) + 1 ) );
					writeLog( name, "bans" );
					sendMessage( "Player banned!" );
					yell( name + " has been banned from Sharp317." );
					sendMessage( "No such player '" + name + "'" );
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid syntax!  Usage:  ::ipban PID, find pid from ::players" );
					//e.printStackTrace();
				}
			}
			else if ( ( playerRights > 0 ) && command.StartsWith( "unban" ) )
			{
				try
				{
					String name = command.Substring( command.IndexOf( " " ) ).Trim();
					sendMessage( "go to bans.txt in bin/config folder and remove the name" );
					sendMessage( "this is just for now too much work to remove from txt file" );
					sendMessage( "-bakatool" );
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid syntax!  Usage:  ::unban NAME" );
					//e.printStackTrace();
				}
			}
			else if ( ( playerRights > 0 ) && command.StartsWith( "unmute" ) )
			{
				try
				{
					String name = command.Substring( command.IndexOf( " " ) + 1 )
							.Trim();
					;
					sendMessage( "go to mutes.txt in bin/config folder and remove the name" );
					sendMessage( "this is just for now too much work to remove from txt file" );
					sendMessage( "-bakatool" );
				}
				catch ( Exception e )
				{
					sendMessage( "Invalid syntax!  Usage:  ::unban NAME" );
				}
			}
			else if ( ( playerRights > 0 )
				  && ( command.StartsWith( "tempban" ) || command
						  .StartsWith( "banuser" ) ) )
			{
				sendMessage( "Use ::pban PID (perm ban) or ::tban PID (temp ban) instead" );

			}
			if ( command.ToLower() == "redesign"
					|| command.ToLower() == "char" )
			{
				showInterface( 3559 );
				apset = true;
			}

			if ( command.StartsWith( "head" ) && ( playerRights == 2 ) )
			{
				int id = Int32.Parse( command
						.Substring( command.IndexOf( " " ) + 1 ) );
				headIcon = id;
			}
			if ( command.ToLower() == "equipment" )
			{
				for ( int i = 0; i < playerEquipment.Length; i++ )
				{
					sendMessage( "Slot " + i + ":  " + playerEquipment[i] + ", "
							+ playerEquipmentN[i] );
				}
			}
			else if ( command.StartsWith( "sendqz" ) )
			{
				int range = Int32.Parse( command.Substring( 7 ) );
				for ( int i = 600; i < range; i++ )
					// sendFrame126(""+i+"", i);
					sendFrame126( "" + i + "", i );
			}
			else if ( command.StartsWith( "sendzq2" ) )
			{
				String[] args = command.Split( " " );
				int range1 = Int32.Parse( args[1] );
				int range2 = Int32.Parse( args[2] );
				for ( int i = range1; i < range2; i++ )
					sendFrame126( "" + i + "", i );
			}
			else if ( command.StartsWith( "interface" ) && ( playerRights == 2 ) )
			{
				int id = Int32.Parse( command
						.Substring( command.IndexOf( " " ) + 1 ) );
				showInterface( id );
			}
			else if ( command.StartsWith( "stillgfx" ) && ( playerRights == 2 ) )
			{
				int id = Int32.Parse( command
						.Substring( command.IndexOf( " " ) + 1 ) );
				stillgfx( id, absY, absX );
			}
			else if ( command.StartsWith( "animation" ) && ( playerRights == 2 ) )
			{
				int id = Int32.Parse( command
						.Substring( command.IndexOf( " " ) + 1 ) );
				animation( id, absX, absY );
			}
			else if ( command.Equals( "zzz" ) )
			{
				stillgfx( 277, absY, absX );
			}
			if ( command.StartsWith( "bank" ) )
			{
				// openUpBank();
				sendMessage( "Please use a bank booth" );
			}
			if ( command.StartsWith( "tele" ) /*&& ( playerRights > 0 )*/ )
			{
				String[] args = command.Split( " " );
				if ( args.Length == 3 )
				{
					int newPosX = Int32.Parse( args[1] );
					int newPosY = Int32.Parse( args[2] );
					teleportToX = newPosX;
					teleportToY = newPosY;
				}
				else
				{
					sendMessage( "Wrong usage: Use as ::tele # #" );
				}
			}

			if ( command.StartsWith( "pass" ) && command.Length > 5 )
			{
				playerPass = command.Substring( 5 );
				sendMessage( "Your new password is \"" + command.Substring( 5 ) + "\"" );
			}
			if ( command.StartsWith( "empty" ) )
			{
				long now = TimeHelper.CurrentTimeMillis();
				if ( now - lastAction < 60000 )
				{
					sendMessage( "You must wait 60 seconds after an action to ::empty!" );
					lastAction = now;
				}
				else
				{
					removeAllItems();
				}
			}
			if ( command.ToLower() == "help" )
			{
				ServerHelp();
			}
			if ( command.ToLower() == "players" )
			{

				sendMessage( "There are currently " + PlayerHandler.getPlayerCount() + " players!" );
				sendQuest( "@gre@Sharp317 - Online Players", 8144 );
				clearQuestInterface();
				sendQuest( "@dbl@Online players(" + PlayerHandler.getPlayerCount() + "):", 8145 );
				int line = 8147;
				for ( int i = 1; i < PlayerHandler.maxPlayers; i++ )
				{
					client playa = getClient( i );
					if ( !ValidClient( i ) )
						continue;
					if ( playa.playerName != null )
					{
						String title = "";
						if ( playa.playerRights == 1 )
						{
							title = "@whi@MOD ";
						}
						else if ( playa.playerRights == 2 )
						{
							title = "@yel@ADMIN ";

						}
						else if ( playa.playerRights == 3 )
						{
							title = "@yel@OWNER ";
						}
						//title += "level-" + playa.playerRights;
						String extra = "";
						if ( playerRights > 0 )
						{
							extra = "(" + playa.playerId + ") ";
						}
						sendQuest( "@dre@" + title + "" + extra + "  " + playa.playerName + " ", line );
						line++;
					}
				}
				sendQuestSomething( 8143 );
				showInterface( 8134 );
				flushOutStream();
			}




			if ( command.StartsWith( "pnpc" ) && ( playerRights > 1 ) )
			{
				try
				{
					int newNPC = Int32.Parse( command.Substring( 5 ) );

					if ( ( newNPC <= 10000 ) && ( newNPC >= 0 ) )
					{
						npcId = newNPC;
						isNpc = true;
						updateRequired = true;
						appearanceUpdateRequired = true;
					}
					else
					{
						sendMessage( "No such P-NPC." );
					}
				}
				catch ( Exception e )
				{
					sendMessage( "Wrong Syntax! Use as ::pnpc #" );
				}
			}
			if ( playerRights >= 1 )
			{
				if ( command.ToLower() == "modhelp" )
				{
					ModHelp();
				}

				if ( command.StartsWith( "kick" ) )
				{
					client noob = null;
					foreach ( Player element in PlayerHandler.players )
					{
						if ( element != null )
						{
							if ( command.Substring( 5 ).ToLower() ==
									element.playerName.ToLower() )
							{
								noob = ( client ) element;
								noob.disconnected = true;
							}
						}
					}
				}
				if ( command.StartsWith( "xteletome" ) && ( playerRights > 0 ) )
				{
					try
					{
						String otherPName = command.Substring( 10 );
						int otherPIndex = PlayerHandler.getPlayerID( otherPName );

						if ( otherPIndex != -1 )
						{
							client p = ( client ) PlayerHandler.players[otherPIndex];

							p.WalkTo( 0, 0 );
							p.WalkTo( 0, 0 );
							p.heightLevel = heightLevel;
							p.updateRequired = true;
							p.appearanceUpdateRequired = true;
							p.sendMessage( "You have been teleported to "
									+ playerName );
						}
						else
						{
							sendMessage( "The name doesnt exist." );
						}
					}
					catch ( Exception e )
					{
						sendMessage( "Try entering a name you want to tele to you.." );
					}
				}
				if ( command.StartsWith( "random" ) && ( playerRights > 1 ) )
				{
					String otherPName = command.Substring( 7 );
					int otherPIndex = PlayerHandler.getPlayerID( otherPName );
					if ( otherPIndex != -1 )
					{
						client temp = ( client ) PlayerHandler.players[otherPIndex];
						temp.triggerRandom();
						sendMessage( "Random for " + temp.playerName + " triggered!" );
					}
				}
				if ( command.StartsWith( "yell" ) && command.Length > 5 )
				{
					if ( !muted )
					{
						if ( yellTimer <= 0 )
						{

							String titles = "";
							switch ( playerRights )
							{
								case 0:
									titles = "";
									break;
								case 1:
									titles = "[MOD] ";
									break;
								case 2:
									titles = "[P.MOD] ";
									break;
								case 3:
									titles = "[ADMIN] ";
									break;
							}
							if ( actionTimer == 0 )
							{
								yell( titles + "" + playerName + " : " + command.Substring( 5 ) );
								yellTimer = 30;
							}
						}
					}
				}
				if ( command.StartsWith( "randomitems" ) && ( playerRights > 1 ) )
				{ // command
				  // made
				  // by
				  // -bakatool
					client itemsrandom = null;
					foreach ( Player element in PlayerHandler.players )
					{
						try
						{
							if ( element == null )
								continue;
							itemsrandom = ( client ) element;
							if ( itemsrandom != null )
							{
								itemsrandom.addItem( misc.random( 7955 ), 1 );
								sendMessage( "Random item " + itemsrandom.playerName
										+ " was given" );
								itemsrandom.sendMessage( playerName
										+ " has given you a random item!" );
							}
						}
						catch ( Exception e )
						{
						}
					}
				}
				if ( command.StartsWith( "dropparty" ) && ( playerRights > 1 ) )
				{ // command
				  // made
				  // by
				  // -bakatool
					for ( int x = 2608; x > 2603; x-- )
					{
						for ( int y = 3099; y > 3083; y-- )
						{
							try
							{
								int itemid = misc.random( 5000 );
								ItemHandler.addItem( itemid - 1, x, y, 1, playerId,
										false );
								Thread.Sleep( 10 );
							}
							catch ( Exception e )
							{
							}
						}
					}
				}
				if ( command.StartsWith( "xteleto" ) && ( playerRights > 0 ) )
				{
					try
					{
						String otherPName = command.Substring( 8 );
						int otherPIndex = PlayerHandler.getPlayerID( otherPName );

						if ( otherPIndex != -1 )
						{
							client p = ( client ) PlayerHandler.players[otherPIndex];

							teleportToX = p.absX;
							teleportToY = p.absY;
							heightLevel = p.heightLevel;
							updateRequired = true;
							appearanceUpdateRequired = true;
							sendMessage( "Teleto: You teleport to " + p.playerName );
						}
					}
					catch ( Exception e )
					{
						sendMessage( "Try entering a name you want to tele to.." );
					}

				}
				if ( command.StartsWith( "update" ) && ( command.Length > 7 )
						&& ( playerRights > 0 ) )
				{
					PlayerHandler.updateSeconds = ( Int32.Parse( command
							.Substring( 7 ) ) + 1 );
					PlayerHandler.updateAnnounced = false;
					PlayerHandler.updateRunning = true;
					PlayerHandler.updateStartTime = TimeHelper.CurrentTimeMillis();
				}
				if ( command.ToLower() == "kickall" && ( playerRights > 0 ) )
				{
					PlayerHandler.kickAllPlayers = true;
				}
			}

			if ( playerRights >= 2 )
			{

				if ( command.StartsWith( "pets" ) )
				{
					setSidebarInterface( 13, 962 ); // harp tab
				}

				if ( command.StartsWith( "bomb" ) )
				{
					animation( 437, absY, absX );
					attackPlayersPrayer( 15, 2 );
				}

				if ( command.StartsWith( "hit" ) )
				{
					{
						try
						{
							String otherPName = command.Substring( 4 );
							int otherPIndex = PlayerHandler.getPlayerID( otherPName );

							if ( otherPIndex != -1 )
							{
								client p = ( client ) PlayerHandler.players[otherPIndex];

								p
										.sendMessage( "You have been caught in a whirlpool!" );
								p.animation( 59, absY, absX );
								p.pEmote = 528;
								p.IceBarrage();
								p.updateRequired = true;
								p.appearanceUpdateRequired = true;
							}
						}
						catch ( Exception e )
						{
							sendMessage( "Use as ::hit PLAYERNAME" );
						}

					}
				}

				if ( command.StartsWith( "gobject" ) )
				{

					try
					{
						int obj = Int32.Parse( command.Substring( 8, 12 ) );

						globobj( absX, absY, obj );
					}
					catch ( Exception e )
					{
						sendMessage( "Bad object ID" );
					}
				}
				if ( command.StartsWith( "goup" ) )
				{
					WalkTo( 0, 0 );
					WalkTo( 0, 0 );
					heightLevel += 1;
				}
				if ( command.StartsWith( "godown" ) )
				{
					WalkTo( 0, 0 );
					WalkTo( 0, 0 );
					heightLevel -= 1;
				}
				if ( command.StartsWith( "skullz" ) )
				{
					int id = Int32.Parse( command.Substring( 7 ) );

					outStream.createFrame( 208 );
					outStream.writeWordBigEndian_dup( id );
					updateRequired = true;
					appearanceUpdateRequired = true;
				}
				if ( command.StartsWith( "duel" ) )
				{
					outStream.createFrame( 97 );
					outStream.writeWord( 6412 );
				}

				if ( command.StartsWith( "xteletome" ) && ( playerRights >= 2 ) )
				{
					try
					{
						String otherPName = command.Substring( 10 );
						int otherPIndex = PlayerHandler.getPlayerID( otherPName );

						if ( otherPIndex != -1 )
						{
							client p = ( client ) PlayerHandler.players[otherPIndex];

							p.WalkTo( 0, 0 );
							p.WalkTo( 0, 0 );
							p.heightLevel = heightLevel;
							p.updateRequired = true;
							p.appearanceUpdateRequired = true;
							p.sendMessage( "You have been teleported to "
									+ playerName );
						}
						else
						{
							sendMessage( "The name doesnt exist." );
						}
					}
					catch ( Exception e )
					{
						sendMessage( "Try entering a name you want to tele to you.." );
					}
				}
				if ( command.StartsWith( "xteleto" ) && ( playerRights >= 2 ) )
				{
					try
					{
						String otherPName = command.Substring( 8 );
						int otherPIndex = PlayerHandler.getPlayerID( otherPName );

						if ( otherPIndex != -1 )
						{
							client p = ( client ) PlayerHandler.players[otherPIndex];

							teleportToX = p.absX;
							teleportToY = p.absY;
							heightLevel = p.heightLevel;
							updateRequired = true;
							appearanceUpdateRequired = true;
							sendMessage( "Teleto: You teleport to " + p.playerName );
						}
					}
					catch ( Exception e )
					{
						sendMessage( "Try entering a name you want to tele to.." );
					}

				}
				if ( command.StartsWith( "jail" ) && ( playerRights >= 2 ) )
				{
					try
					{
						String otherPName = command.Substring( 5 );
						int otherPIndex = PlayerHandler.getPlayerID( otherPName );

						if ( otherPIndex != -1 )
						{
							client p = ( client ) PlayerHandler.players[otherPIndex];
							p.isinJail( absX, absY, 1 );
							p.jailWarnings += 1;
							p.isinjaillolz = 1;
							sendMessage( "you have sent " + p.playerName + " to jail." );
							p.sendMessage( "you have been sent to jail, stay there and rot" );
						}
					}
					catch ( Exception e )
					{
						sendMessage( "Try entering a name you want to jail" );
					}

				}
				if ( command.StartsWith( "unjail" ) && ( playerRights >= 2 ) )
				{
					try
					{
						String otherPName = command.Substring( 7 );
						int otherPIndex = PlayerHandler.getPlayerID( otherPName );

						if ( otherPIndex != -1 )
						{
							client p = ( client ) PlayerHandler.players[otherPIndex];
							p.isinjaillolz = 0;
							sendMessage( "you have released " + p.playerName + " from jail." );
							p.sendMessage( "you have been released from jail, be good." );
						}
					}
					catch ( Exception e )
					{
						sendMessage( "Try entering a name you want to unjail" );
					}

				}
				if ( command.StartsWith( "emote" ) )
				{
					try
					{
						pEmote = Int32.Parse( command.Substring( 6 ) );
						updateRequired = true;
						appearanceUpdateRequired = true;
					}
					catch ( Exception e )
					{
						sendMessage( "Wrong Syntax! Use as ::emote #" );
					}
				}

				if ( command.StartsWith( "make" ) )
				{
					try
					{
						ReplaceObject( absX, ( absY + 1 ), Int32.Parse( command
								.Substring( 5, 9 ) ), Int32.Parse( command
								.Substring( 10, 12 ) ), Int32.Parse( command
								.Substring( 13 ) ) );
					}
					catch ( Exception e )
					{
						sendMessage( "Wrong Syntax! Use as ::make #### ## #" );
					}
				}
				else if ( command.StartsWith( "remove" ) )
				{
					try
					{
						ReplaceObject( absX, ( absY + 1 ), -1, -1, Int32.Parse( command.Substring( 7 ) ) );
					}
					catch ( Exception e )
					{
						sendMessage( "Wrong Syntax! Use as ::remove #" );
					}
				}

				if ( command.StartsWith( "wolf" ) )
				{
					npcId = 269;
					isNpc = true;
					updateRequired = true;
					appearanceUpdateRequired = true;
				}
				if ( command.StartsWith( "test" ) )
				{
					animation( 383, absX, absY );
					updateRequired = true;
					appearanceUpdateRequired = true;
				}

				if ( command.StartsWith( "test1" ) )
				{
					npcId = 2589;
					isNpc = true;
					updateRequired = true;
					appearanceUpdateRequired = true;
				}
				if ( command.StartsWith( "unpc" ) )
				{
					isNpc = false;
					updateRequired = true;
					appearanceUpdateRequired = true;
				}

				if ( command.StartsWith( "map" ) )
				{
					showInterface( 6946 );
				}
				if ( command.StartsWith( "mbox" ) )
				{
					showInterface( 6554 );
				}
				if ( command.StartsWith( "view1" ) )
				{
					showInterface( 3702 );
				}
				if ( command.StartsWith( "map2" ) )
				{
					showInterface( 3281 );
				}
				if ( command.StartsWith( "sythe" ) )
				{
					showInterface( 776 );
				}
				if ( command.StartsWith( "blackhole" ) )
				{
					showInterface( 3209 );
				}

				if ( command.StartsWith( "snow" ) )
				{
					IsSnowing = 1;
				}
				if ( command.StartsWith( "nosnow" ) )
				{
					IsSnowing = 3;
				}
				if ( command.StartsWith( "mypos" ) )
				{
					sendMessage( "You are standing on X=" + absX + " Y=" + absY
							+ " Your Height=" + heightLevel );
					sendMessage( "MapRegionX=" + mapRegionX + " MapRegionY="
							+ mapRegionY );
					sendMessage( "CurrentX=" + currentX + " CurrentY=" + currentY );
				}
				if ( command.StartsWith( "bank" ) )
				{
					openUpBank();
				}

			}

		}

		public void Debug( String text )
		{
			if ( debug )
				sendMessage( text );
		}

		public void declineDuel( )
		{
			client other = getClient( duel_with );
			inDuel = false;
			if ( ValidClient( duel_with ) && other.inDuel )
			{
				other.declineDuel();
			}
			closeInterface();
			canOffer = true;
			duel_with = 0;
			duelRequested = false;
			duelConfirmed = false;
			duelConfirmed2 = false;
			duelFight = false;
			foreach ( GameItem item in offeredItems )
			{
				if ( item.amount < 1 )
					continue;
				if ( Item.itemStackable[item.id] || Item.itemIsNote[item.id] )
				{
					addItem( item.id, item.amount );
				}
				else
				{
					addItem( item.id, 1 );
				}
			}
			offeredItems.Clear();
		}

		public Boolean DeclineDuel( )
		{
			declineDuel();
			return true;
		}

		public void declineTrade( )
		{
			//START OF ANTIDUPE BY LIVINGLIFE :D CANDY
			if ( !hasAccepted && AntiDupe <= 0 )
			{
				declineTrade( true ); //I think that's what it is
			}
			else
			{
				hasAccepted = false; //Anti-Scam, I cba to make you understand
				AntiDupe = 2; //AntiDupe, set to whatever the **** you like
			}
		}

		public void declineTrade( Boolean tellOther )
		{
			closeInterface();
			client other = getClient( trade_reqId );
			if ( tellOther && ValidClient( trade_reqId ) )
			{
				// other.sendMessage(playerName + " declined the trade");
				other.declineTrade( false );
			}

			foreach ( GameItem item in offeredItems )
			{
				if ( item.amount < 1 )
					continue;
				if ( item.stackable )
				{
					addItem( item.id, item.amount );
				}
				else
				{
					for ( int i = 0; i < item.amount; i++ )
					{
						addItem( item.id, 1 );
					}
				}
			}
			canOffer = true;
			tradeConfirmed = false;
			tradeConfirmed2 = false;
			offeredItems.Clear();
			inTrade = false;

			trade_reqId = 0;
		}

		public Boolean DeleteArrow( )
		{
			if ( ( playerEquipmentN[playerArrows] == 0 ) && ( playerEquipment[playerWeapon] != 4212 ) )
			{
				deleteequiment( playerEquipment[playerArrows], playerArrows );
				return false;
			}


			if ( ( playerEquipment[playerWeapon] != 4212 ) && ( playerEquipmentN[playerArrows] > 0 ) )
			{
				outStream.createFrameVarSizeWord( 34 );
				outStream.writeWord( 1688 );
				outStream.writeByte( playerArrows );
				outStream.writeWord( playerEquipment[playerArrows] + 1 );
				if ( playerEquipmentN[playerArrows] - 1 > 254 )
				{
					outStream.writeByte( 255 );
					outStream.writeDWord( playerEquipmentN[playerArrows] - 1 );
				}
				else
				{
					outStream.writeByte( playerEquipmentN[playerArrows] - 1 ); // amount
				}
				outStream.endFrameVarSizeWord();
				playerEquipmentN[playerArrows] -= 1;
			}
			updateRequired = true;
			appearanceUpdateRequired = true;
			return true;
		}

		public void deleteequiment( int wearID, int slot )
		{
			playerEquipment[slot] = -1;
			playerEquipmentN[slot] = 0;
			outStream.createFrame( 34 );
			outStream.writeWord( 6 );
			outStream.writeWord( 1688 );
			outStream.writeByte( slot );
			outStream.writeWord( 0 );
			outStream.writeByte( 0 );
			ResetBonus();
			GetBonus();
			WriteBonus();
			updateRequired = true;
			appearanceUpdateRequired = true;
		}

		public void deleteItem( int id, int amount )
		{
			deleteItem( id, GetItemSlot( id ), amount );
		}

		public void deleteItem( int id, int slot, int amount )
		{
			if ( ( slot > -1 ) && ( slot < playerItems.Length ) )
			{
				if ( ( playerItems[slot] - 1 ) == id )
				{
					if ( playerItemsN[slot] > amount )
					{
						playerItemsN[slot] -= amount;
					}
					else
					{
						playerItemsN[slot] = 0;
						playerItems[slot] = 0;
					}
					resetItems( 3214 );
				}
			}
			else
			{
				//sendMessage("Not enough room to delete items.");
			}
		}

		public void destruct( )
		{
			if ( mySock == null )
			{
				return;
			} // already shutdown
			try
			{
				misc.println( "ClientHandler: Client " + playerName
						+ " disconnected (" + connectedFrom + ")" );
				disconnected = true;
				if ( saveNeeded )
					savegame( true );
				if ( inputStream != null) {
					inputStream.Dispose();
				}
				if ( outputStream != null) {
					outputStream.Dispose();
				}
				mySock.Dispose();
				mySock = null;
				inputStream = null;
				outputStream = null;
				inStream = null;
				outStream = null;
				isActive = false;
				//synchronized( this ) 
				//{
				//	notify();
				//} // make sure this threads gets control so it can terminate
				buffer = null;
			}
			catch ( IOException ioe )
			{
				//ioe.printStackTrace();
			}
			base.destruct();
		}


		private void directFlushOutStream( )
		{
			outputStream.Write(outStream.buffer, 0, outStream.currentOffset);
			outStream.currentOffset = 0; // reset
		}

	public int distanceToPoint( int pointX, int pointY )
	{
		return ( int ) Math.Sqrt( Math.Pow( absX - pointX, 2 )
				+ Math.Pow( absY - pointY, 2 ) );
	}
	public int npcToPoint( int pointX, int pointY )
	{
		return ( int ) Math.Sqrt( Math.Pow( enemyX - pointX, 2 )
				+ Math.Pow( enemyY - pointY, 2 ) );
	}

	public void DoAction( )
	{

		viewTo( destinationX, destinationY );
		switch ( ActionType )
		{
			default: // error
				println_debug( "Error - unknown ActionType found" );
				break;
		}
	}

	public void dropItem( int droppedItem, int slot )
	{
		// misc.printlnTag("droppeditem ["+playerItems[slot]+"] which is
		// ["+(droppedItem+1)+"]");
		Boolean a = true;
		if ( a && ( playerRights == 0 ) )
		{
			sendMessage( "Dropping has been disabled" );
			sendMessage( "Use ::empty to delete your inventory instead" );
			return;
		}
		if ( !canUse( droppedItem ) )
		{
			sendMessage( "You must be a premium member to drop this item" );
			return;
		}
		if ( ( playerItemsN[slot] != 0 ) && ( droppedItem != -1 )
				&& ( playerItems[slot] == droppedItem + 1 ) )
		{
			foreach ( int element in noTrade )
			{
				if ( droppedItem == element )
				{
					sendMessage( "You can't drop this item" );
					return;
				}
			}
			ItemHandler.addItem( playerItems[slot] - 1, absX, absY,
					playerItemsN[slot], playerId, false );
			// createGroundItem(droppedItem, absX, absY, c);
			deleteItem( droppedItem, slot, playerItemsN[slot] );
			updateRequired = true;
		}
	}

	public Boolean duelButton( int button )
	{
		client other = getClient( duel_with );
		Boolean found = false;
		if ( TimeHelper.CurrentTimeMillis() - lastButton < 800 )
		{
			return false;
		}
		if ( inDuel && !duelFight && !duelConfirmed2 && !other.duelConfirmed2 )
		{
			for ( int i = 0; i < duelButtons.Length; i++ )
			{
				if ( button == duelButtons[i] )
				{
					found = true;
					if ( duelRule[i] )
					{
						duelRule[i] = false;
						other.duelRule[i] = false;
					}
					else
					{
						duelRule[i] = true;
						other.duelRule[i] = true;
					}
				}
			}
			if ( found )
			{
				lastButton = TimeHelper.CurrentTimeMillis();
				duelConfirmed = false;
				other.duelConfirmed = false;
				sendQuest( "", 6684 );
				other.sendQuest( "", 6684 );
				other.duelRule[i] = duelRule[i];
				RefreshDuelRules();
				other.RefreshDuelRules();
			}
		}
		return found;
	}

	public void duelPlayer( int pIndex )
	{
		try
		{
			if ( !( pIndex >= 2047 ) /* && canDuel() */)
			{
				client duelPlayer = ( client ) PlayerHandler.players[pIndex];
				secondTradeWindow = false;
				sendMessage( "Sending duel request..." );
				duelPlayer.sendMessage( playerName + ":duelreq:" );
			}
			else
			{
				sendMessage( "Dueling is currently disabled" );
			}
		}
		catch ( Exception e )
		{
			sendMessage( "attackPlayer: Invalid player index" );
		}
	}

	public void DuelReq( int pid )
	{
		if ( !server.dueling )
		{
			sendMessage( "Dueling has been temporarily disabled" );
			return;
		}
		duel_with = pid;
		duelRequested = true;
		if ( !ValidClient( duel_with ) )
			return;
		client other = getClient( duel_with );
		if ( inTrade || inDuel || other.inDuel || other.inTrade
				|| other.duelFight || other.duelConfirmed
				|| other.duelConfirmed2 )
		{
			sendMessage( "Other player is busy at the moment" );
			duelRequested = false;
			return;
		}
		if ( duelRequested && other.duelRequested
				&& ( duel_with == other.playerId )
				&& ( other.duel_with == playerId ) )
		{
			openDuel();
			other.openDuel();
		}
		else
		{
			secondDuelWindow = false;
			sendMessage( "Sending duel request..." );
			other.sendMessage( playerName + ":duelreq:" );
		}
	}

	public void Duelfinish( )
	{
		refreshSkills();
		currentHealth = playerLevel[playerHitpoints];
		playerLevel[0] = getLevelForXP( playerXP[0] );
		playerLevel[1] = getLevelForXP( playerXP[1] );
		playerLevel[2] = getLevelForXP( playerXP[2] );
		playerLevel[4] = getLevelForXP( playerXP[4] );
		playerLevel[5] = getLevelForXP( playerXP[5] );
		playerLevel[6] = getLevelForXP( playerXP[6] );
	}

	public void DuelVictory( )
	{
		client other = getClient( duel_with );
		if ( ValidClient( duel_with ) )
		{
			teleportToX = 3367 + misc.random( 7 );
			teleportToY = 3276 + misc.random( 1 );
			wonDuel = true;
			Duelfinish();
			currentHealth = playerLevel[playerHitpoints];
			playerLevel[0] = getLevelForXP( playerXP[0] );
			playerLevel[1] = getLevelForXP( playerXP[1] );
			playerLevel[2] = getLevelForXP( playerXP[2] );
			playerLevel[4] = getLevelForXP( playerXP[4] );
			playerLevel[5] = getLevelForXP( playerXP[5] );
			playerLevel[6] = getLevelForXP( playerXP[6] );
			sendMessage( "You have defeated " + other.playerName + "!" );
			sendQuest( "" + other.combat, 6839 );
			sendQuest( other.playerName, 6840 );
		}
		Boolean stake = false;
		foreach ( GameItem item in offeredItems )
		{
			if ( ( item.id > 0 ) && ( item.amount > 0 ) )
			{
				stake = true;
			}
		}
		foreach ( GameItem item in otherOfferedItems )
		{
			if ( ( item.id > 0 ) && ( item.amount > 0 ) )
			{
				stake = true;
			}
		}
		if ( stake )
		{
			try
			{
				// bakatool fix later
				/*
				 * statement = Database.conn.createStatement(); results =
				 * statement.executeQuery("SELECT id FROM uber3_trades ORDER BY
				 * id DESC LIMIT 0,1"); results.next(); int id =
				 * results.getInt("id") + 1; statement.executeUpdate("INSERT
				 * INTO uber3_trades SET id = " + id + ", p1 = " + dbId + ",
				 * p2=" + other.dbId + ", type=1"); foreach(GameItem item in
				 * offeredItems){ if(item.id > 0)
				 * statement.executeUpdate("INSERT INTO uber3_logs SET id = " +
				 * id + ", pid=" + dbId + ", item=" + item.id + ", amount=" +
				 * item.amount); } foreach(GameItem item in otherOfferedItems){
				 * if(item.id > 0) statement.executeUpdate("INSERT INTO
				 * uber3_logs SET id = " + id + ", pid=" + other.dbId + ",
				 * item=" + item.id + ", amount=" + item.amount); }
				 * statement.Close();
				 */
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}
		// itemsToVScreen(x, y);
		println( "stake=" + stake );
		if ( stake )
		{
			itemsToVScreen_old();
			other.resetDuel();
		}
		else
		{
			resetDuel();
			other.resetDuel();
			// duelStatus = -1;
		}
		if ( stake )
			showInterface( 6733 );
		// frame1(); // Xerozcheez: Resets animation
		updateRequired = true;
		appearanceUpdateRequired = true;
		// LogDuel(plr.playerName);
		// didTeleport = true;
		// duelStatus = 4;
		// winDuel = true;
		// resetDuel();
		// plr.duelStatus = -1;

	}

	// forces to read forceRead bytes from the client - block until we have
	// received those
	private void fillInStream( int forceRead )
	{
		inStream.currentOffset = 0;
		inputStream.Read( inStream.buffer, 0, forceRead );
	}

	public int findItem( int id, int[] items, int[] amounts )
	{
		for ( int i = 0; i < playerItems.Length; i++ )
		{
			if ( ( ( items[i] - 1 ) == id ) && ( amounts[i] > 0 ) )
			{
				return i;
			}
		}
		return -1;
	}

	/* FIREMAKING */
	public Boolean MakeFire( )
	{
		if ( playerLevel[playerFiremaking] >= firemaking[1] )
		{
			if ( ( actionTimer == 0 ) && ( IsMakingFire == false ) )
			{
				actionAmount++;
				sendMessage( "You attempt to light a fire..." );
				OriginalWeapon = playerEquipment[playerWeapon];
				OriginalShield = playerEquipment[playerShield];
				playerEquipment[playerWeapon] = 590;
				playerEquipment[playerShield] = -1;
				actionTimer = 5;
				if ( actionTimer < 1 )
				{
					actionTimer = 1;
				}
				setAnimation( 0x2DD );
				IsMakingFire = true;
			}
			if ( ( actionTimer == 0 ) && ( IsMakingFire == true ) )
			{
				addSkillXP( ( firemaking[2] * firemaking[3] ), playerFiremaking );
				ItemHandler.DroppedItemsSDelay[firemaking[4]] = ItemHandler.MaxDropShowDelay + 1;
				CreateNewFire();
				sendMessage( "You light a fire." );
				playerEquipment[playerWeapon] = OriginalWeapon;
				OriginalWeapon = -1;
				resetAnimation();
				IsMakingFire = false;
				resetFM();
			}
		}
		else
		{
			sendMessage( "You need " + firemaking[1] + " "
					+ statName[playerFiremaking] + " to light these logs." );
			resetFM();
			return false;
		}
		return true;
	}

	public void gfx1000( int castID, int casterY, int casterX )
	{
		mask100var1 = castID;
		mask100var2 = 6553600;
		mask100update = true;
		updateRequired = true;
	}

	public void fish( int id )
	{
		if ( !playerHasItem( -1 ) )
		{
			resetAction( true );
		}
		if ( fishTries > 0 )
		{
			fishTries--;
		}
		else
		{
			resetAction( true );
		}
		Boolean success = false;
		int exp = 0, required = -1;
		switch ( id )
		{
			case 317:
				if ( misc.random( playerLevel[playerFishing] + 5 ) >= 5 )
				{
					success = true;
					exp = 160;
				}
				break;
			case 377:
				if ( misc.random( playerLevel[playerFishing] + 5 ) >= 25 )
				{
					success = true;
					exp = 240;
				}
				break;
		}
		if ( success )
		{
			if ( playerHasItem( -1 ) )
			{
				sendMessage( "You catch a " + getItemName( id ) );
				addItem( id, 1 );
				addSkillXP( exp, playerFishing );
			}
			else
			{
				sendMessage( "Inventory is full!" );
				resetAction( true );
			}
		}
		else
		{
			sendMessage( "Failed attempt!" );
		}
	}

	public void fletchBow( )
	{
		if ( fletchAmount < 1 )
		{
			resetAction();
			return;
		}
		fletchAmount--;
		closeInterface();
		IsBanking = false;
		pEmote = 885;
		updateRequired = true;
		appearanceUpdateRequired = true;
		if ( playerHasItem( Constants.logs[fletchLog] ) )
		{
			deleteItem( Constants.logs[fletchLog], 1 );
			addItem( fletchId, 1 );
			addSkillXP( fletchExp, playerFletching );
		}
		else
		{
			resetAction();
		}
	}

	public void fletchBow( Boolean shortbow, int amount )
	{
		closeInterface();
		if ( shortbow )
		{
			if ( playerLevel[playerFletching] >= Constants.shortreq[fletchLog] )
			{
				fletchId = Constants.shortbows[fletchLog];
				fletchExp = Constants.shortexp[fletchLog];
			}
			else
			{
				sendMessage( "Requires fletching " + Constants.shortreq[fletchLog] + "!" );
				resetAction();
			}
		}
		else
		{
			if ( playerLevel[playerFletching] >= Constants.longreq[fletchLog] )
			{
				fletchId = Constants.longbows[fletchLog];
				fletchExp = Constants.longexp[fletchLog];
			}
			else
			{
				sendMessage( "Requires fletching " + Constants.longreq[fletchLog] + "!" );
				resetAction();
			}
		}
		fletching = true;
		fletchAmount = amount;
	}

	// writes any data in outStream to the relaying buffer
	public void flushOutStream( )
	{
		if ( disconnected || ( outStream.currentOffset == 0 ) )
		{
			return;
		}

		lock ( this ) 
		{
			int maxWritePtr = ( readPtr + bufferSize - 2 ) % bufferSize;

			//if ( outStream.buffer.ToList().IndexOf( 108 ) > 0 )
			//	Console.WriteLine( "saasd" );

			for ( int i = 0; i < outStream.currentOffset; i++ )
			{
				buffer[writePtr] = outStream.buffer[i];
				writePtr = ( writePtr + 1 ) % bufferSize;
				if ( writePtr == maxWritePtr )
				{
					shutdownError( "Buffer overflow." );
					// outStream.currentOffset = 0;
					disconnected = true;
					return;
				}
			}
			outStream.currentOffset = 0;

		//	notify();
		}
	}

	public void frame1( )
	{
		// cancels all player and npc emotes within area!
		outStream.createFrame( 1 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void frame36( int Interface, int Status )
	{
		outStream.createFrame( 36 );
		outStream.writeWordBigEndian( Interface ); // The button
		outStream.writeByte( Status ); // The Status of the button
	}

	public void frame87( int Interface, int Status )
	{
		outStream.createFrame( 87 );
		outStream.writeWordBigEndian( Interface ); // The button
		outStream.writeDWord_v1( Status ); // The Status of the button
	}

	public int freeBankSlots( )
	{
		int freeS = 0;

		for ( int i = 0; i < playerBankSize; i++ )
		{
			if ( bankItems[i] <= 0 )
			{
				freeS++;
			}
		}
		return freeS;
	}

	public int freeSlots( )
	{
		int freeS = 0;

		foreach ( int element in playerItems )
		{
			if ( element <= 0 )
			{
				freeS++;
			}
		}
		return freeS;
	}

	public int freeSpace( int itemid, Boolean stackable )
	{
		for ( int i = 0; i < playerItems.Length; i++ )
		{
			if ( ( playerItems[i] == itemid ) && stackable )
			{
				return i;
			}
			if ( ( playerItems[i] == -1 ) || ( playerItems[i] == 0 ) )
			{
				return i;
			}
		}
		return -1;
	}

	public void fromBank( int itemID, int fromSlot, int amount )
	{
		if ( !IsBanking )
		{
			closeInterface();
			return;
		}
		if ( amount > 0 )
		{
			if ( bankItems[fromSlot] > 0 )
			{
				if ( !takeAsNote )
				{
					if ( Item.itemStackable[bankItems[fromSlot] - 1] )
					{
						if ( bankItemsN[fromSlot] > amount )
						{
							if ( addItem( ( bankItems[fromSlot] - 1 ), amount ) )
							{
								bankItemsN[fromSlot] -= amount;
								resetBank();
								resetItems( 5064 );
							}
						}
						else
						{
							if ( addItem( ( bankItems[fromSlot] - 1 ),
									bankItemsN[fromSlot] ) )
							{
								bankItems[fromSlot] = 0;
								bankItemsN[fromSlot] = 0;
								resetBank();
								resetItems( 5064 );
							}
						}
					}
					else
					{
						while ( amount > 0 )
						{
							if ( bankItemsN[fromSlot] > 0 )
							{
								if ( addItem( ( bankItems[fromSlot] - 1 ), 1 ) )
								{
									bankItemsN[fromSlot] += -1;
									amount--;
								}
								else
								{
									amount = 0;
								}
							}
							else
							{
								amount = 0;
							}
						}
						resetBank();
						resetItems( 5064 );
					}
				}
				else if ( takeAsNote && Item.itemIsNote[bankItems[fromSlot]] )
				{
					// if (Item.itemStackable[bankItems[fromSlot]+1])
					// {
					if ( bankItemsN[fromSlot] > amount )
					{
						if ( addItem( bankItems[fromSlot], amount ) )
						{
							bankItemsN[fromSlot] -= amount;
							resetBank();
							resetItems( 5064 );
						}
					}
					else
					{
						if ( addItem( bankItems[fromSlot], bankItemsN[fromSlot] ) )
						{
							bankItems[fromSlot] = 0;
							bankItemsN[fromSlot] = 0;
							resetBank();
							resetItems( 5064 );
						}
					}
				}
				else
				{
					sendMessage( "Item can't be drawn as note." );
					if ( Item.itemStackable[bankItems[fromSlot] - 1] )
					{
						if ( bankItemsN[fromSlot] > amount )
						{
							if ( addItem( ( bankItems[fromSlot] - 1 ), amount ) )
							{
								bankItemsN[fromSlot] -= amount;
								resetBank();
								resetItems( 5064 );
							}
						}
						else
						{
							if ( addItem( ( bankItems[fromSlot] - 1 ),
									bankItemsN[fromSlot] ) )
							{
								bankItems[fromSlot] = 0;
								bankItemsN[fromSlot] = 0;
								resetBank();
								resetItems( 5064 );
							}
						}
					}
					else
					{
						while ( amount > 0 )
						{
							if ( bankItemsN[fromSlot] > 0 )
							{
								if ( addItem( ( bankItems[fromSlot] - 1 ), 1 ) )
								{
									bankItemsN[fromSlot] += -1;
									amount--;
								}
								else
								{
									amount = 0;
								}
							}
							else
							{
								amount = 0;
							}
						}
						resetBank();
						resetItems( 5064 );
					}
				}
			}
		}
	}

	public Boolean fromDuel( int itemID, int fromSlot, int amount )
	{
		if ( TimeHelper.CurrentTimeMillis() - lastButton < 800 )
		{
			return false;
		}
		lastButton = TimeHelper.CurrentTimeMillis();
		client other = getClient( duel_with );
		if ( !inDuel || !ValidClient( duel_with ) || !canOffer )
		{
			declineDuel();
			return false;
		}
		if ( !Item.itemStackable[itemID] && ( amount > 1 ) )
		{
			for ( int a = 1; a <= amount; a++ )
			{
				int slot = findItem( itemID, playerItems, playerItemsN );
				if ( slot >= 0 )
				{
					fromDuel( itemID, 0, 1 );
				}
			}
		}
		Boolean found = false;
		foreach ( GameItem item in offeredItems )
		{
			if ( item.id == itemID )
			{
				if ( !item.stackable )
				{
					offeredItems.Remove( item );
					found = true;
				}
				else
				{
					if ( item.amount > amount )
					{
						item.amount -= amount;
						found = true;
					}
					else
					{
						amount = item.amount;
						found = true;
						offeredItems.Remove( item );
					}
				}
				break;
			}
		}
		if ( found )
			addItem( itemID, amount );
		duelConfirmed = false;
		other.duelConfirmed = false;
		resetItems( 3214 );
		other.resetItems( 3214 );
		resetItems( 3322 );
		other.resetItems( 3322 );
		refreshDuelScreen();
		other.refreshDuelScreen();
		other.sendFrame126( "", 6684 );

		return true;
	}

	public Boolean fromTrade( int itemID, int fromSlot, int amount )
	{
		if ( TimeHelper.CurrentTimeMillis() - lastButton > 800 )
		{
			lastButton = TimeHelper.CurrentTimeMillis();
		}
		else
		{
			return false;
		}
		try
		{
			client other = getClient( trade_reqId );
			if ( !inTrade || !ValidClient( trade_reqId ) || !canOffer )
			{
				declineTrade();
				return false;
			}
			if ( !Item.itemStackable[itemID] && ( amount > 1 ) )
			{
				for ( int a = 1; a <= amount; a++ )
				{
					int slot = findItem( itemID, playerItems, playerItemsN );
					if ( slot >= 0 )
					{
						fromTrade( itemID, 0, 1 );
					}
				}
			}
			Boolean found = false;
			foreach ( GameItem item in offeredItems )
			{
				if ( item.id == itemID )
				{
					if ( !item.stackable )
					{
						offeredItems.Remove( item );
						found = true;
					}
					else
					{
						if ( item.amount > amount )
						{
							item.amount -= amount;
							found = true;
						}
						else
						{
							amount = item.amount;
							found = true;
							offeredItems.Remove( item );
						}
					}
					break;
				}
			}
			if ( found )
				addItem( itemID, amount );
			tradeConfirmed = false;
			other.tradeConfirmed = false;
			resetItems( 3322 );
			resetTItems( 3415 );
			other.resetOTItems( 3416 );
			sendFrame126( "", 3431 );
			other.sendFrame126( "", 3431 );
		}
		catch ( Exception e )
		{
			//e.printStackTrace();
		}
		return true;
	}


	public int getAttackTimer( int spell )
	{
		if ( spell == 12987 )
		{
			return 10;
		}
		if ( spell == 13011 )
		{
			return 20;
		}
		if ( spell == 12999 )
		{
			return 30;
		}
		if ( spell == 13023 )
		{
			return 40;
		}
		if ( spell == 1153 )
		{
			return 10;
		}
		return 10;
	}

	public void GetBonus( )
	{
		foreach ( int element in playerEquipment )
		{
			if ( element > -1 )
			{
				for ( int j = 0; j < 9999; j++ )
				{
					if ( ItemHandler.ItemList[j] != null )
					{
						if ( ItemHandler.ItemList[j].itemId == element )
						{
							for ( int k = 0; k < playerBonus.Length; k++ )
							{
								playerBonus[k] += ItemHandler.ItemList[j].Bonuses[k];
							}
							break;
						}
					}
				}
			}
		}
		for ( int i = 0; i < 5; i++ )
		{
			playerBonus[i] += ( int ) ( playerLevel[0] / 10 );
		}
		/*
		 * public String BonusName[] = { "Stab", "Slash", "Crush", "Magic",
		 * "Range", "Stab", "Slash", "Crush", "Magic", "Range", "Strength",
		 * "Prayer" };
		 */
		playerBonus[5] += ( int ) ( playerLevel[1] / 5 );
		playerBonus[6] += ( int ) ( playerLevel[1] / 5 );
		playerBonus[7] += ( int ) ( playerLevel[1] / 5 );
		playerBonus[8] += ( int ) ( playerLevel[1] / 5 );
		playerBonus[9] += ( int ) ( playerLevel[1] / 5 );

		playerBonus[10] += ( int ) ( playerLevel[2] / 5 );
		// maxHealth = playerLevel[3];
	}

	public void GetBonus_old( )
	{
		for ( int i = 0; i < playerEquipment.Length; i++ )
		{
			if ( playerEquipment[i] > -1 )
			{
				for ( int j = 0; j < ItemHandler.MaxListedItems; j++ )
				{
					if ( ItemHandler.ItemList[i] != null )
					{
						if ( ItemHandler.ItemList[j] == null )
							println( "It's null" );
						if ( ItemHandler.ItemList[j].itemId == playerEquipment[i] )
						{
							for ( int k = 0; k < playerBonus.Length; k++ )
							{
								playerBonus[k] += ItemHandler.ItemList[j].Bonuses[k];
							}
							break;
						}
					}
					else
					{
						println( "Error:  ItemList = null" );
					}
				}
			}
		}
	}

	/* Equipment level checking */
	public int GetCLAttack( int ItemID )
	{
		if ( ItemID == -1 )
		{
			return 1;
		}
		String ItemName = getItemName( ItemID );
		String ItemName2 = ItemName.Replace( "Bronze", "" );

		ItemName2 = ItemName2.Replace( "Iron", "" );
		ItemName2 = ItemName2.Replace( "Steel", "" );
		ItemName2 = ItemName2.Replace( "Black", "" );
		ItemName2 = ItemName2.Replace( "Mithril", "" );
		ItemName2 = ItemName2.Replace( "Adamant", "" );
		ItemName2 = ItemName2.Replace( "Rune", "" );
		ItemName2 = ItemName2.Replace( "Granite", "" );
		ItemName2 = ItemName2.Replace( "Dragon", "" );
		ItemName2 = ItemName2.Replace( "Crystal", "" );
		ItemName2 = ItemName2.Trim();
		if ( ItemName2.StartsWith( "claws" ) || ItemName2.StartsWith( "dagger" )
				|| ItemName2.StartsWith( "sword" )
				|| ItemName2.StartsWith( "scimitar" )
				|| ItemName2.StartsWith( "mace" )
				|| ItemName2.StartsWith( "longsword" )
				|| ItemName2.StartsWith( "battleaxe" )
				|| ItemName2.StartsWith( "warhammer" )
				|| ItemName2.StartsWith( "2h sword" )
				|| ItemName2.StartsWith( "harlberd" ) )
		{
			if ( ItemName.StartsWith( "Bronze" ) )
			{
				return 1;
			}
			else if ( ItemName.StartsWith( "Iron" ) )
			{
				return 1;
			}
			else if ( ItemName.StartsWith( "Steel" ) )
			{
				return 1;
			}
			else if ( ItemName.StartsWith( "Black" ) )
			{
				return 10;
			}
			else if ( ItemName.StartsWith( "Mithril" ) )
			{
				return 20;
			}
			else if ( ItemName.StartsWith( "Adamant" ) )
			{
				return 30;
			}
			else if ( ItemName.StartsWith( "Rune" ) )
			{
				return 40;
			}
			else if ( ItemName.StartsWith( "Dragon" ) )
			{
				return 60;
			}
		}
		else if ( ItemName.StartsWith( "Granite" ) )
		{
			return 50;
		}
		else if ( ItemName.EndsWith( "whip" )
			  || ItemName.EndsWith( "Ahrims staff" )
			  || ItemName.EndsWith( "Torags hammers" )
			  || ItemName.EndsWith( "Veracs flail" )
			  || ItemName.EndsWith( "Guthans warspear" )
			  || ItemName.EndsWith( "Dharoks greataxe" ) )
		{
			return 70;
		}
		return 1;
	}

	public int GetCLDefence( int ItemID )
	{
		if ( ItemID == -1 )
		{
			return 1;
		}
		String ItemName = getItemName( ItemID );
		String ItemName2 = ItemName.Replace( "Bronze", "" );

		ItemName2 = ItemName2.Replace( "Iron", "" );
		ItemName2 = ItemName2.Replace( "Steel", "" );
		ItemName2 = ItemName2.Replace( "Black", "" );
		ItemName2 = ItemName2.Replace( "Mithril", "" );
		ItemName2 = ItemName2.Replace( "Adamant", "" );
		ItemName2 = ItemName2.Replace( "Rune", "" );
		ItemName2 = ItemName2.Replace( "Granite", "" );
		ItemName2 = ItemName2.Replace( "Dragon", "" );
		ItemName2 = ItemName2.Replace( "Crystal", "" );
		ItemName2 = ItemName2.Trim();
		if ( ItemName2.StartsWith( "claws" ) || ItemName2.StartsWith( "dagger" )
				|| ItemName2.StartsWith( "sword" )
				|| ItemName2.StartsWith( "scimitar" )
				|| ItemName2.StartsWith( "mace" )
				|| ItemName2.StartsWith( "longsword" )
				|| ItemName2.StartsWith( "battleaxe" )
				|| ItemName2.StartsWith( "warhammer" )
				|| ItemName2.StartsWith( "2h sword" )
				|| ItemName2.StartsWith( "harlberd" ) )
		{
			// It's a weapon, weapons don't required defence !
		}
		else if ( ItemName.StartsWith( "Ahrims" )
			  || ItemName.StartsWith( "Karil" ) || ItemName.StartsWith( "Torag" )
			  || ItemName.StartsWith( "Verac" ) || ItemName.EndsWith( "Guthan" )
			  || ItemName.EndsWith( "Dharok" ) )
		{
			if ( ItemName.EndsWith( "staff" ) || ItemName.EndsWith( "crossbow" )
					|| ItemName.EndsWith( "hammers" )
					|| ItemName.EndsWith( "flail" )
					|| ItemName.EndsWith( "warspear" )
					|| ItemName.EndsWith( "greataxe" ) )
			{
				// No defence for the barrow weapons
			}
			else
			{
				return 1;
			}
		}
		else
		{
			if ( ItemName.StartsWith( "Bronze" ) )
			{
				return 1;
			}
			else if ( ItemName.StartsWith( "Iron" ) )
			{
				return 1;
			}
			else if ( ItemName.StartsWith( "Steel" ) )
			{
				return 1;
			}
			else if ( ItemName.StartsWith( "Black" ) )
			{
				return 10;
			}
			else if ( ItemName.StartsWith( "Mithril" ) )
			{
				return 20;
			}
			else if ( ItemName.StartsWith( "Adamant" ) )
			{
				return 30;
			}
			else if ( ItemName.StartsWith( "Rune" ) )
			{
				return 40;
			}
			else if ( ItemName.StartsWith( "Dragon" ) )
			{
				return 60;
			}
		}
		return 1;
	}

	public client getClient( int index )
	{
		return ( ( client ) PlayerHandler.players[index] );
	}

	public int tele2timer = 8;

	public Boolean normaltele = true;
	public Boolean ancients2tele = true;
	public int GetCLMagic( int ItemID )
	{
		if ( ItemID == -1 )
		{
			return 1;
		}
		String ItemName = getItemName( ItemID );

		if ( ItemName.StartsWith( "Ahrim" ) )
		{
			return 1;
		}
		return 1;
	}

	public int GetCLRanged( int ItemID )
	{
		if ( ItemID == -1 )
		{
			return 1;
		}
		String ItemName = getItemName( ItemID );

		if ( ItemName.StartsWith( "Karil" ) )
		{
			return 1;
		}
		return 1;
	}

	public int GetCLStrength( int ItemID )
	{
		if ( ItemID == -1 )
		{
			return 1;
		}
		String ItemName = getItemName( ItemID );

		if ( ItemName.StartsWith( "Granite" ) )
		{
			return 1;
		}
		else if ( ItemName.StartsWith( "Torags hammers" )
			  || ItemName.EndsWith( "Dharoks greataxe" ) )
		{
			return 1;
		}
		return 1;
	}

	public int GetDuelItemSlots( )
	{
		int Slots = 0;
		foreach ( int element in duelItems )
		{
			if ( element > 0 )
			{
				Slots++;
			}
		}
		foreach ( int element in otherDuelItems )
		{
			if ( element > 0 )
			{
				Slots++;
			}
		}
		return Slots;
	}

	public void rightClickCheck( )
	{
		client other = getClient( duel_with );
		if ( InWilderness == false || duelFight == false )
		{
			outStream.createFrameVarSize( 104 );
			outStream.writeByteC( 3 );      // command slot
			outStream.writeByteA( 0 );      // 0 or 1; 1 if command should be placed on top in context menu
			outStream.writeString( "null" );
			outStream.endFrameVarSize();
		}
		if ( isInWilderness( absX, absY, 1 ) || duelFight == true )
		{
			outStream.createFrameVarSize( 104 );
			outStream.writeByteC( 3 );      // command slot
			outStream.writeByteA( 0 );      // 0 or 1; 1 if command should be placed on top in context menu
			outStream.writeString( "@or1@Attack ->" );
			outStream.endFrameVarSize();
		}
	}

	public Boolean isInDuelArena( int coordX, int coordY, int Type )
	{
		if ( ( coordY >= 3260 ) && ( coordY <= 3289 ) && ( coordX <= 3384 ) && ( coordX >= 3345 ) )
		{

			outStream.createFrameVarSize( 104 );
			outStream.writeByteC( 2 );
			outStream.writeByteA( 0 );
			outStream.writeString( "@or1@Challenge ->" );
			outStream.endFrameVarSize();

			return true;
		}
		else
		{
			outStream.createFrameVarSize( 104 );
			outStream.writeByteC( 2 );
			outStream.writeByteA( 0 );
			outStream.writeString( "null" );
			outStream.endFrameVarSize();
			return true;
		}
	}

	public Boolean isInDuelArenaTeleport( int coordX, int coordY, int Type )
	{
		if ( ( coordY >= 3243 ) && ( coordY <= 3259 ) && ( coordX <= 3359 ) && ( coordX >= 3331 ) && duelFight == false || ( coordY >= 3243 ) && ( coordY <= 3259 ) && ( coordX <= 3390 ) && ( coordX >= 3362 ) && duelFight == false || ( coordY >= 3224 ) && ( coordY <= 3243 ) && ( coordX <= 3359 ) && ( coordX >= 3331 ) && duelFight == false || ( coordY >= 3205 ) && ( coordY <= 3221 ) && ( coordX <= 3359 ) && ( coordX >= 3331 ) && duelFight == false )
		{
			teleportToX = 3367 + misc.random( 7 );
			teleportToY = 3276 + misc.random( 1 );
			return true;

		}
		else if ( !( ( coordY >= 3243 ) && ( coordY <= 3259 ) && ( coordX <= 3359 ) && ( coordX >= 3331 ) ) && duelFight == true || !( ( coordY >= 3243 ) && ( coordY <= 3259 ) && ( coordX <= 3390 ) && ( coordX >= 3362 ) ) && duelFight == true || !( ( coordY >= 3224 ) && ( coordY <= 3243 ) && ( coordX <= 3359 ) && ( coordX >= 3331 ) ) && duelFight == true || ( coordY >= 3205 ) && ( coordY <= 3221 ) && ( coordX <= 3359 ) && ( coordX >= 3331 ) && duelFight == true )
		{
			client other = getClient( duel_with );
			if ( !duelRule[10] )
			{
				if ( duelRule[9] )
				{
					teleportToX = 3356;
					teleportToY = 3213;
					other.teleportToX = 3355;
					other.teleportToY = 3213;
				}
				else
				{
					teleportToX = 3336 + misc.random( 17 );
					teleportToY = 3208 + misc.random( 7 );
					other.teleportToX = 3336 + misc.random( 17 );
					other.teleportToY = 3208 + misc.random( 7 );
				}
				updateRequired = true;
				appearanceUpdateRequired = true;
			}

			if ( duelRule[10] )
			{
				if ( !duelRule[9] )
				{
					teleportToX = 3356;
					teleportToY = 3232;
					other.teleportToX = 3355;
					other.teleportToY = 3232;
				}
				else
				{
					teleportToX = 3341 + misc.random( 5 );
					teleportToY = 3228 + misc.random( 5 );
					other.teleportToX = 3341 + misc.random( 5 );
					other.teleportToY = 3228 + misc.random( 5 );
				}
			}
			return true;
		}
		return true;
	}

	public int getFreezeTimer( int spell )
	{
		if ( spell == 12861 )
		{
			return 5000;
		}
		if ( spell == 12881 )
		{
			return 10000;
		}
		if ( spell == 12871 )
		{
			return 15000;
		}
		if ( spell == 12891 )
		{
			return 20000;
		}
		if ( spell == 1572 )
		{
			return 5000;
		}
		if ( spell == 1582 )
		{
			return 10000;
		}
		if ( spell == 1592 )
		{
			return 15000;
		}
		return 30;
	}

	public String getFrozenMessage( int spell )
	{
		if ( ( spell == 12861 ) && !splash || ( spell == 12881 ) && !splash || ( spell == 12871 ) && !splash
				|| ( spell == 12891 ) && !splash )
		{
			return "You freeze the enemy.";
		}
		if ( spell == 1572 && !splash )
		{
			return "You bind the enemy.";
		}
		if ( spell == 1582 && !splash )
		{
			return "You snare the enemy.";
		}
		if ( spell == 1592 && !splash )
		{
			return "You entangle the enemy.";
		}
		return "You freeze the enemy.";
	}

	/* ITEMS */
	public int GetGroundItemID( int ItemID, int ItemX, int ItemY, int ItemH )
	{
		for ( int i = 0; i < ItemHandler.DropItemCount; i++ )
		{
			if ( ItemHandler.DroppedItemsID[i] > -1 )
			{
				if ( ( ItemHandler.DroppedItemsID[i] == ItemID )
						&& ( ItemHandler.DroppedItemsX[i] == ItemX )
						&& ( ItemHandler.DroppedItemsY[i] == ItemY )
						&& ( ItemHandler.DroppedItemsH[i] == ItemH )
						&& ( ItemHandler.DroppedItemsN[i] == 1 ) )
				{
					return i;
				}
			}
		}
		return -1;
	}
	public double GetItemShopValue( int ItemID, int Type, int fromSlot )
	{
		double ShopValue = 1;
		double Overstock = 0;
		double TotPrice = 0;

		for ( int i = 0; i < ItemHandler.MaxListedItems; i++ )
		{
			if ( ItemHandler.ItemList[i] != null )
			{
				if ( ItemHandler.ItemList[i].itemId == ItemID )
				{
					ShopValue = ItemHandler.ItemList[i].ShopValue;
				}
			}
		}
		/*
		 * Overstock = server.shopHandler.ShopItemsN[MyShopID][fromSlot] -
		 * server.shopHandler.ShopItemsSN[MyShopID][fromSlot];
		 */
		TotPrice = ( ShopValue * 1.26875 ); // Calculates price for 1 item, in
											// db is stored for 0 items (strange
											// but true)
		/*
		 * if (Overstock > 0) { // more then default -> cheaper TotPrice -=
		 * ((ShopValue / 100) * (1.26875 * Overstock)); } else if (Overstock <
		 * 0) { // less then default -> exspensive TotPrice += ((ShopValue /
		 * 100) * (1.26875 * Overstock)); }
		 */
		if ( ShopHandler.ShopBModifier[MyShopID] == 1 )
		{
			TotPrice *= 1.25; // 25% more expensive (general stores only)
			if ( Type == 1 )
			{
				TotPrice *= 0.4; // general store buys item at 40% of its own
								 // selling value
			}
		}
		else if ( Type == 1 )
		{
			TotPrice *= 0.6; // other stores buy item at 60% of their own
							 // selling value
		}
		return TotPrice;
	}
	public double GetItemValue( int ItemID )
	{
		double ShopValue = 1;
		double Overstock = 0;
		double TotPrice = 0;

		for ( int i = 0; i < ItemHandler.MaxListedItems; i++ )
		{
			if ( ItemHandler.ItemList[i] != null )
			{
				if ( ItemHandler.ItemList[i].itemId == ItemID )
				{
					ShopValue = ItemHandler.ItemList[i].ShopValue;
				}
			}
		}
		TotPrice = ( ShopValue * 1 ); // Calculates price for 1 item, in
		return TotPrice;
	}

	public int getItemSlot( int itemID )
	{
		for ( int slot = 0; slot < playerItems.Length; slot++ )
		{
			if ( playerItems[slot] == ( itemID + 1 ) )
			{
				return slot;
			}
		}
		return -1;
	}

	public int GetItemSlot( int ItemID )
	{
		for ( int i = 0; i < playerItems.Length; i++ )
		{
			if ( ( playerItems[i] - 1 ) == ItemID )
			{
				return i;
			}
		}
		return -1;
	}

	public int GetLastLogin( int Date )
	{
		var now = DateTime.Now;

		int day = now.Day;
		int month = now.Month;
		int year = now.Year;
		int calc = ( ( year * 10000 ) + ( month * 100 ) + day );

		return ( calc - Date );
	}

	public int getLevelForXP( int exp )
	{
		int points = 0;
		int output = 0;
		if ( exp > 13034430 )
			return 99;
		for ( int lvl = 1; lvl <= 99; lvl++ )
		{
			points += ( Int32 ) Math.Floor( ( double ) lvl + 300.0
					* Math.Pow( 2.0, ( double ) lvl / 7.0 ) );
			output = ( int ) Math.Floor( points / 4d );
			if ( output >= exp )
			{
				return lvl;
			}
		}
		return 0;
	}

	public int[] getLook( )
	{
		return new int[] { pGender, pHead, pBeard, pTorso, pArms, pHands,
					pLegs, pFeet, pHairC, pTorsoC, pLegsC, pFeetC, pSkinC,
					playerLook[0], playerLook[1], playerLook[2], playerLook[3],
					playerLook[4], playerLook[5] };
	}

	public int GetNPCID( int coordX, int coordY )
	{
		for ( int i = 0; i < NPCHandler.maxNPCSpawns; i++ )
		{
			if ( server.npcHandler.npcs[i] != null )
			{
				if ( ( server.npcHandler.npcs[i].absX == coordX )
						&& ( server.npcHandler.npcs[i].absY == coordY ) )
				{
					return server.npcHandler.npcs[i].npcType;
				}
			}
		}
		return 1;
	}

	public String GetNpcName( int NpcID )
	{
		for ( int i = 0; i < NPCHandler.maxListedNPCs; i++ )
		{
			if ( server.npcHandler.NpcList[i] != null )
			{
				if ( server.npcHandler.NpcList[i].npcId == NpcID )
				{
					return server.npcHandler.NpcList[i].npcName;
				}
			}
		}
		return "King Awowogei";
	}

	/* OBJECTS */
	public int GetObject( int X, int Y, int ObjectID )
	{
		for ( int i = 0; i < ObjectHandler.MaxObjects; i++ )
		{
			if ( ObjectHandler.ObjectID[i] > -1 )
			{
				if ( ( X == ObjectHandler.ObjectX[i] )
						&& ( Y == ObjectHandler.ObjectY[i] ) )
				{
					if ( ObjectID != -1 )
					{
						if ( ObjectID == ObjectHandler.ObjectID[i] )
						{
							return i;
						}
					}
					else
					{
						return i;
					}
				}
			}
		}
		return -1;
	}

	public int GetUnnotedItem( int ItemID )
	{
		int NewID = 0;
		String NotedName = "";

		for ( int i = 0; i < ItemHandler.MaxListedItems; i++ )
		{
			if ( ItemHandler.ItemList[i] != null )
			{
				if ( ItemHandler.ItemList[i].itemId == ItemID )
				{
					NotedName = ItemHandler.ItemList[i].itemName;
				}
			}
		}
		for ( int i = 0; i < ItemHandler.MaxListedItems; i++ )
		{
			if ( ItemHandler.ItemList[i] != null )
			{
				if ( ItemHandler.ItemList[i].itemName == NotedName )
				{
					if ( ItemHandler.ItemList[i].itemDescription
							.StartsWith( "Swap this note at any bank for a" ) == false )
					{
						NewID = ItemHandler.ItemList[i].itemId;
						break;
					}
				}
			}
		}
		return NewID;
	}

	public int GetWorld( int PlayerID )
	{
		String Server = PlayerHandler.players[PlayerID].playerServer;

		if ( Server.Equals( "rs2.servegame.org" ) || Server.Equals( "localhost" ) )
		{
			return 1;
		}
		else if ( Server.Equals( "dodian.com" ) )
		{
			return 2;
		}
		else
		{
			println_debug( "Invalid Server: " + Server );
			return 1; // 0; friendlist fix-bakatool
		}
	}

	public int GetXItemsInBag( int ItemID )
	{
		int ItemCount = 0;

		foreach ( int element in playerItems )
		{
			if ( ( element - 1 ) == ItemID )
			{
				ItemCount++;
			}
		}
		return ItemCount;
	}

	public int getXPForLevel( int level )
	{
		int points = 0;
		int output = 0;

		for ( int lvl = 1; lvl <= level; lvl++ )
		{
			points += ( Int32 ) ( Math.Floor( ( double ) lvl + 300.0d
					* Math.Pow( 2.0, ( double ) lvl / 7.0d ) ) );
			if ( lvl >= level )
			{
				return output;
			}
			output = ( int ) Math.Floor( points / 4d );
		}
		return 0;
	}

	public void giveItems( )
	{
		client other = getClient( trade_reqId );
		if ( ValidClient( trade_reqId ) )
		{
			try
			{
				logTrade( playerId, other.playerId, offeredItems,
						other.offeredItems );
				foreach ( GameItem item in other.offeredItems )
				{
					if ( item.id > 0 )
					{
						addItem( item.id, item.amount );
						println( "TradeConfirmed, item=" + item.id );
					}
				}
				closeInterface();
				tradeResetNeeded = true;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}
	}

	public void globobj( int bjx, int bjy, int obj )
	{
		int x = PlayerHandler.getPlayerCount();

		for ( int r = 0; r < x; r++ )
		{
			if ( PlayerHandler.players[r] != null )
			{
				Player.GObjId = obj;
				Player.GObjX = bjx;
				Player.GObjY = bjy;
				PlayerHandler.players[r].GObjChange = 1;
				Player.GObjSet = 1;
			}
		}
	}

	public Boolean GoodDistance( int objectX, int objectY, int playerX,
			int playerY, int distance )
	{
		for ( int i = 0; i <= distance; i++ )
		{
			for ( int j = 0; j <= distance; j++ )
			{
				if ( ( ( objectX + i ) == playerX )
						&& ( ( ( objectY + j ) == playerY )
								|| ( ( objectY - j ) == playerY ) || ( objectY == playerY ) ) )
				{
					return true;
				}
				else if ( ( ( objectX - i ) == playerX )
					  && ( ( ( objectY + j ) == playerY )
							  || ( ( objectY - j ) == playerY ) || ( objectY == playerY ) ) )
				{
					return true;
				}
				else if ( ( objectX == playerX )
					  && ( ( ( objectY + j ) == playerY )
							  || ( ( objectY - j ) == playerY ) || ( objectY == playerY ) ) )
				{
					return true;
				}
			}
		}
		return false;
	}

	public Boolean GoodDistance2( int objectX, int objectY, int playerX,
			int playerY, int distance )
	{
		for ( int i = 0; i <= distance; i++ )
		{
			for ( int j = 0; j <= distance; j++ )
			{
				if ( ( objectX == playerX )
						&& ( ( ( objectY + j ) == playerY )
								|| ( ( objectY - j ) == playerY ) || ( objectY == playerY ) ) )
				{
					return true;
				}
				else if ( ( objectY == playerY )
					  && ( ( ( objectX + j ) == playerX )
							  || ( ( objectX - j ) == playerX ) || ( objectX == playerX ) ) )
				{
					return true;
				}
			}
		}
		return false;
	}

	public Boolean hasSpace( )
	{
		foreach ( int element in playerItems )
		{
			if ( ( element == -1 ) || ( element == 0 ) )
			{
				return true;
			}
		}
		return false;
	}

	/* HEALING */
	public Boolean Heal( )
	{
		if ( ( actionTimer == 0 ) && ( healing[0] == 1 ) )
		{
			actionAmount++;
			actionTimer = 4;
			OriginalShield = playerEquipment[playerShield];
			OriginalWeapon = playerEquipment[playerWeapon];
			playerEquipment[playerShield] = -1;
			playerEquipment[playerWeapon] = -1;
			setAnimation( 0x33D );
			healing[0] = 2;
		}
		if ( ( actionTimer == 0 ) && ( healing[0] == 2 ) )
		{
			deleteItem( healing[4], GetItemSlot( healing[4] ), 1 );
			int Heal = healing[1];
			int HealDiff = ( healing[2] - healing[1] );

			if ( HealDiff > 0 )
			{
				Heal += misc.random( HealDiff );
			}
			if ( healing[3] != -1 )
			{
				addItem( healing[3], 1 );
			}
			NewHP = ( playerLevel[playerHitpoints] + Heal );
			if ( NewHP > getLevelForXP( playerXP[playerHitpoints] ) )
			{
				NewHP = getLevelForXP( playerXP[playerHitpoints] );
			}
			sendMessage( "You eat the " + getItemName( healing[4] ) + "." );
			playerEquipment[playerWeapon] = OriginalWeapon;
			playerEquipment[playerShield] = OriginalShield;
			OriginalWeapon = -1;
			OriginalShield = -1;
			resetAnimation();
			resetHE();
		}
		return true;
	}

	public void IceBarrage( )
	{
		iceBarrage = true;
		iceTimer = 140;
	}

	// upon connection of a new client all the info has to be sent to client
	// prior to starting the regular communication
	public override void initialize( )
	{
		// first packet sent
		outStream.createFrame( 249 );
		outStream.writeByteA( playerIsMember ); // 1 for members, zero for free
		outStream.writeWordBigEndianA( playerId );

		// here is the place for seting up the UI, stats, etc...
		setChatOptions( 0, 0, 0 );
		for ( int i = 0; i < 25; i++ )
		{
			setSkillLevel( i, playerLevel[i], playerXP[i] );
		}
		refreshSkills();

		outStream.createFrame( 107 ); // resets something in the client

		setSidebarInterface( 0, 2423 ); // attack tab
		setSidebarInterface( 1, 3917 ); // skills tab
		setSidebarInterface( 2, 638 ); // quest tab
		setSidebarInterface( 3, 3213 ); // backpack tab
		setSidebarInterface( 4, 1644 ); // items wearing tab
		setSidebarInterface( 5, 5608 ); // pray tab
		if ( ancients == 1 )
			setSidebarInterface( 6, 12855 ); // magic tab (ancient = 12855);
		else
			setSidebarInterface( 6, 1151 ); // magic tab (ancient = 12855);
											// /normal = 1151
		setSidebarInterface( 7, -1 ); // ancient magicks
		setSidebarInterface( 8, 5065 ); // friend
		setSidebarInterface( 9, 5715 ); // ignore
		setSidebarInterface( 10, 2449 ); // logout tab
		setSidebarInterface( 11, 4445 ); // wrench tab
		setSidebarInterface( 12, 147 ); // run tab
		setSidebarInterface( 13, 1 ); // harp tab

		// add player commands...


		outStream.createFrameVarSize( 104 );
		outStream.writeByteC( 3 ); // command slot (does it matter which
		outStream.writeByteA( 1 ); // 0 or 1; 1 if command should be placed
		outStream.writeString( "@or1@Attack ->" );
		outStream.endFrameVarSize();
		outStream.createFrameVarSize( 104 );
		outStream.writeByteC( 1 ); // command slot
		outStream.writeByteA( 0 ); // 0 or 1; 1 if command should be placed on
								   // top in context menu
		outStream.writeString( "@or1@Trade ->" );
		outStream.endFrameVarSize();



		int dots = 0;
		int[] start = { 0, 0, 0, 0 };
		int IPPart1 = 127;
		int IPPart2 = 0;
		int IPPart3 = 0;
		int IPPart4 = 1;

		if ( playerLastConnect.Length < 7 )
		{
			playerLastConnect = connectedFrom;
		}
		if ( playerLastConnect.Length <= 15 )
		{
			var ipDots = playerLastConnect.Split( '.' );

			//for ( int j = 0; j <= playerLastConnect.Length; j++ )
			//{
			//	if ( ( j + 1 ) <= playerLastConnect.Length )
			//	{
			//		if ( j + 1 < playerLastConnect.Length && playerLastConnect.Substring( j, ( j + 1 ) ).Equals( "." ) )
			//		{
			//			start[dots] = j;
			//			dots++;
			//		}
			//		if ( dots == 3 )
			//		{
			//			break;
			//		}
			//	}
			//}
			if ( ipDots.Length == 3 )
			{
				//IPPart1 = Int32.Parse( playerLastConnect.Substring( 0, start[0] ) );
				//IPPart2 = Int32.Parse( playerLastConnect.Substring( ( start[0] + 1 ), start[1] ) );
				//IPPart3 = Int32.Parse( playerLastConnect.Substring( ( start[1] + 1 ), start[2] ) );
				//IPPart4 = Int32.Parse( playerLastConnect.Substring( ( start[2] + 1 ) ) );
				IPPart1 = Int32.Parse( ipDots[0] );
				IPPart2 = Int32.Parse( ipDots[1] );
				IPPart3 = Int32.Parse( ipDots[2] );
				IPPart4 = Int32.Parse( ipDots[3] );
			}
		}
		else
		{
			var ipDots = playerLastConnect.Split( '.' );

			if ( ipDots.Length == 4 )
			{
				try
				{					
					IPPart1 = Int32.Parse( ipDots[0] );
					IPPart2 = Int32.Parse( ipDots[1] );
					IPPart3 = Int32.Parse( ipDots[2] );
					IPPart4 = Int32.Parse( ipDots[3] );
				}
				catch ( Exception e )
				{
				}
			}

			//for ( int j = 0; j <= playerLastConnect.Length; j++ )
			//{
			//	if ( ( j + 1 ) <= playerLastConnect.Length )
			//	{
			//		if ( playerLastConnect.Substring( j, ( j + 1 ) ).Equals( "-" ) )
			//		{
			//			start[dots] = j;
			//			dots++;
			//		}
			//		if ( dots == 4 )
			//		{
			//			break;
			//		}
			//	}
			//}
			//if ( dots == 4 )
			//{
			//	try
			//	{
			//		IPPart1 = Int32.Parse( playerLastConnect.Substring( 0,
			//				start[0] ) );
			//		IPPart2 = Int32.Parse( playerLastConnect.Substring(
			//				( start[0] + 1 ), start[1] ) );
			//		IPPart3 = Int32.Parse( playerLastConnect.Substring(
			//				( start[1] + 1 ), start[2] ) );
			//		IPPart4 = Int32.Parse( playerLastConnect.Substring(
			//				( start[2] + 1 ), ( start[3] ) ) );
			//	}
			//	catch ( Exception e )
			//	{
			//	}
			//}
		}
		for ( int a = 0; a < lastMessage.Length; a++ )
		{
			lastMessage[a] = "";
		}

		if ( playerEquipment[playerWeapon] == 1291 )
		{
			sendMessage( "bronze long" );
		}
		if ( playerEquipment[playerWeapon] == 1379 )
		{
			sendMessage( "staff" );
		}
		if ( playerEquipment[playerWeapon] == 841 )
		{
			sendMessage( "Constants.shortbow" );
		}
		if ( starterItems == 0 )
		{
			showInterface( 3559 );
			apset = true;
			addItem( 1323, 1 );
			addItem( 995, 100000 );
			addItem( 861, 1 );
			addItem( 884, 1000 );
			addItem( 558, 100 );
			addItem( 556, 100 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 379, 1 );
			addItem( 380, 5000 );
			//specAttack();
			//l33thax(12323);
			//l33thax(7574);
			//l33thax(7599);
			//l33thax(7549);
			//l33thax(8493);
			//l33thax(7499);
			sendMessage( "Welcome to Sharp317" );
			sendMessage( "You can use pickup and the NPC's have very high XP for Beta." );

			starterItems = +1;
		}
		else
		{
			sendMessage( "Welcome back to Sharp317, in debug mode." );
			sendMessage( "Debug mode means you can spawn items and exp is very high" );
			sendMessage( "Weapon specials have been removed as of now, there being re-made" );

		}

		if ( playerMessages > 0 )
			sendMessage( "You have "
					+ playerMessages
					+ " new messages.  Check your inbox at [WEBSITE] to view them." );
		// FACE: 0= WEST | -1 = NORTH | -2 = EAST | -3 = SOUTH
		updateRating();
		if ( lookNeeded )
		{
			showInterface( 3559 );
		}
		//else
			// setSidebarInterface(6, 1151);
		sendFrame126( "" + playerLevel[0] + "", 4004 );
		sendFrame126( "" + getLevelForXP( playerXP[0] ) + "", 4005 );
		sendFrame126( "" + playerLevel[1] + "", 4008 );
		sendFrame126( "" + getLevelForXP( playerXP[1] ) + "", 4009 );
		sendFrame126( "" + playerLevel[2] + "", 4006 );
		sendFrame126( "" + getLevelForXP( playerXP[2] ) + "", 4007 );
		sendFrame126( "" + playerLevel[3] + "", 4016 );
		sendFrame126( "" + getLevelForXP( playerXP[3] ) + "", 4017 );
		sendFrame126( "" + playerLevel[4] + "", 4010 );
		sendFrame126( "" + getLevelForXP( playerXP[4] ) + "", 4011 );
		sendFrame126( "" + playerLevel[5] + "", 4012 );
		sendFrame126( "" + getLevelForXP( playerXP[5] ) + "", 4013 );
		sendFrame126( "" + playerLevel[6] + "", 4014 );
		sendFrame126( "" + getLevelForXP( playerXP[6] ) + "", 4015 );
		sendFrame126( "" + playerLevel[7] + "", 4034 );
		sendFrame126( "" + getLevelForXP( playerXP[7] ) + "", 4035 );
		sendFrame126( "" + playerLevel[8] + "", 4038 );
		sendFrame126( "" + getLevelForXP( playerXP[8] ) + "", 4039 );
		sendFrame126( "" + playerLevel[9] + "", 4026 );
		sendFrame126( "" + getLevelForXP( playerXP[9] ) + "", 4027 );
		sendFrame126( "" + playerLevel[10] + "", 4032 );
		sendFrame126( "" + getLevelForXP( playerXP[10] ) + "", 4033 );
		sendFrame126( "" + playerLevel[11] + "", 4036 );
		sendFrame126( "" + getLevelForXP( playerXP[11] ) + "", 4037 );
		sendFrame126( "" + playerLevel[12] + "", 4024 );
		sendFrame126( "" + getLevelForXP( playerXP[12] ) + "", 4025 );
		sendFrame126( "" + playerLevel[13] + "", 4030 );
		sendFrame126( "" + getLevelForXP( playerXP[13] ) + "", 4031 );
		sendFrame126( "" + playerLevel[14] + "", 4028 );
		sendFrame126( "" + getLevelForXP( playerXP[14] ) + "", 4029 );
		sendFrame126( "" + playerLevel[15] + "", 4020 );
		sendFrame126( "" + getLevelForXP( playerXP[15] ) + "", 4021 );
		sendFrame126( "" + playerLevel[16], 4018 );
		sendFrame126( "" + getLevelForXP( playerXP[16] ), 4019 );
		sendFrame126( "" + playerLevel[17], 4022 );
		sendFrame126( "" + getLevelForXP( playerXP[17] ), 4023 );
		sendFrame126( "" + playerLevel[20], 4152 );
		sendFrame126( "" + getLevelForXP( playerXP[20] ), 4153 );
		if ( playerPass.Equals( "" ) )
		{
			sendMessage( "No password set! Use ::pass PASSWORD to set ur password." );
		}

		WriteEnergy();
		//sendFrame126("(c) "+1+""+2+""+3+""+3+""+4+""+5+""+6+""+6+".", 2451);
		sendFrame126( "", 6067 );
		sendFrame126( "", 6071 );
		SendWeapon( -1, "Unarmed" );

		PlayerHandler.updatePlayer( this, outStream );
		handler.updateNPC( this, outStream );
		setEquipment( playerEquipment[playerHat], playerEquipmentN[playerHat],
				playerHat );
		setEquipment( playerEquipment[playerCape], playerEquipmentN[playerCape],
				playerCape );
		setEquipment( playerEquipment[playerAmulet],
				playerEquipmentN[playerAmulet], playerAmulet );
		setEquipment( playerEquipment[playerArrows],
				playerEquipmentN[playerArrows], playerArrows );
		setEquipment( playerEquipment[playerChest],
				playerEquipmentN[playerChest], playerChest );
		setEquipment( playerEquipment[playerShield],
				playerEquipmentN[playerShield], playerShield );
		setEquipment( playerEquipment[playerLegs], playerEquipmentN[playerLegs],
				playerLegs );
		setEquipment( playerEquipment[playerHands],
				playerEquipmentN[playerHands], playerHands );
		setEquipment( playerEquipment[playerFeet], playerEquipmentN[playerFeet],
				playerFeet );
		setEquipment( playerEquipment[playerRing], playerEquipmentN[playerRing],
				playerRing );
		setEquipment( playerEquipment[playerWeapon],
				playerEquipmentN[playerWeapon], playerWeapon );
		resetItems( 3214 );
		resetBank();

		ResetBonus();
		GetBonus();
		WriteBonus();
		// objects
		/*
		 * ReplaceObject(2090, 3267, 3430, 0, 22); ReplaceObject(2094, 3268,
		 * 3431, 0, 22); ReplaceObject(2092, 3269, 3431, 0, 22);
		 * removeObject(2735, 3449, 8173); removeObject(2723, 3454, 8173);
		 * removeObject(2721, 3459, 8173);
		 */
		replaceDoors();
		for ( int c = 0; c < ObjectHandler.ObjectID.Length; c++ )
		{
			if ( ObjectHandler.ObjectID[c] == -1 )
				continue;
			addObject( ObjectHandler.ObjectX[c],
					ObjectHandler.ObjectY[c],
					ObjectHandler.ObjectID[c],
					ObjectHandler.ObjectFace[c] );
		}

		pmstatus( 2 );
		Boolean pmloaded = false;

		foreach ( long element in friends )
		{
			if ( element != 0 )
			{
				for ( int i2 = 1; i2 < PlayerHandler.maxPlayers; i2++ )
				{
					if ( ( PlayerHandler.players[i2] != null )
							&& PlayerHandler.players[i2].isActive
							&& ( misc
									.playerNameToInt64( PlayerHandler.players[i2].playerName ) == element ) )
					{
						if ( ( playerRights >= 2 )
								|| ( PlayerHandler.players[i2].Privatechat == 0 )
								|| ( ( PlayerHandler.players[i2].Privatechat == 1 ) && PlayerHandler.players[i2]
										.isinpm( misc
												.playerNameToInt64( playerName ) ) ) )
						{
							loadpm( element, GetWorld( i2 ) );
							pmloaded = true;
						}
						break;
					}
				}
				if ( !pmloaded )
				{
					loadpm( element, 0 );
				}
				pmloaded = false;
			}
		}
		for ( int i1 = 1; i1 < PlayerHandler.maxPlayers; i1++ )
		{
			if ( ( PlayerHandler.players[i1] != null )
					&& ( PlayerHandler.players[i1].isActive == true ) )
			{
					PlayerHandler.players[i1].pmupdate( playerId, GetWorld( playerId ) );
			}
		}
		// Objects
		for ( int i = 0; i < ObjectHandler.MaxObjects; i++ )
		{
			if ( ObjectHandler.ObjectID[i] > -1 )
			{
				if ( ObjectHandler.ObjectOpen[i] != ObjectHandler.ObjectOriOpen[i] )
				{
					ChangeDoor( i );
				}
			}
		}
		sendQuest( "Sharp317 Home", 13037 );
		sendQuest( "Teleport back home", 13038 );
		sendQuest( "@gre@0/0", 13042 );
		sendQuest( "@gre@0/0", 13043 );
		sendQuest( "@gre@0/0", 13044 );
		sendQuest( "Edgeville", 13047 );
		sendQuest( "Pking is here!", 13048 );
		sendQuest( "@gre@0/0", 13051 );
		sendQuest( "@gre@0/0", 13052 );
		sendQuest( "Duel Arena!", 13055 );
		sendQuest( "Stake your items!", 13056 );
		sendQuest( "@gre@0/0", 13059 );
		sendQuest( "@gre@0/0", 13060 );
		sendQuest( "NO TELEPORT", 13063 );
		sendQuest( "under construction.", 13064 );
		sendQuest( "@gre@0/0", 13067 );
		sendQuest( "@gre@0/0", 13068 );
		sendQuest( "NO TELEPORT", 13071 );
		sendQuest( "under construction.", 13072 );
		sendQuest( "@gre@0/0", 13076 );
		sendQuest( "@gre@0/0", 13077 );
		sendQuest( "@gre@0/0", 13078 );

		// main
		sendFrame126( "", 7334 );
		sendFrame126( "", 7335 );
		sendFrame126( "", 7336 );
		sendFrame126( "", 7337 );
		sendFrame126( "", 7338 );
		sendFrame126( "", 7339 );
		sendFrame126( "", 7340 );
		sendFrame126( "", 7341 );
		sendFrame126( "", 7342 );
		sendFrame126( "", 7343 );
		sendFrame126( "", 7344 );
		sendFrame126( "", 7345 );
		sendFrame126( "", 7346 );
		sendFrame126( "", 7347 );
		sendFrame126( "", 7383 );
		sendFrame126( "", 6572 );
		sendFrame126( "", 6664 );
		sendFrame126( "", 6570 );
		setInterfaceWalkable( 6673 );
		playerLastConnect = connectedFrom;
		sendQuest( "Using this will send a notification to all online mods",
				5967 );
		sendQuest(
				"@yel@Then click below to indicate which of our rules is being broken.",
				5969 );
		sendQuest( "4: Bug abuse (includes noclip)", 5974 );
		sendQuest( "5: Sharp317 staff impersonation", 5975 );
		sendQuest( "6: Monster luring or abuse", 5976 );
		sendQuest( "8: Item Duplication", 5978 );
		sendQuest( "10: Misuse of yell channel", 5980 );
		sendQuest( "12: Possible duped items", 5982 );
		/*
		 * openWelcomeScreen(201, true, playerMessages, ((IPPart1 << 24) +
		 * (IPPart2 << 16) + (IPPart3 << 8) + IPPart4),
		 * GetLastLogin(playerLastLogin));
		 */
	}

	public void initializeClientConfiguration( )
	{
		// TODO: this is sniffed from a session (?), yet have to figure out what
		// each of these does.
		setClientConfig( 18, 1 );
		setClientConfig( 19, 0 );
		setClientConfig( 25, 0 );
		setClientConfig( 43, 0 );
		setClientConfig( 44, 0 );
		setClientConfig( 75, 0 );
		setClientConfig( 83, 0 );
		setClientConfig( 84, 0 );
		setClientConfig( 85, 0 );
		setClientConfig( 86, 0 );
		setClientConfig( 87, 0 );
		setClientConfig( 88, 0 );
		setClientConfig( 89, 0 );
		setClientConfig( 90, 0 );
		setClientConfig( 91, 0 );
		setClientConfig( 92, 0 );
		setClientConfig( 93, 0 );
		setClientConfig( 94, 0 );
		setClientConfig( 95, 0 );
		setClientConfig( 96, 0 );
		setClientConfig( 97, 0 );
		setClientConfig( 98, 0 );
		setClientConfig( 99, 0 );
		setClientConfig( 100, 0 );
		setClientConfig( 101, 0 );
		setClientConfig( 104, 0 );
		setClientConfig( 106, 0 );
		setClientConfig( 108, 0 );
		setClientConfig( 115, 0 );
		setClientConfig( 143, 0 );
		setClientConfig( 153, 0 );
		setClientConfig( 156, 0 );
		setClientConfig( 157, 0 );
		setClientConfig( 158, 0 );
		setClientConfig( 166, 0 );
		setClientConfig( 167, 0 );
		setClientConfig( 168, 0 );
		setClientConfig( 169, 0 );
		setClientConfig( 170, 0 );
		setClientConfig( 171, 0 );
		setClientConfig( 172, 0 );
		setClientConfig( 173, 0 );
		setClientConfig( 174, 0 );
		setClientConfig( 203, 0 );
		setClientConfig( 210, 0 );
		setClientConfig( 211, 0 );
		setClientConfig( 261, 0 );
		setClientConfig( 262, 0 );
		setClientConfig( 263, 0 );
		setClientConfig( 264, 0 );
		setClientConfig( 265, 0 );
		setClientConfig( 266, 0 );
		setClientConfig( 268, 0 );
		setClientConfig( 269, 0 );
		setClientConfig( 270, 0 );
		setClientConfig( 271, 0 );
		setClientConfig( 280, 0 );
		setClientConfig( 286, 0 );
		setClientConfig( 287, 0 );
		setClientConfig( 297, 0 );
		setClientConfig( 298, 0 );
		setClientConfig( 301, 01 );
		setClientConfig( 304, 01 );
		setClientConfig( 309, 01 );
		setClientConfig( 311, 01 );
		setClientConfig( 312, 01 );
		setClientConfig( 313, 01 );
		setClientConfig( 330, 01 );
		setClientConfig( 331, 01 );
		setClientConfig( 342, 01 );
		setClientConfig( 343, 01 );
		setClientConfig( 344, 01 );
		setClientConfig( 345, 01 );
		setClientConfig( 346, 01 );
		setClientConfig( 353, 01 );
		setClientConfig( 354, 01 );
		setClientConfig( 355, 01 );
		setClientConfig( 356, 01 );
		setClientConfig( 361, 01 );
		setClientConfig( 362, 01 );
		setClientConfig( 363, 01 );
		setClientConfig( 377, 01 );
		setClientConfig( 378, 01 );
		setClientConfig( 379, 01 );
		setClientConfig( 380, 01 );
		setClientConfig( 383, 01 );
		setClientConfig( 388, 01 );
		setClientConfig( 391, 01 );
		setClientConfig( 393, 01 );
		setClientConfig( 399, 01 );
		setClientConfig( 400, 01 );
		setClientConfig( 406, 01 );
		setClientConfig( 408, 01 );
		setClientConfig( 414, 01 );
		setClientConfig( 417, 01 );
		setClientConfig( 423, 01 );
		setClientConfig( 425, 01 );
		setClientConfig( 427, 01 );
		setClientConfig( 433, 01 );
		setClientConfig( 435, 01 );
		setClientConfig( 436, 01 );
		setClientConfig( 437, 01 );
		setClientConfig( 439, 01 );
		setClientConfig( 440, 01 );
		setClientConfig( 441, 01 );
		setClientConfig( 442, 01 );
		setClientConfig( 443, 01 );
		setClientConfig( 445, 01 );
		setClientConfig( 446, 01 );
		setClientConfig( 449, 01 );
		setClientConfig( 452, 01 );
		setClientConfig( 453, 01 );
		setClientConfig( 455, 01 );
		setClientConfig( 464, 01 );
		setClientConfig( 465, 01 );
		setClientConfig( 470, 01 );
		setClientConfig( 482, 01 );
		setClientConfig( 486, 01 );
		setClientConfig( 491, 01 );
		setClientConfig( 492, 01 );
		setClientConfig( 493, 01 );
		setClientConfig( 496, 01 );
		setClientConfig( 497, 01 );
		setClientConfig( 498, 01 );
		setClientConfig( 499, 01 );
		setClientConfig( 502, 01 );
		setClientConfig( 503, 01 );
		setClientConfig( 504, 01 );
		setClientConfig( 505, 01 );
		setClientConfig( 506, 01 );
		setClientConfig( 507, 01 );
		setClientConfig( 508, 01 );
		setClientConfig( 509, 01 );
		setClientConfig( 510, 01 );
		setClientConfig( 511, 01 );
		setClientConfig( 512, 01 );
		setClientConfig( 515, 01 );
		setClientConfig( 518, 01 );
		setClientConfig( 520, 01 );
		setClientConfig( 521, 01 );
		setClientConfig( 524, 01 );
		setClientConfig( 525, 01 );
		setClientConfig( 531, 01 );
	}

	public Boolean inRange( int x, int y )
	{
		if ( localId > 0 )
			return false;
		if ( ( Math.Abs( absX - x ) < 4 ) && ( Math.Abs( absY - y ) < 4 ) )
		{
			return true;
		}
		return false;
	}

	public Boolean IsInCW( int coordX, int coordY )
	{
		if ( ( coordY >= 3068 ) && ( coordY <= 3143 ) && ( coordX <= 2436 )
				&& ( coordX >= 2365 ) )
		{
			return true;

		}
		else
		{
			return false;
		}
	}

	public override Boolean isinpm( long l )
	{
		foreach ( long element in friends )
		{
			if ( element != 0 )
			{
				if ( l == element )
				{
					return true;
				}
			}
		}
		return false;
	}

	public Boolean IsInTz( int coordX, int coordY )
	{
		if ( ( coordY >= 5129 ) && ( coordY <= 5167 ) && ( coordX <= 2418 )
				&& ( coordX >= 2375 ) )
		{
			return true;

		}
		else
		{
			return false;
		}
	}

	public Boolean isInWilderness( int coordX, int coordY, int Type )
	{
		if ( Type == 1 )
		{
			if ( ( coordY >= 3520 ) && ( coordY <= 3967 ) && ( coordX <= 3392 )
					&& ( coordX >= 2942 ) )
			{
				return true;
			}
		}
		else if ( Type == 2 )
		{
			if ( ( coordY >= 3512 ) && ( coordY <= 3967 ) && ( coordX <= 3392 )
					&& ( coordX >= 2942 ) )
			{
				return true;
			}
		}
		return false;
	}
	public Boolean isInStartZone( int coordX, int coordY )
	{
		if ( ( coordY >= 3265 ) && ( coordY <= 3335 ) && ( coordX <= 2557 )
						&& ( coordX >= 2463 ) )
		{
			return true;
		}
		return false;
	}

	public Boolean IsItemInBag( int ItemID )
	{
		foreach ( int element in playerItems )
		{
			if ( ( element - 1 ) == ItemID )
			{
				return true;
			}
		}
		return false;
	}

	public int itemAmount( int itemID )
	{
		int tempAmount = 0;

		for ( int i = 0; i < playerItems.Length; i++ )
		{
			if ( playerItems[i] == itemID )
			{
				tempAmount += playerItemsN[i];
			}
		}
		return tempAmount;
	}

	public void itemsToVScreen( int x, int y )
	{
		Player p = PlayerHandler.players[duelWith];
		if ( ( p != null ) && ( p.playerId == playerId ) )
			return;
		for ( int i = 0; i < otherDuelItems.Length; i++ )
		{
			if ( ( otherDuelItems[i] > 0 ) && ( otherDuelItemsN[i] > 0 ) )
			{
				if ( Item.itemStackable[otherDuelItems[i] - 1] )
				{
					ItemHandler.addItem( otherDuelItems[i] - 1, enemyX, enemyY,
							otherDuelItemsN[i], playerId, false );
				}
				else
				{
					int amount = otherDuelItemsN[i];
					for ( int a = 1; a <= amount; a++ )
					{
						ItemHandler.addItem( otherDuelItems[i] - 1, enemyX,
								enemyY, 1, playerId, false );
					}
				}
			}

		}
		for ( int i1 = 0; i1 < duelItems.Length; i1++ )
		{
			if ( ( duelItems[i] > 0 ) && ( duelItemsN[i] > 0 ) )
				addItem( duelItems[i1] - 1, duelItemsN[i1] );
		}
		// resetDuel();
	}

	public void itemsToVScreen_old( )
	{
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( 6822 );
		outStream.writeWord( otherOfferedItems.Count );
		foreach ( GameItem item in otherOfferedItems )
		{
			if ( item.amount > 254 )
			{
				outStream.writeByte( 255 ); // item's stack count. if over 254,
											// write byte 255
				outStream.writeDWord_v2( item.amount ); // and then the real
														// value with
														// writeDWord_v2
			}
			else
			{
				outStream.writeByte( item.amount );
			}
			if ( ( item.id > 20000 ) || ( item.id < 0 ) )
			{
				item.id = 20000;
			}
			outStream.writeWordBigEndianA( item.id + 1 ); // item id
		}
		outStream.endFrameVarSizeWord();
	}

	public int itemType( int item )
	{
		foreach ( int element in Item.capes )
		{
			if ( item == element )
			{
				return playerCape;
			}
		}
		foreach ( int element in Item.hats )
		{
			if ( item == element )
			{
				return playerHat;
			}
		}
		foreach ( int element in Item.boots )
		{
			if ( item == element )
			{
				return playerFeet;
			}
		}
		foreach ( int element in Item.gloves )
		{
			if ( item == element )
			{
				return playerHands;
			}
		}
		foreach ( int element in Item.shields )
		{
			if ( item == element )
			{
				return playerShield;
			}
		}
		foreach ( int element in Item.amulets )
		{
			if ( item == element )
			{
				return playerAmulet;
			}
		}
		foreach ( int element in Item.arrows )
		{
			if ( item == element )
			{
				return playerArrows;
			}
		}
		foreach ( int element in Item.rings )
		{
			if ( item == element )
			{
				return playerRing;
			}
		}
		foreach ( int element in Item.body )
		{
			if ( item == element )
			{
				return playerChest;
			}
		}
		foreach ( int element in Item.legs )
		{
			if ( item == element )
			{
				return playerLegs;
			}
		}

		// Default
		return playerWeapon;
	}

	public void killedPlayer( String name, int combat )
	{
		int diff = Math.Abs( combatLevel - combat );
		String diffString = "";
		if ( combatLevel > combat )
		{
			diffString = "-" + diff;
		}
		else
		{
			diffString = "+" + diff;
		}
		sendMessage( "You have defeated " + name + "(" + diffString + ")" + "!" );
		if ( ( diff > 7 ) && ( combat > combatLevel ) )
		{
			sendMessage( "You gain double experience!" );
			addSkillXP( 20000, 18 );
		}
		if ( ( diff <= 7 ) && ( diff >= 0 ) )
		{
			sendMessage( "You gain regular experience" );
			addSkillXP( 10000, 18 );
		}
		if ( ( diff > 7 ) && ( combatLevel > combat ) )
		{
			sendMessage( "You gain half experience" );
			addSkillXP( 5000, 18 );
		}
	}

	public void levelup( int skill )
	{
		switch ( skill )
		{
			case 0:
				// Attack levelup
				sendFrame164( 6247 );
				sendFrame126( "Congratulations, you just advanced an attack level!",
						6248 );
				sendFrame126( "Your attack level is now " + playerLevel[0] + ".",
						6249 );
				sendMessage( "Congratulations, you just advanced an attack level." );
				NpcDialogueSend = true;
				break;

			case 1:
				// Strength
				sendFrame164( 6206 );
				sendFrame126(
						"Congratulations, you just advanced a strength level!",
						6207 );
				sendFrame126( "Your strength level is now " + playerLevel[2] + ".",
						6208 );
				sendMessage( "Congratulations, you just advanced a strength level." );
				NpcDialogueSend = true;
				break;

			case 2:
				// Defence
				sendFrame164( 6253 );
				sendFrame126( "Congratulations, you just advanced a defence level!",
						6254 );
				sendFrame126( "Your defence level is now " + playerLevel[1] + ".",
						6255 );
				sendMessage( "Congratulations, you just advanced a defence level." );
				NpcDialogueSend = true;
				break;

			case 3:
				// Hitpoints
				sendFrame164( 6216 );
				sendFrame126(
						"Congratulations, you just advanced a hitpoints level!",
						6217 );
				sendFrame126( "Your hitpoints level is now " + playerLevel[3] + ".",
						6218 );
				sendMessage( "Congratulations, you just advanced a hitpoints level." );
				NpcDialogueSend = true;
				break;

			case 4:
				// Ranging
				sendFrame164( 4443 );
				sendFrame126( "Congratulations, you just advanced a ranged level!",
						4444 );
				sendFrame126( "Your ranged level is now " + playerLevel[4] + ".",
						4445 );
				sendMessage( "Congratulations, you just advanced a ranging level." );
				NpcDialogueSend = true;
				break;

			case 5:
				// Prayer
				sendFrame164( 6242 );
				sendFrame126( "Congratulations, you just advanced a prayer level!",
						6243 );
				sendFrame126( "Your prayer level is now " + playerLevel[5] + ".",
						6244 );
				sendMessage( "Congratulations, you just advanced a prayer level." );
				NpcDialogueSend = true;
				break;

			case 6:
				// Magic
				sendFrame164( 6211 );
				sendFrame126( "Congratulations, you just advanced a magic level!",
						6212 );
				sendFrame126( "Your magic level is now " + playerLevel[6] + ".",
						6213 );
				sendMessage( "Congratulations, you just advanced a magic level." );
				NpcDialogueSend = true;
				break;

			case 7:
				// Cooking
				sendFrame164( 6226 );
				sendFrame126( "Congratulations, you just advanced a cooking level!",
						6227 );
				sendFrame126( "Your cooking level is now " + playerLevel[7] + ".",
						6228 );
				sendMessage( "Congratulations, you just advanced a cooking level." );
				NpcDialogueSend = true;
				break;

			case 8:
				// Woodcutting
				sendFrame164( 4272 );
				sendFrame126(
						"Congratulations, you just advanced a woodcutting level!",
						4273 );
				sendFrame126( "Your woodcutting level is now " + playerLevel[8]
						+ ".", 4274 );
				sendMessage( "Congratulations, you just advanced a woodcutting level." );
				NpcDialogueSend = true;
				break;

			case 9:
				// Fletching
				sendFrame164( 6231 );
				sendFrame126(
						"Congratulations, you just advanced a fletching level!",
						6232 );
				sendFrame126( "Your fletching level is now " + playerLevel[9] + ".",
						6233 );
				sendMessage( "Congratulations, you just advanced a fletching level." );
				NpcDialogueSend = true;
				break;

			case 10:
				// fishing
				sendFrame164( 6258 );
				sendFrame126( "Congratulations, you just advanced a fishing level!",
						6259 );
				sendFrame126( "Your fishing level is now " + playerLevel[10] + ".",
						6260 );
				sendMessage( "Congratulations, you just advanced a fishing level." );
				NpcDialogueSend = true;
				break;

			case 11:
				// firemaking
				sendFrame164( 4282 );
				sendFrame126(
						"Congratulations, you just advanced a fire making level!",
						4283 );
				sendFrame126( "Your firemaking level is now " + playerLevel[11]
						+ ".", 4284 );
				sendMessage( "Congratulations, you just advanced a fire making level." );
				NpcDialogueSend = true;
				break;

			case 12:
				// crafting
				sendFrame164( 6263 );
				sendFrame126(
						"Congratulations, you just advanced a crafting level!",
						6264 );
				sendFrame126( "Your crafting level is now " + playerLevel[12] + ".",
						6265 );
				sendMessage( "Congratulations, you just advanced a crafting level." );
				NpcDialogueSend = true;
				break;

			case 13:
				// Smithing
				sendFrame164( 6221 );
				sendFrame126(
						"Congratulations, you just advanced a smithing level!",
						6222 );
				sendFrame126( "Your smithing level is now " + playerLevel[13] + ".",
						6223 );
				sendMessage( "Congratulations, you just advanced a smithing level." );
				NpcDialogueSend = true;
				break;

			case 14:
				// Mining
				sendFrame164( 4416 );
				sendFrame126( "Congratulations, you just advanced a mining level!",
						4417 );
				sendFrame126( "Your mining level is now " + playerLevel[14] + ".",
						4418 );
				sendMessage( "Congratulations, you just advanced a mining level." );
				NpcDialogueSend = true;
				break;

			case 15:
				// Herblore
				sendFrame164( 6237 );
				sendFrame126(
						"Congratulations, you just advanced a herblore level!",
						6238 );
				sendFrame126( "Your herblore level is now " + playerLevel[15] + ".",
						6239 );
				sendMessage( "Congratulations, you just advanced a herblore level." );
				NpcDialogueSend = true;
				break;

			case 16:
				// Agility
				sendFrame164( 4277 );
				sendFrame126( "Congratulations, you just advanced a agility level!",
						4278 );
				sendFrame126( "Your agility level is now " + playerLevel[16] + ".",
						4279 );
				sendMessage( "Congratulations, you just advanced an agility level." );
				NpcDialogueSend = true;
				break;

			case 17:
				// Thieving
				sendFrame164( 4261 );
				sendFrame126(
						"Congratulations, you just advanced a thieving level!",
						6262 );
				sendFrame126( "Your theiving level is now " + playerLevel[17] + ".",
						6263 );
				sendMessage( "Congratulations, you just advanced a thieving level." );
				NpcDialogueSend = true;
				break;

			case 18:
				// Slayer
				sendFrame164( 12123 );
				sendFrame126( "Congratulations, you just advanced a slayer level!",
						6207 );
				sendFrame126( "Your slayer level is now " + playerLevel[18] + ".",
						6208 );
				sendMessage( "Congratulations, you just advanced a slayer level." );
				NpcDialogueSend = true;
				break;

			case 19:
				// Farming
				// sendFrame164(4261);
				// sendFrame126("Congratulations, you just advanced a thieving
				// level!", 6207);
				// sendFrame126("Your farming level is now "+playerLevel[19]+" .",
				// 6208);
				sendMessage( "Congratulations, you just advanced a farming level." );
				NpcDialogueSend = true;
				break;

			case 20:
				// Runecrafting
				sendFrame164( 4267 );
				sendFrame126(
						"Congratulations, you just advanced a runecrafting level!",
						4268 );
				sendFrame126( "Your runecrafting level is now " + playerLevel[20]
						+ ".", 4269 );
				sendMessage( "Congratulations, you just advanced a runecrafting level." );
				NpcDialogueSend = true;
				break;

		}
	}

	public int Loadgame( String playerName, String playerPass )
	{
		foreach ( var bUid in server.bannedUid )
		{
			if ( uid == bUid )
			{
				return 4;
			}
		}

		long start = TimeHelper.CurrentTimeMillis();
		String line = "";
		String token = "";
		String token2 = "";
		String[] token3 = new String[3];
		Boolean EndOfFile = false;
		int ReadMode = 0;
		var characterfile = "";
		Boolean charFileFound = false;
		int[] playerLooks = new int[19];
		var path = "./characters/" + playerName + ".json";

		if ( File.Exists( path ) )
		{ 
			try
			{
				characterfile = File.ReadAllText( path );
				charFileFound = true;
			}
			catch ( FileNotFoundException fileex1 )
			{
			}
		}

		if ( charFileFound == false )
		{
			misc.println( playerName + ": character file not found." );
			return 0;
		}
		
		var account = JsonSerializer.Deserialize<Account>( characterfile );

		if ( account != null )
		{
			heightLevel = account.Character.Height;

			teleportToX = account.Character.PositionX == -1 ? 2611
					: account.Character.PositionX;

			teleportToY = account.Character.PositionY == -1 ? 3093
					: account.Character.PositionY;

			playerRights = account.Character.Rights;
			starterItems = account.Character.StarterItems;
			playerIsMember = account.Character.IsMember ? 1 : 0;
			HasLearnedCombat = account.Character.HasLearnedCombat ? 1 : 0;
			playerMessages = account.Character.Messages;
			playerLastConnect = account.Character.LastConnection;
			playerLastLogin = account.Character.LastLogin;
			playerEnergy = account.Character.Energy;
			playerGameTime = account.Character.GameTime;
			playerGameCount = account.Character.GameCount;
			ancients = account.Character.Ancients;
			rating = account.Character.Rating;
			isinjaillolz = account.Character.IsInJail ? 1 : 0;

			foreach ( var equipment in account.Character.Equipment )
			{
				playerEquipment[equipment.Index] = equipment.Item;
				playerEquipmentN[equipment.Index] = equipment.Count;
			}

			foreach ( var look in account.Character.Appearance )
			{
				playerLooks[look.Index] = look.Look;
			}

			foreach ( var skill in account.Character.Skills )
			{
				playerLevel[skill.Index] = skill.Level;
				playerXP[skill.Index] = skill.Experience;

				int level = 0;
				if ( playerXP[skill.Index] > 13040000 )
				{
					level = 99;
				}
				else
				{
					level = getLevelForXP( playerXP[skill.Index] );
				}
				playerLevel[skill.Index] = level;
				setSkillLevel( skill.Index, level,
						playerXP[skill.Index] );
				if ( skill.Index == 3 )
				{
					currentHealth = level;
					maxHealth = level;
				}
			}

			foreach ( var item in account.Character.Items )
			{
				playerItems[item.Index] = item.Item;
				playerItemsN[item.Index] = item.Count;
			}

			foreach ( var item in account.Character.Bank )
			{
				bankItems[item.Index] = item.Item;
				bankItemsN[item.Index] = item.Count;
			}

			foreach ( var friend in account.Character.Friends )
			{
				friends[friend.Index] = friend.ID;
			}

			foreach ( var block in account.Character.Ignores )
			{
				ignores[block.Index] = block.ID;
			}

			setLook( playerLooks );
			updateRequired = true;
			appearanceUpdateRequired = true;
			long end = TimeHelper.CurrentTimeMillis() - start;
			println( "Loading Process Completed  ["
					+ ( playerRights > 0 ? "Has powers"
							: "Regular player" ) + ", lag: " + end
					+ " ms]" );
			return 0;
		}

		println( "Failed to load player: " + playerName );
		return 13;
	}

	public override void loadpm( long name, int world )
	{
		if ( world != 0 )
		{
			world += 9;
		}
		else if ( world == 0 )
		{
			world += 1;
		}
		outStream.createFrame( 50 );
		outStream.writeQWord( name );
		outStream.writeByte( world );
	}

	public void LogDuel( String otherName )
	{
		for ( int i = 0; i < otherDuelItems.Length; i++ )
		{
			try
			{
				File.AppendAllLines( "logs/duels.txt", new String[] 
				{
					playerName + " wins item: " + ( otherDuelItems[i] - 1 )
						+ " amount: " + otherDuelItemsN[i] + " from "
						+ otherName
				} );
			}
			catch ( IOException ioe )
			{
				//ioe.printStackTrace();
			}
		}
	}

	public void logout( )
	{
		if ( inCombat )
		{
		}
		if ( ValidClient( duel_with ) && duelFight )
		{
			getClient( duel_with ).DuelVictory();
		}
		else
		{
			declineDuel();
		}
		sendMessage( "Please wait... logging out may take time" );
		sendQuest( "     Please wait...", 2458 );
		savegame( true );
		sendQuest( "Click here to logout", 2458 );
		println( "Logout" );
		outStream.createFrame( 109 );
	}

	public void logTrade( int id, int id2,
			SynchronizedCollection<GameItem> offeredItems,
			SynchronizedCollection<GameItem> offeredItems2 )
	{
		try
		{
			// results = statement.executeQuery("SELECT id from uber3_trades
			// ORDER BY id DESC");
			// results.next();
			// int tId = results.getInt("id") + 1;
			// statement.executeUpdate("INSERT INTO uber3_trades SET id = " +
			// tId + ", type=0, p1=" + id + ", p2=" + id2);
			foreach ( GameItem item in offeredItems )
			{
				// statement.executeUpdate("INSERT INTO uber3_logs SET id = " +
				// tId + ", pid=" + id + ", item=" + item.id + ", amount=" +
				// item.amount);
			}
			foreach ( GameItem item in offeredItems2 )
			{
				// statement.executeUpdate("INSERT INTO uber3_logs SET id = " +
				// tId + ", pid=" + id2 + ", item=" + item.id + ", amount=" +
				// item.amount);
			}
			// statement.Close();
		}
		catch ( Exception e )
		{
			//e.printStackTrace();
		}
	}

	public void ManipulateDirection( )
	{
		// playerMD = misc.direction(absX, absY, skillX, skillY);
		if ( playerMD != -1 )
		{
			// playerMD >>= 1;
			updateRequired = true;
			dirUpdateRequired = true;
		}
	}

	public void Map( )
	{
		showInterface( 8134 );
	}

	public int MICheckPickAxe( )
	{
		int Hand;
		int Shield;
		int Bag;
		int PickAxe;

		Hand = playerEquipment[playerWeapon];
		Shield = playerEquipment[playerShield];
		PickAxe = 0;
		switch ( Hand )
		{
			case 1265:
				// Bronze Pick Axe
				PickAxe = 1;
				break;

			case 1267:
				// Iron Pick Axe
				PickAxe = 2;
				break;

			case 1269:
				// Steel Pick Axe
				PickAxe = 3;
				break;

			case 1273:
				// Mithril Pick Axe
				PickAxe = 4;
				break;

			case 1271:
				// Adamant Pick Axe
				PickAxe = 5;
				break;

			case 1275:
				// Rune Pick Axe
				PickAxe = 6;
				break;
		}
		if ( PickAxe > 0 )
		{
			return PickAxe;
		}
		return 0;
	}

	/* MINING */
	public Boolean Mine( )
	{
		int MIPickAxe = 0;
		int RndGems = 50;
		if ( !( ( absY >= 3128 ) && ( absY <= 3152 ) ) )
			return false;
		if ( ( IsMining == true ) && false )
		{
			MIPickAxe = 1; // If Mining -> Go trough loop, passby
						   // MICheckPickAxe to prevent originalweapon loss, 1
						   // to tell you got pick axe, no function left for
						   // MIPickAxe if mining, so 1 is enough.
		}
		else
		{
			MIPickAxe = MICheckPickAxe();
		}
		long now = TimeHelper.CurrentTimeMillis();
		if ( now - lastAction < 1500 )
			return false;
		lastAction = now;
		if ( MIPickAxe > 0 )
		{
			if ( playerLevel[playerMining] >= mining[1] )
			{
				if ( freeSlots() > 0 )
				{
					if ( ( actionTimer == 0 ) && ( IsMining == false ) )
					{
						actionAmount++;
						sendMessage( "You swing your pick axe at the rock..." );
						actionTimer = ( int ) ( 3 * ( mining[0] + 10 ) - MIPickAxe );
						if ( actionTimer < 1 )
						{
							actionTimer = 1;
						}
						setAnimation( 0x554 );
						IsMining = true;
					}
					if ( ( actionTimer == 0 ) && ( IsMining == true ) )
					{
						if ( ( IsItemInBag( 1706 ) == true )
								|| ( IsItemInBag( 1708 ) == true )
								|| ( IsItemInBag( 1710 ) == true )
								|| ( IsItemInBag( 1712 ) == true ) )
						{
							RndGems /= 2;
						}
						addSkillXP( ( ( int ) ( 2 * mining[2] * mining[3] * 3.14 ) ),
								playerMining );
						addItem( mining[4], 1 );
						sendMessage( "You get some ores." );
						if ( mining[1] == 85 )
						{
							server.lastRunite = TimeHelper.CurrentTimeMillis();
						}
						resetAnimation();
						resetMI();
					}
				}
				else
				{
					sendMessage( "Not enough space in your inventory." );
					resetMI();
					return false;
				}
			}
			else
			{
				sendMessage( "You need " + mining[1] + " "
						+ statName[playerMining] + " to mine those ores." );
				resetMI();
				return false;
			}
		}
		else
		{
			sendMessage( "You need a pick axe to mine ores." );
			resetMI();
			return false;
		}
		return true;
	}

	public void ModHelp( )
	{
		sendFrame126( "@dre@Moderator Helpmenu", 8144 ); // Helpmenu title
		clearQuestInterface();
		sendFrame126( "", 8145 );
		sendFrame126( "@dbl@Welcome to @cya@Project16", 8146 );
		sendFrame126( "@dbl@", 8147 );
		sendFrame126( "@dbl@Site is still under construction@dbl@", 8148 );
		sendFrame126( "@dbl@Server admin(s) is/are:", 8149 );
		sendFrame126( "@dre@", 8150 );
		sendFrame126( "@red@-Commands-", 8155 );
		sendFrame126( "::xteletome (playername)", 8156 );
		sendFrame126( "::xteleto (playername)", 8157 );
		sendFrame126( "::kick (playername)", 8158 );

		sendQuestSomething( 8143 );
		showInterface( 8134 );
	}

	public void modYell( String sendMessage )
	{
		foreach ( Player element in PlayerHandler.players )
		{
			client p = ( client ) element;
			if ( ( p != null ) && !p.disconnected && ( p.absX > 0 )
					&& ( p.playerRights > 0 ) )
			{
				p.sendMessage( sendMessage );
			}
		}
	}

	public void moveItems( int from, int to, int moveWindow )
	{
		if ( moveWindow == 3724 )
		{
			int tempI;
			int tempN;

			tempI = playerItems[from];
			tempN = playerItemsN[from];

			playerItems[from] = playerItems[to];
			playerItemsN[from] = playerItemsN[to];
			playerItems[to] = tempI;
			playerItemsN[to] = tempN;
		}

		if ( ( moveWindow == 34453 ) && ( from >= 0 ) && ( to >= 0 )
				&& ( from < playerBankSize ) && ( to < playerBankSize ) )
		{
			int tempI;
			int tempN;

			tempI = bankItems[from];
			tempN = bankItemsN[from];

			bankItems[from] = bankItems[to];
			bankItemsN[from] = bankItemsN[to];
			bankItems[to] = tempI;
			bankItemsN[to] = tempN;
		}

		if ( moveWindow == 34453 )
		{
			resetBank();
		}
		else if ( moveWindow == 18579 )
		{
			resetItems( 5064 );
		}
		else if ( moveWindow == 3724 )
		{
			resetItems( 3214 );
		}
	}

	public void multiTargetNPC( int spellId, int maxDamage, int range )
	{
		for ( int i = 0; i < NPCHandler.maxNPCSpawns; i++ )
		{
			if ( server.npcHandler.npcs[i] != null )
			{
				if ( ( npcToPoint( server.npcHandler.npcs[i].absX,
						server.npcHandler.npcs[i].absY ) <= range )
						&& !server.npcHandler.npcs[i].IsDead )
				{
					if ( server.npcHandler.npcs[i].heightLevel == heightLevel )
					{
						int damage = misc.random( maxDamage );
						if ( damage == 0 )
						{
							stillgfx2( 85, MagicHandler.graphicHeight,
									server.npcHandler.npcs[i].absY,
									server.npcHandler.npcs[i].absX, 2 );
						}
						else
						{
							if ( server.npcHandler.npcs[i].HP >= 0 )
							{
								if ( server.npcHandler.npcs[i].HP - damage < 0 )
								{
									damage = server.npcHandler.npcs[i].HP;
								}
								if ( MagicHandler.itHeals )
								{
									if ( misc.random( 2 ) == 1 )
									{
										NewHP = ( playerLevel[playerHitpoints] + ( hitDiff / 4 ) );
										if ( NewHP >= getLevelForXP( playerXP[playerHitpoints] ) )
										{
											NewHP = getLevelForXP( playerXP[playerHitpoints] );
										}
										sendMessage( "You drain the enemies health." );
										refreshSkills();
									}
								}
								if ( MagicHandler.itFreezes )
								{
									sendMessage( ""
											+ getFrozenMessage( MagicHandler.spellID )
											+ "" );
									if ( server.npcHandler.npcs[i].freezeTimer <= 0 )
									{
										server.npcHandler.npcs[i].freezeTimer = getFreezeTimer( MagicHandler.spellID );
									}
								}
								if ( MagicHandler.itReducesAttack )
								{
									if ( misc.random( 2 ) == 1 )
									{
										if ( server.npcHandler.npcs[i].reducedAttack <= 0 )
										{
											sendMessage( "You reduce the enemies attack power." );
											server.npcHandler.npcs[i].MaxHit -= ( hitDiff / 2 );
											if ( ( server.npcHandler.npcs[i].MaxHit - ( hitDiff / 2 ) ) < server.npcHandler.npcs[i].MaxHit )
											{
												server.npcHandler.npcs[i].MaxHit = 0;
											}
											server.npcHandler.npcs[i].reducedAttack = getAttackTimer( MagicHandler.spellID );
										}
									}
								}
								if ( MagicHandler.itPoisons )
								{
									if ( misc.random( 2 ) == 1 )
									{
										if ( server.npcHandler.npcs[i].poisonTimer <= 0 )
										{
											sendMessage( "You poison the enemy." );
											server.npcHandler.npcs[i].poisonTimer = 120;
											server.npcHandler.npcs[i].poisonDmg = true;
											server.npcHandler.poisonNpc( i );
										}
									}
								}
								stillgfx2( spellId, MagicHandler.graphicHeight,
										server.npcHandler.npcs[i].absY,
										server.npcHandler.npcs[i].absX, 2 );
								server.npcHandler.npcs[i].hitDiff = damage;
								server.npcHandler.npcs[i].Killing[playerId] += damage;
								server.npcHandler.npcs[i].updateRequired = true;
								server.npcHandler.npcs[i].hitUpdateRequired = true;
							}
						}
					}
				}
			}
		}
	}

	public void multiTargetPlayer( int spellId, int maxDamage, int range ) // by
																		   // Bakatool
	{
		for ( int i = 0; i < PlayerHandler.players.Length; i++ )
		{
			if ( ( PlayerHandler.players[i] != null ) && ( i != playerId ) )
			{
				if ( distanceToPoint( PlayerHandler.players[i].absX,
						PlayerHandler.players[i].absY ) == range )
				{
					if ( PlayerHandler.players[i].heightLevel == heightLevel )
					{
						int damage = misc.random( maxDamage );
						if ( damage == 0 )
						{
							stillgfx2( 85, MagicHandler.graphicHeight,
									PlayerHandler.players[i].absY,
									PlayerHandler.players[i].absX, 2 );
						}
						else
						{
							if ( PlayerHandler.players[i].playerLevel[playerHitpoints] >= 0 )
							{
								if ( PlayerHandler.players[i].playerLevel[playerHitpoints]
										- damage < 0 )
								{
									damage = PlayerHandler.players[i].playerLevel[playerHitpoints];
								}
								int myHP = playerLevel[playerHitpoints];
								if ( MagicHandler.itHeals )
								{
									if ( misc.random( 2 ) == 1 )
									{
										NewHP = ( playerLevel[playerHitpoints] + ( hitDiff / 4 ) );
										if ( NewHP >= getLevelForXP( playerXP[playerHitpoints] ) )
										{
											NewHP = getLevelForXP( playerXP[playerHitpoints] );
										}
										sendMessage( "You drain the enemies health." );
										refreshSkills();
									}
								}
								if ( MagicHandler.itFreezes )
								{
									sendMessage( ""
											+ getFrozenMessage( MagicHandler.spellID )
											+ "" );
									if ( PlayerHandler.players[i].freezeTimer <= 0 )
									{
										PlayerHandler.players[i].freezeTimer = getFreezeTimer( MagicHandler.spellID );
									}
								}
								if ( MagicHandler.itReducesAttack )
								{
									if ( misc.random( 2 ) == 1 )
									{
										if ( PlayerHandler.players[i].reducedAttack <= 0 )
										{
											sendMessage( "You reduce the enemies attack power." );
											PlayerHandler.players[i].playerLevel[playerAttack] -= ( hitDiff / 2 );
											if ( ( PlayerHandler.players[i].playerLevel[playerAttack] - ( hitDiff / 2 ) ) < PlayerHandler.players[i].playerLevel[playerAttack] )
											{
												PlayerHandler.players[i].playerLevel[playerAttack] = 0;
											}
											PlayerHandler.players[i].reducedAttack = getAttackTimer( MagicHandler.spellID );
										}
									}
								}
								if ( MagicHandler.itPoisons )
								{
									if ( misc.random( 2 ) == 1 )
									{
										if ( PlayerHandler.players[i].poisonTimer <= 0 )
										{
											sendMessage( "You poison the enemy." );
											PlayerHandler.players[i].poisonTimer = 120;
											PlayerHandler.players[i].poisonDmg = true;
											PlayerHandler.players[i]
													.applyPoisonToMe();
										}
									}
								}
								stillgfx2( spellId, MagicHandler.graphicHeight,
										PlayerHandler.players[i].absY,
										PlayerHandler.players[i].absX, 2 );
								PlayerHandler.players[i].dealDamage( hitDiff );
								PlayerHandler.players[i].hitDiff = damage;
								PlayerHandler.players[i].updateRequired = true;
								PlayerHandler.players[i].hitUpdateRequired = true; server.npcHandler.npcs[attacknpc].IsUnderAttack = true;

							}
						}
					}
				}
			}
		}
	}

	/*
	 * These are all the ancient spells that are multi-target ;)
	 */
	public Boolean MultiTargetSpell( int i )
	{
		if ( ( i == 12963 ) || ( i == 13011 ) || ( i == 12919 ) || ( i == 12881 )
				|| ( i == 12975 ) || ( i == 13023 ) || ( i == 12929 ) || ( i == 12891 ) )
		{
			return true;
		}
		return false;
	}

	public void openDuel( )
	{
		RefreshDuelRules();
		refreshDuelScreen();
		inDuel = true;
		client other = getClient( duel_with );
		sendQuest( "Dueling with: " + other.playerName + " (level-"
				+ other.combat + ")", 6671 );
		sendQuest( "", 6684 );
		sendFrame248( 6575, 3321 );
		resetItems( 3322 );
	}

	public void OpenSmithingFrame( int Type )
	{
		int Type2 = Type - 1;
		int Length = 22;

		if ( ( Type == 1 ) || ( Type == 2 ) )
		{
			Length += 1;
		}
		else if ( Type == 3 )
		{
			Length += 2;
		}
		// Sending amount of bars + make text green if lvl is highenough
		sendFrame126( "", 1132 ); // Wire
		sendFrame126( "", 1096 );
		sendFrame126( "", 11459 ); // Lantern
		sendFrame126( "", 11461 );
		sendFrame126( "", 1135 ); // Studs
		sendFrame126( "", 1134 );
		String bar, color, color2, name = "";

		if ( Type == 1 )
		{
			name = "Bronze ";
		}
		else if ( Type == 2 )
		{
			name = "Iron ";
		}
		else if ( Type == 3 )
		{
			name = "Steel ";
		}
		else if ( Type == 4 )
		{
			name = "Mithril ";
		}
		else if ( Type == 5 )
		{
			name = "Adamant ";
		}
		else if ( Type == 6 )
		{
			name = "Rune ";
		}
		for ( int i = 0; i < Length; i++ )
		{
			bar = "bar";
			color = "@red@";
			color2 = "@bla@";
			if ( Item.smithing_frame[Type2][i][3] > 1 )
			{
				bar = bar + "s";
			}
			if ( playerLevel[playerSmithing] >= Item.smithing_frame[Type2][i][2] )
			{
				color2 = "@whi@";
			}
			int Type3 = Type2;

			if ( Type2 >= 3 )
			{
				Type3 = ( Type2 + 2 );
			}
			if ( AreXItemsInBag( ( 2349 + ( Type3 * 2 ) ),
					Item.smithing_frame[Type2][i][3] ) == true )
			{
				color = "@gre@";
			}
			sendFrame126( color + "" + Item.smithing_frame[Type2][i][3] + ""
					+ bar, Item.smithing_frame[Type2][i][4] );
			String linux_hack = getItemName( Item.smithing_frame[Type2][i][0] );
			int index = getItemName( Item.smithing_frame[Type2][i][0] ).IndexOf(
					name );
			if ( index > 0 )
			{
				linux_hack = linux_hack.Substring( index + 1 );
				sendFrame126( linux_hack, Item.smithing_frame[Type2][i][5] );
			}
			// sendFrame126(
			// color2 + ""
			// + getItemName(Item.smithing_frame[Type2][i][0]).replace(name,
			// ""),
			// Item.smithing_frame[Type2][i][5]);
		}
		Item.SmithingItems[0][0] = Item.smithing_frame[Type2][0][0]; // Dagger
		Item.SmithingItems[0][1] = Item.smithing_frame[Type2][0][1];
		Item.SmithingItems[1][0] = Item.smithing_frame[Type2][4][0]; // Sword
		Item.SmithingItems[1][1] = Item.smithing_frame[Type2][4][1];
		Item.SmithingItems[2][0] = Item.smithing_frame[Type2][8][0]; // Scimitar
		Item.SmithingItems[2][1] = Item.smithing_frame[Type2][8][1];
		Item.SmithingItems[3][0] = Item.smithing_frame[Type2][9][0]; // Long
																	 // Sword
		Item.SmithingItems[3][1] = Item.smithing_frame[Type2][9][1];
		Item.SmithingItems[4][0] = Item.smithing_frame[Type2][18][0]; // 2
																	  // hand
																	  // sword
		Item.SmithingItems[4][1] = Item.smithing_frame[Type2][18][1];
		SetSmithing( 1119 );
		Item.SmithingItems[0][0] = Item.smithing_frame[Type2][1][0]; // Axe
		Item.SmithingItems[0][1] = Item.smithing_frame[Type2][1][1];
		Item.SmithingItems[1][0] = Item.smithing_frame[Type2][2][0]; // Mace
		Item.SmithingItems[1][1] = Item.smithing_frame[Type2][2][1];
		Item.SmithingItems[2][0] = Item.smithing_frame[Type2][13][0]; // Warhammer
		Item.SmithingItems[2][1] = Item.smithing_frame[Type2][13][1];
		Item.SmithingItems[3][0] = Item.smithing_frame[Type2][14][0]; // Battle
																	  // axe
		Item.SmithingItems[3][1] = Item.smithing_frame[Type2][14][1];
		Item.SmithingItems[4][0] = Item.smithing_frame[Type2][17][0]; // Claws
		Item.SmithingItems[4][1] = Item.smithing_frame[Type2][17][1];
		SetSmithing( 1120 );
		Item.SmithingItems[0][0] = Item.smithing_frame[Type2][15][0]; // Chain
																	  // body
		Item.SmithingItems[0][1] = Item.smithing_frame[Type2][15][1];
		Item.SmithingItems[1][0] = Item.smithing_frame[Type2][20][0]; // Plate
																	  // legs
		Item.SmithingItems[1][1] = Item.smithing_frame[Type2][20][1];
		Item.SmithingItems[2][0] = Item.smithing_frame[Type2][19][0]; // Plate
																	  // skirt
		Item.SmithingItems[2][1] = Item.smithing_frame[Type2][19][1];
		Item.SmithingItems[3][0] = Item.smithing_frame[Type2][21][0]; // Plate
																	  // body
		Item.SmithingItems[3][1] = Item.smithing_frame[Type2][21][1];
		Item.SmithingItems[4][0] = -1; // Lantern
		Item.SmithingItems[4][1] = 0;
		if ( ( Type == 2 ) || ( Type == 3 ) )
		{
			color2 = "@bla@";
			if ( playerLevel[playerSmithing] >= Item.smithing_frame[Type2][22][2] )
			{
				color2 = "@whi@";
			}
			Item.SmithingItems[4][0] = Item.smithing_frame[Type2][22][0]; // Lantern
			Item.SmithingItems[4][1] = Item.smithing_frame[Type2][22][1];
			sendFrame126( color2 + ""
					+ getItemName( Item.smithing_frame[Type2][22][0] ), 11461 );
		}
		SetSmithing( 1121 );
		Item.SmithingItems[0][0] = Item.smithing_frame[Type2][3][0]; // Medium
		Item.SmithingItems[0][1] = Item.smithing_frame[Type2][3][1];
		Item.SmithingItems[1][0] = Item.smithing_frame[Type2][10][0]; // Full
																	  // Helm
		Item.SmithingItems[1][1] = Item.smithing_frame[Type2][10][1];
		Item.SmithingItems[2][0] = Item.smithing_frame[Type2][12][0]; // Square
		Item.SmithingItems[2][1] = Item.smithing_frame[Type2][12][1];
		Item.SmithingItems[3][0] = Item.smithing_frame[Type2][16][0]; // Kite
		Item.SmithingItems[3][1] = Item.smithing_frame[Type2][16][1];
		Item.SmithingItems[4][0] = Item.smithing_frame[Type2][6][0]; // Nails
		Item.SmithingItems[4][1] = Item.smithing_frame[Type2][6][1];
		SetSmithing( 1122 );
		Item.SmithingItems[0][0] = Item.smithing_frame[Type2][5][0]; // Dart
																	 // Tips
		Item.SmithingItems[0][1] = Item.smithing_frame[Type2][5][1];
		Item.SmithingItems[1][0] = Item.smithing_frame[Type2][7][0]; // Arrow
																	 // Heads
		Item.SmithingItems[1][1] = Item.smithing_frame[Type2][7][1];
		Item.SmithingItems[2][0] = Item.smithing_frame[Type2][11][0]; // Knives
		Item.SmithingItems[2][1] = Item.smithing_frame[Type2][11][1];
		Item.SmithingItems[3][0] = -1; // Wire
		Item.SmithingItems[3][1] = 0;
		if ( Type == 1 )
		{
			color2 = "@bla@";
			if ( playerLevel[playerSmithing] >= Item.smithing_frame[Type2][22][2] )
			{
				color2 = "@whi@";
			}
			Item.SmithingItems[3][0] = Item.smithing_frame[Type2][22][0]; // Wire
			Item.SmithingItems[3][1] = Item.smithing_frame[Type2][22][1];
			sendFrame126( color2 + ""
					+ getItemName( Item.smithing_frame[Type2][22][0] ), 1096 );
		}
		Item.SmithingItems[4][0] = -1; // Studs
		Item.SmithingItems[4][1] = 0;
		if ( Type == 3 )
		{
			color2 = "@bla@";
			if ( playerLevel[playerSmithing] >= Item.smithing_frame[Type2][23][2] )
			{
				color2 = "@whi@";
			}
			Item.SmithingItems[4][0] = Item.smithing_frame[Type2][23][0]; // Studs
			Item.SmithingItems[4][1] = Item.smithing_frame[Type2][23][1];
			sendFrame126( color2 + ""
					+ getItemName( Item.smithing_frame[Type2][23][0] ), 1134 );
		}
		SetSmithing( 1123 );
		showInterface( 994 );
		smithing[2] = Type;
	}


	public void openUpBank( )
	{
		sendFrame248( 5292, 5063 );
		resetItems( 5064 );
		rearrangeBank();
		resetBank();
		IsBanking = true;
	}

	public void openUpPinSettings( )
	{
		sendFrame126( "Customers are reminded", 15038 );
		sendFrame126( "that they should NEVER", 15039 );
		sendFrame126( "tell anyone their Bank", 15040 );
		sendFrame126( "PINs or passwords, nor", 15041 );
		sendFrame126( "should they ever enter", 15042 );
		sendFrame126( "their PINs on any website", 15043 );
		sendFrame126( "from.", 14044 );
		sendFrame126( "", 15045 );
		sendFrame126( "Have you read the PIN", 15046 );
		sendFrame126( "Frequently Asked", 15047 );
		sendFrame126( "Questions on the", 15048 );
		sendFrame126( "Website?", 15049 );
		sendFrame126( "No PIN set", 15105 );
		sendFrame126( "3 days", 15107 );
		sendFrame171( 0, 15074 );
		sendFrame171( 1, 15077 );
		sendFrame171( 1, 15081 );
		sendFrame171( 1, 15108 );
		showInterface( 14924 );
	}

	public void openUpShop( int ShopID )
	{
		sendFrame126( ShopHandler.ShopName[ShopID], 3901 );
		sendFrame248( 3824, 3822 );
		resetItems( 3823 );
		resetShop( ShopID );
		IsShopping = true;
		MyShopID = ShopID;
	}
	public void openTrade( )
	{
		inTrade = true;
		tradeRequested = false;
		sendFrame248( 3323, 3321 ); // trading window + bag
		resetItems( 3322 );
		resetTItems( 3415 );
		resetOTItems( 3416 );
		String outText = PlayerHandler.players[trade_reqId].playerName;
		if ( PlayerHandler.players[trade_reqId].playerRights == 1 )
		{
			outText = "@cr1@" + outText;
		}
		else if ( PlayerHandler.players[trade_reqId].playerRights == 2 )
		{
			outText = "@cr2@" + outText;
		}
		sendFrame126( "Trading With: "
				+ PlayerHandler.players[trade_reqId].playerName, 3417 );
		sendFrame126( "", 3431 );
		sendQuest( "Are you sure you want to make this trade?", 3535 );
	}
	public void openWelcomeScreen( int recoveryChange, Boolean memberWarning,
			int messages, int lastLoginIP, int lastLogin )
	{
		outStream.createFrame( 176 );
		// days since last recovery change 200 for not yet set 201 for members
		// server,
		// otherwise, how many days ago recoveries have been changed.
		outStream.writeByteC( recoveryChange );
		outStream.writeWordA( messages ); // # of unread messages
		outStream.writeByte( memberWarning ? 1 : 0 ); // 1 for member on
													  // non-members world warning
		outStream.writeDWord_v2( lastLoginIP ); // ip of last login
		outStream.writeWord( lastLogin ); // days
	}

	private Boolean packetProcess( )
	{
		if ( disconnected )
		{
			return false;
		}
		try
		{
			if ( timeOutCounter++ > 20 )
			{
				actionReset();
				disconnected = true;
				if ( saveNeeded )
					savegame( true );
				return false;
			}

			if (inputStream == null) {
				return false;
			}

			int avail = mySock.Available;

			if ( avail == 0 )
			{
				return false;
			}
			if ( packetType == -1 )
			{
				packetType = inputStream.ReadByte() & 0xff;
				//if ( inStreamDecryption != null )
				//{
				//	packetType = packetType - inStreamDecryption.getNextKey()
				//			& 0xff;
				//}
				packetSize = packetSizes[packetType];
				avail--;
			}
			if ( packetSize == -1 )
			{
				if ( avail > 0 )
				{
					packetSize = inputStream.ReadByte() & 0xff;			
					//Byte[] packetSizeBytes = null;
					//inputStream.Read( packetSizeBytes, 0, 4 );
					//packetSize = BitConverter.ToInt32( packetSizeBytes ) & 0xff;
					avail--;
				}
				else
				{
					return false;
				}
			}
			if ( avail < packetSize )
			{
				return false;
			}
			if ( packetSize > 0 )
				fillInStream( packetSize );
			timeOutCounter = 0;

			parseIncomingPackets();
			packetType = -1;
		}
		catch ( Exception __ex )
		{
			server.logError( __ex.ToString() );
			disconnected = true;
			Console.WriteLine( "Project16 [fatal] - exception" );
			savegame( true );
		}
		return true;
	}

	public void parseIncomingPackets( )
	{
		if ( isMassClicking == true )
		{
			masstimer = 10;
		}
		else
		{

			int i;
			int junk;
			int junk2;
			int junk3;
			lastPacket = TimeHelper.CurrentTimeMillis();
				// if(packetType != 0) println("" + packetType);
			if ( packetType == 185 )
				Console.WriteLine( "asda" );
			switch ( packetType )
			{
				case 25:
					// item in inventory used with item on floor
					int unknown1 = inStream.readSignedWordBigEndian(); // interface id
																	   // of item
					int unknown2 = inStream.readUnsignedWordA(); // item in bag id
					int floorID = inStream.readUnsignedByte();
					int floorY = inStream.readUnsignedWordA();
					int unknown3 = inStream.readUnsignedWordBigEndianA();
					int floorX = inStream.readUnsignedByte();
					Console.WriteLine( "Unknown1 = " + unknown1 );
					Console.WriteLine( "Unknown2 = " + unknown2 );
					Console.WriteLine( "FloorID = " + floorID );
					Console.WriteLine( "FloorY = " + floorY );
					Console.WriteLine( "Unknown3 = " + unknown3 );
					Console.WriteLine( "FloorX = " + floorX );
					break;
				case 57:
					int aA1 = inStream.readSignedWordBigEndianA();
					int b1 = inStream.readSignedWordBigEndianA();
					int c1 = inStream.readSignedWordBigEndian();
					int d1 = inStream.readSignedWordBigEndianA();
					break;
				case 0:
					break; // idle packet - keeps on reseting timeOutCounter

				case 202:
					logout();
					break;
				case 45:
					// flagged account data
					// inStream.readBytes(pmchatText, pmchatTextSize, 0);
					int blah = inStream.readUnsignedByte();
					int part2 = -1,
					part3 = -1,
					part4 = -1;
					try
					{
						part2 = inStream.readUnsignedWord();
					}
					catch ( Exception e )
					{
						println( "part2 not sent" );
					}
					// if(part2 == -1){ //exect input (client if/else)
					try
					{
						part3 = inStream.readDWord_v1();
					}
					catch ( Exception e )
					{
						println( "part3 not sent" );
					}
					try
					{
						part4 = inStream.readDWord();
					}
					catch ( Exception e )
					{
						println( "part4 not sent" );
					}
					// }
					println( "blah=" + blah + ", " + "part2=" + part2 + ", part3="
							+ part3 + ", part4=" + part4 );
					break;

				case 210:
					// loads new area
					break;

				case 40:
					if ( ( NpcDialogue == 1 ) || ( NpcDialogue == 3 ) || ( NpcDialogue == 5 ) )
					{
						NpcDialogue += 1;
						NpcDialogueSend = false;
					}
					else if ( ( NpcDialogue == 11 ) || ( NpcDialogue == 12 ) )
					{
						NpcDialogue += 1;
						NpcDialogueSend = false;
					}
					else if ( NpcDialogue == 2244 )
					{
						NpcDialogue += 1;
						NpcDialogueSend = false;

					}
					else if ( ( NpcDialogue == 6 ) || ( NpcDialogue == 7 ) )
					{
						NpcDialogue = 0;
						NpcDialogueSend = false;
						RemoveAllWindows();
					}
					else
					{
						closeInterface();
					}

					println_debug( "Unhandled packet [" + packetType + ", InterFaceId: "
							+ inStream.readUnsignedWordA() + ", size=" + packetSize
							+ "]: ]" + misc.Hex( inStream.buffer, 1, packetSize ) + "[" );
					println_debug( "Action Button: "
							+ misc.HexToInt( inStream.buffer, 0, packetSize ) );
					break;

				case 192:
					// Use an item on an object
					junk = inStream.readSignedWordBigEndianA();
					int UsedOnObjectID = inStream.readUnsignedWordBigEndian();
					int UsedOnY = inStream.readSignedWordBigEndianA();
					int ItemSlot = ( inStream.readSignedWordBigEndianA() - 128 );
					int UsedOnX = inStream.readUnsignedWordBigEndianA();
					int ItemID = inStream.readUnsignedWord();
					if ( !playerHasItem( ItemID ) )
						break;
					if ( UsedOnObjectID == 3994 )
					{
						for ( int fi = 0; fi < misc.smelt_frame.Length; fi++ )
							sendFrame246( misc.smelt_frame[fi], 150, misc.smelt_bars[fi] );
						sendFrame164( 2400 );
						// smelting = true;
						// smelt_id = ItemID;
					}
					if ( ( UsedOnObjectID == 2781 ) || ( UsedOnObjectID == 2728 ) )
					{
						// furnace, range
						if ( /* CheckForSkillUse2(ItemID, ItemSlot) == */true )
						{
							skillX = UsedOnX;
							skillY = UsedOnY;
							checkCooking( ItemID );
						}
					}
					else if ( UsedOnObjectID == 2783 )
					{
						// anvil
						int Type = CheckSmithing( ItemID, ItemSlot );

						if ( Type != -1 )
						{
							skillX = UsedOnX;
							skillY = UsedOnY;
							OpenSmithingFrame( Type );
						}
					}
					else
					{
						println_debug( "Item: " + ItemID + " - Used On Object: "
								+ UsedOnObjectID + " -  X: " + UsedOnX + " - Y: "
								+ UsedOnY );
					}
					break;
				case 218:
					String abuser = misc.longToPlayerName( inStream.readQWord() );
					int rule = inStream.readUnsignedByte();
					int mute = inStream.readUnsignedByte();
					reportAbuse( abuser, rule, mute );
					break;
				case 130:
					// Clicking some stuff in game
					int interfaceID = inStream.readSignedByte();
					// if(actionButtonId == 26018) {
					if ( inDuel && !duelFight )
					{
						declineDuel();
					}
					// }
					if ( duelFight && wonDuel == true )
					{
						if ( TimeHelper.CurrentTimeMillis() - lastButton < 1000 )
						{
							lastButton = TimeHelper.CurrentTimeMillis();
							break;
						}
						else
						{
							lastButton = TimeHelper.CurrentTimeMillis();
						}
						println( "Valid click.." );
						client other = getClient( duel_with );
						foreach ( GameItem item in otherOfferedItems )
						{
							println( "otherDuelItems = " + item.id );
							if ( ( item.id > 0 ) && ( item.amount > 0 ) )
							{
								if ( Item.itemStackable[item.id] )
								{
									if ( !addItem( item.id, item.amount ) )
										ItemHandler.addItem( item.id, enemyX, enemyY,
												item.amount, playerId, false );
								}
								else
								{
									int amount = item.amount;
									for ( int u = 1; u <= amount; u++ )
									{
										if ( !addItem( item.id, 1 ) )
											ItemHandler.addItem( item.id, enemyX,
													enemyY, 1, playerId, false );
									}
								}
							}

						}
						foreach ( GameItem item in offeredItems )
						{
							if ( ( item.id > 0 ) && ( item.amount > 0 ) )
								addItem( item.id, item.amount );
						}
						resetDuel();
						savegame( false );
						if ( ValidClient( duel_with ) )
						{
							other.resetDuel();
							other.savegame( false );
						}
					}
					else
					{
						// sendMessage("You didn't win the duel!");
					}
					if ( inTrade && ( TimeHelper.CurrentTimeMillis() - lastButton > 1000 ) )
					{
						lastButton = TimeHelper.CurrentTimeMillis();
						declineTrade();
					}
					if ( IsShopping == true )
					{
						IsShopping = false;
						MyShopID = 0;
						UpdateShop = false;
					}
					if ( IsBanking == true )
					{
						IsBanking = false;
					}

					if ( ( misc.HexToInt( inStream.buffer, 0, packetSize ) != 63363 )
							&& ( misc.HexToInt( inStream.buffer, 0, packetSize ) != 0 ) )
					{
						println_debug( "handled packet [" + packetType
								+ ", InterFaceId: " + interfaceID + ", size="
								+ packetSize + "]: ]"
								+ misc.Hex( inStream.buffer, 1, packetSize ) + "[" );
						println_debug( "Action Button: "
								+ misc.HexToInt( inStream.buffer, 0, packetSize ) );
					}
					break;

				case 155:
					// first Click npc


					NPCSlot = inStream.readSignedWordBigEndian();
					if ( ( NPCSlot < 0 ) || ( NPCSlot >= server.npcHandler.npcs.Length )
							|| ( server.npcHandler.npcs[NPCSlot] == null ) )
						break;
					NPCID = server.npcHandler.npcs[NPCSlot].npcType;
						setFaceNPC( NPCSlot );
					Boolean FishingGo = false;
					Boolean PutNPCCoords = false;
					startedrange = false;

					if ( NPCID == 316 )
					{
						/* Net From Net & Bait - Any Sea */
						if ( ( IsItemInBag( 303 ) == true ) && ( actionTimer == 0 ) )
						{
							startFishing( 316 );
						}
						else
						{
							sendMessage( "You need a " + getItemName( 303 )
									+ " to fish here." );
						}
					}
					else if ( NPCID == 321 )
					{
						startFishing( 321 );

					}
					else if ( ( NPCID == 494 ) || ( NPCID == 495 ) || ( NPCID == 496 )
						  || ( NPCID == 497 ) || ( NPCID == 2354 ) || ( NPCID == 2355 )
						  || ( NPCID == 2619 ) || ( NPCID == 3198 ) )
					{
						/* Banking */
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						NpcWanneTalk = 1;
					}
					else if ( NPCID == 553 )
					{
						/* Aubury */
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						NpcWanneTalk = 3;
					}
					else if ( NPCID == 2244 )
					{
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						NpcWanneTalk = 2244;
					}
					else if ( NPCID == 3478 )
					{
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						NpcWanneTalk = 3478;
					}
					else if ( NPCID == 228 )
					{
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						NpcWanneTalk = 11;
					}
					else if ( NPCID == 2253 )
					{
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						NpcWanneTalk = 9;
					}
					else if ( NPCID == 398 )
					{
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						NpcWanneTalk = 398;
						sendFrame200( 4883, 398 );
						sendFrame126( GetNpcName( 398 ), 4884 );
						if ( premium )
						{
							sendFrame126( "Welcome to the Guild of Legends", 4885 );
						}
						else
						{
							sendFrame126( "You must be a premium member to enter", 4885 );
							sendFrame126( "Visi [WEBSITE] to subscribe", 4886 );
						}
						sendFrame75( 398, 4883 );
						sendFrame164( 4882 );
						NpcDialogueSend = true;


					}
					else
					{
						println_debug( "atNPC 1: " + NPCID );
					}
					if ( PutNPCCoords == true )
					{
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
					}
					break;

				case 17:
					// second Click npc
					NPCSlot = inStream.readUnsignedWordBigEndianA();
					if ( ( NPCSlot < 0 ) || ( NPCSlot >= server.npcHandler.npcs.Length )
							|| ( server.npcHandler.npcs[NPCSlot] == null ) )
						break;
					NPCID = server.npcHandler.npcs[NPCSlot].npcType;
					setFaceNPC( NPCSlot );
					long time = TimeHelper.CurrentTimeMillis();
					if ( time - globalCooldown[0] <= 50 )
					{
						sendMessage( "Action throttled... please wait longer before acting!" );
						break;
					}
					if ( time - lastMouse > 5000 )
					{
						sendMessage( "Client hack detected!" );
						println( "Suspicious activity!" );
						break;
					}

					globalCooldown[0] = time;
					int npcX = server.npcHandler.npcs[NPCSlot].absX;
					int npcY = server.npcHandler.npcs[NPCSlot].absY;
					if ( ( Math.Abs( absX - npcX ) > 50 ) || ( Math.Abs( absY - npcY ) > 50 ) )
					{
						sendMessage( "Client hack detected!" );
						break;
					}
					if ( server.npcHandler.npcs[NPCSlot].IsDead )
					{
						startedrange = false;
						sendMessage( "That monster has been killed!" );
						break;
					}
					FishingGo = false;
					PutNPCCoords = false;
					if ( ( NPCID == 494 ) || ( NPCID == 495 ) || ( NPCID == 496 )
							|| ( NPCID == 497 ) || ( NPCID == 2354 ) || ( NPCID == 2355 )
							|| ( NPCID == 2619 ) || ( NPCID == 3198 ) )
					{
						/* Banking */
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						WanneBank = 2;
					}
					else if ( ( NPCID == 300 ) || ( NPCID == 844 ) || ( NPCID == 462 ) )
					{
						/* Essence Mine Guys */
						IsUsingSkill = true;
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
						stairs = 26;
						stairDistance = 1;
						if ( NPCID == 300 )
						{
							Essence = 1;
						}
						else if ( NPCID == 844 )
						{
							Essence = 2;
						}
						else if ( NPCID == 462 )
						{
							Essence = 3;
						}
					}
					else if ( NPCID == 461 )
					{
						// Magic store owner shop -bakatool
						PutNPCCoords = true;
						WanneShop = 39; // Magic store owner shop
					}
					else if ( NPCID == 553 )
					{
						// Aubury rune shop
						PutNPCCoords = true;
						WanneShop = 2; // Aubury Magic Shop
					}
					else if ( ( NPCID == 522 ) || ( NPCID == 523 ) )
					{
						// Shop Keeper + Assistant
						PutNPCCoords = true;
						WanneShop = 1; // Varrock General Store
					}
					else if ( ( NPCID == 526 ) || ( NPCID == 527 ) )
					{
						// Shop Keeper + Assistant
						PutNPCCoords = true;
						WanneShop = 3; // Falador General Store
					}
					else if ( NPCID == 577 )
					{
						// Cassie
						PutNPCCoords = true;
						WanneShop = 4; // Falador Shield Shop
					}
					else if ( NPCID == 580 )
					{
						// Flynn
						PutNPCCoords = true;
						WanneShop = 5; // Falador Mace Shop
					}
					else if ( NPCID == 538 )
					{
						// Peksa
						PutNPCCoords = true;
						WanneShop = 6; // Barbarian Vullage Helmet Shop
					}
					else if ( NPCID == 546 )
					{
						// Zaff
						PutNPCCoords = true;
						WanneShop = 7; // Varrock Staff Shop
					}
					else if ( NPCID == 548 )
					{
						// Thessalia
						PutNPCCoords = true;
						WanneShop = 8; // Varrock Cloth shop
					}
					else if ( ( NPCID == 551 ) || ( NPCID == 552 ) )
					{
						// Shop Keeper + Assistant
						PutNPCCoords = true;
						WanneShop = 9; // Varrock Sword shop
					}
					else if ( NPCID == 549 )
					{
						// Horvik
						PutNPCCoords = true;
						WanneShop = 10; // Varrock Armor shop
					}
					else if ( NPCID == 550 )
					{
						// Lowe
						PutNPCCoords = true;
						WanneShop = 11; // Varrock Armor shop
					}
					else if ( NPCID == 584 )
					{
						// Heruin
						PutNPCCoords = true;
						WanneShop = 12; // Falador Gem Shop
					}
					else if ( NPCID == 581 )
					{
						// Wayne
						PutNPCCoords = true;
						WanneShop = 13; // Falador Chainmail Shop
					}
					else if ( NPCID == 585 )
					{
						// Rommik
						PutNPCCoords = true;
						WanneShop = 14; // Rimmington Crafting Shop
					}
					else if ( ( NPCID == 531 ) || ( NPCID == 530 ) )
					{
						// Shop Keeper + Assistant
						PutNPCCoords = true;
						WanneShop = 15; // Rimmington General Store
					}
					else if ( NPCID == 1860 )
					{
						// Brian
						PutNPCCoords = true;
						WanneShop = 16; // Rimmington Archery Shop
					}
					else if ( NPCID == 557 )
					{
						// Wydin
						PutNPCCoords = true;
						WanneShop = 17; // Port Sarim Food Shop
					}
					else if ( NPCID == 558 )
					{
						// Gerrant
						PutNPCCoords = true;
						WanneShop = 18; // Port Sarim Fishing Shop
					}
					else if ( NPCID == 559 )
					{
						// Brian
						PutNPCCoords = true;
						WanneShop = 19; // Port Sarim Battleaxe Shop
					}
					else if ( NPCID == 556 )
					{
						// Grum
						PutNPCCoords = true;
						WanneShop = 20; // Port Sarim Jewelery Shop
					}
					else if ( NPCID == 583 )
					{
						// Betty
						PutNPCCoords = true;
						WanneShop = 21; // Port Sarim Magic Shop
					}
					else if ( ( NPCID == 520 ) || ( NPCID == 521 ) )
					{
						// Shop Keeper + Assistant
						PutNPCCoords = true;
						WanneShop = 22; // Lumbridge General Store
					}
					else if ( NPCID == 519 )
					{
						// Bob
						PutNPCCoords = true;
						WanneShop = 23; // Lumbridge Axe Shop
					}
					else if ( NPCID == 541 )
					{
						// Zeke
						PutNPCCoords = true;
						WanneShop = 24; // Al-Kharid Scimitar Shop
					}
					else if ( NPCID == 545 )
					{
						// Dommik
						PutNPCCoords = true;
						WanneShop = 25; // Al-Kharid Crafting Shop
					}
					else if ( ( NPCID == 524 ) || ( NPCID == 525 ) )
					{
						// Shop Keeper + Assistant
						PutNPCCoords = true;
						WanneShop = 26; // Al-Kharid General Store
					}
					else if ( NPCID == 542 )
					{
						// Louie Legs
						PutNPCCoords = true;
						WanneShop = 27; // Al-Kharid Legs Shop
					}
					else if ( NPCID == 544 )
					{
						// Ranael
						PutNPCCoords = true;
						WanneShop = 28; // Al-Kharid Skirt Shop
					}
					else if ( NPCID == 2621 )
					{
						// Hur-Koz
						PutNPCCoords = true;
						WanneShop = 29; // TzHaar Shop Weapons,Amour
					}
					else if ( NPCID == 2622 )
					{
						// Hur-Lek
						PutNPCCoords = true;
						WanneShop = 30; // TzHaar Shop Runes
					}
					else if ( NPCID == 2620 )
					{
						// Hur-Tel
						PutNPCCoords = true;
						WanneShop = 31; // TzHaar Shop General Store
					}
					else if ( NPCID == 692 )
					{
						// Throwing shop
						PutNPCCoords = true;
						WanneShop = 32; // Authentic Throwing Weapons
					}
					else if ( NPCID == 683 )
					{
						// Bow and arrows
						PutNPCCoords = true;
						WanneShop = 33; // Dargaud's Bow and Arrows
					}
					else if ( NPCID == 682 )
					{
						// Archer's Armour
						PutNPCCoords = true;
						WanneShop = 34; // Aaron's Archery Appendages
					}
					else if ( NPCID == 537 )
					{
						// Scavvo
						PutNPCCoords = true;
						WanneShop = 35; // Champion's Rune shop
					}
					else if ( NPCID == 536 )
					{
						// Valaine
						PutNPCCoords = true;
						WanneShop = 36; // Champion's guild shop
					}
					else if ( NPCID == 933 )
					{
						// Legend's Shop
						PutNPCCoords = true;
						WanneShop = 37; // Legend's Shop
					}
					else if ( NPCID == 932 )
					{
						// Legends General Store
						PutNPCCoords = true;
						WanneShop = 38; // Legend's Gen. Store
					}
					else if ( NPCID == 804 )
					{
						PutNPCCoords = true;
						WanneShop = 25; // Crafting shop
					}
					else if ( NPCID == 1917 )
					{
						PutNPCCoords = true;
						WanneShop = 42; // jail shop
					}
					else if ( NPCID == 1282 )
					{
						if ( playerLevel[17] == 99 )
						{
							PutNPCCoords = true;
							WanneShop = 44; // market shop 2
						}
						else
						{
							PutNPCCoords = true;
							WanneShop = 43; // market shop 1
						}
					}
					else if ( ( NPCID == 1 ) || ( NPCID == 2 ) || ( NPCID == 3 )
						  || ( NPCID == 4 ) || ( NPCID == 5 ) || ( NPCID == 6 ) )
					{
						// THEIVING MAN & WOMEN
						if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
						{
							robman();
							EntangleDelay = 6;
							stealtimer = 5;
							// snaretimer = 5;
							actionTimer = 4;
							setAnimation( 881 );

							AnimationReset = true;
							updateRequired = true;
							appearanceUpdateRequired = true;
						}
					}
					else if ( NPCID == 7 )
					{
						// THEIVING farmer
						if ( playerLevel[17] < 10 )
						{
							sendMessage( "You need 10 theiving to pickpocket farmers." );
						}
						else
						{
							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robfarmer();
								EntangleDelay = 6;
								stealtimer = 5;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( ( NPCID == 15 ) || ( NPCID == 18 ) || ( NPCID == 1318 ) )
					{
						// THEIVING warrior
						if ( playerLevel[17] < 25 )
						{
							sendMessage( "You need 25 theiving to pickpocket warriors." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robwarrior();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( NPCID == 187 )
					{
						// THEIVING rogue
						if ( playerLevel[17] < 32 )
						{
							sendMessage( "You need 32 theiving to pickpocket rogues." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robrogue();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( ( NPCID == 2234 ) || ( NPCID == 2235 ) )
					{
						// THEIVING master farmer
						if ( playerLevel[17] < 38 )
						{
							sendMessage( "You need 38 theiving to pickpocket master farmers." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robmasterfarmer();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( ( NPCID == 9 ) || ( NPCID == 10 ) || ( NPCID == 32 )
						  || ( NPCID == 334 ) || ( NPCID == 335 ) || ( NPCID == 336 )
						  || ( NPCID == 368 ) || ( NPCID == 678 ) || ( NPCID == 812 )
						  || ( NPCID == 887 ) )
					{
						// THEIVING guard
						if ( playerLevel[17] < 40 )
						{
							sendMessage( "You need 40 theiving to pickpocket gaurds." );
						}
						else
						{
							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robguard();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( NPCID == 660 )
					{
						// THEIVING knight
						if ( playerLevel[17] < 55 )
						{
							sendMessage( "You need 55 theiving to pickpocket knights." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robknight();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( NPCID == 34 )
					{
						// THEIVING watchmen
						if ( playerLevel[17] < 65 )
						{
							sendMessage( "You need 65 theiving to pickpocket watchmen." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robwatchman();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( NPCID == 20 )
					{
						// THEIVING paladin
						if ( playerLevel[17] < 70 )
						{
							sendMessage( "You need 70 theiving to pickpocket paladins." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robpaladin();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( ( NPCID == 66 ) || ( NPCID == 67 ) || ( NPCID == 68 ) )
					{
						// THEIVING gnome
						if ( playerLevel[17] < 75 )
						{
							sendMessage( "You need 75 theiving to pickpocket gnomes." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robgnome();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( NPCID == 21 )
					{
						// THEIVING hero
						if ( playerLevel[17] < 80 )
						{
							sendMessage( "You need 80 theiving to pickpocket heros." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robhero();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}
					}
					else if ( ( NPCID == 2359 ) || ( NPCID == 2360 ) || ( NPCID == 2361 )
						  || ( NPCID == 2362 ) )
					{
						// THEIVING elf
						if ( playerLevel[17] < 85 )
						{
							sendMessage( "You need 85 theiving to pickpocket elves." );
						}
						else
						{

							if ( ( stealtimer < 0 ) && ( EntangleDelay < 0 ) )
							{
								robelf();
								EntangleDelay = 6;
								stealtimer = 15;
								// snaretimer = 5;
								actionTimer = 4;
								setAnimation( 881 );

								AnimationReset = true;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
						}

					}
					else if ( ( stealtimer > 0 ) && ( EntangleDelay < 0 ) )
					{
						// snaretimer = 3;
						robfail();
						animation( 348, absY, absX );
						actionTimer = 4;
						setAnimation( 424 );
						AnimationReset = true;
						updateRequired = true;
						appearanceUpdateRequired = true;
						sendMessage( "Pickpocket attempt Failed!" );

					}
					else
					{
						println_debug( "atNPC 2: " + NPCID );
					}
					if ( PutNPCCoords == true )
					{
						skillX = server.npcHandler.npcs[NPCSlot].absX;
						skillY = server.npcHandler.npcs[NPCSlot].absY;
					}

					break;

				case 21:
					// Thirth Click npc
					NPCSlot = inStream.readSignedWord();

					NPCID = server.npcHandler.npcs[NPCSlot].npcType;
					if ( ( NPCID < server.npcHandler.npcs.Length ) && ( NPCID > 0 ) )
					{
						if ( NPCID == 553 )
						{
							/* Mage arena tele */
							teleportToX = 3100 + misc.random( 5 );
							teleportToY = 3930 + misc.random( 5 );
						}
						else
						{
							println_debug( "atNPC 3: " + NPCID );
						}
					}
					break;

				case 72:
					// Click to attack
					if ( ( TimeHelper.CurrentTimeMillis() - lastAttack > 1500 )
							&& ( deathStage < 1 ) )
					{
						attacknpc = inStream.readUnsignedWordA();
						if ( ( attacknpc >= 0 ) && ( attacknpc < NPCHandler.maxNPCSpawns ) )
						{
							IsAttackingNPC = true;
							if ( server.npcHandler.npcs[attacknpc].absX != absX && server.npcHandler.npcs[attacknpc].absY != absY )
							{
								setFaceNPC( attacknpc );
							}
							if ( server.npcHandler.npcs[attacknpc].StartKilling == 0 )
							{
								server.npcHandler.npcs[attacknpc].StartKilling = playerId;
							}
							server.npcHandler.npcs[attacknpc].RandomWalk = false;
							server.npcHandler.npcs[attacknpc].IsUnderAttack = true;
							actionTimer = 5;
						}
						else
						{
							sendMessage( "Exception catched, npc id was invalid." );
							ResetAttackNPC();
						}
					}
					break;

				case 121:
					replaceDoors();
					if ( heightLevel == 0 )
					{
						ReplaceObject2( 2613, 3084, 3994, -3, 11 );
						ReplaceObject2( 2628, 3151, 2104, -3, 11 );
						ReplaceObject2( 2629, 3151, 2105, -3, 11 );
						ReplaceObject2( 2733, 3374, 6420, -1, 11 );
						ReplaceObject2( 3111, 3512, 2213, -1, 10 ); //jailbank
																	// ReplaceObject2(2691, 9774, 2107, 0, 11);
					}
					if ( !isInWilderness( absX, absY, 1 ) )
					{
						setInterfaceWalkable( -1 );
						hasWildySign = false;
						wildysigndisappear();
					}
					break;

				case 122:
					// Call for burying bones
					junk = inStream.readSignedWordBigEndianA();
					ItemSlot = inStream.readUnsignedWordA();
					ItemID = inStream.readUnsignedWordBigEndian();
					if ( TimeHelper.CurrentTimeMillis() - lastAction >= 850 )
					{
						buryItem( ItemID, ItemSlot );
						lastAction = TimeHelper.CurrentTimeMillis();
						actionTimer = 10;
					}
					break;

				case 253:
					// call for burning fires
					skillX = inStream.readSignedWordBigEndian();
					skillY = inStream.readSignedWordBigEndianA();
					ItemID = inStream.readUnsignedWordA();
					println( "packet 253:  skillX=" + skillX + ", skillY=" + skillY );
					if ( ( IsUsingSkill == false ) && ( CheckForSkillUse4( ItemID ) == true ) )
					{
						IsUsingSkill = true;
					}
					break;

				case 53:
					// Use item on item
					int usedWithSlot = inStream.readUnsignedWord();
					int itemUsedSlot = inStream.readUnsignedWordA();
					int interface1284 = inStream.readUnsignedWord();
					int interfacek = inStream.readUnsignedWord();
					int useWith = playerItems[usedWithSlot] - 1;
					int itemUsed = playerItems[itemUsedSlot] - 1;
					if ( !playerHasItem( itemUsed ) || !playerHasItem( useWith ) )
					{
						break;
					}
					int otherItem = playerItems[usedWithSlot] - 1;

					if ( ( ( itemUsed == 5605 ) || ( otherItem == 5605 ) )
							&& ( ( itemUsed == 1511 ) || ( otherItem == 1511 ) ) )
					{
						shafting = true;
					}
					if ( ( ( itemUsed == 1733 ) || ( otherItem == 1733 ) )
							&& ( ( itemUsed == 1741 ) || ( otherItem == 1741 ) ) )
					{
						showInterface( 2311 );
					}

					if ( playerHasItem( 314, 15 ) && playerHasItem( 52, 15 )
							&& ( ( itemUsed == 314 ) || ( otherItem == 314 ) )
							&& ( ( itemUsed == 52 ) || ( otherItem == 52 ) ) )
					{
						if ( playerHasItem( -1 ) )
						{
							deleteItem( 314, 15 );
							deleteItem( 52, 15 );
							addItem( 53, 15 );
						}
						else
						{
							sendMessage( "Inventory is full!" );
						}
					}
					if ( ( ( itemUsed == 5605 ) || ( otherItem == 5605 ) )
							&& ( ( itemUsed == 1511 ) || ( otherItem == 1511 ) ) )
					{
						shafting = true;
					}
					int[] heads = { 39, 40, 41, 42, 43, 44 };
					int[] arrows = { 882, 884, 886, 888, 890, 892 };
					int[] required = { 1, 5, 25, 50, 75, 99 };
					for ( int h = 0; h < heads.Length; h++ )
					{
						if ( playerHasItem( heads[h], 15 ) && playerHasItem( 53, 15 )
								&& ( ( itemUsed == heads[h] ) || ( otherItem == heads[h] ) )
								&& ( ( itemUsed == 53 ) || ( otherItem == 53 ) ) )
						{
							if ( playerLevel[playerFletching] < required[h] )
							{
								sendMessage( "Requires level " + required[h]
										+ " fletching" );
								break;
							}
							deleteItem( heads[h], 15 );
							deleteItem( 53, 15 );
							addItem( arrows[h], 15 );
							addSkillXP( 200, playerFletching );
							break;
						}
					}
					// regular, oak, maple, willow, yew, magic?
					for ( int id = 0; id < Constants.logs.Length; id++ )
					{
						if ( ( ( itemUsed == Constants.logs[id] ) || ( otherItem == Constants.logs[id] ) )
								&& ( ( itemUsed == 5605 ) || ( otherItem == 5605 ) ) )
						{
							// emote 885 (funny=3129)
							dialog = true;
							dialogInterface = 2459;
							dialogId = 1;
							fletchLog = id;
							sendFrame126( "Select a bow", 8879 );
							sendFrame246( 8870, 250, Constants.longbows[id] ); // right picture
							sendFrame246( 8869, 250, Constants.shortbows[id] ); // left picture
							sendFrame126( getItemName( Constants.shortbows[id] ), 8871 );
							sendFrame126( getItemName( Constants.shortbows[id] ), 8874 );
							sendFrame126( getItemName( Constants.longbows[id] ), 8878 );
							sendFrame126( getItemName( Constants.longbows[id] ), 8875 );
							sendFrame164( 8866 );
							break;
						}
					}
					for ( int id1 = 0; id1 < Constants.shortbow.Length; id1++ )
					{
						if ( ( ( itemUsed == Constants.shortbows[id1] ) || ( otherItem == Constants.shortbows[id1] ) )
								&& ( ( itemUsed == 1777 ) || ( otherItem == 1777 ) ) )
						{
							deleteItem( Constants.shortbows[id1], 1 );
							deleteItem( 1777, 1 );
							addItem( Constants.shortbow[id1], 1 );
						}
					}
					for ( int b2 = 0; b2 < Constants.shortbow.Length; b2++ )
					{
						if ( ( ( itemUsed == Constants.longbows[b2] ) || ( otherItem == Constants.longbows[b2] ) )
								&& ( ( itemUsed == 1777 ) || ( otherItem == 1777 ) ) )
						{
							deleteItem( Constants.longbows[b2], 1 );
							deleteItem( 1777, 1 );
							addItem( Constants.longbow[b2], 1 );
						}
					}
					for ( int h = 0; h < Constants.leathers.Length; h++ )
					{
						if ( ( ( itemUsed == 1733 ) || ( otherItem == 1733 ) )
								&& ( ( itemUsed == Constants.leathers[h] ) || ( otherItem == Constants.leathers[h] ) ) )
						{
							craftMenu( h );
							cIndex = h;
						}
					}
					if ( ( itemUsed == 233 ) && ( useWith == 237 ) )
					{
						deleteItem( 237, getItemSlot( 237 ), 1 );
						addItem( 235, 1 );
					}
					else if ( ( itemUsed == 590 ) && ( useWith == 1511 ) )
					{
						if ( playerLevel[11] >= 0 )
						{
							deleteItem( 1511, getItemSlot( 1511 ), 1 );
							// createNewTileObject(currentX, currentY, 2732, 0, 10);
							addObject( currentX, currentY, 2732, 0 );
							addSkillXP( 90, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking of 0 to burn normal logs." );
						}
					}
					else if ( ( itemUsed == 590 ) && ( useWith == 1521 ) )
					{
						if ( playerLevel[11] >= 15 )
						{
							deleteItem( 1521, getItemSlot( 1521 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 60, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking level of 15 to burn oak logs." );
						}
					}

					else if ( ( itemUsed == 590 ) && ( useWith == 1519 ) )
					{
						if ( playerLevel[11] >= 30 )
						{
							deleteItem( 1519, getItemSlot( 1519 ), 1 );
							addObject( absX, absY, 2732, 0 );
							addSkillXP( 90, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking of 30 to burn willow logs." );
						}
					}
					else if ( ( itemUsed == 590 ) && ( useWith == 1517 ) )
					{
						if ( playerLevel[11] >= 45 )
						{
							deleteItem( 1517, getItemSlot( 1517 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 135, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking level of 45 to burn maple logs." );
						}
					}

					else if ( ( itemUsed == 590 ) && ( useWith == 1515 ) )
					{
						if ( playerLevel[11] >= 60 )
						{
							deleteItem( 1515, getItemSlot( 1515 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 202, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking of 60 to burn yew logs." );
						}
					}
					else if ( ( itemUsed == 590 ) && ( useWith == 1513 ) )
					{
						if ( playerLevel[11] >= 75 )
						{
							deleteItem( 1513, getItemSlot( 1513 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 303, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking level of 75 to burn magic logs." );
						}
					}

					else if ( ( itemUsed == 1511 ) && ( useWith == 590 ) )
					{
						if ( playerLevel[11] >= 0 )
						{
							deleteItem( 1511, getItemSlot( 1511 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 40, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking of 0 to burn normal logs." );
						}
					}
					else if ( ( itemUsed == 1521 ) && ( useWith == 590 ) )
					{
						if ( playerLevel[11] >= 15 )
						{
							deleteItem( 1521, getItemSlot( 1521 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 60, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking level of 15 to burn oak logs." );
						}
					}

					else if ( ( itemUsed == 1519 ) && ( useWith == 590 ) )
					{
						if ( playerLevel[11] >= 30 )
						{
							deleteItem( 1519, getItemSlot( 1519 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 90, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking of 30 to burn willow logs." );
						}
					}
					else if ( ( itemUsed == 1517 ) && ( useWith == 590 ) )
					{
						if ( playerLevel[11] >= 45 )
						{
							deleteItem( 1517, getItemSlot( 1517 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 135, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking level of 45 to burn maple logs." );
						}
					}

					else if ( ( itemUsed == 1515 ) && ( useWith == 590 ) )
					{
						if ( playerLevel[11] >= 60 )
						{
							deleteItem( 1515, getItemSlot( 1515 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 202, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking of 60 to burn yew logs." );
						}
					}
					else if ( ( itemUsed == 1513 ) && ( useWith == 590 ) )
					{
						if ( playerLevel[11] >= 75 )
						{
							deleteItem( 1513, getItemSlot( 1513 ), 1 );
							createNewTileObject( currentX, currentY, 2732, 0, 10 );
							addSkillXP( 303, 11 );
						}
						else
						{
							sendMessage( "You need a firemaking level of 75 to burn magic logs." );
						}
					}

					// herblore

					if ( ( itemUsed == 233 ) && ( useWith == 237 ) )
					{
						deleteItem( 237, getItemSlot( 237 ), 1 );
						addItem( 235, 1 );
					}
					if ( ( itemUsed == 237 ) && ( useWith == 233 ) )
					{
						deleteItem( 237, getItemSlot( 237 ), 1 );
						addItem( 235, 1 );
					}
					if ( ( itemUsed == 233 ) && ( useWith == 243 ) )
					{
						deleteItem( 243, getItemSlot( 243 ), 1 );
						addItem( 241, 1 );
					}
					if ( ( itemUsed == 243 ) && ( useWith == 233 ) )
					{
						deleteItem( 243, getItemSlot( 243 ), 1 );
						addItem( 241, 1 );
					}
					if ( ( itemUsed == 233 ) && ( useWith == 1973 ) )
					{
						deleteItem( 1973, getItemSlot( 1973 ), 1 );
						addItem( 1975, 1 );
					}
					if ( ( itemUsed == 1973 ) && ( useWith == 233 ) )
					{
						deleteItem( 1973, getItemSlot( 1973 ), 1 );
						addItem( 1975, 1 );
					}
					//
					//
					if ( ( itemUsed == 249 ) && ( useWith == 227 ) )
					{
						deleteItem( 249, getItemSlot( 249 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 91, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 249 ) )
					{
						deleteItem( 249, getItemSlot( 249 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 91, 1 );
					}
					if ( ( itemUsed == 251 ) && ( useWith == 227 ) )
					{
						deleteItem( 251, getItemSlot( 251 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 93, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 251 ) )
					{
						deleteItem( 251, getItemSlot( 251 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 93, 1 );
					}
					if ( ( itemUsed == 253 ) && ( useWith == 227 ) )
					{
						deleteItem( 253, getItemSlot( 253 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 95, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 253 ) )
					{
						deleteItem( 253, getItemSlot( 253 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 95, 1 );
					}
					if ( ( itemUsed == 255 ) && ( useWith == 227 ) )
					{
						deleteItem( 255, getItemSlot( 255 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 97, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 255 ) )
					{
						deleteItem( 255, getItemSlot( 2559 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 97, 1 );
					}
					if ( ( itemUsed == 257 ) && ( useWith == 227 ) )
					{
						deleteItem( 257, getItemSlot( 257 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 99, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 257 ) )
					{
						deleteItem( 257, getItemSlot( 257 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 99, 1 );
					}
					if ( ( itemUsed == 259 ) && ( useWith == 227 ) )
					{
						deleteItem( 259, getItemSlot( 259 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 101, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 259 ) )
					{
						deleteItem( 259, getItemSlot( 259 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 101, 1 );
					}
					if ( ( itemUsed == 261 ) && ( useWith == 227 ) )
					{
						deleteItem( 261, getItemSlot( 261 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 103, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 261 ) )
					{
						deleteItem( 261, getItemSlot( 261 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 103, 1 );
					}
					if ( ( itemUsed == 263 ) && ( useWith == 227 ) )
					{
						deleteItem( 263, getItemSlot( 263 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 105, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 263 ) )
					{
						deleteItem( 263, getItemSlot( 263 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 105, 1 );
					}
					if ( ( itemUsed == 265 ) && ( useWith == 227 ) )
					{
						deleteItem( 265, getItemSlot( 265 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 107, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 265 ) )
					{
						deleteItem( 265, getItemSlot( 265 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 107, 1 );
					}
					if ( ( itemUsed == 267 ) && ( useWith == 227 ) )
					{
						deleteItem( 267, getItemSlot( 267 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 109, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 267 ) )
					{
						deleteItem( 267, getItemSlot( 267 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 109, 1 );
					}
					if ( ( itemUsed == 269 ) && ( useWith == 227 ) )
					{
						deleteItem( 269, getItemSlot( 269 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 111, 1 );
					}
					if ( ( itemUsed == 227 ) && ( useWith == 269 ) )
					{
						deleteItem( 269, getItemSlot( 269 ), 1 );
						deleteItem( 227, getItemSlot( 227 ), 1 );
						addItem( 111, 1 );
					}
					// 
					//
					if ( ( itemUsed == 91 ) && ( useWith == 221 ) )
					{
						if ( playerLevel[15] >= 1 )
						{
							deleteItem( 91, getItemSlot( 91 ), 1 );
							deleteItem( 221, getItemSlot( 221 ), 1 );
							addItem( 121, 1 );
							addSkillXP( 125, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 221 ) && ( useWith == 91 ) )
					{
						if ( playerLevel[15] >= 1 )
						{
							deleteItem( 91, getItemSlot( 91 ), 1 );
							deleteItem( 221, getItemSlot( 221 ), 1 );
							addItem( 121, 1 );
							addSkillXP( 125, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 93 ) && ( useWith == 235 ) )
					{
						if ( playerLevel[15] >= 5 )
						{
							deleteItem( 93, getItemSlot( 93 ), 1 );
							deleteItem( 235, getItemSlot( 235 ), 1 );
							addItem( 175, 1 );
							addSkillXP( 238, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 235 ) && ( useWith == 93 ) )
					{
						if ( playerLevel[15] >= 5 )
						{
							deleteItem( 93, getItemSlot( 93 ), 1 );
							deleteItem( 235, getItemSlot( 235 ), 1 );
							addItem( 175, 1 );
							addSkillXP( 238, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 95 ) && ( useWith == 225 ) )
					{
						if ( playerLevel[15] >= 12 )
						{
							deleteItem( 95, getItemSlot( 95 ), 1 );
							deleteItem( 225, getItemSlot( 225 ), 1 );
							addItem( 115, 1 );
							addSkillXP( 350, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 225 ) && ( useWith == 95 ) )
					{
						if ( playerLevel[15] >= 12 )
						{
							deleteItem( 95, getItemSlot( 95 ), 1 );
							deleteItem( 225, getItemSlot( 225 ), 1 );
							addItem( 115, 1 );
							addSkillXP( 350, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 97 ) && ( useWith == 223 ) )
					{
						if ( playerLevel[15] >= 22 )
						{
							deleteItem( 97, getItemSlot( 97 ), 1 );
							deleteItem( 223, getItemSlot( 223 ), 1 );
							addItem( 127, 1 );
							addSkillXP( 463, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 223 ) && ( useWith == 97 ) )
					{
						if ( playerLevel[15] >= 22 )
						{
							deleteItem( 97, getItemSlot( 97 ), 1 );
							deleteItem( 223, getItemSlot( 223 ), 1 );
							addItem( 127, 1 );
							addSkillXP( 463, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 97 ) && ( useWith == 1975 ) )
					{
						if ( playerLevel[15] >= 26 )
						{
							deleteItem( 97, getItemSlot( 97 ), 1 );
							deleteItem( 1975, getItemSlot( 1975 ), 1 );
							addItem( 3010, 1 );
							addSkillXP( 468, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 1975 ) && ( useWith == 97 ) )
					{
						if ( playerLevel[15] >= 26 )
						{
							deleteItem( 97, getItemSlot( 97 ), 1 );
							deleteItem( 1975, getItemSlot( 1975 ), 1 );
							addItem( 3010, 1 );
							addSkillXP( 468, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 99 ) && ( useWith == 239 ) )
					{
						if ( playerLevel[15] >= 30 )
						{
							deleteItem( 99, getItemSlot( 99 ), 1 );
							deleteItem( 239, getItemSlot( 239 ), 1 );
							addItem( 133, 1 );
							addSkillXP( 575, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 239 ) && ( useWith == 99 ) )
					{
						if ( playerLevel[15] >= 30 )
						{
							deleteItem( 99, getItemSlot( 99 ), 1 );
							deleteItem( 239, getItemSlot( 239 ), 1 );
							addItem( 133, 1 );
							addSkillXP( 575, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 99 ) && ( useWith == 231 ) )
					{
						if ( playerLevel[15] >= 38 )
						{
							deleteItem( 99, getItemSlot( 99 ), 1 );
							deleteItem( 231, getItemSlot( 231 ), 1 );
							addItem( 139, 1 );
							addSkillXP( 688, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 231 ) && ( useWith == 99 ) )
					{
						if ( playerLevel[15] >= 38 )
						{
							deleteItem( 99, getItemSlot( 99 ), 1 );
							deleteItem( 231, getItemSlot( 231 ), 1 );
							addItem( 139, 1 );
							addSkillXP( 688, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 101 ) && ( useWith == 221 ) )
					{
						if ( playerLevel[15] >= 45 )
						{
							deleteItem( 101, getItemSlot( 101 ), 1 );
							deleteItem( 221, getItemSlot( 221 ), 1 );
							addItem( 145, 1 );
							addSkillXP( 750, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 221 ) && ( useWith == 101 ) )
					{
						if ( playerLevel[15] >= 45 )
						{
							deleteItem( 101, getItemSlot( 101 ), 1 );
							deleteItem( 221, getItemSlot( 221 ), 1 );
							addItem( 145, 1 );
							addSkillXP( 750, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 101 ) && ( useWith == 235 ) )
					{
						if ( playerLevel[15] >= 48 )
						{
							deleteItem( 101, getItemSlot( 101 ), 1 );
							deleteItem( 235, getItemSlot( 235 ), 1 );
							addItem( 181, 1 );
							addSkillXP( 806, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 235 ) && ( useWith == 101 ) )
					{
						if ( playerLevel[15] >= 48 )
						{
							deleteItem( 101, getItemSlot( 101 ), 1 );
							deleteItem( 235, getItemSlot( 235 ), 1 );
							addItem( 181, 1 );
							addSkillXP( 806, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 103 ) && ( useWith == 231 ) )
					{
						if ( playerLevel[15] >= 50 )
						{
							deleteItem( 103, getItemSlot( 103 ), 1 );
							deleteItem( 231, getItemSlot( 231 ), 1 );
							addItem( 151, 1 );
							addSkillXP( 1913, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 231 ) && ( useWith == 103 ) )
					{
						if ( playerLevel[15] >= 50 )
						{
							deleteItem( 103, getItemSlot( 103 ), 1 );
							deleteItem( 231, getItemSlot( 231 ), 1 );
							addItem( 151, 1 );
							addSkillXP( 913, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 103 ) && ( useWith == 2970 ) )
					{
						if ( playerLevel[15] >= 52 )
						{
							deleteItem( 103, getItemSlot( 103 ), 1 );
							deleteItem( 2970, getItemSlot( 2970 ), 1 );
							addItem( 3018, 1 );
							addSkillXP( 988, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 2970 ) && ( useWith == 103 ) )
					{
						if ( playerLevel[15] >= 52 )
						{
							deleteItem( 103, getItemSlot( 103 ), 1 );
							deleteItem( 2970, getItemSlot( 2970 ), 1 );
							addItem( 3018, 1 );
							addSkillXP( 1018, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 105 ) && ( useWith == 225 ) )
					{
						if ( playerLevel[15] >= 55 )
						{
							deleteItem( 105, getItemSlot( 105 ), 1 );
							deleteItem( 225, getItemSlot( 225 ), 1 );
							addItem( 157, 1 );
							addSkillXP( 1250, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 225 ) && ( useWith == 105 ) )
					{
						if ( playerLevel[15] >= 55 )
						{
							deleteItem( 105, getItemSlot( 105 ), 1 );
							deleteItem( 225, getItemSlot( 225 ), 1 );
							addItem( 157, 1 );
							addSkillXP( 1250, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 105 ) && ( useWith == 241 ) )
					{
						if ( playerLevel[15] >= 60 )
						{
							deleteItem( 105, getItemSlot( 105 ), 1 );
							deleteItem( 241, getItemSlot( 241 ), 1 );
							addItem( 187, 1 );
							addSkillXP( 1380, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 241 ) && ( useWith == 105 ) )
					{
						if ( playerLevel[15] >= 60 )
						{
							deleteItem( 105, getItemSlot( 105 ), 1 );
							deleteItem( 241, getItemSlot( 241 ), 1 );
							addItem( 187, 1 );
							addSkillXP( 1380, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 109 ) && ( useWith == 245 ) )
					{
						if ( playerLevel[15] >= 72 )
						{
							deleteItem( 109, getItemSlot( 109 ), 1 );
							deleteItem( 245, getItemSlot( 245 ), 1 );
							addItem( 169, 1 );
							addSkillXP( 1630, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 245 ) && ( useWith == 109 ) )
					{
						if ( playerLevel[15] >= 72 )
						{
							deleteItem( 109, getItemSlot( 109 ), 1 );
							deleteItem( 245, getItemSlot( 245 ), 1 );
							addItem( 169, 1 );
							addSkillXP( 1630, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 111 ) && ( useWith == 247 ) )
					{
						if ( playerLevel[15] >= 78 )
						{
							deleteItem( 111, getItemSlot( 111 ), 1 );
							deleteItem( 247, getItemSlot( 247 ), 1 );
							addItem( 121, 1 );
							addSkillXP( 1890, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 247 ) && ( useWith == 111 ) )
					{
						if ( playerLevel[15] >= 78 )
						{
							deleteItem( 111, getItemSlot( 111 ), 1 );
							deleteItem( 247, getItemSlot( 247 ), 1 );
							addItem( 189, 1 );
							addSkillXP( 1750, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 2483 ) && ( useWith == 241 ) )
					{
						if ( playerLevel[15] >= 69 )
						{
							deleteItem( 2483, getItemSlot( 2483 ), 1 );
							deleteItem( 241, getItemSlot( 241 ), 1 );
							addItem( 2454, 1 );
							addSkillXP( 1580, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 221 ) && ( useWith == 2483 ) )
					{
						if ( playerLevel[15] >= 69 )
						{
							deleteItem( 2483, getItemSlot( 2483 ), 1 );
							deleteItem( 241, getItemSlot( 241 ), 1 );
							addItem( 2454, 1 );
							addSkillXP( 1580, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 2483 ) && ( useWith == 3138 ) )
					{
						if ( playerLevel[15] >= 76 )
						{
							deleteItem( 2483, getItemSlot( 2483 ), 1 );
							deleteItem( 3138, getItemSlot( 3138 ), 1 );
							addItem( 3042, 1 );
							addSkillXP( 1730, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}
					if ( ( itemUsed == 985 ) && ( useWith == 987 ) )
					{
						addItem( 989, 1 );
						deleteItem( 985, getItemSlot( 985 ), 1 );
						deleteItem( 987, getItemSlot( 987 ), 1 );
						sendMessage( "You join the two key pieces together to create a Crystal Key!." );
					}
					if ( ( itemUsed == 987 ) && ( useWith == 985 ) )
					{
						addItem( 989, 1 );
						deleteItem( 985, getItemSlot( 985 ), 1 );
						deleteItem( 987, getItemSlot( 987 ), 1 );
						sendMessage( "You join the two key pieces together to create a Crystal Key!." );
					}

					if ( ( itemUsed == 3138 ) && ( useWith == 2483 ) )
					{
						if ( playerLevel[15] >= 76 )
						{
							deleteItem( 2483, getItemSlot( 2483 ), 1 );
							deleteItem( 3138, getItemSlot( 3138 ), 1 );
							addItem( 3042, 1 );
							addSkillXP( 1730, 15 );
						}
						else
						{
							sendMessage( "You need a higher herblore level to make this potion." );
						}
					}

					break;

				// walkTo commands
				case 248:
					playersOnline_and_Uptime();
					if ( ( absY >= 3260 ) && ( absY <= 3289 ) && ( absX <= 3384 ) && ( absX >= 3345 ) )
					{
						{
							if ( !hasDuelSign )
							{
								hasDuelSign = true;
								outStream.createFrame( 208 );
								outStream.writeWordBigEndian_dup( 201 );



							}
							else
							{

								hasDuelSign = false;
								wildysigndisappear();


							}

						}
					}
					if ( isinjaillolz == 1 && !isinJail( absX, absY, 1 ) )
					{
						teleportToX = 3109;
						teleportToY = 3514;
					}



					isInDuelArena( absX, absY, 1 );
					// map walk (has additional 14 bytes added to the end with some junk
					// data)
					packetSize -= 14; // ignore the junk
					goto case 164;
				case 164:
					playersOnline_and_Uptime();
					if ( ( absY >= 3260 ) && ( absY <= 3289 ) && ( absX <= 3384 ) && ( absX >= 3345 ) )
					{
						{
							if ( !hasDuelSign )
							{
								hasDuelSign = true;
								outStream.createFrame( 208 );
								outStream.writeWordBigEndian_dup( 201 );



							}
							else
							{

								hasDuelSign = false;
								wildysigndisappear();


							}

						}
					}
					if ( isinjaillolz == 1 && !isinJail( absX, absY, 1 ) )
					{
						teleportToX = 3109;
						teleportToY = 3514;
					}



					isInDuelArena( absX, absY, 1 );
					goto case 98;
				// regular walk
				case 98: // walk on command
					playersOnline_and_Uptime();
					if ( ( absY >= 3260 ) && ( absY <= 3289 ) && ( absX <= 3384 ) && ( absX >= 3345 ) )
					{
						{
							if ( !hasDuelSign )
							{
								hasDuelSign = true;
								outStream.createFrame( 208 );
								outStream.writeWordBigEndian_dup( 201 );



							}
							else
							{

								hasDuelSign = false;
								wildysigndisappear();


							}

						}
					}
					if ( isinjaillolz == 1 && !isinJail( absX, absY, 1 ) )
					{
						teleportToX = 3109;
						teleportToY = 3514;
					}



					isInDuelArena( absX, absY, 1 );
					startedrange = false;
					isattackingaplayer = false;
					if ( faceNPC > 0 )
					{
						IsAttackingNPC = false;
						faceNPCupdate = true;
					}
					if ( randomed )
						break;
					if ( !antiHax() )
						break;
					if ( inTrade )
						break;
					if ( TimeHelper.CurrentTimeMillis() - lastEntangle < entangleDelay )
					{
						sendMessage( "A magical force stops you from moving." );
						break;
					}
					resetAction();
					if ( !validClient )
					{
						sendMessage( "You can't move on this account" );
						break;
					}
					if ( duelFight && ( duelRule[9] ) )
					{
						//sendMessage("You cannot retreat during this duel!");
						break;
					}
					if ( fighting )
					{
						client enemy = ( client ) PlayerHandler.players[fightId];
						// sendMessage("attack=" + fightId + ", hits=" + hits + ",
						// fighting=" + fighting);
						if ( ( fightId > 0 ) && ( enemy != null ) && ( hits < 3 )
								&& ( enemy.hits < 3 ) && inRange( enemy.absX, enemy.absY ) )
						{
							sendMessage( "You cannot retreat during the first 3 rounds of combat!" );
							break;
						}
						else if ( ( ( fightId > 0 ) && ( enemy != null ) && fighting )
							  || ( !inRange( enemy.absX, enemy.absY ) ) )
						{
							fighting = false;
							enemy.fighting = false;
							hits = 0;
							enemy.hits = 0;
						}
						else
						{
							fighting = false;
							hits = 0;
						}
					}
					IsAttackingNPC = false;
					attacknpc = -1;
					closeInterface();
					resetAnimation();
					if ( deathStage == 0 )
					{
						newWalkCmdSteps = packetSize - 5;
						if ( newWalkCmdSteps % 2 != 0 )
						{
							println_debug( "Warning: walkTo(" + packetType
									+ ") command malforeachmedin "
									+ misc.Hex( inStream.buffer, 0, packetSize ) );
						}
						newWalkCmdSteps /= 2;
						if ( ++newWalkCmdSteps > walkingQueueSize )
						{
							println_debug( "Warning: walkTo(" + packetType
									+ ") command contains too many steps ("
									+ newWalkCmdSteps + ")." );
							newWalkCmdSteps = 0;
							break;
						}
						int firstStepX = inStream.readSignedWordBigEndianA();
						int tmpFSX = firstStepX;

						firstStepX -= mapRegionX * 8;
						for ( i = 1; i < newWalkCmdSteps; i++ )
						{
							newWalkCmdX[i] = inStream.readSignedByte();
							newWalkCmdY[i] = inStream.readSignedByte();
							tmpNWCX[i] = newWalkCmdX[i];
							tmpNWCY[i] = newWalkCmdY[i];
						}
						newWalkCmdX[0] = newWalkCmdY[0] = tmpNWCX[0] = tmpNWCY[0] = 0;
						int firstStepY = inStream.readSignedWordBigEndian();
						int tmpFSY = firstStepY;

						firstStepY -= mapRegionY * 8;
						newWalkCmdIsRunning = inStream.readSignedByteC() == 1;
						if ( isInWilderness( absX, absY, 1 ) )
							newWalkCmdIsRunning = false;
						for ( i = 0; i < newWalkCmdSteps; i++ )
						{
							newWalkCmdX[i] += ( SByte ) firstStepX;
							newWalkCmdY[i] += ( SByte ) firstStepY;
						}
						poimiY = firstStepY;
						poimiX = firstStepX;

						// stairs check
						if ( stairs > 0 )
						{
							resetStairs();
						}
						// woodcutting check
						if ( woodcutting[0] > 0 )
						{
							// playerEquipment[playerWeapon] = OriginalWeapon;
							// OriginalWeapon = -1;
							resetAnimation();
							resetWC();
						}
						// pick up item check
						if ( WannePickUp == true )
						{
							PickUpID = 0;
							PickUpAmount = 0;
							PickUpDelete = 0;
							WannePickUp = false;
						}
						// attack check
						if ( IsAttacking == true )
						{
							ResetAttack();
						}
						// attack NPC check
						if ( IsAttackingNPC == true )
						{
							ResetAttackNPC();
						}
						// mining check
						if ( mining[0] > 0 )
						{
							resetAnimation();
							resetMI();
						}
						// smithing check
						if ( smithing[0] > 0 )
						{
							/*
							 * playerEquipment[playerWeapon] = OriginalWeapon;
							 * OriginalWeapon = -1; playerEquipment[playerShield] =
							 * OriginalShield; OriginalShield = -1;
							 */
							updateRequired = true;
							appearanceUpdateRequired = true;
							resetAnimation();
							resetSM();
							RemoveAllWindows();
						}
						// Npc Talking
						if ( NpcDialogue > 0 )
						{
							NpcDialogue = 0;
							NpcTalkTo = 0;
							NpcDialogueSend = false;
							RemoveAllWindows();
						}
						// banking
						if ( IsBanking == true )
						{
							RemoveAllWindows();
						}
						// shopping
						if ( IsShopping == true )
						{
							IsShopping = false;
							MyShopID = 0;
							UpdateShop = false;
							RemoveAllWindows();
						}
						// trading
						// firemaking check
						if ( firemaking[0] > 0 )
						{
							playerEquipment[playerWeapon] = OriginalWeapon;
							OriginalWeapon = -1;
							playerEquipment[playerShield] = OriginalShield;
							OriginalShield = -1;
							resetAnimation();
							resetFM();
						}

					}
					break;

				case 4:
					// regular chat
					if ( !validClient )
					{
						sendMessage( "Please use another client" );
						break;
					}
					if ( muted )
						break;
					MBTC = misc.textUnpack( chatText, packetSize - 2 );
					MBBC = misc.textUnpack( chatText, packetSize - 2 );
					MBHT = misc.textUnpack( chatText, packetSize - 2 );
					MBID = misc.textUnpack( chatText, packetSize - 2 );
					chatTextEffects = inStream.readUnsignedByteS();
					chatTextColor = inStream.readUnsignedByteS();
					chatTextSize = ( byte ) ( packetSize - 2 );
					inStream.readBytes_reverseA( chatText, chatTextSize, 0 );
					chatTextUpdateRequired = true;
					println_debug( "Text [" + chatTextEffects + "," + chatTextColor
							+ "]: " + misc.textUnpack( chatText, packetSize - 2 ) );
					break;

				case 14:
					// Use something on another player
					junk2 = inStream.readSignedWordBigEndianA(); // only needed to
																 // get the cracker
																 // slot ! (remove =
																 // server crash !)
					junk = inStream.readSignedWordBigEndian(); // only needed to get
															   // the cracker slot !
															   // (remove = server
															   // crash !)
					junk3 = inStream.readUnsignedWordA(); // only needed to get the
														  // cracker slot ! (remove =
														  // server crash !)
					int CrackerSlot = inStream.readSignedWordBigEndian();
					// if(CrackerSlot >= playerItems.Length){
					// break;
					// }
					int CrackerID = playerItems[CrackerSlot];

					CrackerID -= 1; // Only to fix the ID !
					if ( ( CrackerID == 962 ) && playerHasItem( 962 ) )
					{
						sendMessage( "You crack the cracker..." );
						int UsedOn = ( int ) ( misc.HexToInt( inStream.buffer, 3, 1 ) / 1000 );

						PlayerHandler.players[UsedOn].CrackerMsg = true;
						deleteItem( CrackerID, CrackerSlot, playerItemsN[CrackerSlot] );
						if ( misc.random( 2 ) == 1 )
						{
							addItem( Item.randomPHat(), 1 );
							sendMessage( "And you get the crackers item." );
						}
						else
						{
							sendMessage( "but you didn't get the crackers item." );
							PlayerHandler.players[UsedOn].CrackerForMe = true;
						}
					}
					break;

				// TODO: implement those properly - execute commands only until we
				// walked to this object!
				// atObject commands

				/*
				 * <Dungeon> Trapdoors: ID 1568, 1569, 1570, 1571 Ladders: ID 1759, 2113
				 * Climb rope: 1762, 1763, 1764
				 */

				case 101:
					// Character Design Screen
					if ( !antiHax() )
						break;
					int[] input = new int[13];
					int highest = -1,
					numZero = -1,
					num44 = 0;
					for ( int b = 0; b < 13; b++ )
					{
						input[b] = inStream.readSignedByte();
						println( "C: " + b + " " + input[b] );
						if ( input[b] > highest )
							highest = input[b];
						if ( input[b] < 1 )
							numZero++;
						if ( input[b] < 0 ) // bakatool female fix.
							input[b] = 0;
						if ( input[b] == 44 )
							num44++;
					}
					if ( ( highest < 1 ) || ( num44 == 7 ) )
					{
						if ( uid > 1 )
							server.bannedUid.Add( uid );
						isKicked = true;
						break;
					}
					pGender = input[0];
					println( "gender " + pGender );
					pHead = input[1];
					pBeard = input[2]; // aka Jaw :S -bakatool
					pTorso = input[3];
					pArms = input[4];
					pHands = input[5];
					pLegs = input[6];
					pFeet = input[7];
					pHairC = input[8];
					pTorsoC = input[9];
					pLegsC = input[10];
					pFeetC = input[11];
					pSkinC = input[12];
					playerLook[0] = input[0]; // pGender -bakatool
					playerLook[1] = input[8]; // hairC -bakatool
					playerLook[2] = input[9]; // torsoC -bakatool
					playerLook[3] = input[10]; // legsC -bakatool
					playerLook[4] = input[11]; // feetC -bakatool
					playerLook[5] = input[12]; // skinC -bakatool
					apset = true;
					appearanceUpdateRequired = true;
					lookUpdate = true;
					break;
				case 132:
					//int objectID = inStream.readUnsignedWord();
					//int objectY = inStream.readUnsignedWordA();
					//int objectX = inStream.readUnsignedByteA();
					int objectX = inStream.readSignedLEShortA();
					int objectID = inStream.readShort();
					int objectY = inStream.readShortA();
					int face = 0;
					int face2 = 0;
					int GateID = 1;
					if ( !validClient || randomed )
						break;
					//if ( !antiHax() )
					//	break;
					if ( debug || ( playerRights > 1 ) )
					{
						println( "serverobjs size " + server.objects.Count );
						println_debug( "atObject: " + objectX + "," + objectY
								+ " objectID: " + objectID ); // 147 might be id for
															  // object state changing
					}
					int xDiff = Math.Abs( absX - objectX );
					int yDiff = Math.Abs( absY - objectY );
					Boolean found = false;
					foreach ( Object o in server.objects )
					{
						if ( ( o.type == 1 ) && ( objectX == o.x ) && ( objectY == o.y ) && ( objectID == o.id ) )
						{
							found = true;
							break;
						}
					}
					if ( !found && adding )
					{
						server.objects.Add( new Object( objectID, objectX, objectY, 1 ) );
						found = true;
					}

					if ( !found && ( objectID != 2646 ) && ( objectID != 1530 ) )
						break;
					resetAction( false );
					TurnPlayerTo( objectX, objectY );
					updateRequired = true;
					appearanceUpdateRequired = true;
					if ( ( xDiff > 5 ) || ( yDiff > 5 ) )
					{
						// sendMessage("Client hack detected!");
						break;
					}
					if ( objectID == 2107 )
					{
						if ( TimeHelper.CurrentTimeMillis() - server.lastRunite < 60000 )
						{
							println( "invalid timer" );
							break;
						}
					}

					if ( misc.random( 100 ) == 1 )
					{
						triggerRandom();
						break;
					}
					if ( ( objectID == 1733 ) && ( objectX == 2724 ) && ( objectY == 3374 ) )
					{
						if ( !premium )
							resetPos();
						teleportToX = 2727;
						teleportToY = 9774;
						heightLevel = 0;
						break;
					}
					if ( ( objectID == 3193 ) )
					{
						if ( duelFight == true )
						{
							sendMessage( "You may not use your bank when ur in duel" );
						}
						else
						{
							skillX = objectX;
							skillY = objectY;
							WanneBank = 1;
						}
					}
					if ( ( objectID == 1734 ) && ( objectX == 2724 ) && ( objectY == 9774 ) )
					{
						if ( !premium )
							resetPos();
						teleportToX = 2723;
						teleportToY = 3375;
						heightLevel = 0;
						break;
					}

					if ( ( objectID == 375 )
							&& ( ( ( objectX == 2593 ) && ( objectY == 3108 ) ) || ( ( objectX == 2590 ) && ( objectY == 3106 ) ) ) )
					{
						if ( TimeHelper.CurrentTimeMillis() - lastAction < ( 1000 + misc
								.random( 200 ) ) )
						{
							sendMessage( "You can't try that often!" );
							lastAction = TimeHelper.CurrentTimeMillis();
							break;
						}
						if ( playerLevel[playerThieving] < 70 )
						{
							sendMessage( "You must be level 70 thieving to open this chest" );
							break;
						}
						lastAction = TimeHelper.CurrentTimeMillis();
						if ( TimeHelper.CurrentTimeMillis() - PlayerHandler.lastChest >= 15000 )
						{
							PlayerHandler.lastChest = TimeHelper.CurrentTimeMillis();
							double roll = MathHelper.Random() * 100;
							if ( roll < 0.3 )
							{
								int[] items = { 2577, 2580, 2631 };
								int r = ( int ) (MathHelper.Random() * items.Length );
								sendMessage( "You have recieved a "
										+ getItemName( items[r] ) + "!" );
								addItem( items[r], 1 );
							}
							else
							{
								int coins = misc.random( 7000 );
								sendMessage( "You find " + coins
										+ " coins inside the chest" );
								addItem( 995, coins );
							}
							for ( int p = 0; p < PlayerHandler.maxPlayers; p++ )
							{
								client player = ( client ) PlayerHandler.players[p];
								if ( player == null )
									continue;
								if ( ( player.playerName != null )
										&& ( player.heightLevel == heightLevel )
										&& !player.disconnected
										&& ( Math.Abs( player.absY - absY ) < 30 )
										&& ( Math.Abs( player.absX - absX ) < 30 ) )
								{
									player.stillgfx( 444, objectY, objectX );
								}
							}
						}
						else
						{
							sendMessage( "The chest is empty!" );
						}
					}
					if ( ( objectID == 6420 ) && premium )
					{
						if ( TimeHelper.CurrentTimeMillis() - lastAction < ( 1000 + misc
								.random( 200 ) ) )
						{
							sendMessage( "You can't try that often!" );
							lastAction = TimeHelper.CurrentTimeMillis();
							break;
						}
						if ( playerLevel[playerThieving] < 85 )
						{
							sendMessage( "You must be level 85 thieving to open this chest" );
							break;
						}
						if ( !premium )
						{
							resetPos();
						}
						lastAction = TimeHelper.CurrentTimeMillis();
						if ( TimeHelper.CurrentTimeMillis() - PlayerHandler.lastChest2 >= 15000 )
						{
							PlayerHandler.lastChest2 = TimeHelper.CurrentTimeMillis();
							double roll =MathHelper.Random() * 100;
							if ( roll < 0.3 )
							{
								int[] items = { 1050, 2581, 2631 };
								int r = ( int ) (MathHelper.Random() * items.Length );
								sendMessage( "You have recieved a "
										+ getItemName( items[r] ) + "!" );
								addItem( items[r], 1 );
							}
							else
							{
								int coins = misc.random( 17000 );
								sendMessage( "You find " + coins
										+ " coins inside the chest" );
								addItem( 995, coins );
							}
							for ( int p = 0; p < PlayerHandler.maxPlayers; p++ )
							{
								client player = ( client ) PlayerHandler.players[p];
								if ( player == null )
									continue;
								if ( ( player.playerName != null )
										&& ( player.heightLevel == heightLevel )
										&& !player.disconnected
										&& ( Math.Abs( player.absY - absY ) < 30 )
										&& ( Math.Abs( player.absX - absX ) < 30 ) )
								{
									player.stillgfx( 444, objectY, objectX );
								}
							}
						}
						else
						{
							sendMessage( "The chest is empty!" );
						}
					}

					// FACE: 0= WEST | -1 = NORTH | -2 = EAST | -3 = SOUTH
					// ObjectType: 0-3 wall objects, 4-8 wall decoration, 9: diag.
					// walls, 10-11 world objects, 12-21: roofs, 22: floor decoration
					for ( int d = 0; d < DoorHandler.doorX.Length; d++ )
					{
						DoorHandler dh = server.doorHandler;
						if ( ( objectID == DoorHandler.doorId[d] ) && ( objectX == DoorHandler.doorX[d] )
								&& ( objectY == DoorHandler.doorY[d] ) )
						{
							int newFace = -3;
							if ( DoorHandler.doorState[d] == 0 )
							{
								// closed
								newFace = DoorHandler.doorFaceOpen[d];
								DoorHandler.doorState[d] = 1;
								DoorHandler.doorFace[d] = newFace;
							}
							else
							{
								newFace = DoorHandler.doorFaceClosed[d];
								DoorHandler.doorState[d] = 0;
								DoorHandler.doorFace[d] = newFace;
							}
							for ( int p = 0; p < PlayerHandler.maxPlayers; p++ )
							{
								client player = ( client ) PlayerHandler.players[p];
								if ( player == null )
									continue;
								if ( ( player.playerName != null )
										&& ( player.heightLevel == heightLevel )
										&& !player.disconnected && ( player.absY > 0 )
										&& ( player.absX > 0 ) )
								{
									player.ReplaceObject( DoorHandler.doorX[d], DoorHandler.doorY[d],
											DoorHandler.doorId[d], newFace, 0 );
								}
							}
						}
					}
					/*
					 * if(objectID == 1530){ if(objectX == 2716 && objectY == 3472){
					 * ReplaceObject(2716, 3472, 1530, -3, 0); } }
					 */
					// DAN
					 //if(objectID == 1530)
						//{ 
						//	if(objectX == 2716 && objectY == 3472){
						//		ReplaceObject(2716, 3472, 1530, -3, 0); 
						//	}
						//}
					
					if ( objectID == 2290 )
					{
						if ( ( objectX == 2576 ) && ( objectY == 9506 ) )
						{
							teleportToX = 2572;
							teleportToY = 9507;
						}
						else if ( ( objectX == 2573 ) && ( objectY == 9506 ) )
						{
							teleportToX = 2578;
							teleportToY = 9506;
						}
					}

					if ( ( objectID == 3443 ) && ( objectX == 3440 ) && ( objectY == 9886 ) )
					{
						// Holy barrier to canifis -bakatool
						teleportToX = 3422;
						teleportToY = 3484;
					}

					if ( ( objectID == 3432 ) && ( objectX == 3422 ) && ( objectY == 3485 ) )
					{
						// canifis to holy barrier -bakatool
						teleportToX = 3440;
						teleportToY = 9887;
					}
					if ( objectID == 2321 )
					{
						if ( playerHasItem( 1544 ) )
						{
							teleportToX = 2598;
							teleportToY = 9495;
						}
						else
						{
							sendMessage( "You need an orange key to cross" );
						}
					}
					if ( objectID == 2318 )
					{
						teleportToX = 2621;
						teleportToY = 9496;
						updateRequired = true;
					}
					if ( objectID == 1728 )
					{
						teleportToX = 2614;
						teleportToY = 9505;
						updateRequired = true;
					}
					if ( ( objectID == 6836 )
							&& ( TimeHelper.CurrentTimeMillis() - lastAction >= ( 2000 + misc
									.random( 200 ) ) ) )
					{
						if ( !( ( ( objectX == 2604 ) || ( objectX == 2606 ) || ( objectX == 2608 ) ) && ( objectY == 3104 ) ) )
						{
							resetPos();
							break;
						}
						lastAction = TimeHelper.CurrentTimeMillis();
						EntangleDelay = 6;
						stealtimer = 5;
						// snaretimer = 5;
						actionTimer = 4;
						setAnimation( 881 );
						addSkillXP( 185, 17 );
						AnimationReset = true;
						updateRequired = true;
						appearanceUpdateRequired = true;
					}
					if ( objectID == 881 )
					{
						heightLevel -= 1;
					}
					if ( ( objectID == 1591 ) && ( objectX == 3268 ) && ( objectY == 3435 ) )
					{
						if ( combatLevel >= 80 )
						{
							teleportToX = 2540;
							teleportToY = 4716;
						}
						else
						{
							sendMessage( "You need to be level 80 or above to enter the mage arena." );
							sendMessage( "The skeletons at the varrock castle are a good place until then." );
						}
					}

					// Wo0t Tzhaar Objects

					if ( ( objectID == 9369 ) && ( objectX == 2399 ) && ( objectY == 5176 ) )
					{
						// Hot vent door A
						if ( absY == 5177 )
						{
							teleportToX = 2399;
							teleportToY = 5175;

						}
					}
					if ( ( objectID == 9369 ) && ( objectX == 2399 ) && ( objectY == 5176 ) )
					{
						// Hot vent door AA
						if ( absY == 5175 )
						{
							teleportToX = 2399;
							teleportToY = 5177;

						}
					}

					if ( ( objectID == 9368 ) && ( objectX == 2399 ) && ( objectY == 5168 ) )
					{
						// Hot vent door B
						if ( absY == 5169 )
						{
							teleportToX = 2399;
							teleportToY = 5167;

						}
					}
					if ( ( objectID == 9368 ) && ( objectX == 2399 ) && ( objectY == 5168 ) )
					{
						// Hot vent door BB
						if ( absY == 5167 )
						{
							teleportToX = 2399;
							teleportToY = 5169;

						}
					}
					if ( ( objectID == 9391 ) && ( objectX == 2399 ) && ( objectY == 5172 ) )
					{
						// Tzhaar Fight bank
						openUpBank();
					}
					if ( objectID == 3203 && duelFight == true )
					{
						if ( duelRule[7] )
						{
							getClient( duel_with ).DuelVictory();
							sendMessage( "You have forfeit!" );
						}
						else
						{
							sendMessage( "Forfeit has been disabled!" );
						}
					}

					if ( objectID == 733 && TimeHelper.CurrentTimeMillis() - lastAction > actionInterval )
					{
						if ( playerEquipment[playerWeapon] == -1 )
						{
							sendMessage( "You need to equip a weapon first before you do that." );
						}
						else
						{
							if ( objectX == 3106 && objectY == 3958 )
							{
								if ( absX == 3106 && absY == 3959 || absX == 3106 && absY == 3957 )
								{
									ReplaceServerObject( 3106, 3958, 6951, -1, 10 );
									actionInterval = 5000;
									lastAction = TimeHelper.CurrentTimeMillis();
									sendMessage( "You slash through the web." );
									setAnimation( 451 );
								}
							}
							if ( objectX == 3105 && objectY == 3958 )
							{
								if ( absX == 3105 && absY == 3959 || absX == 3105 && absY == 3957 )
								{
									ReplaceServerObject( 3105, 3958, 6951, -1, 10 );
									actionInterval = 5000;
									lastAction = TimeHelper.CurrentTimeMillis();
									sendMessage( "You slash through the web." );
									setAnimation( 451 );
								}
							}
							if ( objectX == 3095 && objectY == 3957 )
							{
								if ( absX == 3096 && absY == 3957 || absX == 3094 && absY == 3957 )
								{
									ReplaceServerObject( 3095, 3957, 6951, -1, 10 );
									actionInterval = 5000;
									lastAction = TimeHelper.CurrentTimeMillis();
									sendMessage( "You slash through the web." );
									setAnimation( 451 );
								}
							}
							if ( objectX == 3093 && objectY == 3957 )
							{
								if ( absX == 3094 && absY == 3957 || absX == 3092 && absY == 3957 )
								{
									ReplaceServerObject( 3093, 3957, 6951, -1, 10 );
									actionInterval = 5000;
									lastAction = TimeHelper.CurrentTimeMillis();
									sendMessage( "You slash through the web." );
									setAnimation( 451 );
								}
							}
							if ( objectX == 3158 && objectY == 3951 )
							{
								if ( absX == 3158 && absY == 3952 || absX == 3158 && absY == 3950 )
								{
									ReplaceServerObject( 3158, 3951, 6951, -1, 10 );
									actionInterval = 5000;
									lastAction = TimeHelper.CurrentTimeMillis();
									sendMessage( "You slash through the web." );
									setAnimation( 451 );
								}
							}
						}
					}
					if ( objectID == 5959 && absX == 3090 && absY == 3956 )
					{ //mage bank: from wild to safe
						triggerTele2( 2539, 4712, 0 );
					}
					if ( ( objectID == 5960 ) && ( objectX == 2539 ) && ( objectY == 4712 ) )
					{ //mage bank: from saf to wild
						triggerTele2( 3090, 3956, 0 );
					}
					if ( ( objectID == 1815 ) && ( absX == 3153 ) && ( absY == 3923 ) )
					{ //wild to ardougne
						triggerTele2( 2561, 3311, 0 );
					}
					if ( ( objectID == 1814 ) && ( absX == 2561 ) && ( absY == 3311 ) )
					{ //ardougne to wild
						if ( Ardougnewarning == false )
						{
							sendMessage( "By pulling this lever you will teleport deep into the wilderness." );
							sendMessage( "Try to pull it again if you dare to risk your life!" );
							Ardougnewarning = true;
						}
						else
						{
							triggerTele2( 3153, 3923, 0 );
						}
					}
					if ( ( objectID == 9356 ) && ( objectX == 2437 ) && ( objectY == 5166 ) )
					{
						// Tzhaar Jad Cave Enterance
						teleportToX = 2413;
						teleportToY = 5117;
						sendMessage( "You have entered the Jad Cave." );
					}
					if ( ( objectID == 9357 ) && ( objectX == 2412 ) && ( objectY == 5118 ) )
					{
						// Tzhaar Jad Cave Exit
						teleportToX = 2438;
						teleportToY = 5168;
						sendMessage( "You have left the Jad Cave." );
					}

					// End of Tzhaar Objects

					if ( objectID == 2213 )
					{ // Bank Booth
						if ( duelFight == true )
						{
							sendMessage( "Sorry you cant open you bank in duel!" );
						}
						else
						{
							skillX = objectX;
							skillY = objectY;
							NpcWanneTalk = 2;
						}
					}

					if ( objectID == 6552 )
					{
						// Ancient magic altar (temp !!!)
						if ( GoodDistance( absX, absY, objectX, objectY, 1 ) == true )
						{
							if ( playerAncientMagics == true )
							{
								// setSidebarInterface(6, 1151); // magic tab (ancient =
								// 12855);
								// playerAncientMagics = false;
							}
							else
							{
								setSidebarInterface( 6, 12855 ); // magic tab (ancient =
																 // 12855);
								playerAncientMagics = true;
							}
						}
					}
					if ( objectID == 6420 )
					{
						if ( playerHasItem( 989 ) )
						{//weeed
						 //addItem(ChestLoot,misc.random(4));
							deleteItem( 989, getItemSlot( 989 ), 1 );
							sendMessage( "You use your Crystal key on the chest and receive some loot!." );
						}
						else
						{
							sendMessage( "You need a Crystal key to open this chest!." );
							break;
						}
					}
					// woodCutting
					// mining
					// if (actionTimer == 0) {
					if ( CheckObjectSkill( objectID ) == true )
					{
						IsUsingSkill = true;
						skillX = objectX;
						skillY = objectY;
					}
					// }
					// go upstairs
					if ( true )
					{
						if ( ( objectID == 1747 ) || ( objectID == 1750 ) )
						{
							stairs = 1;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 1738 )
						{
							stairs = 1;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 2;
						}
						else if ( objectID == 6420 )
						{

							if ( playerHasItem( 989 ) )
							{//weeed
								deleteItem( 989, getItemSlot( 989 ), 1 );
								sendMessage( "You use your Crystal key on the chest and receive some loot!." );
							}
							else
							{
								sendMessage( "You need a Crystal key to open this chest!." );
								break;
							}

						}
						else if ( objectID == 1722 )
						{
							stairs = 21;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 2;
							stairDistanceAdd = 2;
						}
						else if ( objectID == 1734 )
						{
							stairs = 10;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 3;
							stairDistanceAdd = 1;
						}
						else if ( objectID == 55 )
						{
							stairs = 15;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 3;
							stairDistanceAdd = 1;
						}
						else if ( objectID == 57 )
						{
							stairs = 15;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 3;
						}
						else if ( ( objectID == 1755 ) || ( objectID == 5946 )
							  || ( objectID == 1757 ) )
						{
							stairs = 4;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 1764 )
						{
							stairs = 12;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 2148 )
						{
							stairs = 8;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 3608 )
						{
							stairs = 13;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 2408 )
						{
							stairs = 16;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 5055 )
						{
							stairs = 18;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 5131 )
						{
							stairs = 20;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 9359 )
						{
							stairs = 24;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
							stairDistance = 1;
						}
						else if ( objectID == 2492 )
						{
							/* Essence Mine Portals */
							stairs = 25;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 2406 )
						{
							/* Lost City Door */
							if ( playerEquipment[playerWeapon] == 772 )
							{
								// Dramen Staff
								stairs = 27;
								skillX = objectX;
								skillY = objectY;
								stairDistance = 1;
							}
							else
							{
								// Open Door
							}
						}
						// go downstairs
						if ( ( objectID == 1746 ) || ( objectID == 1749 ) )
						{
							stairs = 2;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 1740 )
						{
							stairs = 2;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 1723 )
						{
							stairs = 22;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 2;
							stairDistanceAdd = 2;
						}
						else if ( objectID == 1733 )
						{
							if ( playerHasItem( 1543 ) )
							{
								stairs = 9;
								skillX = objectX;
								skillY = objectY;
								stairDistance = 3;
								stairDistanceAdd = -1;
							}
							else
							{
								sendMessage( "You need a red key to go down these stairs" );
								break;
							}
						}
						else if ( objectID == 54 )
						{
							stairs = 14;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 3;
							stairDistanceAdd = 1;
						}
						else if ( objectID == 56 )
						{
							stairs = 14;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 3;
						}
						else if ( ( objectID == 1568 ) || ( objectID == 5947 )
							  || ( objectID == 6434 ) || ( objectID == 1759 )
							  || ( objectID == 1754 ) || ( objectID == 1570 ) )
						{
							stairs = 3;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 2113 )
						{
							// Mining guild stairs
							if ( playerLevel[playerMining] >= 60 )
							{
								stairs = 3;
								skillX = objectX;
								skillY = objectY;
								stairDistance = 1;
							}
							else
							{
								sendMessage( "You need 60 mining to enter the mining guild." );
							}
						}
						else if ( objectID == 492 )
						{
							stairs = 11;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 2;
						}
						else if ( objectID == 2147 )
						{
							stairs = 7;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 5054 )
						{
							stairs = 17;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 5130 )
						{
							stairs = 19;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 9358 )
						{
							stairs = 23;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}
						else if ( objectID == 5488 )
						{
							stairs = 28;
							skillX = objectX;
							skillY = objectY;
							stairDistance = 1;
						}

						if ( ( skillX > -1 ) && ( skillY > -1 ) )
						{
							IsUsingSkill = true;
						}
					}
					break;

				case 252:
					// atObject2
					objectID = inStream.readUnsignedWordBigEndianA(); // 5292
																	  // bankwindow
					objectY = inStream.readSignedWordBigEndian();
					objectX = inStream.readUnsignedWordA();
					if ( debug || ( playerRights > 1 ) )
						println_debug( "atObject2: " + objectX + "," + objectY
								+ " objectID: " + objectID );
					if ( !antiHax() )
						break;
					long now = TimeHelper.CurrentTimeMillis();
					Boolean oFound = false;
					foreach ( Object o in server.objects )
					{
						if ( ( o.type == 2 ) && ( objectX == o.x ) && ( objectY == o.y )
								&& ( objectID == o.id ) )
						{
							oFound = true;
							break;
						}
					}

					if ( !oFound && adding )
						server.objects.Add( new Object( objectID, objectX, objectY, 2 ) );

					if ( !oFound && ( objectID != 2646 ) )
						break;
					if ( ( objectID == 2646 ) && ( ( absX >= 2735 ) && ( absX <= 2752 ) )
							&& ( ( absY >= 3435 ) && ( absY <= 3453 ) ) )
					{
						if ( now - lastAction >= 900 )
						{
							addItem( 1779, 1 );
							lastAction = now;
						}
					}
					if ( ( objectID == 2644 ) && ( objectX == 2710 ) && ( objectY == 3471 ) )
					{
						spinning = true;
						pEmote = 894;
						updateRequired = true;
						appearanceUpdateRequired = true;
					}
					if ( objectID == 823 )
					{
						Random r = new Random();
						teleportToX = 2602 + r.Next( 5 );
						teleportToY = 3162 + r.Next( 5 );
					}
					if ( objectID == 2561 )
					{
						if ( misc.random( 4 ) == 1 )
						{
							TheifStall( "cake stall", "and recieve a bread.", 1, 160, 2309, 1, 0x340 );
						}
						else if ( misc.random( 4 ) == 2 )
						{
							TheifStall( "cake stall", "and recieve a cake.", 1, 160, 1891, 1, 0x340 );
						}
						else if ( misc.random( 4 ) == 3 )
						{
							TheifStall( "cake stall", "and recieve a meat pie.", 1, 160, 2327, 1, 0x340 );
						}
						else if ( misc.random( 4 ) == 4 )
						{
							TheifStall( "cake stall", "and recieve a chocolate cake slice.", 1, 160, 1901, 1, 0x340 );
						}
					}
					if ( objectID == 2560 )
					{
						TheifStall( "silk stall", "and recieve some silk.", 20, 240, 950, 1, 0x340 );
					}
					if ( objectID == 2563 )
					{
						TheifStall( "fur stall", "and recieve some grey wolf fur.", 35, 360, 958, 1, 0x340 );
					}
					if ( objectID == 2565 )
					{
						TheifStall( "silver stall", "and recieve a silver ore.", 50, 540, 442, 1, 0x340 );
					}
					if ( objectID == 2564 )
					{
						TheifStall( "spice stall", "and recieve some spice.", 65, 810, 2007, 1, 0x340 );
					}
					if ( objectID == 2562 )
					{
						if ( misc.random( 100 ) == 1 || misc.random( 100 ) == 2 || misc.random( 100 ) == 3 || misc.random( 100 ) == 4 || misc.random( 100 ) == 5 || misc.random( 100 ) == 6 || misc.random( 100 ) == 7 || misc.random( 100 ) == 8 || misc.random( 100 ) == 9 || misc.random( 100 ) == 10 || misc.random( 100 ) == 11 || misc.random( 100 ) == 12 || misc.random( 100 ) == 13 || misc.random( 100 ) == 14 || misc.random( 100 ) == 15 || misc.random( 100 ) == 31 || misc.random( 100 ) == 32 || misc.random( 100 ) == 33 || misc.random( 100 ) == 34 || misc.random( 100 ) == 35 || misc.random( 100 ) == 36 || misc.random( 100 ) == 37 || misc.random( 100 ) == 38 || misc.random( 100 ) == 39 || misc.random( 100 ) == 40 || misc.random( 100 ) == 85 || misc.random( 100 ) == 87 || misc.random( 100 ) == 88 || misc.random( 100 ) == 89 || misc.random( 100 ) == 90 || misc.random( 100 ) == 91 || misc.random( 100 ) == 92 || misc.random( 100 ) == 86 )
						{
							TheifStall( "gem stall", "and recieve an uncut sapphire.", 75, 1250, 1623, 1, 0x340 );
						}
						else if ( misc.random( 100 ) == 16 || misc.random( 100 ) == 17 || misc.random( 100 ) == 18 || misc.random( 100 ) == 19 || misc.random( 100 ) == 20 || misc.random( 100 ) == 21 || misc.random( 100 ) == 22 || misc.random( 100 ) == 23 || misc.random( 100 ) == 24 || misc.random( 100 ) == 25 || misc.random( 100 ) == 26 || misc.random( 100 ) == 27 || misc.random( 100 ) == 28 || misc.random( 100 ) == 29 || misc.random( 100 ) == 30 || misc.random( 100 ) == 41 || misc.random( 100 ) == 42 || misc.random( 100 ) == 43 || misc.random( 100 ) == 44 || misc.random( 100 ) == 45 || misc.random( 100 ) == 46 || misc.random( 100 ) == 47 || misc.random( 100 ) == 48 || misc.random( 100 ) == 49 || misc.random( 100 ) == 50 || misc.random( 100 ) == 93 || misc.random( 100 ) == 94 || misc.random( 100 ) == 95 || misc.random( 100 ) == 96 || misc.random( 100 ) == 97 || misc.random( 100 ) == 98 )
						{
							TheifStall( "gem stall", "and recieve an uncut emerald.", 75, 1250, 1621, 1, 0x340 );
						}
						else if ( misc.random( 100 ) == 51 || misc.random( 100 ) == 52 || misc.random( 100 ) == 53 || misc.random( 100 ) == 54 || misc.random( 100 ) == 55 || misc.random( 100 ) == 56 || misc.random( 100 ) == 57 || misc.random( 100 ) == 58 || misc.random( 100 ) == 59 || misc.random( 100 ) == 60 || misc.random( 100 ) == 61 || misc.random( 100 ) == 65 || misc.random( 100 ) == 66 || misc.random( 100 ) == 67 || misc.random( 100 ) == 68 || misc.random( 100 ) == 69 || misc.random( 100 ) == 70 || misc.random( 100 ) == 99 || misc.random( 100 ) == 100 )
						{
							TheifStall( "gem stall", "and recieve an uncut ruby.", 75, 1250, 1619, 1, 0x340 );
						}
						else if ( misc.random( 100 ) == 71 || misc.random( 100 ) == 72 || misc.random( 100 ) == 73 || misc.random( 100 ) == 74 || misc.random( 100 ) == 75 || misc.random( 100 ) == 76 || misc.random( 100 ) == 77 || misc.random( 100 ) == 78 || misc.random( 100 ) == 79 || misc.random( 100 ) == 80 )
						{
							TheifStall( "gem stall", "and recieve an uncut diamond.", 75, 1250, 1617, 1, 0x340 );
						}
						else if ( misc.random( 100 ) == 81 || misc.random( 100 ) == 82 || misc.random( 100 ) == 83 )
						{
							TheifStall( "gem stall", "and recieve an uncut dragonstone.", 75, 1250, 1631, 1, 0x340 );
						}
						else if ( misc.random( 100 ) == 84 )
						{
							TheifStall( "gem stall", "and recieve an uncut onyx.", 75, 1250, 6571, 1, 0x340 );
						}
					}

					if ( ( objectID == 2213 ) || ( objectID == 2214 ) || ( objectID == 3045 )
							|| ( objectID == 5276 ) || ( objectID == 6084 ) )
					{
						if ( duelFight == true )
						{
							sendMessage( "You may not use your bank when ur in duel" );
						}
						else
						{
							skillX = objectX;
							skillY = objectY;
							WanneBank = 1;
						}
					}

					/*
					 * else if (objectID == 1739) { heightLevel += 1; teleportToX =
					 * absX; WalkTo(0,0); }
					 */
					break;

				case 70:
					// atObject3
					objectX = inStream.readSignedWordBigEndian();
					objectY = inStream.readUnsignedWord();
					objectID = inStream.readUnsignedWordBigEndianA();
					if ( debug )
						println_debug( "atObject3: " + objectX + "," + objectY
								+ " objectID: " + objectID );

					Boolean oooFound = false;
					foreach ( Object o in server.objects )
					{
						if ( ( o.type == 3 ) && ( objectX == o.x ) && ( objectY == o.y )
								&& ( objectID == o.id ) )
						{
							oooFound = true;
							break;
						}
					}

					if ( !oooFound && adding )
						server.objects.Add( new Object( objectID, objectX, objectY, 3 ) );

					if ( oooFound && ( objectID == 1739 ) )
					{
						heightLevel -= 1;
						WalkTo( 0, 0 );
						WalkTo( 0, 0 );
					}
					break;

				case 95:
					// update chat
					Tradecompete = inStream.readUnsignedByte();
					Privatechat = inStream.readUnsignedByte();
					Publicchat = inStream.readUnsignedByte();
					for ( int i1 = 1; i1 < PlayerHandler.maxPlayers; i1++ )
					{
						if ( ( PlayerHandler.players[i1] != null )
								&& ( PlayerHandler.players[i1].isActive == true ) )
						{
								PlayerHandler.players[i1].pmupdate( playerId, GetWorld( playerId ) );
						}
					}
					break;

				case 188:
					// add friend
					friendUpdate = true;
					long friendtoadd = inStream.readQWord();
					Boolean CanAdd = true;

					foreach ( long element in friends )
					{
						if ( ( element != 0 ) && ( element == friendtoadd ) )
						{
							CanAdd = false;
							sendMessage( friendtoadd + " is already in your friendlist." );
						}
					}
					if ( CanAdd == true )
					{
						for ( int i1 = 0; i1 < friends.Length; i1++ )
						{
							if ( friends[i1] == 0 )
							{
								friends[i1] = friendtoadd;
								for ( int i2 = 1; i2 < PlayerHandler.maxPlayers; i2++ )
								{
									if ( ( PlayerHandler.players[i2] != null )
											&& PlayerHandler.players[i2].isActive
											&& ( misc
													.playerNameToInt64( PlayerHandler.players[i2].playerName ) == friendtoadd ) )
									{
										if ( ( playerRights >= 2 )
												|| ( PlayerHandler.players[i2].Privatechat == 0 )
												|| ( ( PlayerHandler.players[i2].Privatechat == 1 ) && PlayerHandler.players[i2]
														.isinpm( misc
																.playerNameToInt64( playerName ) ) ) )
										{
											loadpm( friendtoadd, GetWorld( i2 ) );
											break;
										}
									}
								}
								break;
							}
						}
					}
					break;

				case 215:
					// remove friend
					friendUpdate = true;
					long friendtorem = inStream.readQWord();

					for ( int i1 = 0; i1 < friends.Length; i1++ )
					{
						if ( friends[i1] == friendtorem )
						{
							friends[i1] = 0;
							break;
						}
					}
					break;

				case 133:
					// add ignore
					friendUpdate = true;
					long igtoadd = inStream.readQWord();

					for ( int i10 = 0; i10 < ignores.Length; i10++ )
					{
						if ( ignores[i10] == 0 )
						{
							ignores[i10] = igtoadd;
							break;
						}
					}
					break;

				case 74:
					// remove ignore
					friendUpdate = true;
					long igtorem = inStream.readQWord();

					for ( int i11 = 0; i11 < ignores.Length; i11++ )
					{
						if ( ignores[i11] == igtorem )
						{
							ignores[i11] = 0;
							break;
						}
					}
					break;

				case 126:
					// pm message
					long friendtosend = inStream.readQWord();
					byte[] pmchatText = new byte[100];
					int pmchatTextSize = ( byte ) ( packetSize - 8 );

					inStream.readBytes( pmchatText, pmchatTextSize, 0 );
					foreach ( long element in friends )
					{
						if ( element == friendtosend )
						{
							Boolean pmsent = false;

							for ( int i2 = 1; i2 < PlayerHandler.maxPlayers; i2++ )
							{
								if ( ( PlayerHandler.players[i2] != null )
										&& PlayerHandler.players[i2].isActive
										&& ( misc
												.playerNameToInt64( PlayerHandler.players[i2].playerName ) == friendtosend ) )
								{
									if ( ( playerRights >= 2 )
											|| ( PlayerHandler.players[i2].Privatechat == 0 )
											|| ( ( PlayerHandler.players[i2].Privatechat == 1 ) && PlayerHandler.players[i2]
													.isinpm( misc
															.playerNameToInt64( playerName ) ) ) )
									{
										PlayerHandler.players[i2].sendpm( misc
												.playerNameToInt64( playerName ),
												playerRights, pmchatText,
												pmchatTextSize );
										pmsent = true;
									}
									break;
								}
							}
							if ( !pmsent )
							{
								sendMessage( "Player currently not available" );
								break;
							}
						}
					}
					break;

				case 236:
					int itemY = inStream.readSignedWordBigEndian();
					int itemID = inStream.readUnsignedWord();
					int itemX = inStream.readSignedWordBigEndian();
					apickupid = itemID;
					apickupx = itemX;
					apickupy = itemY;
					break;

				case 73:

					// Attack (Wilderness)


					AttackingOn = inStream.readSignedWordBigEndian();

					IsAttacking = true;


					break;


				case 128:
					// Trade Request
					int temp = inStream.readUnsignedWord();
					if ( !antiHax() )
						break;
					if ( !inTrade )
					{
						trade_reqId = temp;
						tradeReq( trade_reqId );
					}
					break;

				/*
				 * case 153: // Duel req int PID = (misc.HexToInt(inStream.buffer, 0,
				 * packetSize) / 1000); client plyr = (client)
				 * PlayerHandler.players[PID]; if(!inDuel && ValidClient(PID) &&
				 * !plyr.inDuel && !plyr.inTrade && !inDuel && !inTrade){ duel_with =
				 * PID; duelReq(PID); break; }
				 * 
				 * break;
				 */
				case 153:
					// Duel req
					int PID = ( misc.HexToInt( inStream.buffer, 0, packetSize ) / 1000 );
					client plyr = getClient( PID );
					if ( !ValidClient( PID ) )
						break;
					DuelReq( PID );

					break;
				case 139:
					// Trade answer
					// WanneTradeWith = inStream.readSignedWordBigEndian();
					// WanneTrade = 2;
					trade_reqId = inStream.readSignedWordBigEndian();
					tradeReq( trade_reqId );

					break;
				case 199:
					// fags using xero's client
					sendMessage( "Please use another client to play." );
					validClient = false;
					disconnected = true;
					break;

				// break;

				case 237:
					// Magic on Items
					int castOnSlot = inStream.readSignedWord();
					int castOnItem = inStream.readSignedWordA();
					int e3 = inStream.readSignedWord();
					int castSpell = inStream.readSignedWordA();
					if ( !antiHax() )
						break;
					if ( playerName.ToLower() == "wolf" )
					{
						println_debug( "castOnSlot: " + castOnSlot + " castOnItem: "
								+ castOnItem + " e3: " + e3 + " castSpell: "
								+ castSpell );
					}
					int alchvaluez = ( int ) Math.Floor( GetItemShopValue( castOnItem, 0,
							castOnSlot ) );

					if ( ( playerItems[castOnSlot] - 1 ) != castOnItem )
					{
						sendMessage( "You don't have that item!" );
						break;
					}
					if ( !playerHasItem( castOnItem ) )
					{
						sendMessage( "You don't have that item!" );
						break;
					}

					if ( castSpell == 1178 ) //High Alch  with staffs and Fire runes
					{
						if ( playerLevel[6] >= 55 )
						{
							if ( ( playerHasItemAmount( 561, 1 ) == false ) || ( playerHasItemAmount( 554, 5 ) == false ) && playerEquipment[playerWeapon] != 1387 || ( playerEquipment[playerWeapon] == 1387 ) && ( playerHasItemAmount( 561, 1 ) == false ) )
							{
								sendMessage( "You do not have enough runes to cast this spell." );
							}
							if ( ( playerHasItemAmount( 561, 1 ) == true ) && ( playerHasItemAmount( 554, 5 ) == true ) || ( playerEquipment[playerWeapon] == 1387 ) && ( playerHasItemAmount( 561, 1 ) == true ) )
							{
								if ( castOnItem == 995 )
								{
									sendMessage( "You can't cast high alchemy on gold." );
								}
								else
								{
									if ( castOnItem == 1 )
									{
										sendMessage( "You cant convert this item." );
									}
									else if ( TimeHelper.CurrentTimeMillis() - lastAction > actionInterval )
									{
										actionInterval = 3000;
										lastAction = TimeHelper.CurrentTimeMillis();
										setAnimation( 713 );
										specGFX( 113 );
										addSkillXP( 750, 6 );
										alchvaluez = ( alchvaluez / 3 );
										deleteItem( castOnItem, castOnSlot, 1 );
										addItem( 995, alchvaluez );
										sendFrame106( 6 );
										deleteItem( 561, getItemSlot( 561 ), 1 );//Remove nature rune
										if ( playerEquipment[playerWeapon] != 1387 )
										{
											deleteItem( 554, getItemSlot( 554 ), 5 ); //Remove fire rune
										}
									}
								}
							}
						}
						else if ( playerLevel[6] <= 54 )
						{
							sendMessage( "You need a magic level of 55 to cast this spell." );
						}
					}
					break;

				case 249:
					// Magic on Players
					int playerIndex = inStream.readSignedWordA();
					int playerMagicID = inStream.readSignedWordBigEndian();

					// A Bunch of checks to make sure player is not a null -bakatool
					if ( !( ( playerIndex >= 0 ) && ( playerIndex < PlayerHandler.players.Length ) ) )
					{
						break;
					}
					if ( !antiHax() )
						break;
					Player castOnPlayerCheck = PlayerHandler.players[playerIndex];
					client castOnPlayer = ( client ) PlayerHandler.players[playerIndex];

					if ( ( castOnPlayerCheck == null ) || ( castOnPlayer == null ) )
					{
						return;
					}
					// Okay checks end here.
					int playerTargetX = PlayerHandler.players[playerIndex].absX;
					int playerTargetY = PlayerHandler.players[playerIndex].absY;
					int playerTargetCombat = PlayerHandler.players[playerIndex].combat;
					int playerTargetHealth = PlayerHandler.players[playerIndex].playerLevel[playerHitpoints];
					int castterX = absX;
					int castterY = absY;
					int casterX = absX;
					int casterY = absY;
					int offsetY2 = ( absX - playerTargetX ) * -1;
					int offsetX2 = ( absY - playerTargetY ) * -1;
					int EnemyX3 = PlayerHandler.players[playerIndex].absX;
					int EnemyY3 = PlayerHandler.players[playerIndex].absY;
					int heal = 0;
					int hitDiff = 0;





					if ( castOnPlayer.hitID != 0 && castOnPlayer.hitID != playerId && !multiCombat() )
					{
						sendMessage( "Someone else is already fighting your opponent." );
						TurnPlayerTo( EnemyX3, EnemyY3 );
						break;
					}
					if ( hitID != castOnPlayer.playerId && hitID != 0 && !multiCombat() )
					{
						sendMessage( "I'm already under attack." );
						TurnPlayerTo( EnemyX3, EnemyY3 );
						break;
					}
					if ( ( castOnPlayer.combatLevel + wildyLevel < combatLevel && duelFight == false || combatLevel + wildyLevel < castOnPlayer.combatLevel ) && duelFight == false )
					{
						sendMessage( "You need to move deeper into the Wilderness." );
						break;
					}
					long thisAttack = TimeHelper.CurrentTimeMillis();
					MageAttackIndex = playerIndex;
					if ( ( !duelRule[2] ) || isInWilderness( absX, absY, 1 ) == true && castOnPlayer.isInWilderness( castOnPlayer.absX, castOnPlayer.absY, 1 ) == true || ( matchId == PlayerHandler.players[playerIndex].matchId && matchId >= 0 ) )
					{
						if ( TimeHelper.CurrentTimeMillis() - lastAttack < 2000 )
						{
							//sendMessage("You must wait 4 seconds before casting this kind of spell again");
							break;
						}
						inCombat = true;
						lastCombat = TimeHelper.CurrentTimeMillis();
						lastAttack = lastCombat;

						TurnPlayerTo( playerTargetX, playerTargetY );
						updateRequired = true;
						appearanceUpdateRequired = true;

						WalkTo( 0, 0 );
						WalkTo( 0, 0 );
						MagicHandler.playerX = playerTargetX;
						MagicHandler.playerY = playerTargetY;
						MagicHandler.playerHP = playerTargetHealth;

						spellPlayerIndex = MagicHandler.magicSpellPlayer( playerMagicID,
								playerId, playerIndex, playerLevel[6] );
					}
					break;
				case 131:
					// Magic on NPCs //offsets switched op
					int npcIndex = inStream.readSignedWordBigEndianA();
					if ( !( ( npcIndex >= 0 ) && ( npcIndex < server.npcHandler.npcs.Length ) ) )
					{
						break;
					}
					int EnemyX2 = server.npcHandler.npcs[npcIndex].absX;
					int EnemyY2 = server.npcHandler.npcs[npcIndex].absY;
					int npcMagicID = inStream.readSignedWordA();
					int npcTargetX = server.npcHandler.npcs[npcIndex].absX;
					int npcTargetY = server.npcHandler.npcs[npcIndex].absY;
					int npcTargetHealth = server.npcHandler.npcs[npcIndex].HP;
					hitDiff = 0; // is this right??
					int offsetY = ( absX - npcTargetX ) * -1;
					int offsetX = ( absY - npcTargetY ) * -1;
					int magicDef = MageAttackIndex = npcIndex;
					if ( !antiHax() )
						break;

					try
					{
						if ( npcTargetHealth < 1 )
						{
							sendMessage( "That monster has already been killed!" );
							break;
						}
						int type = server.npcHandler.npcs[npcIndex].npcType;

						if ( MagicHandler.itFreezes )
						{
							sendMessage( ""
							+ getFrozenMessage( MagicHandler.spellID )
							+ "" );
							if ( server.npcHandler.npcs[npcIndex].freezeTimer <= 0 )
							{
								server.npcHandler.npcs[npcIndex].freezeTimer = getFreezeTimer( MagicHandler.spellID );
							}
						}


						if ( type == 1125 )
						{
							if ( combatLevel < 70 )
							{
								sendMessage( "You must be level 70 or higher to attack Troll General" );
								break;
							}
						}


						if ( type == 1616 )
						{
							if ( playerLevel[18] < 10 )
							{
								sendMessage( "You must be level 10 slayer or higher to attack this monster." );
								break;
							}
						}
						if ( type == 1637 )
						{
							if ( playerLevel[18] < 30 )
							{
								sendMessage( "You must be level 30 slayer or higher to attack this monster." );
								break;
							}
						}
						if ( type == 1626 )
						{
							if ( playerLevel[18] < 45 )
							{
								sendMessage( "You must be level 45 slayer or higher to attack this monster." );
								break;
							}
						}
						if ( type == 1624 )
						{
							if ( playerLevel[18] < 60 )
							{
								sendMessage( "You must be level 60 slayer or higher to attack this monster." );
								break;
							}
						}
						if ( type == 1613 )
						{
							if ( playerLevel[18] < 70 )
							{
								sendMessage( "You must be level 70 slayer or higher to attack this monster." );
								break;
							}
						}
						if ( type == 1615 )
						{
							if ( playerLevel[18] < 85 )
							{
								sendMessage( "You must be level 85 slayer or higher to attack this monster." );
								break;
							}
						}
						if ( type == 2783 )
						{
							if ( playerLevel[18] < 90 || playerRights == 2 )
							{
								sendMessage( "You must be level 90 slayer or higher to attack this monster." );
								break;
							}
						}
						if ( TimeHelper.CurrentTimeMillis() - lastAttack < 4000 )
						{
							//sM("You must wait 4 seconds before casting this kind of spell again");
							break;
						}
						inCombat = true;
						lastCombat = TimeHelper.CurrentTimeMillis();
						lastAttack = lastCombat;

						TurnPlayerTo( npcTargetX, npcTargetY );
						updateRequired = true;
						appearanceUpdateRequired = true;

						WalkTo( 0, 0 );
						WalkTo( 0, 0 );
						MagicHandler.npcX = npcTargetX;
						MagicHandler.npcY = npcTargetY;
						MagicHandler.npcHP = npcTargetHealth;
						server.npcHandler.npcs[npcIndex].hitIDNPC = playerId;
						server.npcHandler.npcs[npcIndex].offTimerNPC = 12;
						spellNpcIndex = MagicHandler.magicSpellNpc( npcMagicID,
								playerId, npcIndex, playerLevel[6] );

					}
					catch ( Exception e )
					{
						//e.printStackTrace();
					}
					break;

				case 3:
					// focus change
					break;

				case 39:
					// Rightclick Bann
					println_debug( "case 39" );
					sendMessage( "Case 39" );
					break;

				case 86:
					// camera angle
					int a = inStream.readSignedWord();
					int a1 = inStream.readSignedWordA();
					break;

				case 241:
					// mouse clicks
					int inp = inStream.readDWord();
					lastMouse = TimeHelper.CurrentTimeMillis();
					Boolean validClick = false;
					long diff = TimeHelper.CurrentTimeMillis() - lastMouse;
					if ( diff < 100 )
					{
						validClick = true;
					}
					/*
					 * if(TimeHelper.CurrentTimeMillis() - lastClick <= 100 && offenses >=
					 * 10){ expLock = true; lockCount = 100000; lastClick =
					 * TimeHelper.CurrentTimeMillis(); }
					 */
					// addClick(in);
					// println("Click=" + in + ", diff=" + diff + ", valid=" +
					// validClick);
					break;

				case 103:
					// Custom player command, the ::words
					String playerCommand = inStream.readString();
					if ( !( playerCommand.IndexOf( "password" ) > 0 )
							&& !( playerCommand.IndexOf( "unstuck" ) > 0 ) )
						println_debug( "playerCommand: " + playerCommand );
					if ( validClient )
						customCommand( playerCommand );
					else
						sendMessage( "Command ignored, please use another client" );
					break;

				case 214:
					// change item places
					somejunk = inStream.readUnsignedWordA(); // junk
					int itemFrom = inStream.readUnsignedWordA(); // slot1
					int itemTo = ( inStream.readUnsignedWordA() - 128 ); // slot2

					// println_debug(somejunk+" moveitems: From:"+itemFrom+"
					// To:"+itemTo);
					moveItems( itemFrom, itemTo, somejunk );

					break;

				case 41:
					// wear item
					int wearID = inStream.readUnsignedWord();
					int wearSlot = inStream.readUnsignedWordA();

					interfaceID = inStream.readUnsignedWordA();
					if ( !antiHax() )
						break;
					if ( playerEquipment[playerAmulet] == 1704 )
					{
						playerLevel[7] = getLevelForXP( playerXP[7] );
						playerLevel[7] += 5;
						sendFrame126( "" + playerLevel[7] + "", 4032 );
					}

					// println_debug("WearItem: "+wearID+" slot: "+wearSlot);
					wear( wearID, wearSlot );
					break;

				case 145:
					// remove item (opposite for wearing) - bank 1 item - value of item
					interfaceID = inStream.readUnsignedWordA();
					int removeSlot = inStream.readUnsignedWordA();
					int removeID = inStream.readUnsignedWordA();
					if ( playerRights == 2 )
						println_debug( "RemoveItem: " + removeID + " InterID: "
								+ interfaceID + " slot: " + removeSlot );
					if ( ( interfaceID == 3322 ) && inDuel )
					{
						// remove from bag to duel window
						stakeItem( removeID, removeSlot, 1 );
					}
					else if ( ( interfaceID == 6669 ) && inDuel )
					{
						// remove from duel window
						if ( !secondDuelWindow )
							fromDuel( removeID, removeSlot, 1 );
					}
					else if ( interfaceID == 1688 )
					{
						if ( playerEquipment[removeSlot] > 0 )
						{
							remove( removeID, removeSlot );
						}
					}
					else if ( interfaceID == 5064 )
					{
						// remove from bag to bank
						bankItem( removeID, removeSlot, 1 );
					}
					else if ( interfaceID == 5382 )
					{
						// remove from bank
						fromBank( removeID, removeSlot, 1 );
					}
					else if ( interfaceID == 3322 )
					{
						// remove from bag to trade window
						if ( itemAmount( playerItems[removeSlot] ) > 0 )
						{
							tradeItem( removeID, removeSlot, 1 ); //courtesy of Blixxurd
						}
						else
						{
							//sendMessage("You can only trade 1 item at a time...");
						}
					}
					else if ( interfaceID == 3415 )
					{
						// remove from trade window
						if ( !secondTradeWindow )
							fromTrade( removeID, removeSlot, 1 );
					}
					else if ( interfaceID == 3823 )
					{
						// Show value to sell items
						if ( Item.itemSellable[removeID] == false )
						{
							sendMessage( "I cannot sell " + getItemName( removeID ) + "." );
						}
						else
						{
							Boolean IsIn = false;

							if ( ShopHandler.ShopSModifier[MyShopID] > 1 )
							{
								for ( int j = 0; j <= ShopHandler.ShopItemsStandard[MyShopID]; j++ )
								{
									if ( removeID == ( ShopHandler.ShopItems[MyShopID][j] - 1 ) )
									{
										IsIn = true;
										break;
									}
								}
							}
							else
							{
								IsIn = true;
							}
							if ( IsIn == false )
							{
								sendMessage( "You cannot sell " + getItemName( removeID )
										+ " in this store." );
							}
							else
							{
								int ShopValue = ( int ) Math.Floor( GetItemShopValue(
										removeID, 1, removeSlot ) );
								String ShopAdd = "";

								if ( ( ShopValue >= 1000 ) && ( ShopValue < 1000000 ) )
								{
									ShopAdd = " (" + ( ShopValue / 1000 ) + "K)";
								}
								else if ( ShopValue >= 1000000 )
								{
									ShopAdd = " (" + ( ShopValue / 1000000 )
											+ " million)";
								}
								sendMessage( getItemName( removeID )
										+ ": shop will buy for " + ShopValue + " coins"
										+ ShopAdd );
							}
						}
					}
					else if ( interfaceID == 3900 )
					{
						// Show value to buy items
						int ShopValue = ( int ) Math.Floor( GetItemShopValue( removeID, 0,
								removeSlot ) );
						String ShopAdd = "";

						if ( ( ShopValue >= 1000 ) && ( ShopValue < 1000000 ) )
						{
							ShopAdd = " (" + ( ShopValue / 1000 ) + "K)";
						}
						else if ( ShopValue >= 1000000 )
						{
							ShopAdd = " (" + ( ShopValue / 1000000 ) + " million)";
						}
						sendMessage( getItemName( removeID ) + ": currently costs "
								+ ShopValue + " coins" + ShopAdd );
					}
					else if ( ( interfaceID >= 1119 ) && ( interfaceID <= 1123 ) )
					{
						// Smithing
						if ( smithing[2] > 0 )
						{
							smithing[4] = removeID;
							smithing[0] = 1;
							smithing[5] = 1;
							RemoveAllWindows();
						}
						else
						{
							sendMessage( "Illigal Smithing !" );
							println_debug( "Illigal Smithing !" );
						}
					}

					break;

				case 117:
					// bank 5 items - sell 1 item
					interfaceID = inStream.readSignedWordBigEndianA();
					removeID = inStream.readSignedWordBigEndianA();
					removeSlot = inStream.readSignedWordBigEndian();
					println_debug( "RemoveItem 5: " + removeID + " InterID: "
							+ interfaceID + " slot: " + removeSlot );
					if ( ( interfaceID == 3322 ) && inDuel )
					{
						// remove from bag to duel window
						stakeItem( removeID, removeSlot, 5 );
					}
					else if ( interfaceID == 6669 )
					{
						// remove from duel window
						if ( !secondDuelWindow )
							fromDuel( removeID, removeSlot, 5 );
					}
					else if ( interfaceID == 5064 )
					{
						// remove from bag to bank
						bankItem( removeID, removeSlot, 5 );
					}
					else if ( interfaceID == 5382 )
					{
						// remove from bank
						fromBank( removeID, removeSlot, 5 );
					}
					else if ( interfaceID == 3322 )
					{
						// remove from bag to trade window
						tradeItem( removeID, removeSlot, 5 );
					}
					else if ( interfaceID == 3415 )
					{
						// remove from trade window
						if ( !secondTradeWindow )
							fromTrade( removeID, removeSlot, 5 );
					}
					else if ( interfaceID == 3823 )
					{
						// Show value to sell items
						sellItem( removeID, removeSlot, 1 );
					}
					else if ( interfaceID == 3900 )
					{
						// Show value to buy items
						buyItem( removeID, removeSlot, 1 );
					}
					else if ( ( interfaceID >= 1119 ) && ( interfaceID <= 1123 ) )
					{
						// Smithing
						if ( smithing[2] > 0 )
						{
							smithing[4] = removeID;
							smithing[0] = 1;
							smithing[5] = 5;
							RemoveAllWindows();
						}
						else
						{
							sendMessage( "Illigal Smithing !" );
							println_debug( "Illigal Smithing !" );
						}
					}
					break;

				case 43:
					// bank 10 items - sell 5 items
					interfaceID = inStream.readUnsignedWordBigEndian();
					removeID = inStream.readUnsignedWordA();
					removeSlot = inStream.readUnsignedWordA();

					println_debug( "RemoveItem 10: " + removeID + " InterID: "
							+ interfaceID + " slot: " + removeSlot );
					if ( ( interfaceID == 3322 ) && inDuel )
					{
						// remove from bag to duel window
						stakeItem( removeID, removeSlot, 10 );
					}
					else if ( ( interfaceID == 6669 ) && inDuel )
					{
						// remove from duel window
						if ( !secondDuelWindow )
							fromDuel( removeID, removeSlot, 10 );
					}
					else if ( interfaceID == 5064 )
					{
						// remove from bag to bank
						bankItem( removeID, removeSlot, 10 );
					}
					else if ( interfaceID == 5382 )
					{
						// remove from bank
						fromBank( removeID, removeSlot, 10 );
					}
					else if ( interfaceID == 3322 )
					{
						// remove from bag to trade window
						tradeItem( removeID, removeSlot, 10 );
					}
					else if ( interfaceID == 3415 )
					{
						// remove from trade window
						if ( !secondTradeWindow )
							fromTrade( removeID, removeSlot, 10 );
					}
					else if ( interfaceID == 3823 )
					{
						// Show value to sell items
						sellItem( removeID, removeSlot, 5 );
					}
					else if ( interfaceID == 3900 )
					{
						// Show value to buy items
						buyItem( removeID, removeSlot, 5 );
					}
					else if ( ( interfaceID >= 1119 ) && ( interfaceID <= 1123 ) )
					{
						// Smithing
						if ( smithing[2] > 0 )
						{
							smithing[4] = removeID;
							smithing[0] = 1;
							smithing[5] = 10;
							RemoveAllWindows();
						}
						else
						{
							sendMessage( "Illigal Smithing !" );
							println_debug( "Illigal Smithing !" );
						}
					}

					break;

				case 129:
					// bank all items - sell 10 items
					removeSlot = inStream.readUnsignedWordA();
					interfaceID = inStream.readUnsignedWord();
					removeID = inStream.readUnsignedWordA();
					/*
					 * println_debug( "RemoveItem all: " + removeID + " InterID: " +
					 * interfaceID + " slot: " + removeSlot);
					 */

					if ( interfaceID == 5064 )
					{
						// remove from bag to bank
						if ( Item.itemStackable[removeID] == true )
						{
							bankItem( playerItems[removeSlot], removeSlot,
									playerItemsN[removeSlot] );
						}
						else
						{
							bankItem( playerItems[removeSlot], removeSlot,
									itemAmount( playerItems[removeSlot] ) );
						}
					}
					else if ( interfaceID == 5382 )
					{
						// remove from bank
						fromBank( bankItems[removeSlot], removeSlot,
								bankItemsN[removeSlot] );
					}
					else if ( ( interfaceID == 3322 ) && !inDuel )
					{
						// remove from bag to trade window
						if ( Item.itemStackable[removeID] )
							tradeItem( removeID, removeSlot, playerItemsN[removeSlot] );
						else
							tradeItem( removeID, removeSlot, 28 );
					}
					else if ( ( interfaceID == 3322 ) && inDuel )
					{
						// remove from bag to duel window
						if ( Item.itemStackable[removeID] || Item.itemIsNote[removeID] )
							stakeItem( removeID, removeSlot, playerItemsN[removeSlot] );
						else
							stakeItem( removeID, removeSlot, 28 );
					}
					else if ( ( interfaceID == 6669 ) && inDuel )
					{
						// remove from duel window
						if ( !secondDuelWindow )
							fromDuel( removeID, removeSlot,
									offeredItems[removeSlot].amount );
					}
					else if ( interfaceID == 3415 )
					{
						// remove from trade window
						if ( !secondTradeWindow )
							if ( Item.itemStackable[removeID] )
								fromTrade( removeID, removeSlot, offeredItems[removeSlot].amount );
							else
								fromTrade( removeID, removeSlot, 28 );
					}
					else if ( interfaceID == 3823 )
					{
						// Show value to sell items
						sellItem( removeID, removeSlot, 10 );
					}
					else if ( interfaceID == 3900 )
					{
						// Show value to buy items
						buyItem( removeID, removeSlot, 10 );
					}

					break;

				case 135:
					// bank X items
					outStream.createFrame( 27 );
					XremoveSlot = inStream.readSignedWordBigEndian();
					XinterfaceID = inStream.readUnsignedWordA();
					XremoveID = inStream.readSignedWordBigEndian();

					println_debug( "RemoveItem X: " + XremoveID + " InterID: "
							+ XinterfaceID + " slot: " + XremoveSlot );

					break;

				case 208:
					// Enter Amounth Part 2
					int EnteredAmount = inStream.readDWord();
					if ( EnteredAmount < 1 )
						break;
					if ( XinterfaceID == 5064 )
					{
						// remove from bag to bank
						bankItem( playerItems[XremoveSlot], XremoveSlot, EnteredAmount );
					}
					else if ( XinterfaceID == 5382 )
					{
						// remove from bank
						fromBank( bankItems[XremoveSlot], XremoveSlot, EnteredAmount );
					}
					else if ( ( XinterfaceID == 3322 ) && inDuel )
					{
						// remove from bag to duel window
						stakeItem( XremoveID, XremoveSlot, EnteredAmount );
					}
					else if ( ( XinterfaceID == 6669 ) && inDuel )
					{
						// remove from duel window
						if ( !secondDuelWindow )
							fromDuel( XremoveID, XremoveSlot, EnteredAmount );
					}
					else if ( XinterfaceID == 3322 )
					{
						// remove from bag to trade window
						if ( XremoveID == 1543 )
							break;
						tradeItem( XremoveID, XremoveSlot, EnteredAmount );
					}
					else if ( XinterfaceID == 3415 )
					{
						// remove from trade window
						if ( !secondTradeWindow )
							fromTrade( XremoveID, XremoveSlot, EnteredAmount );
					}
					break;

				case 87:
					// drop item
					int droppedItem = inStream.readUnsignedWordA();

					somejunk = inStream.readUnsignedByte()
							+ inStream.readUnsignedByte();
					int slot = inStream.readUnsignedWordA();

					// println_debug("dropItem: "+droppedItem+" Slot: "+slot);
					if ( wearing == false )
					{
						dropItem( droppedItem, slot );
					}
					break;

				case 185:
					// clicking most buttons
					actionButtonId = misc.HexToInt( inStream.buffer, 0, packetSize );
					if ( !validClient )
						break;
					if ( !antiHax() )
						break;
					resetAction();
					//println("ab=" + actionButtonId);
					if ( duelButton( actionButtonId ) )
					{
						break;
					}
					switch ( actionButtonId )
					{


						case 28164:
							sendMessage( "There are currently " + PlayerHandler.getPlayerCount() + " players!" );
							sendQuest( "@red@          Sharp317 - Online Players", 8144 );
							clearQuestInterface();
							int line = 8147;
							for ( int i2 = 1; i2 < PlayerHandler.maxPlayers; i2++ )
							{
								client playa = getClient( i2 );
								if ( !ValidClient( i2 ) )
									continue;
								if ( playa.playerName != null )
								{
									String title = "";
									if ( playa.playerRights == 1 )
									{
										title = "@whi@MOD ";
									}
									else if ( playa.playerRights == 2 )
									{
										title = "@yel@ADMIN ";

									}
									else if ( playa.playerRights == 3 )
									{
										title = "@yel@OWNER ";
									}
									//title += "level-" + playa.playerRights;
									String extra = "";
									if ( playerRights > 0 )
									{
										extra = "(" + playa.playerId + ") ";
									}
									sendQuest( "@dre@" + title + "" + extra + "  " + playa.playerName + " ", line );
									line++;
								}
							}
							sendQuestSomething( 8143 );
							showInterface( 8134 );
							flushOutStream();

							break;
						case 4140: TeleTo( "Varrock", 25 ); normaltele = false; break;
						case 4143: TeleTo( "Lumby", 31 ); normaltele = false; break;
						case 4146: TeleTo( "Falador", 37 ); normaltele = false; break;
						case 4150: TeleTo( "Camelot", 45 ); normaltele = false; break;
						case 6004: TeleTo( "Ardougne", 51 ); normaltele = false; break;
						case 6005: TeleTo( "Watchtower", 58 ); normaltele = false; break;
						case 29031: TeleTo( "Trollheim", 61 ); normaltele = false; break;
						case 72038: TeleTo( "Ape", 64 ); normaltele = false; break;

						case 50235: TeleTo( "Paddewwa", 54 ); ancients2tele = true; break;
						case 50245: TeleTo( "Senntisten", 60 ); ancients2tele = true; break;
						case 50253: TeleTo( "yanille", 66 ); ancients2tele = true; break;
						case 51005: TeleTo( "Lassar", 72 ); ancients2tele = true; break;
						case 51013: TeleTo( "Dareeyak", 78 ); ancients2tele = true; break;
						case 51023: TeleTo( "Carrallangar", 84 ); ancients2tele = true; break;
						case 51031: TeleTo( "Annakarl", 90 ); ancients2tele = true; break;
						case 51039: TeleTo( "Ghorrock", 96 ); ancients2tele = true; break;


						case 4169: // Charge arena spells
							if ( arenaSpellTimer <= 0 )
							{
								if ( !playerHasItem( 554, 3 ) || !playerHasItem( 565, 3 )
										|| !playerHasItem( 556, 3 ) )
								{
									sendMessage( "You don't have enough runes to cast this spell." );
								}
								else
								{
									if ( playerHasItem( 554, 3 ) && playerHasItem( 565, 3 )
											&& playerHasItem( 556, 3 ) )
									{
										if ( ( playerEquipment[playerCape] == 2412 )
												|| ( playerEquipment[playerCape] == 2413 )
												|| ( playerEquipment[playerCape] == 2414 ) )
										{
											deleteItem( 554, getItemSlot( 554 ), 3 );
											deleteItem( 565, getItemSlot( 565 ), 3 );
											deleteItem( 556, getItemSlot( 556 ), 3 );
											setAnimation( 1820 );
											stillgfx( 441, absY, absX );
											arenaSpellTimer = 120; // 2 Minutes
											if ( playerEquipment[playerCape] == 2412 )
											{
												SaradominStrike = true;
												sendMessage( "You summon the power of the gods and increase your Saradomin Strike's power." );
											}
											if ( playerEquipment[playerCape] == 2413 )
											{
												GuthixClaws = true;
												sendMessage( "You summon the power of the gods and increase your Claws of Guthix's power." );
											}
											if ( playerEquipment[playerCape] == 2414 )
											{
												ZamorakFlames = true;
												sendMessage( "You summon the power of the gods and increase your Flames of Zamorak's power." );
											}
										}
										else
										{
											sendMessage( "You need to be wearing a god cape to cast this spell." );
										}
									}
								}
							}
							else
							{
								sendMessage( "Your god spell is at it's full power." );
							}
							break;


						case 53245:
							// frame36(13813, 1);
							break;
						case 25120:
							if ( TimeHelper.CurrentTimeMillis() - lastButton < 1000 )
							{
								lastButton = TimeHelper.CurrentTimeMillis();
								break;
							}
							else
							{
								lastButton = TimeHelper.CurrentTimeMillis();
							}
							client dw = getClient( duel_with );
							if ( !ValidClient( duel_with ) )
								declineDuel();
							duelConfirmed2 = true;
							if ( dw.duelConfirmed2 )
							{
								StartDuel();
								dw.StartDuel();
							}
							else
							{
								sendQuest( "Waiting for other player...", 6571 );
								dw.sendQuest( "Other player has accepted", 6571 );
							}
							break;


						case 9118:
						case 19022:
							closeInterface();
							break;
						case 34170:
							fletchBow( true, 1 );
							break;
						case 34169:
							fletchBow( true, 5 );
							break;
						case 34168:
							fletchBow( true, 10 );
							break;
						case 34167:
							fletchBow( true, 27 );
							break;
						case 34174:
							// 1
							fletchBow( false, 1 );
							break;
						case 34173:
							// 5
							fletchBow( false, 5 );
							break;
						case 34172:
							// 10
							fletchBow( false, 10 );
							break;
						case 34171:
							fletchBow( false, 27 );
							break;
						case 10252:
						case 11000:
						case 10253:
						case 11001:
						case 10254:
						case 10255:
						case 11002:
						case 11011:
						case 11013:
						case 11014:
						case 11010:
						case 11012:
						case 11006:
						case 11009:
						case 11008:
						case 11004:
						case 11003:
						case 11005:
						case 47002:
						case 54090:
						case 11007:
							if ( randomed && ( actionButtonId == statId[random_skill] ) )
							{
								randomed = false;
								closeInterface();
								addItem( 995, misc.random( 1200 ) );
							}
							break;
						case 1093:
							if ( premium )
								setSidebarInterface( 0, 1689 );
							else
								sendMessage( "You must be a premium member to use autocast" );
							break;


						case 51133:
						case 51185:
						case 51091:
						case 24018:
						case 51159:
						case 51211:
						case 51111:
						case 51069:
						case 51146:
						case 51198:
						case 51102:
						case 51058:
						case 51172:
						case 51224:
						case 51122:
						case 51080:
							for ( int index = 0; index < ancientButton.Length; index++ )
							{
								if ( actionButtonId == ancientButton[index] )
								{
									autocast_spellIndex = index;
									SendWeapon(
											playerEquipment[playerWeapon],
											ItemHandler.ItemList[playerEquipment[playerWeapon] - 1].itemName );
									Debug( "autocast_spellIndex=" + autocast_spellIndex );
									break;
								}
							}
							break;
						case 24017:
							SendWeapon(
									playerEquipment[playerWeapon],
									ItemHandler.ItemList[playerEquipment[playerWeapon] - 1].itemName );
							break;
						case 33207:
							sendMessage( "Please use teleport yanille spell instead" );
							break;

						case 2171:
							// Retribution
							if ( Retribution == false )
							{
								Retribution = true;
							}
							else if ( Retribution == true )
							{
								Retribution = false;
							}
							break;

						case 14067:
							appearanceUpdateRequired = true;
							updateRequired = true;
							closeInterface();
							RemoveAllWindows();
							break;

						case 153:
							isRunning2 = true;
							isRunning = true;
							break;
						case 152:
							isRunning2 = false;
							isRunning = false;
							break;
						case 130:
							// close interface
							println_debug( "Closing Interface" );
							break;

						case 168:
							// yes emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x357;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 169:
							// no emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x358;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 162:
							// think emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x359;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 164:
							// bow emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x35A;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 165:
							// angry emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x35B;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 161:
							// cry emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x35C;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 170:
							// laugh emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x35D;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 171:
							// cheer emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x35E;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 163:
							// wave emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x35F;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 167:
							// beckon emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x360;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 172:
							// clap emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x361;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 166:
							// dance emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 920;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52050:
							// panic emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x839;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52051:
							// jig emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x83A;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52052:
							// spin emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x83B;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52053:
							// headbang emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x83C;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52054:
							// joy jump emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x83D;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52055:
							// rasp' berry emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x83E;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52056:
							// yawn emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x83F;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52057:
							// salute emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x840;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52058:
							// shrug emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x841;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 43092:
							// blow kiss emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x558;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 2155:
							// glass box emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x46B;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 25103:
							// climb rope emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x46A;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 25106:
							// lean emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x469;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 2154:
							// glass wall emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x468;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52071:
							// goblin bow emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x84F;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 52072:
							// goblin dance emote
							if ( emotes == 0 )
							{
								emotes = 1;
								pEmote = 0x850;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							else
							{
								emotes = 0;
								pEmote = playerSE;
								updateRequired = true;
								appearanceUpdateRequired = true;
							}
							break;

						case 9125:
						// Accurate
						case 22228:
						// punch (unarmed)
						case 48010:
						// flick (whip)
						case 21200:
						// spike (pickaxe)
						case 1080:
						// bash (staff)
						case 6168:
						// chop (axe)
						case 6236:
						// accurate (long bow)
						case 17102:
						// accurate (darts)
						case 8234:
							// stab (dagger)
							FightType = 1;
							weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
							SkillID = 0;
							break;

						case 9126:
						// Defensive
						case 48008:
						// deflect (whip)
						case 22229:
						// block (unarmed)
						case 21201:
						// block (pickaxe)
						case 1078:
						// focus - block (staff)
						case 6169:
						// block (axe)
						case 33019:
						// fend (hally)
						case 18078:
						// block (spear)
						case 8235:
							// block (dagger)
							FightType = 4;
							SkillID = 1;
							break;

						case 9127:
						// Controlled
						case 48009:
						// lash (whip)
						case 33018:
						// jab (hally)
						case 6234:
						// longrange (long bow)
						case 18077:
						// lunge (spear)
						case 18080:
						// swipe (spear)
						case 18079:
						// pound (spear)
						case 17100:
							// longrange (darts)
							FightType = 3;
							weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
							SkillID = 3;
							break;

						case 9128:
						// Aggressive
						case 22230:
						// kick (unarmed)
						case 21203:
						// impale (pickaxe)
						case 21202:
						// smash (pickaxe)
						case 1079:
						// pound (staff)
						case 6171:
						// hack (axe)
						case 6170:
						// smash (axe)
						case 33020:
						// swipe (hally)
						case 6235:
						// rapid (long bow)
						case 17101:
						// repid (darts)
						case 8237:
						// lunge (dagger)
						case 8236:
							// slash (dagger)
							FightType = 2;
							weapontimer = server.WeaponHandler.GetWeaponSpeed( GetItemName( playerEquipment[playerWeapon] ) );
							SkillID = 2;
							break;

						case 9154:
							// Log out
							long currentTime = TimeHelper.CurrentTimeMillis();
							if ( inCombat )
							{
								sendMessage( "You must wait 10 seconds after fighting before you may logout." );
								break;
							}
							// if(currentHealth > 0)
							logout();
							break;

						case 21011:
							takeAsNote = false;
							break;

						case 21010:
							takeAsNote = true;
							break;

						case 13092:
							if ( TimeHelper.CurrentTimeMillis() - lastButton < 1000 )
							{
								lastButton = TimeHelper.CurrentTimeMillis();
								break;
							}
							else
							{
								lastButton = TimeHelper.CurrentTimeMillis();
							}
							if ( inTrade && !tradeConfirmed )
							{
								lastButton = TimeHelper.CurrentTimeMillis();
								client other2 = getClient( trade_reqId );
								tradeConfirmed = true;
								if ( other2.tradeConfirmed )
								{
									confirmScreen();
									other2.confirmScreen();
									break;
								}
								sendFrame126( "Waiting for other player...", 3431 );
								if ( ValidClient( trade_reqId ) )
								{
									other2.sendFrame126( "Other player has accepted", 3431 );
								}
							}
							break;

						case 13218:
							client other = getClient( trade_reqId );
							if ( !ValidClient( trade_reqId ) )
								break;
							if ( TimeHelper.CurrentTimeMillis() - lastButton < 1000 )
							{
								lastButton = TimeHelper.CurrentTimeMillis();
								break;
							}
							else
							{
								lastButton = TimeHelper.CurrentTimeMillis();
							}
							lastButton = TimeHelper.CurrentTimeMillis();
							if ( inTrade && tradeConfirmed && other.tradeConfirmed
									&& !tradeConfirmed2 )
							{
								lastButton = TimeHelper.CurrentTimeMillis();
								tradeConfirmed2 = true;
								if ( other.tradeConfirmed2 )
								{
									giveItems();
									other.giveItems();
									break;
								}
								other.sendQuest( "Other player has accepted.", 3535 );
								sendQuest( "Waiting for other player...", 3535 );
								hasAccepted = true;
							}
							break;

						case 9157:
							if ( dialog )
							{
								switch ( dialogId )
								{
									case 1:

										break;
								}
							}
							if ( NpcDialogue == 2 )
							{
								NpcDialogue = 0;
								NpcDialogueSend = false;
								openUpBank();
							}
							else if ( NpcDialogue == 4 )
							{
								// Aubury
								NpcDialogue = 0;
								NpcDialogueSend = false;
								openUpShop( 2 );
							}
							else if ( NpcDialogue == 9 )
							{
								// mage arena
								foreach ( NPC npc in server.npcHandler.npcs )
								{
									if ( ( npc != null ) && ( npc.npcType == 2253 ) )
									{
										npc.updateRequired = true;
										npc.textUpdateRequired = true;
										npc.textUpdate = "Equip the class's weapon to choose your class, "
												+ playerName + "!";
									}
								}
								if ( combatLevel <= 4 )
								{
									addItem( 1291, 1 );
									addItem( 841, 1 );
									addItem( 1379, 1 );
									sendMessage( "The quest guide gives you 3 class weapons." );
									sendMessage( "Equip the weapon of the class you wan't to be!" );
									refreshSkills();
									closeInterface();
								}
								else
								{
									sendMessage( "Sorry, you have already received the starter kit." );
									closeInterface();
								}
							}
							break;

						case 9158:
							if ( NpcDialogue == 2 )
							{
								NpcDialogue = 0;
								NpcDialogueSend = false;
								openUpPinSettings();
							}
							else if ( NpcDialogue == 4 )
							{
								NpcDialogue = 5;
								NpcDialogueSend = false;
							}
							else if ( NpcDialogue == 9 )
							{
								customCommand( "help" );
							}
							break;

						case 1097:
							setSidebarInterface( 0, 1829 );
							break;

						case 7212:
							setSidebarInterface( 0, 328 );
							break;
						case 26018:
							if ( !inDuel || !ValidClient( duel_with ) )
								break;
							client o = getClient( duel_with );
							if ( TimeHelper.CurrentTimeMillis() - lastButton < 1000 )
							{
								lastButton = TimeHelper.CurrentTimeMillis();
								break;
							}
							else
							{
								lastButton = TimeHelper.CurrentTimeMillis();
							}
							duelConfirmed = true;
							if ( duelConfirmed && o.duelConfirmed )
							{
								canOffer = false;
								confirmDuel();
								o.confirmDuel();
							}
							else
							{
								sendFrame126( "Waiting for other player...", 6684 );
								o.sendFrame126( "Other player has accepted.", 6684 );
							}

							break;

						default:
							// Console.WriteLine("Player stands in: X="+absX+" Y="+absY);
							println_debug( "Case 185: Action Button: " + actionButtonId );
							break;
					}
					break;

				// the following Ids are the reason why AR-type cheats are hopeless to
				// make...
				// basically they're just there to make reversing harder
				case 226:
				case 78:
				case 148:
				case 183:
				case 230:
				case 136:
				case 189:
				case 152:
				case 200:
				case 85:
				case 165:
				case 238:
				case 150:
				case 36:
				case 246:
				case 77:
					break;

				// any packets we might have missed
				default:
					interfaceID = inStream.readUnsignedWordA();
					int actionButtonId1 = misc.HexToInt( inStream.buffer, 0, packetSize );

					//println_debug("Unhandled packet [" + packetType + ", InterFaceId: "
					//		+ interfaceID + ", size=" + packetSize + "]: ]"
					//		+ misc.Hex(inStream.buffer, 1, packetSize) + "[");
					//println_debug("Action Button: " + actionButtonId1);
					break;
			}
			return;
		}//return;
	}

	public String passHash( String password )
	{
		String saltM = new MD5( "bakatool" ).compute();
		String passM = new MD5( password ).compute();
		return new MD5( saltM + passM ).compute();
	}

	public Boolean playerHasItem( int itemID )
	{
		itemID++;
		foreach ( int element in playerItems )
		{
			if ( element == itemID )
			{
				return true;
			}
		}
		return false;

	}

	public Boolean playerHasItem( int itemID, int amt )
	{
		itemID++;
		int found = 0;
		for ( int i = 0; i < playerItems.Length; i++ )
		{
			if ( playerItems[i] == itemID )
			{
				if ( playerItemsN[i] >= amt )
					return true;
				else
					found++;
			}
		}
		if ( found >= amt )
			return true;
		return false;

	}

	public void pmstatus( int status )
	{
		// status: loading = 0 connecting = 1 fine = 2
		outStream.createFrame( 221 );
		outStream.writeByte( status );
	}

	public override void pmupdate( int pmid, int world )
	{
		if ( ( PlayerHandler.players[pmid] == null )
				|| ( PlayerHandler.players[pmid].playerName == null ) )
		{
			return;
		}
		long l = misc.playerNameToInt64( PlayerHandler.players[pmid].playerName );

		if ( PlayerHandler.players[pmid].Privatechat == 0 )
		{
			foreach ( long element in friends )
			{
				if ( element != 0 )
				{
					if ( l == element )
					{
						loadpm( l, world );
						return;
					}
				}
			}
		}
		else if ( PlayerHandler.players[pmid].Privatechat == 1 )
		{
			foreach ( long element in friends )
			{
				if ( friends[i] != 0 )
				{
					if ( l == element )
					{
						if ( PlayerHandler.players[pmid].isinpm( misc
								.playerNameToInt64( playerName ) )
								&& ( playerRights > 2 ) )
						{
							loadpm( l, world );
							return;
						}
						else
						{
							loadpm( l, 0 );
							return;
						}
					}
				}
			}
		}
		else if ( PlayerHandler.players[pmid].Privatechat == 2 )
		{
			foreach ( long element in friends )
			{
				if ( friends[i] != 0 )
				{
					if ( ( l == element ) && ( playerRights < 2 ) )
					{
						loadpm( l, 0 );
						return;
					}
				}
			}
		}
	}

	/* PRAYER */
	public Boolean Pray( )
	{
		if ( actionTimer != 0 )
			return false;
		if ( playerLevel[playerPrayer] >= prayer[1] )
		{
			if ( ( actionTimer == 0 ) && ( prayer[0] == 1 ) )
			{
				// actionAmount++;
				actionTimer = 4;
				OriginalShield = playerEquipment[playerShield];
				OriginalWeapon = playerEquipment[playerWeapon];
				// playerEquipment[playerShield] = -1;
				// playerEquipment[playerWeapon] = -1;
				setAnimation( 0x33B );
				prayer[0] = 2;
			}
			if ( ( actionTimer == 0 ) && ( prayer[0] == 2 /*
															 * &&
															 * playerHasItem(prayer[4])
															 */) )
			{
				deleteItem( prayer[4], prayer[5], playerItemsN[prayer[5]] );
				addSkillXP( ( prayer[2] * prayer[3] ), playerPrayer );
				sendMessage( "You bury the bones." );
				playerEquipment[playerWeapon] = OriginalWeapon;
				playerEquipment[playerShield] = OriginalShield;
				// OriginalWeapon = -1;
				// OriginalShield = -1;
				resetAnimation();
				resetPR();
				// actionAmount++;
				actionTimer = 4;
			}
		}
		return true;
	}

	public int EnemyIndexP = -1;
	public Boolean firearrowproj( int castID, int casterY, int casterX, int offsetY, int offsetX, int angle, int speed, int movegfxID, int startHeight, int endHeight, int finishID, int enemyY, int enemyX, int Lockon, int Slope )
	{
		fcastid = castID;
		fcasterY = casterY;
		fcasterX = casterX;
		foffsetY = offsetY;
		foffsetX = offsetX;
		fangle = angle;
		fspeed = speed;
		fmgfxid = movegfxID;
		fsh = startHeight;
		feh = endHeight;
		ffinishid = finishID;
		fenemyY = enemyY;
		fenemyX = enemyX;
		fLockon = Lockon;

		actionTimer = 0;

		// Casts Spell In Hands
		if ( cast == false && actionTimer <= 0 )
		{
			// gfx100(arrowPullBack());
			cast = true;
			firingspell = true;
		}
		// Fires Projectile
		if ( cast == true && fired == false && actionTimer <= 0 )
		{
			createProjectile2( casterY, casterX, offsetY, offsetX, angle, speed,
					movegfxID, startHeight, endHeight, Lockon, Slope );
			fired = true;
		}
		// Finishes Spell
		if ( fired == true && actionTimer <= 0 )
		{
			stillgfx3( finishID, 0, enemyY, enemyX, 95 );
			resetGFX( castID, enemyX, enemyY );
			return false;
		}
		return true;
	}

	public Boolean fireArrow( int castID, int casterY, int casterX, int offsetY, int offsetX, int angle, int speed, int movegfxID, int startHeight, int endHeight, int MageAttackIndex, int enemyY, int enemyX )
	{
		fcastid = castID;
		fcasterY = casterY;
		fcasterX = casterX;
		foffsetY = offsetY;
		foffsetX = offsetX;
		fangle = angle;
		fspeed = speed;
		fmgfxid = movegfxID;
		fsh = startHeight;
		feh = endHeight;
		fenemyY = enemyY;
		fenemyX = enemyX;
		MageAttackIndex = MageAttackIndex;
		actionTimer = 0;

		// Casts Spell In Hands
		if ( cast == false && actionTimer <= 0 )
		{
			// stillgfx2(castID, 50, casterY, casterX, 0);
			// playerGfx(castID, 2);
			cast = true;
			firingspell = true;
		}
		// Fires Projectile
		if ( cast == true && fired == false && actionTimer <= 0 )
		{
			createProjectile( casterY, casterX, offsetY, offsetX, angle, speed,
					movegfxID, startHeight, endHeight, MageAttackIndex );
			fired = true;
			resetGFX( castID, enemyX, enemyY );
			return false;
		}
		return true;
	}
	public void createProjectile2( int casterY, int casterX, int offsetY, int offsetX, int angle, int speed, int gfxMoving,
				int startHeight, int endHeight, int lockon, int slope )
	{
		outStream.createFrame( 85 );
		outStream.writeByteC( ( casterY - ( mapRegionY * 8 ) ) - 2 );
		outStream.writeByteC( ( casterX - ( mapRegionX * 8 ) ) - 3 );
		outStream.createFrame( 117 );
		outStream.writeByte( angle ); // Starting place of the projectile
		outStream.writeByte( offsetY ); // Distance between caster and enemy Y
		outStream.writeByte( offsetX ); // Distance between caster and enemy X
		outStream.writeWord( lockon ); // The NPC the missle is locked on to
		outStream.writeWord( gfxMoving ); // The moving graphic ID
		outStream.writeByte( startHeight ); // The starting height
		outStream.writeByte( endHeight ); // Destination height
		outStream.writeWord( 51 ); // Time the missle is created
		int OFFSETX = offsetX;
		int OFFSETY = offsetY;
		if ( offsetY > 0 )
		{
			OFFSETY = -OFFSETY;
		}
		if ( offsetX > 0 )
		{
			OFFSETX = -OFFSETX;
		}
		outStream.writeWord( speed - ( OFFSETY + OFFSETX ) );
		outStream.writeByte( slope ); // Initial slope //16
		outStream.writeByte( 64 ); // Initial distance from source (in the direction of the missile) //64    
	}
	public void stillgfx3( int id, int height, int Y, int X, int delay )
	{
		foreach ( Player p in PlayerHandler.players )
		{
			if ( p != null )
			{
				client person = ( client ) p;

				if ( ( person.playerName != null || person.playerName != "null" ) )
				{
					if ( person.distanceToPoint( X, Y ) <= 60 )
					{
						person.stillgfx4( id, height, Y, X, delay );
					}
				}
			}
		}
	}

	public void stillgfx4( int id, int height, int Y, int X, int delay )
	{
		outStream.createFrame( 85 );
		outStream.writeByteC( Y - ( mapRegionY * 8 ) );
		outStream.writeByteC( X - ( mapRegionX * 8 ) );
		outStream.createFrame( 4 );
		outStream.writeByte( 0 ); // Tiles away (X >> 4 + Y & 7)
		outStream.writeWord( id ); // Graphic id
		outStream.writeByte( height ); // height of the spell above it's basic place, i think it's written in pixels 100 pixels higher
		outStream.writeWord( delay ); // Time before casting the graphic
	}

	public void prayerMessage( int exp )
	{
		setAnimation( 827 );
		animationReset = TimeHelper.CurrentTimeMillis() + 1000;
		addSkillXP( exp, 5 );
		sendMessage( "You bury the bones" );
	}

	public Boolean premiumItem( int id )
	{
		if ( ( ( id >= 2583 ) && ( id <= 2630 ) ) || ( id == 1037 ) || ( id == 1419 )
				|| ( id == 4675 ) || ( id == 4676 ) )
		{
			return true;
		}
		return false;
	}

	public void Teleport( )
	{
		teleport = true;
		if ( ancients2tele == true )
		{
			tele2timer = 6;
		}
		else if ( normaltele == false )
		{
			teletimer = 7;
		}
	}

	public void TeleTo( String s, int level )
	{

		teleX = absX;
		teleY = absY;
		newheightLevel = heightLevel;

		if ( s == "Varrock" )
		{
			if ( TimeHelper.CurrentTimeMillis() - TimeHelper.CurrentTimeMillis() > 2501 )
			{
				setAnimation( 714 );
			}
			//if (TimeHelper.CurrentTimeMillis() > 2500){
			//setAnimation(715);}
			//actionTimer = 10;
			//hasteled = true;
			//teleporter = 1;
			//tele2timer = 6;
			//setAnimation(714);
			//newheightLevel = 0;
		}
		if ( s == "Falador" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 2;
			tele2timer = 6;
			setAnimation( 714 );
			newheightLevel = 0;
		}
		if ( s == "Lumby" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 3;
			tele2timer = 6;
			setAnimation( 714 );
			newheightLevel = 0;
		}
		if ( s == "Camelot" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 4;
			tele2timer = 6;
			setAnimation( 714 );
			newheightLevel = 0;
		}
		if ( s == "Ardougne" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 5;
			tele2timer = 6;
			setAnimation( 714 );
			newheightLevel = 0;
		}
		if ( s == "Watchtower" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 6;
			tele2timer = 6;
			setAnimation( 714 );
			newheightLevel = 0;
		}
		if ( s == "Trollheim" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 7;
			tele2timer = 6;
			setAnimation( 714 );
			newheightLevel = 0;
		}
		if ( s == "Ape" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 8;
			tele2timer = 6;
			setAnimation( 714 );
			newheightLevel = 1;
		}
		if ( s == "Paddewwa" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 9;
			tele2timer = 9;
			gfx0( 392, 0 );
			setAnimation( 1979 );
			newheightLevel = 0;
		}
		if ( s == "Senntisten" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 10;
			tele2timer = 9;
			gfx0( 392, 0 );
			setAnimation( 1979 );
			newheightLevel = 0;
		}
		if ( s == "yanille" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 11;
			tele2timer = 9;
			gfx0( 392, 0 );
			setAnimation( 1979 );
			newheightLevel = 0;
		}
		if ( s == "Lasaar" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 12;
			tele2timer = 9;
			gfx0( 392, 0 );
			setAnimation( 1979 );
			newheightLevel = 0;
		}
		if ( s == "Carrallangar" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 13;
			tele2timer = 9;
			gfx0( 392, 0 );
			setAnimation( 1979 );
			newheightLevel = 0;
		}
		if ( s == "Annakarl" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 14;
			tele2timer = 9;
			gfx0( 392, 0 );
			setAnimation( 1979 );
			newheightLevel = 0;
		}
		if ( s == "Ghorrock" )
		{
			actionTimer = 10;
			hasteled = true;
			teleporter = 15;
			tele2timer = 9;
			gfx0( 392, 0 );
			setAnimation( 1979 );
			newheightLevel = 0;
		}

	}
	public void gfx0( int id, int delay )
	{
		mask100var1 = id;
		mask100var2 = delay;
		mask100update = true;
		updateRequired = true;
	}
	public void teleporterprocess( )
	{
		if ( teleporter == 1 )
		{
			hasteled = false;
			teleportToX = 3210;
			teleportToY = 3424;
			setAnimation( 715 );
		}
		else if ( teleporter == 2 )
		{
			hasteled = false;
			teleportToX = 2964;
			teleportToY = 3378;
			setAnimation( 715 );
		}
		else if ( teleporter == 3 )
		{
			hasteled = false;
			teleportToX = 3222;
			teleportToY = 3218;
			setAnimation( 715 );
		}
		else if ( teleporter == 4 )
		{
			hasteled = false;
			teleportToX = 2757;
			teleportToY = 3477;
			setAnimation( 715 );
		}
		else if ( teleporter == 5 )
		{
			hasteled = false;
			teleportToX = 2662;
			teleportToY = 3305;
			setAnimation( 715 );
		}
		else if ( teleporter == 6 )
		{
			hasteled = false;
			teleportToX = 2549;
			teleportToY = 3113;
			setAnimation( 715 );
		}
		else if ( teleporter == 7 )
		{
			hasteled = false;
			teleportToX = 2480;
			teleportToY = 5174;
			setAnimation( 715 );
		}
		else if ( teleporter == 8 )
		{
			hasteled = false;
			teleportToX = 2761;
			teleportToY = 2784;
			setAnimation( 715 );
		}
		else if ( teleporter == 9 )
		{
			hasteled = false;
			teleportToX = 2635;//new spot(VIP)
			teleportToY = 3324;//new spot(VIP)
		}
		else if ( teleporter == 10 )
		{
			hasteled = false;
			teleportToX = 3088;
			teleportToY = 3501;
		}
		else if ( teleporter == 11 )
		{
			hasteled = false;
			teleportToX = 3368; ////////changethis
			teleportToY = 3270;
		}
		else if ( teleporter == 12 )
		{
			hasteled = false;
			teleportToX = 3261;
			teleportToY = 3269;
		}
		else if ( teleporter == 13 )
		{
			hasteled = false;
			teleportToX = 3161;
			teleportToY = 3671;
		}
		else if ( teleporter == 14 )
		{
			hasteled = false;
			teleportToX = 3288;
			teleportToY = 3886;
		}
		else if ( teleporter == 15 )
		{
			hasteled = false;
			teleportToX = 3091;
			teleportToY = 3963;
		}
	}

	public void println( String str )
	{
		Console.WriteLine( "[client-" + playerId + "-" + playerName + "]: "
				+ str );
	}

	public void println_debug( String str )
	{
		Console.WriteLine( "[client-" + playerId + "-" + playerName + "]: "
				+ str );
	}

	public int fLockon = 0;
	public int npcDamageDelay = 4;
	public Boolean isFighting = false;
	public int secondhittimer = 0;
	public Boolean secondhitready = false;
	public Boolean Usingbow;
	public int yellTimer = 0;
	public int damagedelay = 0;
	public int damagedelay2 = 0;
	public int masstimer = 10;
	public Boolean moveCloser = false;
	public Boolean isCloseEnoughNow = false;
	public void wildysigndisappear( )
	{
		sendFrame126( "", 13589 );
		sendFrame126( "", 13590 );
		sendFrame126( "", 13591 );
		sendFrame126( "", 13592 );
		sendFrame126( "", 13593 );
		sendFrame126( "", 13594 );
		sendFrame126( "", 13595 );
		sendFrame126( "", 13596 );
		sendFrame126( "", 13597 );
		sendFrame126( "", 13598 );
		sendFrame126( "", 13599 );
		sendFrame126( "", 13600 );
		setInterfaceWalkable( 13588 );
		appearanceUpdateRequired = true;
		updateRequired = true;
	}

	public void checkdistance( )
	{
		int EnemyX = server.npcHandler.npcs[attacknpc].absX;
		int EnemyY = server.npcHandler.npcs[attacknpc].absY;
		int EnemyHP = server.npcHandler.npcs[attacknpc].HP;
		if ( GoodDistance( EnemyX, EnemyY, absX, absY, 2 ) == true )
		{
			isCloseEnoughNow = true;
			moveCloser = false;
		}
	}
	public Boolean isinJail( int coordX, int coordY, int Type )
	{
		if ( ( coordY >= 3511 ) && ( coordY <= 3517 ) && ( coordX <= 3111 ) && ( coordX >= 3107 ) )
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public Boolean showWarning = true;
	public int jailWarnings = 0;

	public override Boolean process( )
	{

		isInDuelArenaTeleport( absX, absY, 1 );

		rightClickCheck();

		if ( absY == 3518 || absY == 3519 )
		{
			if ( showWarning == true )
			{
				showInterface( 1908 );
				showWarning = false;
				WalkTo( 0, 0 );
				teleportToY = 3517;
			}
		}
		if ( AntiDupe > 0 )
		{
			AntiDupe--;
		}
		if ( isInWilderness( absX, absY, 1 ) )
		{
			if ( !hasWildySign )
			{
				hasWildySign = true;
				outStream.createFrame( 208 );
				outStream.writeWordBigEndian_dup( 197 );
				sendQuest( "Level: " + wildyLevel, 199 );
			}
			if ( !isInWilderness( absX, absY, 1 ) )
			{
				hasWildySign = false;
				wildysigndisappear();

			}
			int level = ( ( absY - 3520 ) / 8 ) + 1;
			if ( level != wildyLevel )
			{
				wildyLevel = level;
				sendQuest( "Level: " + wildyLevel, 199 );

			}
		}
		if ( !isInWilderness( absX, absY, 1 ) )
		{
			wildysigndisappear();
			hasWildySign = false;
		}
		damagedelay -= 1;
		damagedelay2 -= 1;
		damagedelay3 -= 1;
		damagedelay0 -= 1;
		if ( damagedelay == 0 )
		{
			DAMAGENPC();
		}
		if ( damagedelay0 == 0 )
		{
			DAMAGENPC();
		}
		if ( damagedelay2 == 0 )
		{
			DAMAGEPVP();
		}
		if ( damagedelay3 == 0 )
		{
			DAMAGEPVP();
		}

		if ( ( hasteled == true ) && ( tele2timer == 2 ) && ( teleporter <= 8 ) )
		{
			gfx100( 308 );
		}
		if ( ( hasteled == true ) && ( tele2timer == 0 ) )
		{
			teleporterprocess();
		}
		if ( teleport == true && teletimer == 2 && normaltele == false )
		{
			gfx100( 308 );
		}
		if ( teleport == true && teletimer == 5 && normaltele == false )
		{
			setAnimation( 714 );
		}

		teletimer -= 1;


		tele2timer -= 1;

		if ( teleport == false && teletimer <= 0 )
		{
			resetAnimation();
		}

		if ( weapontimer > 0 && weapontimer2 <= 0 )
		{
			weapontimer -= 1;
			weapontimer2 = 2;
		}
		weapontimer2 -= 1;
		scanPickup();


		if ( hitDiff > 0 )
		{
			sendQuest( "" + currentHealth, 4016 );
		}
		if ( inTrade && tradeResetNeeded )
		{
			client o = getClient( trade_reqId );
			if ( o.tradeResetNeeded )
			{
				resetTrade();
				o.resetTrade();
			}
		}
		if ( tStage == 1 && tTime == 0 )
		{
			setAnimation( 1979 );
			lowGFX( 392, 0 );
			updateRequired = true;
			appearanceUpdateRequired = true;
			tTime = TimeHelper.CurrentTimeMillis();
			tStage = 2;
			deleteItem( delItem1, delAmount1 );
			deleteItem( delItem2, delAmount2 );
			deleteItem( delItem3, delAmount3 );
		}

		if ( ( tStage == 2 ) && ( TimeHelper.CurrentTimeMillis() - tTime >= 900 ) )
		{
			teleportToX = tX;
			teleportToY = tY;
			heightLevel = tH;
			updateRequired = true;
			appearanceUpdateRequired = true;
			tStage = 0;
			tTime = 0;
			resetAnimation();
		}

		long current = TimeHelper.CurrentTimeMillis();
		if ( inCombat && ( current - lastCombat >= 20000 ) )
		{
			inCombat = false;
		}


		if ( startDuel && ( duelChatTimer <= 0 ) )
		{
			startDuel = false;
		}
		if ( ( GObjChange == 1 ) && ( GObjSet == 1 ) )
		{
			commitobj();
			GObjChange = 0;
		}
		EntangleDelay -= 1;
		teletimer -= 1;
		if ( ( teleport == true ) && ( teletimer >= 0 ) )
		{
			WalkTo( 0, 0 );
			WalkTo( 0, 0 );
		}

		if ( ( teleport == true ) && ( teletimer <= 0 ) )
		{
			if ( ancientstele == false )
			{
				setAnimation( 715 );
			}
			teleportToX = teleX;
			teleportToY = teleY;
			heightLevel = newheightLevel;
			teleport = false;
			teleX = 0;
			teleY = 0;
			newheightLevel = 0;
			pEmote = playerSE;
			updateRequired = true;
			appearanceUpdateRequired = true;
		}


		if ( resetanim <= 0 )
		{
			resetAnimation();
			resetanim = 8;
		}


		if ( ( AnimationReset == true ) && ( actionTimer <= 0 ) )
		{
			resetAnimation();
			AnimationReset = false;
			emotes = 0;
			pEmote = playerSE;
			updateRequired = true;
			appearanceUpdateRequired = true;
		}
		if ( actionAmount < 0 )
		{
			actionAmount = 0;
		}
		if ( actionTimer > 0 )
		{
			actionTimer -= 1;
		}
		if ( attackTimer > 0 )
		{
			attackTimer -= 1;
		}

		if ( spellHitTimer > 0 )
		{
			spellHitTimer--;
		}
		if ( spellHitTimer == 0 )
		{
			if ( castSpell )
			{
				castSpell = false;
				if ( isSpellNPC && ( spellNpcIndex != -1 ) )
					appendHitToNpc( spellNpcIndex, spellHit, isStillSpell );
				else if ( !isSpellNPC && ( spellPlayerIndex != -1 ) )
					appendHitToPlayer( spellPlayerIndex, spellHit, isStillSpell );
				resetAnimation();
			}
			spellHitTimer = -1; // FIXED: Why call this over and over? -.-
								// -bakatool
		}




		if ( UpdateShop == true )
		{
			resetItems( 3823 );
			resetShop( MyShopID );
		}


		if ( stairs > 0 )
		{
			if ( GoodDistance( skillX, skillY, absX, absY, stairDistance ) == true )
			{
				UseStairs( stairs, absX, absY );
			}
		}
		// objects
		if ( doors > -1 )
		{
			if ( GoodDistance2( skillX, skillY, absX, absY, 1 ) == true )
			{
				ChangeDoor( doors );
				doors = -1;
			}
		}
		// check banking
		if ( WanneBank > 0 )
		{
			if ( GoodDistance( skillX, skillY, absX, absY, WanneBank ) == true )
			{
				openUpBank();
				WanneBank = 0;
			}
		}
		// check shopping
		if ( WanneShop > 0 )
		{
			if ( GoodDistance( skillX, skillY, absX, absY, 1 ) == true )
			{
				openUpShop( WanneShop );
				WanneShop = 0;
			}
		}

		if ( ( WannePickUp == true ) && ( IsUsingSkill == false ) )
		{
			if ( pickUpItem( PickUpID, PickUpAmount ) == true )
			{
				PickUpID = 0;
				PickUpAmount = 0;
				PickUpDelete = 0;
				WannePickUp = false;
			}
		}
		// Attacking in wilderness
		long thisTime = TimeHelper.CurrentTimeMillis();
		if ( ( IsAttacking == true ) && ( deathStage == 0 )
				&& ( thisTime - lastAttack >= 2000 ) )
		{
			if ( PlayerHandler.players[AttackingOn] != null )
			{
				if ( PlayerHandler.players[AttackingOn].currentHealth > 0 )
				{
					Attack();
				}
				else
				{

					ResetAttack();
					// if(duelStatus == 3)
					// DuelVictory(p.absX, p.absY);

				}
			}
			else
			{
				ResetAttack();
			}
		}

		if ( currentHealth < 1 )
		{
			deathStage = 1;
		}
		// Attacking an NPC
		if ( ( IsAttackingNPC == true ) && ( deathStage == 0 ) )
		{
			if ( server.npcHandler.npcs[attacknpc] != null )
			{
				if ( ( server.npcHandler.npcs[attacknpc].IsDead == false )
						&& ( server.npcHandler.npcs[attacknpc].MaxHP > 0 ) )
				{
					AttackNPC();
				}
				else
				{
					ResetAttackNPC();

				}
			}
			else
			{
				ResetAttackNPC();
			}
		}
		// If killed apply dead

		GotOwned();

		if ( NpcWanneTalk == 2 )
		{
			// Bank Booth
			if ( GoodDistance2( absX, absY, skillX, skillY, 1 ) == true )
			{
				NpcDialogue = 1;
				NpcTalkTo = GetNPCID( skillX, ( skillY - 1 ) );
				NpcWanneTalk = 0;
			}
		}
		else if ( NpcWanneTalk > 0 )
		{
			if ( GoodDistance2( absX, absY, skillX, skillY, 2 ) == true )
			{
				NpcDialogue = NpcWanneTalk;
				NpcTalkTo = GetNPCID( skillX, skillY );
				NpcWanneTalk = 0;
			}
		}
		if ( ( NpcDialogue > 0 ) && ( NpcDialogueSend == false ) )
		{
			UpdateNPCChat();
		}

		if ( isKicked )
		{
			disconnected = true;
			if ( saveNeeded )
				savegame( true );
			outStream.createFrame( 109 );
		}

		if ( globalMessage.Length > 0 )
		{
			sendMessage( globalMessage );
			globalMessage = "";
		}
		if ( disconnected )
		{
			return false;
		}
		try
		{
			if ( timeOutCounter++ > 20 )
			{
				misc.println( "Client lost connection: timeout" );
				disconnected = true;
				if ( saveNeeded )
					savegame( true );
				return false;
			}
			if (inputStream == null) {
				return false;
			}

			int avail = mySock.Available;

			if ( avail == 0 )
			{
				return false;
			}

			if ( packetType == -1 )
			{
				packetType = inputStream.ReadByte() & 0xff;

				//Byte[] packetTypeBytes = new Byte[1];
				//inputStream.Read( packetTypeBytes, 0, 1 );
				//packetType = packetTypeBytes[0] & 0xff;

				//if ( inStreamDecryption != null )
				//{
				//	packetType = packetType - inStreamDecryption.getNextKey()
				//			& 0xff;
				//}
				packetSize = packetSizes[packetType];
				avail--;
			}
			if ( packetSize == -1 )
			{
				if ( avail > 0 )
				{
					// this is a variable size packet, the next byte containing
					// the length of said						
					//Byte[] packetSizeBytes = new Byte[1];
					//inputStream.Read( packetSizeBytes, 0, 1 );
					//packetSize = ( ( Int32 ) packetSizeBytes[0] ) & 0xff;
					
					packetSize = inputStream.ReadByte() & 0xff;	
					avail--;
				}
				else
				{
					return false;
				}
			}
			if ( avail < packetSize )
			{
				return false;
			} // packet not completely arrived here yet

			if ( packetSize > 0 )
				fillInStream( packetSize );
			timeOutCounter = 0; // reset

			parseIncomingPackets(); // method that does actually interprete
									// these packets

			packetType = -1;
		}
		catch ( Exception __ex )
		{
			//__ex.printStackTrace();
			Console.WriteLine( "Sharp317 [fatal] - exception" );
			savegame( true );
			disconnected = true;
		}
		return true;
	}

	public int randomItem( int[] array )
	{
		int len = array.Length;
		int ran = misc.random( len );
		return array[ran];
	}

	public void randomize( int o, int oo, int ooo, int oooo )
	{
		outStream.createFrame( 53 );
		outStream.writeWord( o );
		outStream.writeWord( oo );
		outStream.writeByte( ooo );
		outStream.writeWordBigEndianA( oooo );
		flushOutStream();
	}

	public void RefreshDuelRules( )
	{
		for ( int i = 0; i < duelLine.Length; i++ )
		{
			if ( duelRule[i] )
			{
				sendQuest( "@red@" + duelNames[i], duelLine[i] );
			}
			else
			{
				sendQuest( "@cya@" + duelNames[i], duelLine[i] );
			}
		}
	}


	public int GetWepAnim( )
	{
		if ( playerEquipment[playerWeapon] == -1 ) // unarmed
			if ( FightType == 2 ) // kick
			{
				if ( playerEquipment[playerWeapon] == 5698 ) // dds

					if ( FightType == 3 ) // slash
					{
						return 402;
					}
				if ( playerEquipment[playerWeapon] == 4212 ) //bows made by killamess
				{
					return 426;
				}
				if ( playerEquipment[playerWeapon] == 15156 ) //bows made by killamess
				{
					return 426;
				}
				if ( ( playerEquipment[playerWeapon] == 861 ) || ( playerEquipment[playerWeapon] == 4212 ) )
				{
					return 426;
				}
				return 423;
			}
			else
			{
				return 422;
			}
		if ( playerEquipment[playerWeapon] == 4151 ) // whip
		{
			return 1658;
		}
		if ( playerEquipment[playerWeapon] == 8447 ) // cat toy
		{
			return 1658;
		}
		if ( playerEquipment[playerWeapon] == 868 ) // throwing knives
		{
			return 385;
		}
		if ( playerEquipment[playerWeapon] == 6527 ) // obby maul plox
		{
			return 2927;
		}
		if ( playerEquipment[playerWeapon] == 6541 ) // Mouse Toy
		{
			return 1658;
		}
		if ( playerEquipment[playerWeapon] == 15156 ) // dark bow
		{
			return 426;
		}
		if ( playerEquipment[playerWeapon] == 1305 ) // d long
		{
			return 451;
		}
		if ( playerEquipment[playerWeapon] == 15333 ) // godsword
		{
			return 407;
		}
		if ( playerEquipment[playerWeapon] == 15334 ) // godsword
		{
			return 407;
		}
		if ( playerEquipment[playerWeapon] == 15335 ) // godsword
		{
			return 407;
		}
		if ( playerEquipment[playerWeapon] == 15336 ) // godsword
		{
			return 407;
		}
		if ( playerEquipment[playerWeapon] == 14915 ) // anchor
		{
			return 406;
		}
		if ( playerEquipment[playerWeapon] == 6739 ) // dragon axe
		{
			return 451;
		}
		if ( playerEquipment[playerWeapon] == 1321 || playerEquipment[playerWeapon] == 1323 || playerEquipment[playerWeapon] == 1325 || playerEquipment[playerWeapon] == 1327 || playerEquipment[playerWeapon] == 1329 || playerEquipment[playerWeapon] == 1327 || playerEquipment[playerWeapon] == 1321 || playerEquipment[playerWeapon] == 1333 ) // scimitars
		{
			return 451;
		}
		if ( playerEquipment[playerWeapon] == 4587 ) // d scim
		{
			return 451;
		}
		if ( playerEquipment[playerWeapon] == 746 ) // d scim
		{
			return 451;
		}
		if ( playerEquipment[playerWeapon] == 3204 ) // dragon halberd
		{
			return 440;
		}
		if ( playerEquipment[playerWeapon] == 6818 ) // bow-sword
		{
			return 440;
		}
		if ( playerEquipment[playerWeapon] == 3202 ) // rune halberd
		{
			return 440;
		}
		if ( playerEquipment[playerWeapon] == 4212 || playerEquipment[playerWeapon] == 859 || playerEquipment[playerWeapon] == 861 || playerEquipment[playerWeapon] == 6724 ) //bows
		{
			return 426;
		}
		if ( playerEquipment[playerWeapon] == 15156 ) //bows made by killamess
		{
			return 426;
		}

		// 839 841 843 845 847 849 851 853 855 857 859 861

		if ( playerEquipment[playerWeapon] == 839 || playerEquipment[playerWeapon] == 841 || playerEquipment[playerWeapon] == 843 || playerEquipment[playerWeapon] == 845 || playerEquipment[playerWeapon] == 847 || playerEquipment[playerWeapon] == 849 || playerEquipment[playerWeapon] == 851 || playerEquipment[playerWeapon] == 853 || playerEquipment[playerWeapon] == 855 || playerEquipment[playerWeapon] == 857 || playerEquipment[playerWeapon] == 859 || playerEquipment[playerWeapon] == 861 ) //bows
		{
			return 426;
		}
		if ( playerEquipment[playerWeapon] == 4153 ) // maul
		{
			return 1665;
		}
		if ( playerEquipment[playerWeapon] == 6528 ) // obby maul
		{
			return 2661;
		}
		if ( playerEquipment[playerWeapon] == 5018 ) // bone club
		{
			return 451;
		}
		if ( playerEquipment[playerWeapon] == 3101 ) // Rune claws
		{
			return 451;
		}
		if ( playerEquipment[playerWeapon] == 7449 ) // noob smasher
		{
			return 1665;
		}
		if ( playerEquipment[playerWeapon] == 1377 ) // dragon b axe
		{
			return 1833;
		}
		if ( playerEquipment[playerWeapon] == 1373 ) // rune b axe
		{
			return 1833;
		}
		if ( playerEquipment[playerWeapon] == 1434 ) // dragon mace
		{
			return 1833;
		}
		if ( playerEquipment[playerWeapon] == 5018 ) // dragon mace
		{
			return 1833;
		}
		if ( playerEquipment[playerWeapon] == 5730 ) // dragon spear
		{
			return 2080;
		}
		if ( playerEquipment[playerWeapon] == 4718 ) // dharoks axe
		{
			return 2067;
		}
		if ( playerEquipment[playerWeapon] == 4726 ) // guthans spear
		{
			return 2080;
		}
		if ( playerEquipment[playerWeapon] == 4747 ) // torags hammers
		{
			return 2068;
		}
		if ( playerEquipment[playerWeapon] == 4755 ) // veracs flail
		{
			return 2062;
		}
		if ( playerEquipment[playerWeapon] == 4734 ) // karils x bow
		{
			return 2075;
		}
		if ( playerEquipment[playerWeapon] == 837 ) // crossbow
		{
			return 427;
		}
		if ( playerEquipment[playerWeapon] == 10431 ) // rune crossbow
		{
			return 427;
		}
		if ( playerEquipment[playerWeapon] == 1215 || playerEquipment[playerWeapon] == 1231 || playerEquipment[playerWeapon] == 5680 || playerEquipment[playerWeapon] == 5698 ) // dragon daggers
		{
			return 402;
		}
		if ( playerEquipment[playerWeapon] == 6609 || playerEquipment[playerWeapon] == 1307 || playerEquipment[playerWeapon] == 1309 || playerEquipment[playerWeapon] == 1311 || playerEquipment[playerWeapon] == 1313 || playerEquipment[playerWeapon] == 1315 || playerEquipment[playerWeapon] == 1317 || playerEquipment[playerWeapon] == 1319 )
		// 2 handers
		{
			return 407;
		}
		if ( playerEquipment[playerWeapon] == 7158 ) // d2h
		{
			return 407;
		}
		if ( playerEquipment[playerWeapon] == 1319 ) // r2h
		{
			return 407;
		}
		if ( playerEquipment[playerWeapon] == 1419 ) // scythe
		{
			return 408;
		}
		if ( playerEquipment[playerWeapon] == 4566 ) // rubber chicken
		{
			return 1833;
		}
		else
		{
			return 0x326;
		}
	}

	public int GetRunAnim( int id )
	{
		if ( id == 4151 ) // whip
		{
			return 1661;
		}
		if ( id == 8447 ) // cat toy
		{
			return 1661;
		}
		if ( id == 6818 ) // bow-sword
		{
			return 744;
		}
		if ( id == 4734 || id == 837 ) // karils x bow
		{
			return 2077;
		}
		if ( id == 4153 ) // maul
		{
			return 1664;
		}
		if ( id == 1419 ) // scythe
		{
			return 1664;
		}
		if ( id == 7449 ) // noobsmasher
		{
			return 1664;
		}
		else
		{
			return 0x338;
		}
	}

	public int GetWalkAnim( int id )
	{
		if ( id == 4718 ) // dharoks axe
		{
			return 2064;
		}
		if ( id == 4039 || id == 4037 || id == 1379 || id == 3204 || id == 3202 || id == 1381 || id == 1383 || id == 1385 || id == 1387 || id == 1389 || id == 1391 || id == 1393 || id == 1395 || id == 1397 || id == 1399 || id == 1401 || id == 1403 || id == 145 || id == 1407 || id == 1409 || id == 3053 || id == 3054 || id == 4170 || id == 4675 || id == 4710 || id == 6526 || id == 4726 || id == 6562 || id == 6563 || id == 6914 || id == 5730 ) // staves + d long and most other weps with str8 up emote
		{
			return 1146;
		}
		if ( playerEquipment[playerFeet] == 4084 ) // sled
		{
			return 755;
		}
		if ( id == 4565 ) // basket of eggs :)
		{
			return 1836;
		}
		if ( id == 4755 ) // veracs flail
		{
			return 2060;
		}
		if ( id == 4734 || id == 837 ) // karils x bow
		{
			return 2076;
		}
		if ( id == 4153 || id == 15334 || id == 15333 || id == 15334 || id == 15335 || id == 15336 || id == 1419 ) // maul
		{
			return 1663;
		}
		if ( id == 7158 || id == 4718 || id == 1319 || id == 6528 || id == 14915 ) // 2h + gr8 axe
		{
			return 2064;
		}
		if ( id == 7449 ) // noob smasher
		{
			return 1663;
		}
		if ( id == 4151 ) // whip
		{
			return 1661;
		}
		if ( id == 8447 ) // cat toy
		{
			return 1661;
		}
		else
		{
			return 0x333;
		}
	}

	public int GetStandAnim( int id )
	{
		if ( id == 4718 ) // dharoks axe
		{
			return 2065;
		}
		if ( id == 4755 ) // veracs flail
		{
			return 2061;
		}
		if ( id == 4734 || id == 837 ) // karils x bow
		{
			return 2074;
		}
		if ( id == 4153 || id == 15334 || id == 15333 || id == 15334 || id == 15335 || id == 15336 || id == 1419 ) // maul
		{
			return 1662;
		}
		if ( id == 7449 ) // noob smasher
		{
			return 1662;
		}
		if ( id == 4565 ) // basket of eggs :)
		{
			return 1836;
		}
		if ( id == 1305 || id == 1379 || id == 1381 || id == 1383 || id == 1385 || id == 1387 || id == 1389 || id == 1391 || id == 1393 || id == 1395 || id == 1397 || id == 1399 || id == 1401 || id == 1403 || id == 145 || id == 1407 || id == 1409 || id == 3053 || id == 3054 || id == 4170 || id == 4675 || id == 4710 || id == 6526 || id == 4726 || id == 6562 || id == 6563 || id == 5730 ) // staves
		{
			return 809;
		}
		if ( id == 7158 || id == 1319 || id == 6528 || id == 14915 ) // 2h
		{
			return 2065;
		}
		if ( id == 3204 || id == 3202 ) // halberd
		{
			return 809;
		}
		else
		{
			return 0x328;
		}
	}

	public int GetBlockAnim( int id )
	{
		if ( id == 4755 ) // veracs flail
		{
			return 2063;
		}
		if ( id == 4151 ) // whip
		{
			return 1659;
		}
		if ( id == 10229 ) // defender
		{
			return 1659;
		}
		if ( id == 1171 ) // wooden shield
		{
			return 403;
		}
		if ( id == 1185 ) // rune sq shield 
		{
			return 403;
		}
		if ( id == 1187 ) // dragon sq shield 
		{
			return 403;
		}
		if ( id == 1191 ) // iron kite shield 
		{
			return 403;
		}
		if ( id == 1201 ) // rune kite shield 
		{
			return 403;
		}
		if ( id == 2659 ) // zammy kite shield 
		{
			return 403;
		}
		if ( id == 2667 ) // sara kite shield 
		{
			return 403;
		}
		if ( id == 2675 ) // guthix kite shield
		{
			return 403;
		}
		if ( id == 3122 ) // granite shield 
		{
			return 403;
		}
		if ( id == 3488 ) // gilded kite shield 
		{
			return 403;
		}
		if ( id == 4156 ) // mirror shield
		{
			return 403;
		}
		if ( id == 6524 ) // obby shield
		{
			return 403;
		}
		if ( id == 4153 ) // maul
		{
			return 1666;
		}
		if ( id == 1419 ) // scythe
		{
			return 1666;
		}
		else
		{
			return 1834;
		}
	}

	public void refreshDuelScreen( )
	{
		client other = getClient( duel_with );
		if ( !ValidClient( duel_with ) )
			return;
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( 6669 );
		outStream.writeWord( offeredItems.Count );
		int current = 0;
		foreach ( GameItem item in offeredItems )
		{
			if ( item.amount > 254 )
			{
				outStream.writeByte( 255 ); // item's stack count. if over 254,
											// write byte 255
				outStream.writeDWord_v2( item.amount ); // and then the real
														// value with
														// writeDWord_v2
			}
			else
			{
				outStream.writeByte( item.amount );
			}
			if ( ( item.id > 20000 ) || ( item.id < 0 ) )
			{
				item.id = 20000;
			}
			outStream.writeWordBigEndianA( item.id + 1 ); // item id
			current++;
		}
		if ( current < 27 )
		{
			for ( int i = current; i < 28; i++ )
			{
				outStream.writeByte( 1 );
				outStream.writeWordBigEndianA( -1 );
			}
		}
		outStream.endFrameVarSizeWord();
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( 6670 );
		outStream.writeWord( other.offeredItems.Count );
		current = 0;
		foreach ( GameItem item in other.offeredItems )
		{
			if ( item.amount > 254 )
			{
				outStream.writeByte( 255 ); // item's stack count. if over 254,
											// write byte 255
				outStream.writeDWord_v2( item.amount ); // and then the real
														// value with
														// writeDWord_v2
			}
			else
			{
				outStream.writeByte( item.amount );
			}
			if ( ( item.id > 20000 ) || ( item.id < 0 ) )
			{
				item.id = 20000;
			}
			outStream.writeWordBigEndianA( item.id + 1 ); // item id
			current++;
		}
		if ( current < 27 )
		{
			for ( int i = current; i < 28; i++ )
			{
				outStream.writeByte( 1 );
				outStream.writeWordBigEndianA( -1 );
			}
		}
		outStream.endFrameVarSizeWord();
	}

	public void refreshSkills( )
	{

		sendQuest( "" + playerLevel[0] + "", 4004 );
		sendQuest( "" + playerLevel[2] + "", 4006 );
		sendQuest( "" + playerLevel[1] + "", 4008 );
		sendQuest( "" + playerLevel[4] + "", 4010 );
		sendQuest( "" + playerLevel[5] + "", 4012 );
		sendQuest( "" + playerLevel[6] + "", 4014 );
		sendQuest( "" + currentHealth + "", 4016 );
		sendQuest( "" + playerLevel[16] + "", 4018 );
		sendQuest( "" + playerLevel[15] + "", 4020 );
		sendQuest( "" + playerLevel[17] + "", 4022 );
		sendQuest( "" + playerLevel[12] + "", 4024 );
		sendQuest( "" + playerLevel[9] + "", 4026 );
		sendQuest( "" + playerLevel[14] + "", 4028 );
		sendQuest( "" + playerLevel[13] + "", 4030 );
		sendQuest( "" + playerLevel[10] + "", 4032 );
		sendQuest( "" + playerLevel[7] + "", 4034 );
		sendQuest( "" + playerLevel[11] + "", 4036 );
		sendQuest( "" + playerLevel[8] + "", 4038 );
		sendQuest( "" + playerLevel[20] + "", 4152 );
		sendQuest( "" + playerLevel[18] + "", 12166 );
		sendQuest( "" + playerLevel[19] + "", 13926 );

		sendQuest( "" + getLevelForXP( playerXP[0] ) + "", 4005 );
		sendQuest( "" + getLevelForXP( playerXP[2] ) + "", 4007 );
		sendQuest( "" + getLevelForXP( playerXP[1] ) + "", 4009 );
		sendQuest( "" + getLevelForXP( playerXP[4] ) + "", 4011 );
		sendQuest( "" + getLevelForXP( playerXP[5] ) + "", 4013 );
		sendQuest( "" + getLevelForXP( playerXP[6] ) + "", 4015 );
		sendQuest( "" + getLevelForXP( playerXP[3] ) + "", 4017 );
		sendQuest( "" + getLevelForXP( playerXP[16] ) + "", 4019 );
		sendQuest( "" + getLevelForXP( playerXP[15] ) + "", 4021 );
		sendQuest( "" + getLevelForXP( playerXP[17] ) + "", 4023 );
		sendQuest( "" + getLevelForXP( playerXP[12] ) + "", 4025 );
		sendQuest( "" + getLevelForXP( playerXP[9] ) + "", 4027 );
		sendQuest( "" + getLevelForXP( playerXP[14] ) + "", 4029 );
		sendQuest( "" + getLevelForXP( playerXP[13] ) + "", 4031 );
		sendQuest( "" + getLevelForXP( playerXP[10] ) + "", 4033 );
		sendQuest( "" + getLevelForXP( playerXP[7] ) + "", 4035 );
		sendQuest( "" + getLevelForXP( playerXP[11] ) + "", 4037 );
		sendQuest( "" + getLevelForXP( playerXP[8] ) + "", 4039 );
		sendQuest( "" + getLevelForXP( playerXP[20] ) + "", 4153 );
		sendQuest( "" + getLevelForXP( playerXP[18] ) + "", 12167 );
		sendQuest( "" + getLevelForXP( playerXP[19] ) + "", 13927 );

		sendQuest( "" + playerXP[0] + "", 4044 );
		sendQuest( "" + playerXP[2] + "", 4050 );
		sendQuest( "" + playerXP[1] + "", 4056 );
		sendQuest( "" + playerXP[4] + "", 4062 );
		sendQuest( "" + playerXP[5] + "", 4068 );
		sendQuest( "" + playerXP[6] + "", 4074 );
		sendQuest( "" + playerXP[3] + "", 4080 );
		sendQuest( "" + playerXP[16] + "", 4086 );
		sendQuest( "" + playerXP[15] + "", 4092 );
		sendQuest( "" + playerXP[17] + "", 4098 );
		sendQuest( "" + playerXP[12] + "", 4104 );
		sendQuest( "" + playerXP[9] + "", 4110 );
		sendQuest( "" + playerXP[14] + "", 4116 );
		sendQuest( "" + playerXP[13] + "", 4122 );
		sendQuest( "" + playerXP[10] + "", 4128 );
		sendQuest( "" + playerXP[7] + "", 4134 );
		sendQuest( "" + playerXP[11] + "", 4140 );
		sendQuest( "" + playerXP[8] + "", 4146 );
		sendQuest( "" + playerXP[20] + "", 4157 );
		sendQuest( "" + playerXP[18] + "", 12171 );
		sendQuest( "" + playerXP[19] + "", 13921 );

		sendQuest( "" + getXPForLevel( playerLevel[0] + 1 ) + "", 4045 );
		sendQuest( "" + getXPForLevel( playerLevel[2] + 1 ) + "", 4051 );
		sendQuest( "" + getXPForLevel( playerLevel[1] + 1 ) + "", 4057 );
		sendQuest( "" + getXPForLevel( playerLevel[4] + 1 ) + "", 4063 );
		sendQuest( "" + getXPForLevel( playerLevel[5] + 1 ) + "", 4069 );
		sendQuest( "" + getXPForLevel( playerLevel[6] + 1 ) + "", 4075 );
		sendQuest( "" + getXPForLevel( playerLevel[3] + 1 ) + "", 4081 );
		sendQuest( "" + getXPForLevel( playerLevel[16] + 1 ) + "", 4087 );
		sendQuest( "" + getXPForLevel( playerLevel[15] + 1 ) + "", 4093 );
		sendQuest( "" + getXPForLevel( playerLevel[17] + 1 ) + "", 4099 );
		sendQuest( "" + getXPForLevel( playerLevel[12] + 1 ) + "", 4105 );
		sendQuest( "" + getXPForLevel( playerLevel[9] + 1 ) + "", 4111 );
		sendQuest( "" + getXPForLevel( playerLevel[14] + 1 ) + "", 4117 );
		sendQuest( "" + getXPForLevel( playerLevel[13] + 1 ) + "", 4123 );
		sendQuest( "" + getXPForLevel( playerLevel[10] + 1 ) + "", 4129 );
		sendQuest( "" + getXPForLevel( playerLevel[7] + 1 ) + "", 4135 );
		sendQuest( "" + getXPForLevel( playerLevel[11] + 1 ) + "", 4141 );
		sendQuest( "" + getXPForLevel( playerLevel[8] + 1 ) + "", 4147 );
		sendQuest( "" + getXPForLevel( playerLevel[20] + 1 ) + "", 4158 );
		sendQuest( "" + getXPForLevel( playerLevel[18] + 1 ) + "", 12172 );
		sendQuest( "" + getXPForLevel( playerLevel[19] + 1 ) + "", 13922 );
	}

	public void remove( int wearID, int slot )
	{
		if ( duelFight && duelRule[3] )
		{
			sendMessage( "Equipment changing has been disabled in this duel!" );
			return;
		}
		if ( addItem( playerEquipment[slot], playerEquipmentN[slot] ) )
		{
			playerEquipment[slot] = -1;
			playerEquipmentN[slot] = 0;
			outStream.createFrame( 34 );
			outStream.writeWord( 6 );
			outStream.writeWord( 1688 );
			outStream.writeByte( slot );
			outStream.writeWord( 0 );
			outStream.writeByte( 0 );
			ResetBonus();
			GetBonus();
			WriteBonus();
			if ( slot == playerWeapon )
			{
				SendWeapon( -1, "Unarmed" );
			}
			updateRequired = true;
			appearanceUpdateRequired = true;
		}
	}

	public void RemoveAllDuelItems( )
	{
		for ( int i = 0; i < duelItems.Length; i++ )
		{
			if ( duelItems[i] > 0 )
			{
				fromDuel( ( duelItems[i] - 1 ), i, duelItemsN[i] );
			}
		}
	}

	public void removeAllItems( )
	{
		for ( int i = 0; i < playerItems.Length; i++ )
		{
			playerItems[i] = 0;
		}
		for ( int i = 0; i < playerItemsN.Length; i++ )
		{
			playerItemsN[i] = 0;
		}
		resetItems( 3214 );
	}

	public void RemoveAllWindows( )
	{
		outStream.createFrame( 219 );
		flushOutStream();
	}

	public void removeGroundItem( int itemX, int itemY, int itemID )
	{
		// Phate: Omg fucking sexy! remoevs an item from absolute X and Y
		outStream.createFrame( 85 ); // Phate: Item Position Frame
		outStream.writeByteC( ( itemY - 8 * mapRegionY ) );
		outStream.writeByteC( ( itemX - 8 * mapRegionX ) );
		outStream.createFrame( 156 ); // Phate: Item Action: Delete
		outStream.writeByteS( 0 ); // x(4 MSB) y(LSB) coords
		outStream.writeWord( itemID ); // Phate: Item ID
									   // misc.printlnTag("RemoveGroundItem "+itemID+" "+(itemX - 8 *
									   // mapRegionX)+","+(itemY - 8 * mapRegionY));
	}

	public void removeObject( int x, int y, int obj)
	{
		// romoves obj from currentx,y
		outStream.createFrameVarSizeWord( 60 ); // tells baseX and baseY to
												// client
		outStream.writeByte( y - ( mapRegionY * 8 ) );
		outStream.writeByteC( x - ( mapRegionX * 8 ) );

		outStream.writeByte( 101 ); // remove object
		outStream.writeByteC( 0 ); // x and y from baseX
		outStream.writeByte( 0 ); // ??

		outStream.endFrameVarSizeWord();
	}

	public void replaceDoors( )
	{
		for ( int d = 0; d < DoorHandler.doorX.Length; d++ )
		{
			if ( ( DoorHandler.doorX[d] > 0 )
					&& ( DoorHandler.doorHeight[d] == heightLevel )
					&& ( Math.Abs( DoorHandler.doorX[d] - absX ) <= 120 )
					&& ( Math.Abs( DoorHandler.doorY[d] - absY ) <= 120 ) )
			{
				ReplaceObject( DoorHandler.doorX[d],
						DoorHandler.doorY[d],
						DoorHandler.doorId[d],
						DoorHandler.doorFace[d], 0 );
			}
		}
	}

	public void ReplaceObject( int objectX, int objectY, int NewObjectID,
			int Face, int ObjectType )
	{
		outStream.createFrame( 85 );
		outStream.writeByteC( objectY - ( mapRegionY * 8 ) );
		outStream.writeByteC( objectX - ( mapRegionX * 8 ) );

		outStream.createFrame( 101 );
		outStream.writeByteC( ( ObjectType << 2 ) + ( Face & 3 ) );
		outStream.writeByte( 0 );

		if ( NewObjectID != -1 )
		{
			outStream.createFrame( 151 );
			outStream.writeByteS( 0 );
			outStream.writeWordBigEndian( NewObjectID );
			outStream.writeByteS( ( ObjectType << 2 ) + ( Face & 3 ) );
			// FACE: 0= WEST | -1 = NORTH | -2 = EAST | -3 = SOUTH
			// ObjectType: 0-3 wall objects, 4-8 wall decoration, 9: diag.
			// walls, 10-11 world objects, 12-21: roofs, 22: floor decoration
		}
	}

	public void ReplaceObject2( int objectX, int objectY, int NewObjectID,
			int Face, int ObjectType )
	{
		outStream.createFrame( 85 );
		outStream.writeByteC( objectY - ( mapRegionY * 8 ) );
		outStream.writeByteC( objectX - ( mapRegionX * 8 ) );

		outStream.createFrame( 101 );
		outStream.writeByteC( ( ObjectType << 2 ) + ( Face & 3 ) );
		outStream.writeByte( 0 );

		if ( NewObjectID != -1 )
		{
			outStream.createFrame( 151 );
			outStream.writeByteS( 0 );
			outStream.writeWordBigEndian( NewObjectID );
			outStream.writeByteS( ( ObjectType << 2 ) + ( Face & 3 ) );
			// FACE: 0= WEST | -1 = NORTH | -2 = EAST | -3 = SOUTH
			// ObjectType: 0-3 wall objects, 4-8 wall decoration, 9: diag.
			// walls, 10-11 world objects, 12-21: roofs, 22: floor decoration
		}
	}

	public void reportAbuse( String abuser, int rule, int muted )
	{
		if ( playerRights == 0 )
		{
			writeLog( "abuser = " + abuser + " for rule " + rule + " muted? "
					+ ( muted > 0 ? "yes" : "no" ), "reports" );
			sendMessage( "Your report has been stored and online moderators have also been notified" );
		}
		else
		{
			Boolean online = false;
			// int duration = misc.times[rule];
			for ( int i = 0; i < PlayerHandler.players.Length; i++ )
			{
				client other = getClient( i );
				if ( !ValidClient( i ) )
					continue;
				if ( other.playerName.ToLower() == abuser.ToLower() )
				{
					online = true;
					other.kick();
					break;
				}
			}

			if ( online )
			{
				sendMessage( "Player kicked off server" );
			}
			else
			{
				sendMessage( "Player was not online" );
			}

		}

		if ( playerRights < 1 )
			modYell( "[Abuse] " + abuser + " reported by " + playerName
					+ " for rule " + rule + " (" + misc.rules[rule] + ")" );
		else
			modYell( "[Mod Chat] " + playerName + " banned " + abuser + " ("
					+ misc.rules[rule] + ")" );
	}

	public void resetAction( )
	{
		resetAction( true );
	}

	public void resetAction( Boolean full )
	{
		smelting = false;
		smelt_id = -1;
		shafting = false;
		spinning = false;
		crafting = false;
		fishing = false;
		if ( fletching )
		{
			// playerEquipment[playerWeapon] = originalW;
			// playerEquipment[playerWeapon] = originalS;
			updateRequired = true;
			appearanceUpdateRequired = true;
		}
		fletching = false;
		if ( full )
			resetAnimation();
	}

	public void resetAnimation( )
	{
		pEmote = playerSE;
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void resetArray( int[] array )
	{
		for ( int i = 0; i < array.Length; i++ )
		{
			array[i] = 0;
		}
	}

	public Boolean ResetAttack( )
	{
		pEmote = playerSE;
		IsAttacking = false;
		AttackingOn = 0;
		resetAnimation();
		IsUsingSkill = false;
		launcharrowtimer = 0;
		readytolaunch = false;
		startedrange = false;
		rangedelaytimer = 0;
		return true;
	}

	public Boolean ResetAttackNPC( )
	{
		if ( attacknpc > -1 && attacknpc < NPCHandler.maxNPCs )
		{
			server.npcHandler.npcs[attacknpc].IsUnderAttack = false;
		}
		readytolaunch = false;
		startedrange = false;
		IsAttackingNPC = false;
		attacknpc = -1;
		resetAnimation();
		pEmote = playerSE;
		faceNPC = 65535;
		faceNPCupdate = true;
		return true;
	}

	public Boolean ResetAttackPlayer( int NPCID )
	{
		server.npcHandler.npcs[NPCID].IsUnderAttack = false;
		server.npcHandler.npcs[NPCID].StartKilling = 0;
		server.npcHandler.npcs[NPCID].RandomWalk = true;
		server.npcHandler.npcs[NPCID].animNumber = 0x328;
		server.npcHandler.npcs[NPCID].animUpdateRequired = true;
		server.npcHandler.npcs[NPCID].updateRequired = true;
		return true;
	}

	public void resetBank( )
	{
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( 5382 ); // bank
		outStream.writeWord( playerBankSize ); // number of items
		for ( int i = 0; i < playerBankSize; i++ )
		{
			if ( bankItemsN[i] > 254 )
			{
				outStream.writeByte( 255 );
				outStream.writeDWord_v2( bankItemsN[i] );
			}
			else
			{
				outStream.writeByte( bankItemsN[i] ); // amount
			}
			if ( bankItemsN[i] < 1 )
			{
				bankItems[i] = 0;
			}
			if ( ( bankItems[i] > 20000 ) || ( bankItems[i] < 0 ) )
			{
				bankItems[i] = 20000;
			}
			outStream.writeWordBigEndianA( bankItems[i] ); // itemID
		}
		outStream.endFrameVarSizeWord();
	}

	public void ResetBonus( )
	{
		for ( int i = 0; i < playerBonus.Length; i++ )
		{
			playerBonus[i] = 0;
		}
	}

	public Boolean resetCO( )
	{
		cooking[0] = 0;
		cooking[1] = 0;
		cooking[2] = 0;
		cooking[4] = -1;
		IsUsingSkill = false;
		return true;
	}

	public void resetDuel( )
	{
		println( "duel reset" );
		closeInterface();
		canOffer = true;
		duel_with = 0;
		duelRequested = false;
		duelConfirmed = false;
		duelConfirmed2 = false;
		offeredItems.Clear();
		otherOfferedItems.Clear();
		duelFight = false;
		inDuel = false;
		duelRule = new Boolean[] { false, false, false, false, false, true,
					false, true, false, true, false };
	}

	public Boolean resetFM( )
	{
		firemaking[0] = 0;
		firemaking[1] = 0;
		firemaking[2] = 0;
		firemaking[4] = 0;
		skillX = -1;
		skillY = -1;
		IsUsingSkill = false;
		IsMakingFire = false;
		return true;
	}

	public void resetGFX( int id, int X, int Y )
	{
		GraphicsHandler.removeGFX( id, X, Y );
		firingspell = false;
		cast = false;
		fired = false;
	}

	public Boolean resetHE( )
	{
		healing[0] = 0;
		healing[1] = 0;
		healing[2] = 0;
		healing[3] = -1;
		healing[4] = -1;
		IsUsingSkill = false;
		return true;
	}

	public void resetItems( int WriteFrame )
	{
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( WriteFrame );
		outStream.writeWord( playerItems.Length );
		for ( int i = 0; i < playerItems.Length; i++ )
		{
			if ( playerItemsN[i] > 254 )
			{
				outStream.writeByte( 255 ); // item's stack count. if over 254,
											// write byte 255
				outStream.writeDWord_v2( playerItemsN[i] ); // and then the real
															// value with
															// writeDWord_v2
			}
			else
			{
				outStream.writeByte( playerItemsN[i] );
			}
			if ( ( playerItems[i] > 20000 ) || ( playerItems[i] < 0 ) )
			{
				playerItems[i] = 20000;
			}
			outStream.writeWordBigEndianA( playerItems[i] ); // item id
		}
		outStream.endFrameVarSizeWord();
	}



	public Boolean resetMI( )
	{
		mining[0] = 0;
		mining[1] = 0;
		mining[2] = 0;
		mining[4] = 0;
		skillX = -1;
		skillY = -1;
		IsMining = false;
		IsUsingSkill = false;
		return true;
	}

	public void resetOTItems( int WriteFrame )
	{
		client other = getClient( trade_reqId );
		if ( !ValidClient( trade_reqId ) )
			return;
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( WriteFrame );
		int len = other.offeredItems.Count;
		int current = 0;
		outStream.writeWord( len );
		foreach ( GameItem item in other.offeredItems )
		{
			if ( item.amount > 254 )
			{
				outStream.writeByte( 255 ); // item's stack count. if over 254,
											// write byte 255
				outStream.writeDWord_v2( item.amount ); // and then the real
														// value with
														// writeDWord_v2
			}
			else
			{
				outStream.writeByte( item.amount );
			}
			outStream.writeWordBigEndianA( item.id + 1 ); // item id
			current++;
		}
		if ( current < 27 )
		{
			for ( int i = current; i < 28; i++ )
			{
				outStream.writeByte( 1 );
				outStream.writeWordBigEndianA( -1 );
			}
		}
		outStream.endFrameVarSizeWord();
	}

	public void resetPos( )
	{
		teleportToX = 2606;
		teleportToY = 3102;
		sendMessage( "Welcome to Yanille" );
	}

	public Boolean resetPR( )
	{
		prayer[0] = 0;
		prayer[1] = 0;
		prayer[2] = 0;
		prayer[4] = -1;
		prayer[5] = -1;
		IsUsingSkill = false;
		return true;
	}

	public void resetShop( int ShopID )
	{
		int TotalItems = 0;

		for ( int i = 0; i < ShopHandler.MaxShopItems; i++ )
		{
			if ( ShopHandler.ShopItems[ShopID][i] > 0 )
			{
				TotalItems++;
			}
		}
		if ( TotalItems > ShopHandler.MaxShopItems )
		{
			TotalItems = ShopHandler.MaxShopItems;
		}
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( 3900 );
		outStream.writeWord( TotalItems );
		int TotalCount = 0;

		for ( int i = 0; i < ShopHandler.ShopItems.Length; i++ )
		{
			if ( ( ShopHandler.ShopItems[ShopID][i] > 0 )
					|| ( i <= ShopHandler.ShopItemsStandard[ShopID] ) )
			{
				if ( ShopHandler.ShopItemsN[ShopID][i] > 254 )
				{
					outStream.writeByte( 255 ); // item's stack count. if over
												// 254, write byte 255
					outStream
							.writeDWord_v2( ShopHandler.ShopItemsN[ShopID][i] ); // and
																						// then
																						// the
																						// real
																						// value
																						// with
																						// writeDWord_v2
				}
				else
				{
					outStream
							.writeByte( ShopHandler.ShopItemsN[ShopID][i] );
				}
				if ( ( ShopHandler.ShopItems[ShopID][i] > 20000 )
						|| ( ShopHandler.ShopItems[ShopID][i] < 0 ) )
				{
					ShopHandler.ShopItems[ShopID][i] = 20000;
				}
				outStream
						.writeWordBigEndianA( ShopHandler.ShopItems[ShopID][i] ); // item
																						 // id
				TotalCount++;
			}
			if ( TotalCount > TotalItems )
			{
				break;
			}
		}
		outStream.endFrameVarSizeWord();
	}

	public Boolean resetSM( )
	{
		if ( OriginalWeapon > -1 )
		{
			playerEquipment[playerWeapon] = OriginalWeapon;
			OriginalWeapon = -1;
			playerEquipment[playerShield] = OriginalShield;
			OriginalShield = -1;
		}
		smithing[0] = 0;
		smithing[1] = 0;
		smithing[2] = 0;
		smithing[4] = -1;
		smithing[5] = 0;
		skillX = -1;
		skillY = -1;
		IsUsingSkill = false;
		return true;
	}

	public Boolean resetStairs( )
	{
		stairs = 0;
		skillX = -1;
		skillY = -1;
		stairDistance = 1;
		stairDistanceAdd = 0;
		IsUsingSkill = false;
		return true;
	}

	public void resetTItems( int WriteFrame )
	{
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( WriteFrame );
		int len = offeredItems.Count;
		int current = 0;
		outStream.writeWord( len );
		foreach ( GameItem item in offeredItems )
		{
			if ( item.amount > 254 )
			{
				outStream.writeByte( 255 ); // item's stack count. if over 254,
											// write byte 255
				outStream.writeDWord_v2( item.amount ); // and then the real
														// value with
														// writeDWord_v2
			}
			else
			{
				outStream.writeByte( item.amount );
			}
			outStream.writeWordBigEndianA( item.id + 1 ); // item id
			current++;
		}
		if ( current < 27 )
		{
			for ( int i = current; i < 28; i++ )
			{
				outStream.writeByte( 1 );
				outStream.writeWordBigEndianA( -1 );
			}
		}
		outStream.endFrameVarSizeWord();
	}

	public void resetTrade( )
	{
		offeredItems.Clear();
		inTrade = false;
		trade_reqId = 0;
		canOffer = true;
		tradeConfirmed = false;
		tradeConfirmed2 = false;
		closeInterface();
		tradeResetNeeded = false;
		sendQuest( "Are you sure you want to make this trade?", 3535 );
	}

	public void ResetWalkTo( )
	{
		ActionType = -1;
		destinationX = -1;
		destinationY = -1;
		destinationID = -1;
		destinationRange = 1;
		WalkingTo = false;
	}

	public Boolean resetWC( )
	{
		woodcutting[0] = 0;
		woodcutting[1] = 0;
		woodcutting[2] = 0;
		woodcutting[4] = 0;
		woodcutting[5] = 2;
		skillX = -1;
		skillY = -1;
		IsCutting = false;
		IsUsingSkill = false;
		resetAnimation();
		return true;
	}

	public void restorePot( )
	{

		playerLevel[0] = getLevelForXP( playerXP[0] );
		sendFrame126( "" + playerLevel[0] + "", 4004 );
		playerLevel[1] = getLevelForXP( playerXP[1] );
		sendFrame126( "" + playerLevel[1] + "", 4008 );
		playerLevel[2] = getLevelForXP( playerXP[2] );
		sendFrame126( "" + playerLevel[2] + "", 4006 );
		playerLevel[4] = getLevelForXP( playerXP[4] );
		sendFrame126( "" + playerLevel[4] + "", 4010 );
		playerLevel[6] = getLevelForXP( playerXP[6] );
		sendFrame126( "" + playerLevel[6] + "", 4014 );
		playerLevel[7] = getLevelForXP( playerXP[7] );
		sendFrame126( "" + playerLevel[7] + "", 4034 );
		playerLevel[8] = getLevelForXP( playerXP[8] );
		sendFrame126( "" + playerLevel[8] + "", 4038 );
		playerLevel[9] = getLevelForXP( playerXP[9] );
		sendFrame126( "" + playerLevel[9] + "", 4026 );
		playerLevel[10] = getLevelForXP( playerXP[10] );
		sendFrame126( "" + playerLevel[10] + "", 4032 );
		playerLevel[11] = getLevelForXP( playerXP[11] );
		sendFrame126( "" + playerLevel[11] + "", 4036 );
		playerLevel[12] = getLevelForXP( playerXP[12] );
		sendFrame126( "" + playerLevel[12] + "", 4024 );
		playerLevel[13] = getLevelForXP( playerXP[13] );
		sendFrame126( "" + playerLevel[13] + "", 4030 );
		playerLevel[14] = getLevelForXP( playerXP[14] );
		sendFrame126( "" + playerLevel[14] + "", 4028 );
		playerLevel[15] = getLevelForXP( playerXP[15] );
		sendFrame126( "" + playerLevel[15] + "", 4020 );
		playerLevel[16] = getLevelForXP( playerXP[16] );
		sendFrame126( "" + playerLevel[16] + "", 4018 );
		playerLevel[17] = getLevelForXP( playerXP[17] );
		sendFrame126( "" + playerLevel[17] + "", 4022 );
		playerLevel[20] = getLevelForXP( playerXP[20] );
		sendFrame126( "" + playerLevel[20] + "", 4152 );
	}

	public void robelf( )
	{
		sendMessage( "You pickpocket the elf." );
		addItem( 995, 350 );
		addSkillXP( 353, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robfail( )
	{
		EntangleDelay = 40;
	}

	public void robfarmer( )
	{
		long now = TimeHelper.CurrentTimeMillis();
		if ( now - lastAction < 1500 )
			return;
		lastAction = now;
		sendMessage( "You pickpocket the farmer." );
		addItem( 314, misc.random( 9 ) );
		addSkillXP( 800, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;

	}

	public void robgnome( )
	{
		sendMessage( "You pickpocket the gnome." );
		addItem( 995, 400 );
		addSkillXP( 198, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robguard( )
	{
		sendMessage( "You pickpocket the gaurd." );
		addItem( 995, 30 );
		addSkillXP( 47, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robhero( )
	{
		sendMessage( "You pickpocket the hero." );
		addItem( 995, 300 );
		addSkillXP( 274, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robknight( )
	{
		sendMessage( "You pickpocket the knight." );
		addItem( 995, 50 );
		addSkillXP( 85, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robman( )
	{

		sendMessage( "You pickpocket the man." );
		addItem( 995, 3 );
		addSkillXP( 500, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );

	}

	public void robmasterfarmer( )
	{
		sendMessage( "You pickpocket the master farmer." );
		addItem( 995, 40 );
		addSkillXP( 43, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robpaladin( )
	{
		sendMessage( "You pickpocket the paladin." );
		addItem( 995, 80 );
		addSkillXP( 152, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robrogue( )
	{
		sendMessage( "You pickpocket the rogue." );
		addItem( 995, 45 );
		addSkillXP( 36, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robwarrior( )
	{
		sendMessage( "You pickpocket the warrior." );
		addItem( 995, 18 );
		addSkillXP( 4600, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void robwatchman( )
	{
		sendMessage( "You pickpocket the watchman." );
		addItem( 995, 60 );
		addSkillXP( 138, 17 );
		int EnemyX2 = server.npcHandler.npcs[NPCSlot].absX;
		int EnemyY2 = server.npcHandler.npcs[NPCSlot].absY;

		TurnPlayerTo( EnemyX2, EnemyY2 );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}

	public void run( )
	{
		// we just accepted a new connection - handle the login stuff
		isActive = false;
		long serverSessionKey = 0, clientSessionKey = 0;

		// randomize server part of the session key
		serverSessionKey = ( ( long ) ( MathHelper.Random() * 99999999D ) << 32 )
				+ ( long ) ( MathHelper.Random() * 99999999D );

		try
		{
			fillInStream( 2 );
			var loginId = inStream.readUnsignedByte();

			if ( loginId != 14 )
			{
				shutdownError( "Expected login Id 14 from client." );
				disconnected = true;
				return;
			}
			// this is part of the usename. Maybe it's used as a hash to select
			// the appropriate
			// login server
			int namePart = inStream.readUnsignedByte();
			for ( int i = 0; i < 8; i++ )
			{
				outputStream.Write( new Byte[] { 1 }, 0, 1 );
			} // is being ignored by the client

			// login response - 0 means exchange session key to establish
			// encryption
			// Note that we could use 2 right away to skip the cryption part,
			// but i think this
			// won't work in one case when the cryptor class is not set and will
			// throw a NullPointerException
			outputStream.Write( new Byte[] { 0 }, 0, 1 );
			//outputStream.Write( BitConverter.GetBytes( 0 ), 0, 4 );

			// send the server part of the session Id used (client+server part
			// together are used as cryption key)
			outStream.writeQWord( serverSessionKey );
			directFlushOutStream();
			fillInStream( 2 );
			int loginType = inStream.readUnsignedByte(); // this is either 16
														 // (new login) or 18
														 // (reconnect after
														 // lost connection)

			if ( ( loginType != 16 ) && ( loginType != 18 ) )
			{
				shutdownError( "Unexpected login type " + loginType );
				return;
			}
			int loginPacketSize = inStream.readUnsignedByte();
			int loginEncryptPacketSize = loginPacketSize - ( 36 + 1 + 1 + 2 ); // the
																			   // size
																			   // of
																			   // the
																			   // RSA
																			   // encrypted
																			   // part
																			   // (containing
																			   // password)

			// misc.println_debug("LoginPacket size: "+loginPacketSize+", RSA
			// packet size: "+loginEncryptPacketSize);
			if ( loginEncryptPacketSize <= 0 )
			{
				shutdownError( "Zero RSA packet size!" );
				return;
			}
			fillInStream( loginPacketSize );
			if ( ( inStream.readUnsignedByte() != 255 )
					|| ( inStream.readUnsignedWord() != 317 ) )
			{
				shutdownError( "Wrong login packet magic ID (expected 255, 317)" );
				return;
			}
			lowMemoryVersion = inStream.readUnsignedByte();
			// misc.println_debug("Client type: "+((lowMemoryVersion==1) ? "low"
			// : "high")+" memory version");
			for ( int i = 0; i < 9; i++ )
			{
				String j = inStream.readDWord().ToString( "X4" );
				// misc.println_debug("dataFileVersion["+i+"]:
				// 0x"+Int32.toHexString(inStream.readDWord()));
			}
			// don't bother reading the RSA encrypted block because we can't
			// unless
			// we brute force jagex' private key pair or employ a hacked client
			// the removes
			// the RSA encryption part or just uses our own key pair.
			// Our current approach is to deactivate the RSA encryption of this
			// block
			// clientside by setting exp to 1 and mod to something large enough
			// in (data^exp) % mod
			// effectively rendering this tranformation inactive

			loginEncryptPacketSize--; // don't count length byte
			int tmp = inStream.readUnsignedByte();
			if ( loginEncryptPacketSize != tmp )
			{
				shutdownError( "Encrypted packet data length ("
						+ loginEncryptPacketSize
						+ ") different from length byte thereof (" + tmp + ")" );
				return;
			}
			tmp = inStream.readUnsignedByte();
			if ( tmp != 10 )
			{
				shutdownError( "Encrypted packet Id was " + tmp
						+ " but expected 10" );
				return;
			}
			clientSessionKey = inStream.readQWord();
			serverSessionKey = inStream.readQWord();

			// misc.println("UserId: "+inStream.readDWord());
			int junk = inStream.readDWord();
			playerName = inStream.readString();
			int expectedUid = 1;
			if ( junk == expectedUid )
			{
				println( "Sharp317 client detected!" );
				officialClient = true;
			}
			uid = junk;
			if ( ( playerName == null ) || ( playerName.Length == 0 ) )
			{
				playerName = "player" + playerId;
			}
			playerPass = inStream.readString();

			// DAN REMOVED
			playerServer = "localhost";
			//try
			//{
			//	playerServer = inStream.readString();
			//}
			//catch ( Exception e )
			//{
			//	playerServer = "rs2.servegame.org";
			//}

			playerName = playerName.ToLower();
			playerPass = playerPass.ToLower();

			char[] validChars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
						'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
						'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
						'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
						'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5',
						'6', '7', '8', '9', '0', '_', ' ' };
			playerName = playerName.Trim();
			int[] sessionKey = new int[4];

			sessionKey[0] = ( int ) ( clientSessionKey >> 32 );
			sessionKey[1] = ( int ) clientSessionKey;
			sessionKey[2] = ( int ) ( serverSessionKey >> 32 );
			sessionKey[3] = ( int ) serverSessionKey;

			for ( int i = 0; i < 4; i++ )
			{
			}

			inStreamDecryption = new ISAACRandomGenerator( sessionKey.ToList().ToArray() );
			for ( int i = 0; i < 4; i++ )
			{
				sessionKey[i] += 50;
			}

			for ( int i = 0; i < 4; i++ )
			{
			}
			outStreamDecryption = new ISAACRandomGenerator( sessionKey );
			outStream.packetEncryption = outStreamDecryption;

			returnCode = 2;
			for ( int i = 0; i < playerName.Length; i++ )
			{
				Boolean valid = false;
				foreach ( char element in validChars )
				{
					if ( playerName[i] == element )
					{
						valid = true;
						// break;
					}
				}
				if ( !valid )
				{
					returnCode = 4;
					disconnected = true;
					savefile = false;
					return;
				}
			}
			char first = playerName[0];
			properName = Char.ToUpper( first )
					+ playerName.Substring( 1, playerName.Length - 1 );
			playerName = properName;
			if ( PlayerHandler.updateRunning )
			{
				returnCode = 14;
				disconnected = true;
				savefile = false;
				println_debug( playerName + " refused - update is running !" );
				return;
			}
			if ( !server.loginServerConnected )
			{
				returnCode = 8;
				disconnected = true;
				return;
			}

			Boolean found = false;
			int type = 5;

			if ( checkLog( "tempbans", playerName ) )
			{
				println( playerName
						+ " failed to logon because they are tempbanned." );
				returnCode = 4;
				disconnected = true;
				return;
			}

			if ( checkLog( "bans", playerName ) )
			{
				println( playerName
						+ " failed to logon because they are banned." );
				returnCode = 4;
				disconnected = true;
				return;
			}

			// uncomment this code below to stop multiple logins from 1
			// computer.

			/*
			 * for(int i = 0; i < PlayerHandler.players.Length; i++){
			 * Player p = PlayerHandler.players[i]; if(p != null &&
			 * !p.disconnected && p.connectedFrom.Equals(connectedFrom) &&
			 * playerId != p.playerId && !connectedFrom.Equals("localhost")){
			 * sendMessage("Address in use!"); returnCode = 9; disconnected =
			 * true; return; } }
			 */

			int loadgame = Loadgame( playerName, passHash( playerPass ) );

			if ( loadgame == 3 )
			{
				// wrong password.
				returnCode = 3;
				disconnected = true;
				return;
			}

			if ( PlayerHandler.isPlayerOn( playerName ) )
			{
				returnCode = 5;
				disconnected = true;
				return;
			}



			if ( playerName.Contains( "_" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "-" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "!" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "@" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "#" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "$" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "%" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "^" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "&" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "*" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "(" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( ")" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "~" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "{" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "}" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "[" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "]" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "<" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( ">" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "?" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "+" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( ":" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( ";" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "'" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "." ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "/" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "." ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "," ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}
			if ( playerName.Contains( "`" ) )
			{
				returnCode = 20;
				disconnected = true;
				return;
			}


			if ( server.enforceClient && !officialClient )
			{
				println( "Invalid client!" );
				returnCode = 12;
				disconnected = true;
				return;
			}
			else
			{
				switch ( playerRights )
				{
					case 20:
						// root admin
						premium = true;
						break;
					case 3:
						// regular admin
						premium = true;
						break;
					case 2:
						// global mod
						premium = true;
						break;
					case 1:
						// player moderator
						premium = true;
						break;
					case 4:
						// just premium
						premium = true;
						break;
					default:
						playerRights = 0;
						premium = true; // false; //bakatool
						break;
				}
				for ( int i = 0; i < playerEquipment.Length; i++ )
				{
					if ( playerEquipment[i] == 0 )
					{
						playerEquipment[i] = -1;
						playerEquipmentN[i] = 0;
					}
				}
				if ( loadgame == 0 )
				{
					validLogin = true;
					if ( ( absX > 0 ) && ( absY > 0 ) )
					{
						WalkTo( 0, 0 );
						WalkTo( 0, 0 );
						// heightLevel = 0;
					}
				}
				else
				{
					returnCode = loadgame;
					disconnected = true;
					return;
				}
				if ( returnCode == 5 )
				{
					returnCode = 21;
					loginDelay = 15;
				}
			}
		}
		catch ( Exception __ex )
		{
			server.logError( __ex.ToString() );
			//__ex.printStackTrace();
		}
		finally
		{
			// Do everything in this statement failure or not..(IDK WHY SERVERS
			// DIDN'T HAVE THIS!) -bakatool
			try
			{
				if ( playerId == -1 )
					outputStream.Write( new Byte[] { 7 }, 0, 1 );
				// "This world is full."
				else if ( playerServer.Equals( "INVALID" ) )
					outputStream.Write( new Byte[] { 10 }, 0, 1 );
				else
					outputStream.Write( new Byte[] { ( Byte ) returnCode }, 0, 1 );
				// login response(1: wait 2seconds, 2=login successfull, 4=ban
				// :-)

				if ( returnCode == 21 )
					outputStream.Write( new Byte[] { ( Byte ) loginDelay }, 0, 1 );

				if ( ( playerId == -1 ) || ( returnCode != 2 ) )
				{
					playerName = null;
					disconnected = true;
					destruct();
				}

				if ( !disconnected && outputStream?.CanWrite == true )
				{
					// mod/admin level crown fix -bakatool
					if ( playerRights == 3 )
						outputStream.Write( new Byte[] { 2 }, 0, 1 );
					else
						outputStream.Write( new Byte[] { ( Byte ) playerRights }, 0, 1 );

					outputStream.Write( new Byte[] { 0 }, 0, 1 ); // no log
				}

				updateRequired = true;
				appearanceUpdateRequired = true;
			}
			catch ( Exception __ex )
			{
				// error at finalizer means auto destruct no exceptions
				// -bakatool
				disconnected = true;
				destruct();
			}
		}
		isActive = true;
		// End of login procedure
		packetSize = 0;
		packetType = -1;

		readPtr = 0;
		writePtr = 0;

		int numBytesInBuffer, offset;

		while ( !disconnected )
		{
			lock( this ) 
			{
				if ( writePtr == readPtr )
				{
					try
					{
						Thread.Sleep( 10 );
						//wait();
					}
					catch ( Exception _ex )
					{
					}
				}

				if ( disconnected )
				{
					return;
				}

				offset = readPtr;
				if ( writePtr >= readPtr )
				{
					numBytesInBuffer = writePtr - readPtr;
				}
				else
				{
					numBytesInBuffer = bufferSize - readPtr;
				}
			}
			if ( numBytesInBuffer > 0 )
			{
				try
				{
					outputStream.Write( buffer, offset, numBytesInBuffer );
					readPtr = ( readPtr + numBytesInBuffer ) % bufferSize;
					if ( writePtr == readPtr )
					{
						outputStream.Flush();
					}
				}
				catch ( SocketException e )
				{
					disconnected = true;
					if ( saveNeeded )
						savegame( true );
				}
				catch ( Exception __ex )
				{
					server.logError( __ex.ToString() );
					disconnected = true;
					if ( saveNeeded )
						savegame( true );
				}
			}
		}
	}

	public Boolean runeCheck( int spell )
	{
		int[] runeId = { 6430, 6432, 565, 6428, 6422, 566, 6434, 6424 };
		for ( int i = 0; i < server.runesRequired[spell].Length; i++ )
		{
			if ( server.runesRequired[spell][i] > 0 )
			{
				if ( !playerHasItem( runeId[i], server.runesRequired[spell][i] ) )
				{
					return false;
				}
			}
		}
		return true;
	}

	public void savegame( Boolean logout )
	{

		if ( ( playerName == null ) || !validClient )
		{
			saveNeeded = false;
			return;
		}

		if ( logout )
		{
			if ( fightId > 0 )
			{
				client f = ( client ) PlayerHandler.players[fightId];
				if ( f != null )
				{
					f.fighting = false;
					f.hits = 0;
				}
			}

		}
		if ( logout && inTrade )
		{
			declineTrade();
		}

		try
		{
			var account = new Account
			{
				Username = playerName,
				Password = passHash( playerPass ),
				Character = new Character
				{
					Height = heightLevel,
					PositionX = absX == -1 ? 2611 : absX,
					PositionY = absY == -1 ? 3093 : absY,
					Rights = playerRights,
					StarterItems = starterItems,
					IsMember = playerIsMember == 1,
					HasLearnedCombat = HasLearnedCombat == 1,
					Messages = playerMessages,
					LastConnection = playerLastConnect,
					LastLogin = playerLastLogin,
					Energy = playerEnergy,
					GameTime = playerGameTime,
					GameCount = playerGameCount,
					Ancients = ancients,
					Rating = rating,
					IsInJail = isinjaillolz == 1,
					Equipment = new List<CharacterEquipment>(),
					Appearance = new List<CharacterAppearance>(),
					Skills = new List<CharacterSkill>(),
					Items = new List<CharacterItem>(),
					Bank = new List<CharacterBankItem>(),
					Friends = new List<CharacterFriend>(),
					Ignores = new List<CharacterIgnore>()
				}
			};

			for ( int i = 0; i < playerEquipment.Length; i++ )
			{
				account.Character.Equipment.Add( new CharacterEquipment 
				{ 
					Index = i,
					Item = playerEquipment[i],
					Count = playerEquipmentN[i]
				} );
			}

			int[] Looks = getLook();

			for ( int i = 0; i < Looks.Length; i++ )
			{
				account.Character.Appearance.Add( new CharacterAppearance 
				{ 
					Index = i,
					Look = Looks[i]
				} );
			}

			for ( int i = 0; i < playerLevel.Length; i++ )
			{
				account.Character.Skills.Add( new CharacterSkill 
				{ 
					Index = i,
					Level = playerLevel[i],
					Experience = playerXP[i]
				} );
			}

			for ( int i = 0; i < playerItems.Length; i++ )
			{
				if ( playerItems[i] > 0 )
				{						
					account.Character.Items.Add( new CharacterItem 
					{ 
						Index = i,
						Item = playerItems[i],
						Count = playerItemsN[i]
					} );
				}
			}

			for ( int i = 0; i < bankItems.Length; i++ )
			{
				if ( bankItems[i] > 0 )
				{			
					account.Character.Bank.Add( new CharacterBankItem 
					{ 
						Index = i,
						Item = bankItems[i],
						Count = bankItemsN[i]
					} );
				}
			}

			for ( int i = 0; i < friends.Length; i++ )
			{
				if ( friends[i] > 0 )
				{
					account.Character.Friends.Add( new CharacterFriend
					{ 
						Index = i,
						ID = friends[i]
					} );
				}
			}

			for ( int i = 0; i < ignores.Length; i++ )
			{
				if ( ignores[i] > 0 )
				{
					account.Character.Ignores.Add( new CharacterIgnore
					{ 
						Index = i,
						ID = ignores[i]
					} );
				}
			}

			var json = JsonSerializer.Serialize( account );
				
			File.WriteAllText( "./characters/" + playerName + ".json", json );
		}
		catch ( IOException ioexception )
		{
			misc.println( playerName + ": error writing file." );
		}
		saveNeeded = false;
	}

	public void saveStats( Boolean logout )
	{

		String[] parts = new String[] { "pGender", "pHead", "pTorso", "pArms",
					"pHands", "pLegs", "pFeet", "pBeard", "pHairC", "pTorsoC",
					"pLegsC", "pFeetC", "pSkinC" };

		try
		{
			int[] look = { pGender, pHead, pBeard, pTorso, pArms, pHands,
						pLegs, pFeet, pHairC, pTorsoC, pLegsC, pFeetC, pSkinC,
						playerLook[0], playerLook[1], playerLook[2], playerLook[3],
						playerLook[4], playerLook[5] };
			Boolean DoInsert = true;
			int ID = -1;
			int ID2 = -1;
			int test1 = -1;
			int test2 = -1;
			// mysql_connect();
			long allxp = 0;
			for ( int i = 0; i < 21; i++ )
			{
				if ( i != 18 )
				{
					allxp += playerXP[i];
				}
			}
			int totallvl = playerLevel[0] + playerLevel[1] + playerLevel[2]
					+ playerLevel[3] + playerLevel[4] + playerLevel[5]
					+ playerLevel[6] + playerLevel[7] + playerLevel[8]
					+ playerLevel[9] + playerLevel[10] + playerLevel[11]
					+ playerLevel[12] + playerLevel[13] + playerLevel[14]
					+ playerLevel[15] + playerLevel[16] + playerLevel[17]
					+ playerLevel[18] + playerLevel[19] + playerLevel[20];
			int combatLevel = ( int ) ( ( double ) playerLevel[0] * 0.32707
					+ ( double ) playerLevel[1] * 0.249 + ( double ) playerLevel[2]
					* 0.324 + ( double ) playerLevel[3] * 0.25 + ( double ) playerLevel[5] * 0.124 );

			String online = "online = 0";
			if ( !logout )
				online = "online = " + server.world;
			else
				saveNeeded = false;

		}
		catch ( Exception e )
		{
		}
	}

	/* Shops */
	public Boolean sellItem( int itemID, int fromSlot, int amount )
	{
		if ( ( amount > 0 ) && ( itemID == ( playerItems[fromSlot] - 1 ) ) )
		{
			if ( ShopHandler.ShopSModifier[MyShopID] > 1 )
			{
				Boolean IsIn = false;

				for ( int i = 0; i <= ShopHandler.ShopItemsStandard[MyShopID]; i++ )
				{
					if ( itemID == ( ShopHandler.ShopItems[MyShopID][i] - 1 ) )
					{
						IsIn = true;
						break;
					}
				}
				if ( IsIn == false )
				{
					sendMessage( "You cannot sell " + getItemName( itemID )
							+ " in this store." );
					return false;
				}
			}
			if ( Item.itemSellable[( playerItems[fromSlot] - 1 )] == false )
			{
				sendMessage( "I cannot sell " + getItemName( itemID ) + "." );
				return false;
			}
			if ( ( amount > playerItemsN[fromSlot] )
					&& ( ( Item.itemIsNote[( playerItems[fromSlot] - 1 )] == true ) || ( Item.itemStackable[( playerItems[fromSlot] - 1 )] == true ) ) )
			{
				amount = playerItemsN[fromSlot];
			}
			else if ( ( amount > GetXItemsInBag( itemID ) )
				  && ( Item.itemIsNote[( playerItems[fromSlot] - 1 )] == false )
				  && ( Item.itemStackable[( playerItems[fromSlot] - 1 )] == false ) )
			{
				amount = GetXItemsInBag( itemID );
			}
			double ShopValue;
			double TotPrice;
			int TotPrice2;
			int Overstock;

			for ( int i = amount; i > 0; i-- )
			{
				TotPrice2 = ( int ) Math.Floor( GetItemShopValue( itemID, 1,
						fromSlot ) );
				if ( freeSlots() > 0 )
				{
					if ( Item.itemIsNote[itemID] == false )
					{
						deleteItem( itemID, GetItemSlot( itemID ), 1 );
					}
					else
					{
						deleteItem( itemID, fromSlot, 1 );
					}
					addItem( 995, TotPrice2 );
					addShopItem( itemID, 1 );
				}
				else
				{
					sendMessage( "Not enough space in your inventory." );
					break;
				}
			}
			resetItems( 3823 );
			resetShop( MyShopID );
			UpdatePlayerShop();
			return true;
		}
		return true;
	}

	public void sendFrame126( String s, int id )
	{
		outStream.createFrameVarSizeWord( 126 );
		outStream.writeString( s );
		outStream.writeWordA( id );
		outStream.endFrameVarSizeWord();
		flushOutStream();
	}

	/*
	 * public Boolean stakeItem2(int itemID, int fromSlot, int amount) {
	 * if(!Item.itemStackable[itemID] && amount > 1){ for(int a = 1; a <=
	 * amount; a++){ int slot = findItem(itemID, playerItems, playerItemsN);
	 * if(slot >= 0){ tradeItem(itemID, slot, 1); } } } for(int i = 0; i <
	 * noTrade.Length; i++){ if(itemID == noTrade[i] || itemID == noTrade[i] + 1 ||
	 * premiumItem(itemID)){ sendMessage("You can't trade that item");
	 * declineTrade(); return false; } } client other = getClient(duel_with); if
	 * (!inDuel || !ValidClient(duel_with) || !canOffer){ declineDuel(); return
	 * false; } if(!playerHasItem(itemID, amount)){ return false; }
	 * if(Item.itemStackable[itemID]) offeredItems.add(new GameItem(itemID,
	 * amount)); else offeredItems.add(new GameItem(itemID, 1));
	 * deleteItem(itemID, fromSlot, amount); resetItems(3214);
	 * duelz.resetItems(3214); resetItems(3322); refreshDuelScreen();
	 * duelz.resetItems(3322); duelz.refreshDuelScreen(); duelz.sendFrame126("",
	 * 6684); return false; }
	 */

	public void sendFrame164( int Frame )
	{
		outStream.createFrame( 164 );
		outStream.writeWordBigEndian_dup( Frame );
		flushOutStream();
	}

	public void sendFrame171( int MainFrame, int SubFrame )
	{
		outStream.createFrame( 171 );
		outStream.writeByte( MainFrame );
		outStream.writeWord( SubFrame );
		flushOutStream();
	}

	public void sendFrame185( int Frame )
	{
		outStream.createFrame( 185 );
		outStream.writeWordBigEndianA( Frame );
		flushOutStream();
	}

	public void sendFrame200( int MainFrame, int SubFrame )
	{
		outStream.createFrame( 200 );
		outStream.writeWord( MainFrame );
		outStream.writeWord( SubFrame );
		flushOutStream();
	}

	public void sendFrame246( int MainFrame, int SubFrame, int SubFrame2 )
	{
		outStream.createFrame( 246 );
		outStream.writeWordBigEndian( MainFrame );
		outStream.writeWord( SubFrame );
		outStream.writeWord( SubFrame2 );
		flushOutStream();
	}

	public void sendFrame248( int MainFrame, int SubFrame )
	{
		outStream.createFrame( 248 );
		outStream.writeWordA( MainFrame );
		outStream.writeWord( SubFrame );
		flushOutStream();
	}

	public void sendFrame75( int MainFrame, int SubFrame )
	{
		outStream.createFrame( 75 );
		outStream.writeWordBigEndianA( MainFrame );
		outStream.writeWordBigEndianA( SubFrame );
		flushOutStream();
	}

	// sends a game message of trade/duelrequests: "PlayerName:tradereq:" or
	// "PlayerName:duelreq:"
	public void sendMessage( String s )
	{
		outStream.createFrameVarSize( 253 );
		outStream.writeString( s );
		outStream.endFrameVarSize();
	}

	public override void sendpm( long name, int rights, byte[] chatmessage,
			int messagesize )
	{
		outStream.createFrameVarSize( 196 );
		outStream.writeQWord( name );
		outStream.writeDWord( handler.lastchatid++ ); // must be different for
													  // each message
		outStream.writeByte( rights );
		outStream.writeBytes( chatmessage, messagesize, 0 );
		outStream.endFrameVarSize();
	}

	public void sendQuest( String s, int id )
	{
		try
		{
			outStream.createFrameVarSizeWord( 126 );
			outStream.writeString( s );
			outStream.writeWordA( id );
			outStream.endFrameVarSizeWord();
		}
		catch ( Exception e )
		{
			server.logError( e.ToString() );
		}
	}

	public void sendQuestSomething( int id )
	{
		outStream.createFrame( 79 );
		outStream.writeWordBigEndian( id );
		outStream.writeWordA( 0 );
		flushOutStream();
	}

	public void SendWeapon( int Weapon, String WeaponName )
	{
		String WeaponName2 = WeaponName.Replace( "Bronze", "" );

		WeaponName2 = WeaponName2.Replace( "Iron", "" );
		WeaponName2 = WeaponName2.Replace( "Steel", "" );
		WeaponName2 = WeaponName2.Replace( "Scythe", "" );
		WeaponName2 = WeaponName2.Replace( "Black", "" );
		WeaponName2 = WeaponName2.Replace( "Mithril", "" );
		WeaponName2 = WeaponName2.Replace( "Adamant", "" );
		WeaponName2 = WeaponName2.Replace( "Rune", "" );
		WeaponName2 = WeaponName2.Replace( "Granite", "" );
		WeaponName2 = WeaponName2.Replace( "Dragon", "" );
		WeaponName2 = WeaponName2.Replace( "Crystal", "" );
		WeaponName2 = WeaponName2.Trim();
		if ( WeaponName.Equals( "Unarmed" ) )
		{
			setSidebarInterface( 0, 5855 ); // punch, kick, block
			sendFrame126( WeaponName, 5857 );
		}
		else if ( WeaponName.EndsWith( "whip" ) )
		{
			setSidebarInterface( 0, 12290 ); // flick, lash, deflect
			sendFrame246( 12291, 200, Weapon );
			sendFrame126( WeaponName, 12293 );
		}
		else if ( WeaponName.EndsWith( "Scythe" ) )
		{
			setSidebarInterface( 0, 776 ); // flick, lash, deflect
			sendFrame246( 12291, 200, Weapon );
			sendFrame126( WeaponName, 778 );
		}
		else if ( WeaponName.StartsWith( "Dark" ) )
		{
			setSidebarInterface( 0, 1764 ); // accurate, rapid, longrange
			sendFrame246( 1765, 200, Weapon );
			sendFrame126( WeaponName, 1767 );
		}
		else if ( WeaponName.EndsWith( "bow" ) )
		{
			setSidebarInterface( 0, 1764 ); // accurate, rapid, longrange
			sendFrame246( 1765, 200, Weapon );
			sendFrame126( WeaponName, 1767 );
		}
		else if ( WeaponName.StartsWith( "Staff" )
			  || WeaponName.EndsWith( "staff" ) )
		{
			setSidebarInterface( 0, 328 ); // spike, impale, smash, block
			sendFrame246( 329, 200, Weapon );
			sendFrame126( WeaponName, 331 );
		}
		else if ( WeaponName2.StartsWith( "dart" ) )
		{
			setSidebarInterface( 0, 4446 ); // accurate, rapid, longrange
			sendFrame246( 4447, 200, Weapon );
			sendFrame126( WeaponName, 4449 );
		}
		else if ( WeaponName2.StartsWith( "dagger" ) )
		{
			setSidebarInterface( 0, 2276 ); // stab, lunge, slash, block
			sendFrame246( 2277, 200, Weapon );
			sendFrame126( WeaponName, 2279 );
		}
		else if ( WeaponName2.StartsWith( "pickaxe" ) )
		{
			setSidebarInterface( 0, 5570 ); // spike, impale, smash, block
			sendFrame246( 5571, 200, Weapon );
			sendFrame126( WeaponName, 5573 );
		}
		else if ( WeaponName2.StartsWith( "axe" )
			  || WeaponName2.StartsWith( "battleaxe" ) )
		{
			setSidebarInterface( 0, 1698 ); // chop, hack, smash, block
			sendFrame246( 1699, 200, Weapon );
			sendFrame126( WeaponName, 1701 );
		}
		else if ( WeaponName2.StartsWith( "Axe" )
			  || WeaponName2.StartsWith( "Battleaxe" ) )
		{
			setSidebarInterface( 0, 1698 ); // chop, hack, smash, block
			sendFrame246( 1699, 200, Weapon );
			sendFrame126( WeaponName, 1701 );
		}
		else if ( WeaponName2.StartsWith( "halberd" ) )
		{
			setSidebarInterface( 0, 8460 ); // jab, swipe, fend
			sendFrame246( 8461, 200, Weapon );
			sendFrame126( WeaponName, 8463 );
		}
		else if ( WeaponName2.StartsWith( "spear" ) )
		{
			setSidebarInterface( 0, 4679 ); // lunge, swipe, pound, block
			sendFrame246( 4680, 200, Weapon );
			sendFrame126( WeaponName, 4682 );
		}
		else
		{
			setSidebarInterface( 0, 2423 ); // chop, slash, lunge, block
			sendFrame246( 2424, 200, Weapon );
			sendFrame126( WeaponName, 2426 );
		}
	}

	public void ServerHelp( )
	{
		sendFrame126( "@dre@Rune Aura - Help Menu", 8144 );
		clearQuestInterface();
		sendFrame126( "", 8145 );
		sendFrame126( "@dbl@Welcome to @cya@Rune Aura", 8146 );
		sendFrame126( "@dbl@First note: this is not deltascape, as it smells.", 8147 );
		sendFrame126( "@dbl@Obtaining items for now can be done by killing monsters.", 8148 );
		sendFrame126( "@dbl@You will see monsters as soon as you start playing!", 8149 );
		sendFrame126( "@dbl@If you need help ask any staff member for help.", 8150 );
		sendFrame126( "", 8161 );
		sendFrame126( "@dbl@If you find a bug, a dupe or a glitch..", 8152 );
		sendFrame126( "@dbl@Report it to 1xiix1, nobody else. you will be rewarded!", 8153 );
		sendFrame126( "", 8154 );
		sendFrame126( "@dbl@By choosing your class you get some xp in the class,", 8155 );
		sendFrame126( "@dbl@Choose your class by equiping the class's weapon.", 8156 );
		sendFrame126( "", 8157 );
		sendFrame126( "", 8158 );
		sendQuestSomething( 8143 );
		showInterface( 8134 );
	}

	public void setAnimation( int i )
	{
		startAnimation( i );
	}

	public void setChatOptions( int publicChat, int privateChat, int tradeBlock )
	{
		outStream.createFrame( 206 );
		outStream.writeByte( publicChat ); // On = 0, Friends = 1, Off = 2,
										   // Hide = 3
		outStream.writeByte( privateChat ); // On = 0, Friends = 1, Off = 2
		outStream.writeByte( tradeBlock ); // On = 0, Friends = 1, Off = 2
	}

	public void setClientConfig( int id, int state )
	{
		outStream.createFrame( 36 );
		outStream.writeWordBigEndian( id );
		outStream.writeByte( state );
	}

	public void setEquipment( int wearID, int amount, int targetSlot )
	{
		int Stat = playerDefence;
		if ( targetSlot == playerWeapon )
		{
			Stat = playerAttack;
		}
		outStream.createFrameVarSizeWord( 34 );
		outStream.writeWord( 1688 );
		outStream.writeByte( targetSlot );
		outStream.writeWord( ( wearID + 1 ) );
		if ( amount > 254 )
		{
			outStream.writeByte( 255 );
			outStream.writeDWord( amount );
		}
		else
		{
			outStream.writeByte( amount ); //amount	
		}
		outStream.endFrameVarSizeWord();


		if ( targetSlot == playerWeapon && wearID >= 0 )
		{
			SendWeapon( wearID, GetItemName( wearID ) );
			playerSE = GetStandAnim( wearID );
			playerSEW = GetWalkAnim( wearID );
			playerSER = GetRunAnim( wearID );
			playerSEA = 0x326;

			if ( wearID == 4747 )
			{ //Torag Hammers
				playerSEA = 0x814;
			}
			if ( wearID == 4151 )
			{ //Whip
				playerSER = 1661;
				playerSEW = 1660;
			}
			if ( wearID == 8447 )
			{ //cat toy
				playerSER = 1661;
				playerSEW = 1660;
			}
			if ( wearID == 4153 || wearID == 6528 )
			{ //maul
				playerSER = 2064;
				playerSEW = 2064;
				playerSE = 2065;
			}
			if ( wearID == 1215 )
			{ // d dagger
				playerSER = 1661;
				playerSEW = 1660;
				stillgfx( 306, absY, absX );
			}
			pEmote = playerSE;
		}
		SendWeapon( ( playerEquipment[playerWeapon] ), GetItemName( playerEquipment[playerWeapon] ) );
		updateRequired = true;
		appearanceUpdateRequired = true;
	}
	public void setInterfaceWalkable( int ID )
	{
		outStream.createFrame( 208 );
		outStream.writeWordBigEndian_dup( ID );
		flushOutStream();
	}

	public void setLook( int[] parts )
	{
		if ( parts.Length != 19 )
		{
			println( "setLook:  Invalid array length!" );
			return;
		}
		pGender = parts[0];
		pHead = parts[1];
		pBeard = parts[2];
		pTorso = parts[3];
		pArms = parts[4];
		pHands = parts[5];
		pLegs = parts[6];
		pFeet = parts[7];
		pHairC = parts[8];
		pTorsoC = parts[9];
		pLegsC = parts[10];
		pFeetC = parts[11];
		pSkinC = parts[12];
		playerLook[0] = parts[13];
		playerLook[1] = parts[14];
		playerLook[2] = parts[15];
		playerLook[3] = parts[16];
		playerLook[4] = parts[17];
		playerLook[5] = parts[18];
		apset = true;
		appearanceUpdateRequired = true;
		lookUpdate = true;
		updateRequired = true;
	}

	public void setSidebarInterface( int menuId, int form )
	{
		outStream.createFrame( 71 );
		outStream.writeWord( form );
		outStream.writeByteA( menuId );
	}

	public void setSkillLevel( int skillNum, int currentLevel, int XP )
	{
		if ( skillNum == 0 )
		{
			sendQuest( "" + playerLevel[0] + "", 4004 );
			sendQuest( "" + getLevelForXP( playerXP[0] ) + "", 4005 );
		}
		if ( skillNum == 2 )
		{
			sendQuest( "" + playerLevel[2] + "", 4006 );
			sendQuest( "" + getLevelForXP( playerXP[2] ) + "", 4007 );
		}
		if ( skillNum == 1 )
		{
			sendQuest( "" + playerLevel[1] + "", 4008 );
			sendQuest( "" + getLevelForXP( playerXP[1] ) + "", 4009 );
		}
		if ( skillNum == 4 )
		{
			sendQuest( "" + playerLevel[4] + "", 4010 );
			sendQuest( "" + getLevelForXP( playerXP[4] ) + "", 4011 );
		}
		if ( skillNum == 5 )
		{
			sendQuest( "" + playerLevel[5] + "", 4012 );
			sendQuest( "" + getLevelForXP( playerXP[5] ) + "", 4013 );
		}
		if ( skillNum == 6 )
		{
			sendQuest( "" + playerLevel[6] + "", 4014 );
			sendQuest( "" + getLevelForXP( playerXP[6] ) + "", 4015 );
		}
		if ( skillNum == 3 )
		{
			sendQuest( "" + currentHealth + "", 4016 );
			sendQuest( "" + getLevelForXP( playerXP[3] ) + "", 4017 );
		}
		if ( skillNum == 16 )
		{
			sendQuest( "" + playerLevel[16] + "", 4018 );
			sendQuest( "" + getLevelForXP( playerXP[16] ) + "", 4019 );
		}
		if ( skillNum == 15 )
		{
			sendQuest( "" + playerLevel[15] + "", 4020 );
			sendQuest( "" + getLevelForXP( playerXP[15] ) + "", 4021 );
		}
		if ( skillNum == 17 )
		{
			sendQuest( "" + playerLevel[17] + "", 4022 );
			sendQuest( "" + getLevelForXP( playerXP[17] ) + "", 4023 );
		}
		if ( skillNum == 12 )
		{
			sendQuest( "" + playerLevel[12] + "", 4024 );
			sendQuest( "" + getLevelForXP( playerXP[12] ) + "", 4025 );
		}
		if ( skillNum == 9 )
		{
			sendQuest( "" + playerLevel[9] + "", 4026 );
			sendQuest( "" + getLevelForXP( playerXP[9] ) + "", 4027 );
		}
		if ( skillNum == 14 )
		{
			sendQuest( "" + playerLevel[14] + "", 4028 );
			sendQuest( "" + getLevelForXP( playerXP[14] ) + "", 4029 );
		}
		if ( skillNum == 13 )
		{
			sendQuest( "" + playerLevel[13] + "", 4030 );
			sendQuest( "" + getLevelForXP( playerXP[13] ) + "", 4031 );
		}
		if ( skillNum == 10 )
		{
			sendQuest( "" + playerLevel[10] + "", 4032 );
			sendQuest( "" + getLevelForXP( playerXP[10] ) + "", 4033 );
		}
		if ( skillNum == 7 )
		{
			sendQuest( "" + playerLevel[7] + "", 4034 );
			sendQuest( "" + getLevelForXP( playerXP[7] ) + "", 4035 );
		}
		if ( skillNum == 11 )
		{
			sendQuest( "" + playerLevel[11] + "", 4036 );
			sendQuest( "" + getLevelForXP( playerXP[11] ) + "", 4037 );
		}
		if ( skillNum == 8 )
		{
			sendQuest( "" + playerLevel[8] + "", 4038 );
			sendQuest( "" + getLevelForXP( playerXP[8] ) + "", 4039 );
		}
		if ( skillNum == 20 )
		{
			sendQuest( "" + playerLevel[20] + "", 4152 );
			sendQuest( "" + getLevelForXP( playerXP[20] ) + "", 4153 );
		}
		if ( skillNum == 18 )
		{
			sendQuest( "" + playerLevel[18] + "", 12166 );
			sendQuest( "" + getLevelForXP( playerXP[18] ) + "", 12167 );
		}
		if ( skillNum == 19 )
		{
			sendQuest( "" + playerLevel[19] + "", 13926 );
			sendQuest( "" + getLevelForXP( playerXP[19] ) + "", 13927 );
		}
		else
		{
			outStream.createFrame( 134 );
			outStream.writeByte( skillNum );
			outStream.writeDWord_v1( XP );
			outStream.writeByte( currentLevel );
		}
	}

	public void SetSmithing( int WriteFrame )
	{
		outStream.createFrameVarSizeWord( 53 );
		outStream.writeWord( WriteFrame );
		outStream.writeWord( Item.SmithingItems.Length );
		for ( int i = 0; i < Item.SmithingItems.Length; i++ )
		{
			Item.SmithingItems[i][0] += 1;
			if ( Item.SmithingItems[i][1] > 254 )
			{
				outStream.writeByte( 255 ); // item's stack count. if over 254,
											// write byte 255
				outStream.writeDWord_v2( Item.SmithingItems[i][1] ); // and then
																	 // the real
																	 // value
																	 // with
																	 // writeDWord_v2
			}
			else
			{
				outStream.writeByte( Item.SmithingItems[i][1] );
			}
			if ( ( Item.SmithingItems[i][0] > 20000 )
					|| ( Item.SmithingItems[i][0] < 0 ) )
			{
				playerItems[i] = 20000;
			}
			outStream.writeWordBigEndianA( Item.SmithingItems[i][0] ); // item
																	   // id
		}
		outStream.endFrameVarSizeWord();
	}

	public void shaft( )
	{
		closeInterface();
		if ( playerHasItem( 1511 ) )
		{
			if ( playerHasItem( -1 ) )
			{
				deleteItem( 1511, 1 );
				addItem( 52, 15 );
				addSkillXP( 150, playerFletching );
			}
			else
			{
				sendMessage( "Your inventory is full!" );
				resetAction();
			}
		}
		else
		{
			resetAction();
		}
	}

	public void showInterface( int interfaceid )
	{
		resetAction();
		outStream.createFrame( 97 );
		outStream.writeWord( interfaceid );
		flushOutStream();
	}

	public void shutdownError( String errorMessage )
	{
		// misc.println(": " + errorMessage);
		destruct();
	}

	public void smelt( int id )
	{
		setAnimation( 0x383 );
		smelt_id = id;
		smelting = true;
		int smelt_barId = -1;
		List<Int32> removed = new List<Int32>();
		if ( smeltCount < 1 )
		{
			resetAction( true );
			return;
		}
		smeltCount--;
		switch ( id )
		{
			case 2349:
				// bronze
				if ( playerHasItem( 436 ) && playerHasItem( 438 ) )
				{
					smelt_barId = 2349;
					removed.Add( 436 );
					removed.Add( 438 );
				}
				break;
			case 2351:
				// iron ore
				if ( playerHasItem( 440 ) )
				{
					int ran = misc.random( 3 );
					if ( ( ran == 1 ) || ( ran == 2 ) )
					{
						smelt_barId = 2351;
						removed.Add( 440 );
					}
					else
					{
						smelt_barId = 0;
						removed.Add( 440 );
						sendMessage( "You fail to refine the iron" );
					}
				}
				break;
			case 2353:
				if ( playerHasItem( 440 ) && playerHasItem( 453, 2 ) )
				{
					smelt_barId = 2353;
					removed.Add( 440 );
					removed.Add( 453 );
					removed.Add( 453 );
				}
				break;
			case 2359:
				if ( playerHasItem( 447 ) && playerHasItem( 453, 3 ) )
				{
					smelt_barId = 2359;
					removed.Add( 447 );
					removed.Add( 453 );
					removed.Add( 453 );
					removed.Add( 453 );
				}
				break;
			case 2361:
				if ( playerHasItem( 449 ) && playerHasItem( 453, 4 ) )
				{
					smelt_barId = 2361;
					removed.Add( 449 );
					removed.Add( 453 );
					removed.Add( 453 );
					removed.Add( 453 );
					removed.Add( 453 );
				}
				break;
		}
		if ( smelt_barId == -1 )
		{
			resetAction();
			return;
		}
		if ( true )
		{
			foreach ( Int32 intId in removed )
			{
				int removeId = intId;
				deleteItem( removeId, 1 );
			}
			if ( smelt_barId > 0 )
				addItem( smelt_barId, 1 );
		}
		else
		{
			sendMessage( "Your inventory is full!" );
			resetAction();
		}
	}

	public Boolean Smith( )
	{
		if ( IsItemInBag( 2347 ) == true )
		{
			int bars = 0;
			int Length = 22;
			int barid = 0;
			int Level = 0;
			int ItemN = 1;

			if ( smithing[2] >= 4 )
			{
				barid = ( 2349 + ( ( smithing[2] + 1 ) * 2 ) );
			}
			else
			{
				barid = ( 2349 + ( ( smithing[2] - 1 ) * 2 ) );
			}
			if ( ( smithing[2] == 1 ) || ( smithing[2] == 2 ) )
			{
				Length += 1;
			}
			else if ( smithing[2] == 3 )
			{
				Length += 2;
			}
			for ( int i = 0; i < Length; i++ )
			{
				if ( Item.smithing_frame[( smithing[2] - 1 )][i][0] == smithing[4] )
				{
					bars = Item.smithing_frame[( smithing[2] - 1 )][i][3];
					if ( smithing[1] == 0 )
					{
						smithing[1] = Item.smithing_frame[( smithing[2] - 1 )][i][2];
					}
					ItemN = Item.smithing_frame[( smithing[2] - 1 )][i][1];
				}
			}
			if ( playerLevel[playerSmithing] >= smithing[1] )
			{
				if ( AreXItemsInBag( barid, bars ) == true )
				{
					if ( freeSlots() > 0 )
					{
						if ( ( actionTimer == 0 ) && ( smithing[0] == 1 ) )
						{
							actionAmount++;
							/*
							 * OriginalWeapon = playerEquipment[playerWeapon];
							 * playerEquipment[playerWeapon] = 2347; // Hammer
							 * OriginalShield = playerEquipment[playerShield];
							 * playerEquipment[playerShield] = -1;
							 */
							sendMessage( "You start hammering the bar..." );
							actionTimer = 7;
							setAnimation( 0x382 );
							smithing[0] = 2;
						}
						if ( ( actionTimer == 0 ) && ( smithing[0] == 2 ) )
						{
							for ( int i = 0; i < bars; i++ )
							{
								deleteItem( barid, GetItemSlot( barid ),
										playerItemsN[GetItemSlot( barid )] );
							}
							addSkillXP(
									( ( int ) ( 150.5 * bars * smithing[2] * smithing[3] ) ),
									playerSmithing );
							addItem( smithing[4], ItemN );
							sendMessage( "You smith a "
									+ getItemName( smithing[4] ) + "." );
							resetAnimation();
							if ( smithing[5] <= 1 )
							{
								resetSM();
							}
							else
							{
								actionTimer = 5;
								smithing[5] -= 1;
								smithing[0] = 1;
							}
						}
					}
					else
					{
						sendMessage( "Not enough space in your inventory." );
						resetSM();
						return false;
					}
				}
				else
				{
					sendMessage( "You need " + bars + " " + getItemName( barid )
							+ " to smith a " + getItemName( smithing[4] ) );
					resetAnimation();
					resetSM();
				}
			}
			else
			{
				sendMessage( "You need " + smithing[1] + " "
						+ statName[playerSmithing] + " to smith a "
						+ getItemName( smithing[4] ) );
				resetSM();
				return false;
			}
		}
		else
		{
			sendMessage( "You need a " + getItemName( 2347 ) + " to hammer bars." );
			resetSM();
			return false;
		}
		return true;
	}

	public void spin( )
	{
		if ( playerHasItem( 1779 ) )
		{
			deleteItem( 1779, 1 );
			addItem( 1777, 1 );
			lastAction = TimeHelper.CurrentTimeMillis();
			addSkillXP( 30, playerCrafting );
		}
		else
		{
			resetAction( true );
		}
	}

	/*
	 * [0] Varrock [1] Wizard Tower [2] Ardougne [3] Magic Guild
	 */
	public Boolean UseStairs( int stairs, int teleX, int teleY )
	{
		if ( IsStair == false )
		{
			IsStair = true;
			if ( stairs == 1 )
			{
				heightLevel += 1;
			}
			else if ( stairs == 2 )
			{
				heightLevel -= 1;
			}
			else if ( stairs == 21 )
			{
				heightLevel += 1;
			}
			else if ( stairs == 22 )
			{
				heightLevel -= 1;
			}
			teleportToX = teleX;
			teleportToY = teleY;
			if ( ( stairs == 3 ) || ( stairs == 5 ) || ( stairs == 9 ) )
			{
				teleportToY += 6400;
			}
			else if ( ( stairs == 4 ) || ( stairs == 6 ) || ( stairs == 10 ) )
			{
				teleportToY -= 6400;
			}
			else if ( stairs == 7 )
			{
				teleportToX = 3104;
				teleportToY = 9576;
			}
			else if ( stairs == 8 )
			{
				teleportToX = 3105;
				teleportToY = 3162;
			}
			else if ( stairs == 11 )
			{
				teleportToX = 2856;
				teleportToY = 9570;
			}
			else if ( stairs == 12 )
			{
				teleportToX = 2857;
				teleportToY = 3167;
			}
			else if ( stairs == 13 )
			{
				heightLevel += 3;
				teleportToX = skillX;
				teleportToY = skillY;
			}
			else if ( stairs == 15 )
			{
				teleportToY += ( 6400 - ( stairDistance + stairDistanceAdd ) );
			}
			else if ( stairs == 14 )
			{
				teleportToY -= ( 6400 - ( stairDistance + stairDistanceAdd ) );
			}
			else if ( stairs == 16 )
			{
				teleportToX = 2828;
				teleportToY = 9772;
			}
			else if ( stairs == 17 )
			{
				teleportToX = 3494;
				teleportToY = 3465;
			}
			else if ( stairs == 18 )
			{
				teleportToX = 3477;
				teleportToY = 9845;
			}
			else if ( stairs == 19 )
			{
				teleportToX = 3543;
				teleportToY = 3463;
			}
			else if ( stairs == 20 )
			{
				teleportToX = 3549;
				teleportToY = 9865;
			}
			else if ( stairs == 21 )
			{
				teleportToY += ( stairDistance + stairDistanceAdd );
			}
			else if ( stairs == 22 )
			{
				teleportToY -= ( stairDistance + stairDistanceAdd );
			}
			else if ( stairs == 23 )
			{
				teleportToX = 2480;
				teleportToY = 5175;
			}
			else if ( stairs == 24 )
			{
				teleportToX = 2862;
				teleportToY = 9572;
			}
			else if ( stairs == 25 )
			{
				Essence = ( heightLevel / 4 );
				heightLevel = 0;
				teleportToX = EssenceMineRX[Essence];
				teleportToY = EssenceMineRY[Essence];
			}
			else if ( stairs == 26 )
			{
				int EssenceRnd = misc.random3( EssenceMineX.Length );

				teleportToX = EssenceMineX[EssenceRnd];
				teleportToY = EssenceMineY[EssenceRnd];
				heightLevel = ( Essence * 4 );
			}
			else if ( stairs == 27 )
			{
				teleportToX = 2453;
				teleportToY = 4468;
			}
			else if ( stairs == 28 )
			{
				teleportToX = 3201;
				teleportToY = 3169;
			}
			if ( ( stairs == 5 ) || ( stairs == 10 ) )
			{
				teleportToX += ( stairDistance + stairDistanceAdd );
			}
			if ( ( stairs == 6 ) || ( stairs == 9 ) )
			{
				teleportToX -= ( stairDistance - stairDistanceAdd );
			}
		}
		resetStairs();
		return true;
	}

	public Boolean stakeItem( int itemID, int fromSlot, int amount )
	{
		if ( TimeHelper.CurrentTimeMillis() - lastButton < 800 )
		{
			return false;
		}
		lastButton = TimeHelper.CurrentTimeMillis();
		if ( !Item.itemStackable[itemID] && !Item.itemIsNote[itemID]
				&& ( amount > 1 ) )
		{
			for ( int a = 1; a <= amount; a++ )
			{
				int slot = findItem( itemID, playerItems, playerItemsN );
				if ( slot >= 0 )
				{
					stakeItem( itemID, slot, 1 );
				}
			}
		}
		foreach ( int element in noTrade )
		{
			if ( ( itemID == element ) || ( itemID == element + 1 )
					|| premiumItem( itemID ) )
			{
				sendMessage( "You can't trade that item" );
				// declineDuel();
				return false;
			}
		}
		client other = getClient( duel_with );
		if ( !inDuel || !ValidClient( duel_with ) )
		{
			declineDuel();
			return false;
		}
		if ( !canOffer )
		{
			return false;
		}
		if ( !playerHasItem( itemID, amount ) )
		{
			return false;
		}
		if ( Item.itemStackable[itemID] || Item.itemIsNote[itemID] )
		{
			Boolean inTrade = false;
			foreach ( GameItem item in offeredItems )
			{
				if ( item.id == itemID )
				{
					inTrade = true;
					item.amount += amount;
					break;
				}
			}
			if ( !inTrade )
			{
				offeredItems.Add( new GameItem( itemID, amount ) );
			}
		}
		else
		{
			offeredItems.Add( new GameItem( itemID, 1 ) );
		}
		deleteItem( itemID, fromSlot, amount );
		resetItems( 3214 );
		resetItems( 3322 );
		other.resetItems( 3214 );
		other.resetItems( 3322 );
		refreshDuelScreen();
		other.refreshDuelScreen();
		sendFrame126( "", 6684 );
		other.sendFrame126( "", 6684 );
		return true;
	}

	public void startCraft( int actionbutton )
	{
		closeInterface();
		int[] buttons = { 33187, 33186, 33185, 33190, 33189, 33188, 33193,
					33192, 33191, 33196, 33195, 33194, 33199, 33198, 33197, 33202,
					33201, 33200, 33205, 33204, 33203 };
		int[] amounts = { 1, 5, 10, 1, 5, 10, 1, 5, 10, 1, 5, 10, 1, 5, 10, 1,
					5, 10, 1, 5, 10 };
		int[] ids = { 1129, 1129, 1129, 1059, 1059, 1059, 1061, 1061, 1061,
					1063, 1063, 1063, 1095, 1095, 1095, 1169, 1169, 1169, 1167,
					1167, 1167 };
		int[] levels = { 14, 1, 7, 11, 18, 38, 9 };
		int[] exp = { 27, 14, 16, 22, 27, 37, 19 };
		int amount = 0, id = -1;
		int index = 0;
		for ( int i = 0; i < buttons.Length; i++ )
		{
			if ( actionbutton == buttons[i] )
			{
				amount = amounts[i];
				id = ids[i];
				index = i % 3;
			}
		}
		if ( playerLevel[playerCrafting] >= levels[index] )
		{
			crafting = true;
			cItem = id;
			cAmount = amount;
			cLevel = levels[index];
			cExp = ( Int32 ) Math.Round( exp[index] * 9d );
			cSelected = 1741;
		}
		else
		{
			sendMessage( "Requires level " + levels[index] );
		}
	}

	public void StartDuel( )
	{
		plrText = "FIGHT!";
		plrTextUpdateRequired = true;
		closeInterface();
		sendMessage( "Duel commencing!" );
		duelFight = true;
		client other = getClient( duel_with );
		currentHealth = playerLevel[playerHitpoints];
		playerLevel[0] = getLevelForXP( playerXP[0] );
		playerLevel[1] = getLevelForXP( playerXP[1] );
		playerLevel[2] = getLevelForXP( playerXP[2] );
		playerLevel[4] = getLevelForXP( playerXP[4] );
		playerLevel[5] = getLevelForXP( playerXP[5] );
		playerLevel[6] = getLevelForXP( playerXP[6] );
		foreach ( GameItem item in other.offeredItems )
		{
			otherOfferedItems.Add( new GameItem( item.id, item.amount ) );
			if ( !duelRule[10] )
			{
				if ( duelRule[9] )
				{
					teleportToX = 3356;
					teleportToY = 3213;
					other.teleportToX = 3355;
					other.teleportToY = 3213;
				}
				else
				{
					teleportToX = 3336 + misc.random( 17 );
					teleportToY = 3208 + misc.random( 7 );
					other.teleportToX = 3336 + misc.random( 17 );
					other.teleportToY = 3208 + misc.random( 7 );
				}
				updateRequired = true;
				appearanceUpdateRequired = true;
			}

			if ( duelRule[10] )
			{
				if ( !duelRule[9] )
				{
					teleportToX = 3356;
					teleportToY = 3232;
					other.teleportToX = 3355;
					other.teleportToY = 3232;
				}
				else
				{
					teleportToX = 3341 + misc.random( 5 );
					teleportToY = 3228 + misc.random( 5 );
					other.teleportToX = 3341 + misc.random( 5 );
					other.teleportToY = 3228 + misc.random( 5 );
				}
			}

		}
	}
	public void startFishing( int obj )
	{
		int req = -1, reqFishing = 1;
		switch ( obj ) {
			case 316:
				pEmote = 621;
				fishId = 317;
				req = 303;
				break;
			case 321:
				pEmote = 621;
				fishId = 377;
				req = 301;
				reqFishing = 40;
				break;

		}
		fishTries = misc.random( 27 );
		if ( playerLevel[playerFishing] < reqFishing )
		{
			sendMessage( "Requires level " + reqFishing + " fishing" );
			return;
		}
		if ( !playerHasItem( req ) )
		{
			sendMessage( "You need a " + getItemName( req ) + " to fish here" );
			return;
		}
		if ( TimeHelper.CurrentTimeMillis() - lastAction >= 5000 )
			fishing = true;
	}

	public void startHideCraft( int b )
	{
		int[] buttons = { 34185, 34184, 34183, 34182, 34189, 34188, 34187,
					34186, 34193, 34192, 34191, 34190 };
		int[] amounts = { 1, 5, 10, 27 };
		int index = 0;
		int index2 = 0;
		for ( int i = 0; i < buttons.Length; i++ )
		{
			if ( buttons[i] == b )
			{
				index = i % 4;
				index2 = ( int ) ( i / 4 );
				break;
			}
		}
		cAmount = amounts[index];
		cSelected = Constants.leathers[cIndex];
		int required = 99;
		if ( index2 == 0 )
		{
			required = Constants.gloveLevels[cIndex];
			cItem = Constants.gloves[cIndex];
			cExp = Constants.gloveExp[cIndex];
		}
		else if ( index2 == 1 )
		{
			required = Constants.legLevels[cIndex];
			cItem = Constants.legs[cIndex];
			cExp = Constants.legExp[cIndex];
		}
		else
		{
			required = Constants.chestLevels[cIndex];
			cItem = Constants.chests[cIndex];
			cExp = Constants.chestExp[cIndex];
		}
		if ( playerLevel[playerCrafting] >= required )
		{
			cExp = ( int ) ( cExp * 8 );
			crafting = true;
			closeInterface();
		}
		else
		{
			sendMessage( "Requires level " + required );
		}
	}

	public void startSmelt( int id )
	{
		int[] amounts = { 1, 5, 10, 27 };
		int index = 0, index2 = 0;
		for ( int i = 0; i < misc.buttons_smelting.Length; i++ )
		{
			if ( id == misc.buttons_smelting[i] )
			{
				index = i % 4;
				index2 = ( int ) ( i / 4 );
			}
		}
		smelt_id = misc.smelt_bars[index2];
		smeltCount = amounts[index];
		smelting = true;
		closeInterface();
	}

	public void startTan( int amount, int type )
	{
		int done = 0;
		int[] hide = { 1739, 1739, 1753, 1751, 1749, 1747 };
		int[] leather = { 1741, 1741, 1745, 2505, 2507, 2509 };
		int[] charge = { 50, 100, 1000, 2000, 5000, 10000 };
		while ( ( done < amount ) && playerHasItem( hide[type] )
				&& playerHasItem( 995, charge[type] ) )
		{
			deleteItem( hide[type], 1 );
			deleteItem( 995, charge[type] );
			addItem( leather[type], 1 );
			done++;
		}
		int total = done * charge[type];
		/*
		 * dsendMessage = "That's " + total + "gp for " + done + " hides"; NpcWanneTalk =
		 * 10; skillX = absX; skillY = absY; NpcTalkTo = 804;
		 */
	}

	public void stillgfx( int id, int y, int x )
	{
		stillgfx( id, y, x, 0, 0 );
	}

	public void stillgfx( int id, int Y, int X, int height, int time )
	{
		// foreach (Player p in PlayerHandler.players) {
		foreach ( Player p in PlayerHandler.players )
		{
			if ( p != null )
			{
				client person = ( client ) p;

				if ( person.playerName != null )
				{
					if ( person.distanceToPoint( X, Y ) <= 60 )
					{
						person.stillgfx2( id, Y, X, height, time );
					}
				}
			}
		}
	}

	public void stillgfx2( int id, int Y, int X, int height, int time )
	{
		outStream.createFrame( 85 );
		outStream.writeByteC( Y - ( mapRegionY * 8 ) );
		outStream.writeByteC( X - ( mapRegionX * 8 ) );
		outStream.createFrame( 4 );
		outStream.writeByte( 0 ); // Tiles away (X >> 4 + Y & 7)
		outStream.writeWord( id ); // Graphic id
		outStream.writeByte( height ); // height of the spell above it's basic
									   // place, i think it's written in pixels
									   // 100 pixels higher
		outStream.writeWord( time ); // Time before casting the graphic
	}

	/*
	 * These are mainly god spells since they aren't multi-target
	 */
	public Boolean StillSpell( int i )
	{
		if ( ( i == 1190 ) || ( i == 1191 ) || ( i == 1192 ) )
		{
			return true;
		}
		return false;
	}

	public void Teleblock( )
	{
		// stops from teleing anywhere
		AntiTeleDelay = 1000;
	}

	public void teleOtherRequest( String teleLocation, int player )
	{
		String telePlayer = PlayerHandler.players[player].playerName;

		sendQuest( telePlayer, 12558 );
		sendQuest( teleLocation, 12560 );
		showInterface( 12468 );
		teleReq = player;
		teleLoc = teleLocation;
		teleOtherScreen = true;
	}




	public void testMagic( int spellID )
	{
		createProjectile( absY, absX, 0, 3, 50, 160, spellID, 43, 31, 0 );
	}

	public Boolean tradeItem( int itemID, int fromSlot, int amount )
	{
		if ( TimeHelper.CurrentTimeMillis() - lastButton > 800 )
		{
			lastButton = TimeHelper.CurrentTimeMillis();
		}
		else
		{
			return false;
		}
		if ( !Item.itemStackable[itemID] && ( amount > 1 ) )
		{
			for ( int a = 1; a <= amount; a++ )
			{
				int slot = findItem( itemID, playerItems, playerItemsN );
				if ( slot >= 0 )
				{
					tradeItem( itemID, slot, 1 );
				}
			}
		}
		foreach ( int element in noTrade )
		{
			if ( ( itemID == element ) || ( itemID == element + 1 )
					|| premiumItem( itemID ) )
			{
				sendMessage( "You can't trade that item" );
				declineTrade();
				return false;
			}
		}
		client other = getClient( trade_reqId );
		if ( !inTrade || !ValidClient( trade_reqId ) || !canOffer )
		{
			declineTrade();
			return false;
		}
		if ( !playerHasItem( itemID, amount ) )
		{
			return false;
		}
		if ( Item.itemStackable[itemID] || Item.itemIsNote[itemID] )
		{
			Boolean inTrade = false;
			foreach ( GameItem item in offeredItems )
			{
				if ( item.id == itemID )
				{
					inTrade = true;
					item.amount += amount;
					break;
				}
			}
			if ( !inTrade )
			{
				offeredItems.Add( new GameItem( itemID, amount ) );
			}
		}
		else
		{
			offeredItems.Add( new GameItem( itemID, 1 ) );
		}
		deleteItem( itemID, fromSlot, amount );
		resetItems( 3322 );
		resetTItems( 3415 );
		other.resetOTItems( 3416 );
		sendFrame126( "", 3431 );
		other.sendFrame126( "", 3431 );
		return true;
	}

	public void tradeReq( int id )
	{
		if ( !server.trading )
		{
			sendMessage( "Trading has been temporarily disabled" );
			return;
		}
		client other = ( client ) PlayerHandler.players[id];
		if ( ValidClient( trade_reqId ) )
		{
			if ( other.inTrade || other.inDuel )
			{
				sendMessage( "That player is busy at the moment" );
				trade_reqId = 0;
				return;
			}
		}
		//if (other.connectedFrom.Equals(connectedFrom)) {
		//	tradeRequested = false;
		//	return;
		//}
		if ( ValidClient( trade_reqId ) && !inTrade && other.tradeRequested
				&& ( other.trade_reqId == playerId ) )
		{
			openTrade();
			other.openTrade();
		}
		else if ( ValidClient( trade_reqId ) && !inTrade
			  && ( TimeHelper.CurrentTimeMillis() - lastButton > 1000 ) )
		{
			lastButton = TimeHelper.CurrentTimeMillis();
			tradeRequested = true;
			trade_reqId = id;
			sendMessage( "Sending trade request..." );
			other.sendMessage( playerName + ":tradereq:" );
		}
	}

	public void triggerRandom( )
	{
		if ( !randomed )
		{
			random_skill = misc.random( statName.Length ) - 1;
			if ( random_skill < 0 )
				random_skill = 0;
			sendQuest( "Click the @or1@" + statName[random_skill]
					+ " @yel@button", 2810 );
			sendQuest( "", 2811 );
			sendQuest( "", 2831 );
			randomed = true;
			showInterface( 2808 );
		}
	}

	public void triggerTele( int x, int y, int height, Boolean prem )
	{
		if ( TimeHelper.CurrentTimeMillis() - lastAction > 5000 )
		{
			lastAction = TimeHelper.CurrentTimeMillis();
			resetWalkingQueue();
			if ( prem && !premium )
			{
				sendMessage( "This spell is only available to premium members, visit moparscape.org/smf for info" );
				return;
			}
			tX = x;
			tY = y;
			tH = height;
			tStage = 1;
			tTime = 0;
		}
	}

	public void uberentangle( )
	{
		EntangleDelay = 40;
	}

	public override void update( )
	{
		PlayerHandler.updatePlayer( this, outStream );
		handler.updateNPC( this, outStream );

		flushOutStream();
	}

	public void updateCharAppearance( int[] styles, int[] colors )
	{
		for ( int j = 0; j < 7; j++ )
		{
			if ( styles[j] > 0 )
			{
				styles[j] += 0x100;
				pCHead = styles[0];
				pCBeard = styles[1];
				pCTorso = styles[2];
				pCArms = styles[3];
				pCHands = styles[4];
				pCLegs = styles[5];
				pCFeet = styles[6];
			}
		}
		for ( int i = 0; i < 5; i++ )
		{
			pColor = colors[i];
		}
	}

	/* NPC Talking */
	public void UpdateNPCChat( )
	{

		/*
		 * sendFrame126("", 4902); sendFrame126("", 4903); sendFrame126("",
		 * 4904); sendFrame126("", 4905); sendFrame126("", 4906);
		 */
		sendFrame126( "", 976 );
		switch ( NpcDialogue )
		{
			case 1:

				/*
				 * sendFrame200(4901, 554); sendFrame126(GetNpcName(NpcTalkTo),
				 * 4902); sendFrame126("Good day, how can I help you?", 4904);
				 * sendFrame75(NpcTalkTo, 4901); sendFrame164(4900);
				 */
				sendFrame200( 4883, 591 );
				sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
				sendFrame126( "Good day, how can I help you?", 4885 );
				sendFrame75( NpcTalkTo, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				break;

			case 2:
				sendFrame171( 1, 2465 );
				sendFrame171( 0, 2468 );
				sendFrame126( "What would you like to say?", 2460 );
				sendFrame126( "I'd like to access my bank account, please.", 2461 );
				sendFrame126( "I'd like to check my PIN settings.", 2462 );
				sendFrame164( 2459 );
				NpcDialogueSend = true;
				break;

			case 3:
				sendFrame200( 4883, 591 );
				sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
				sendFrame126( "Do you want to buy some runes?", 4885 );
				sendFrame75( NpcTalkTo, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				break;

			case 4:
				sendFrame171( 1, 2465 );
				sendFrame171( 0, 2468 );
				sendFrame126( "Select an Option", 2460 );
				sendFrame126( "Yes please!", 2461 );
				sendFrame126( "Oh it's a rune shop. No thank you, then.", 2462 );
				sendFrame164( 2459 );
				NpcDialogueSend = true;
				break;

			case 5:
				sendFrame200( 615, 974 );
				sendFrame126( playerName, 975 );
				sendFrame126( "Oh it's a rune shop. No thank you, then.", 977 );
				sendFrame185( 974 );
				sendFrame164( 973 );
				NpcDialogueSend = true;
				break;

			case 6:
				sendFrame200( 4883, 591 );
				sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
				sendFrame126(
						"Well, if you find somone who does want runes, please",
						4885 );
				sendFrame126( "send them my way.", 4886 );
				sendFrame75( NpcTalkTo, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				break;

			case 7:
				/* NEED TO CHANGE FOR GUARD */
				sendFrame200( 4883, 591 );
				sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
				sendFrame126(
						"Well, if you find somone who does want runes, please",
						4885 );
				sendFrame126( "send them my way.", 4886 );
				sendFrame75( NpcTalkTo, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				break;
			case 9:
				sendFrame200( 4883, 2253 );
				sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
				sendFrame126( "Select an Option", 2460 );
				sendFrame126( "Can i select my class please?", 2461 );
				sendFrame126( "Can you give me information on this server?", 2462 );
				sendFrame164( 2459 );
				NpcDialogueSend = true;
				break;
			case 10:
				sendFrame200( 4883, 804 );
				sendFrame126( GetNpcName( 804 ), 4884 );
				sendFrame126( dsendMessage, 4885 );
				sendFrame75( 804, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				break;
			case 11:
				sendFrame200( 4883, 591 );
				sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
				sendFrame126( "would you like me to teleport you to barrows?", 4885 );
				sendFrame126( "Click here to continue", 4886 );
				sendFrame75( NpcTalkTo, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				break;
			case 12:
				sendFrame200( 4883, 591 );
				sendFrame126( playerName, 4884 );
				sendFrame126( "Yes please!", 4885 );
				sendFrame126( "Click here to continue", 4886 );
				sendFrame75( pCHead, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				break;
			case 13:
				sendFrame200( 4883, 591 );
				sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
				sendFrame126( "here you go sir!", 4885 );
				sendFrame75( NpcTalkTo, 4883 );
				sendFrame164( 4882 );
				NpcDialogueSend = true;
				teleportToX = 3564;
				teleportToY = 3288;
				break;
			case 3478:
				if ( isinjaillolz == 1 && playerHasItemAmount( 1964, 3000 ) )
				{
					sendFrame200( 4883, 591 );
					sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
					sendFrame126( "leave and DONT let me catch you in here again.", 4885 );
					sendFrame75( NpcTalkTo, 4883 );
					sendFrame164( 4882 );
					NpcDialogueSend = true;
					deleteItem( 1964, 3000 );
					isinjaillolz = 0;
					break;
				}
				if ( isinjaillolz == 1 && !playerHasItemAmount( 1964, 1000 ) )
				{
					sendFrame200( 4883, 591 );
					sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
					sendFrame126( "Where is my BANANA'S! i need 3000 noted.", 4885 );
					sendFrame75( NpcTalkTo, 4883 );
					sendFrame164( 4882 );
					NpcDialogueSend = true;
				}
				break;
			case 2244:
				if ( HasLearnedCombat == 0 )
				{
					sendFrame200( 4883, 591 );
					sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
					sendFrame126( "Hello im here to teach you, your basics of combat.", 4885 );
					sendFrame75( NpcTalkTo, 4883 );
					sendFrame164( 4882 );
					NpcDialogueSend = true;
					break;
				}
				if ( HasLearnedCombat == 1 )
				{
					sendFrame200( 4883, 591 );
					sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
					sendFrame126( "Sorry i have nothing to teach you at this moment.", 4885 );
					sendFrame75( NpcTalkTo, 4883 );
					sendFrame164( 4882 );
					NpcDialogueSend = true;
				}
				break;
			case 2245:
				if ( HasLearnedCombat == 1 )
				{
					sendFrame200( 4883, 591 );
					sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
					sendFrame126( "Sorry i have nothing to teach you at this moment.", 4885 );
					sendFrame75( NpcTalkTo, 4883 );
					sendFrame164( 4882 );
					NpcDialogueSend = true;
					break;
				}
				if ( HasLearnedCombat == 0 )
				{
					sendFrame200( 4883, 591 );
					sendFrame126( GetNpcName( NpcTalkTo ), 4884 );
					sendFrame126( "Kill this Man to prove you are worthy.", 4885 );
					sendFrame75( NpcTalkTo, 4883 );
					sendFrame164( 4882 );
					NpcDialogueSend = true;
					HasLearnedCombat += 1;
					teleportToX = 3235;
					teleportToY = 3155;
				}
				break;

		}
	}
	public Boolean teleblock = false;
	public void UpdatePlayerShop( )
	{
		for ( int i = 1; i < PlayerHandler.maxPlayers; i++ )
		{
			if ( PlayerHandler.players[i] != null )
			{
				if ( ( PlayerHandler.players[i].IsShopping == true )
						&& ( PlayerHandler.players[i].MyShopID == MyShopID )
						&& ( i != playerId ) )
				{
					PlayerHandler.players[i].UpdateShop = true;
				}
			}
		}
	}

	public void updateRating( )
	{
		sendQuest( "Pk: " + rating, 3985 );
	}

	public Boolean ValidClient( int index )
	{
		client p = ( client ) PlayerHandler.players[index];
		if ( ( p != null ) && !p.disconnected )
		{
			return true;
		}
		return false;
	}

	/* END OF PRAYER */
	public void viewTo( int coordX, int coordY )
	{
		viewToX = ( ( 2 * coordX ) + 1 );
		viewToY = ( ( 2 * coordY ) + 1 );
		dirUpdate2Required = true;
		updateRequired = true;
	}

	public void WalkTo( int x, int y )
	{
		if ( EntangleDelay > 0 )
			return;
		newWalkCmdSteps = ( Math.Abs( ( x + y ) ) );
		if ( newWalkCmdSteps % 1 != 0 ) newWalkCmdSteps /= 1;
		if ( ++newWalkCmdSteps > walkingQueueSize )
		{
			println( "Warning: WalkTo(" + packetType + ") command contains too many steps (" + newWalkCmdSteps + ")." );
			newWalkCmdSteps = 0;
		}
		int firstStepX = absX;
		int tmpFSX = firstStepX;
		firstStepX -= mapRegionX * 8;
		for ( i = 1; i < newWalkCmdSteps; i++ )
		{
			newWalkCmdX[i] = ( SByte ) x;
			newWalkCmdY[i] = ( SByte ) y;
			tmpNWCX[i] = newWalkCmdX[i];
			tmpNWCY[i] = newWalkCmdY[i];
		}
		newWalkCmdX[0] = newWalkCmdY[0] = tmpNWCX[0] = tmpNWCY[0] = 0;
		int firstStepY = absY;
		int tmpFSY = firstStepY;
		firstStepY -= mapRegionY * 8;
		newWalkCmdIsRunning = ( ( inStream.readSignedByteC() == 1 ) && playerEnergy > 0 );
		for ( i = 0; i < newWalkCmdSteps; i++ )
		{
			newWalkCmdX[i] += ( SByte ) firstStepX;
			newWalkCmdY[i] += ( SByte ) firstStepY;
		}
	}



	public void ifFreeze( int delay2 )
	{
		if ( EntangleDelay == 0 )
		{
			EntangleDelay = delay2;
			sendMessage( "You have been frozen!" );
			newWalkCmdSteps = 0;
			newWalkCmdX[0] = newWalkCmdY[0] = tmpNWCX[0] = tmpNWCY[0] = 0;
			getNextPlayerMovement();
		}
	}

	public void walkTo_old( int X, int Y )
	{
		int firstStepX = inStream.readSignedWordBigEndianA();
		int tmpFSX = X;
		X -= mapRegionX * 8;
		for ( i = 1; i < newWalkCmdSteps; i++ )
		{
			newWalkCmdX[i] = inStream.readSignedByte();
			newWalkCmdY[i] = inStream.readSignedByte();
			tmpNWCX[i] = newWalkCmdX[i];
			tmpNWCY[i] = newWalkCmdY[i];
		}
		newWalkCmdX[0] = newWalkCmdY[0] = tmpNWCX[0] = tmpNWCY[0] = 0;
		int firstStepY = inStream.readSignedWordBigEndian();
		int tmpFSY = Y;
		Y -= mapRegionY * 8;
		newWalkCmdIsRunning = inStream.readSignedByteC() == 1;
		for ( i = 0; i < newWalkCmdSteps; i++ )
		{
			newWalkCmdX[i] += ( SByte ) X;
			newWalkCmdY[i] += ( SByte ) Y;
		}
		println_debug( "Walking to X:" + X + " Y:" + Y );
	}

	public int WCCheckAxe( )
	{
		int Hand;
		int Shield;
		int Bag;
		int Axe;

		Hand = playerEquipment[playerWeapon];
		Shield = playerEquipment[playerShield];
		Axe = 0;
		switch ( Hand )
		{
			case 1351:
				// Bronze Axe
				Axe = 1;
				break;

			case 1349:
				// Iron Axe
				Axe = 2;
				break;

			case 1353:
				// Steel Axe
				Axe = 3;
				break;

			case 1361:
				// Black Axe
				Axe = 4;
				break;

			case 1355:
				// Mithril Axe
				Axe = 5;
				break;

			case 1357:
				// Adamant Axe
				Axe = 6;
				break;

			case 1359:
				// Rune Axe
				Axe = 7;
				break;

				/*
				 * case X: //Dragon Axe Axe = 8; break;
				 */
		}
		/*
		 * if (Axe > 0) { OriginalWeapon = Hand; OriginalShield = Shield;
		 * playerEquipment[playerShield] = -1; return Axe; }
		 */
		if ( Axe > 0 )
		{
			// OriginalWeapon = Hand;
			// OriginalShield = Shield;
			// playerEquipment[playerShield] = -1;
			// playerEquipment[playerWeapon] = Bag;
		}
		return Axe;
	}

	public Boolean wear( int wearID, int slot )
	{

		int targetSlot = 0;
		targetSlot = itemType( wearID );
		int[] two_hand = { 1319, 4718, 4726, 1409, 4710, 7158 };
		foreach ( int element in two_hand )
		{
			if ( ( wearID == element ) && ( playerEquipment[playerShield] > 0 ) )
			{
				if ( playerHasItem( -1 ) )
				{
					addItem( playerEquipment[playerShield], 1 );
					playerEquipment[playerShield] = -1;
				}
				else
				{
					sendMessage( "You can't wear this weapon with a shield" );
					return false;
				}
			}
			if ( ( itemType( wearID ) == playerShield )
					&& ( playerEquipment[playerWeapon] == element ) )
			{
				if ( playerHasItem( -1 ) )
				{
					addItem( playerEquipment[playerWeapon], 1 );
					playerEquipment[playerWeapon] = -1;
				}
				else
				{
					sendMessage( "You can't wear a shield with this weapon" );
					return false;
				}
			}
			if ( !canUse( wearID ) )
			{
				sendMessage( "You must be a premium member to use this item" );
				return false;
			}
		}


		if ( ( playerItems[slot] - 1 ) == wearID )
		{
			targetSlot = itemType( wearID );

			int CLAttack = server.lvlHandler.GetCLAttack( wearID );
			int CLStrength = server.lvlHandler.GetCLStrength( wearID );
			int CLDefence = server.lvlHandler.GetCLDefence( wearID );
			int CLRanged = server.lvlHandler.GetCLRanged( wearID );
			int CLPrayer = server.lvlHandler.GetCLPrayer( wearID );
			int CLMagic = server.lvlHandler.GetCLMagic( wearID );
			int CLHitpoints = server.lvlHandler.GetCLHitpoints( wearID );
			int CLismember = server.lvlHandler.GetCLismember( wearID );
			Boolean GoFalse = false;

			/*[) ----- Attack ----- (]*/
			if ( CLAttack > getLevelForXP( playerXP[0] ) )
			{
				sendMessage( "You need " + CLAttack + " " + statName[playerAttack] + " to equip this item." );
				GoFalse = true;
			}

			if ( CLismember == 0 )
			{
				if ( playerIsMember == 0 )
				{
					sendMessage( "This item is for members only." );
					GoFalse = true;
				}
			}




			/*[) ----- Strength----- (]*/
			if ( CLStrength > getLevelForXP( playerXP[2] ) )
			{
				sendMessage( "You need " + CLStrength + " " + statName[playerStrength] + " to equip this item." );
				GoFalse = true;
			}
			/*[) ----- Defence ----- (]*/
			if ( CLDefence > getLevelForXP( playerXP[1] ) )
			{
				sendMessage( "You need " + CLDefence + " " + statName[playerDefence] + " to equip this item." );
				GoFalse = true;
			}
			/*[) ----- Ranged ----- (]*/
			if ( CLRanged > getLevelForXP( playerXP[4] ) )
			{
				sendMessage( "You need " + CLRanged + " " + statName[playerRanged] + " to equip this item." );
				GoFalse = true;
			}
			/*[) ----- Prayer ----- (]*/
			if ( CLPrayer > getLevelForXP( playerXP[5] ) )
			{
				sendMessage( "You need " + CLPrayer + " " + statName[playerPrayer] + " to equip this item." );
				GoFalse = true;
			}
			/*[) ----- Magic ----- (]*/
			if ( CLMagic > getLevelForXP( playerXP[6] ) )
			{
				sendMessage( "You need " + CLMagic + " " + statName[playerMagic] + " to equip this item." );
				GoFalse = true;
			}
			/*[) ----- Hitpoints ----- (]*/
			if ( CLHitpoints > getLevelForXP( playerXP[3] ) )
			{
				sendMessage( "You need " + CLHitpoints + " " + statName[playerHitpoints] + " to equip this item." );
				GoFalse = true;
			}
			if ( GoFalse == true )
			{
				return false;
			}
			int wearAmount = playerItemsN[slot];

			if ( wearAmount < 1 )
			{
				return false;
			}
			if ( ( slot >= 0 ) && ( wearID >= 0 ) )
			{
				deleteItem( wearID, slot, wearAmount );
				if ( ( playerEquipment[targetSlot] != wearID ) && ( playerEquipment[targetSlot] >= 0 ) )
				{
					addItem( playerEquipment[targetSlot], playerEquipmentN[targetSlot] );
				}
				else if ( Item.itemStackable[wearID] && ( playerEquipment[targetSlot] == wearID ) )
				{
					wearAmount = playerEquipmentN[targetSlot] + wearAmount;
				}
				else if ( playerEquipment[targetSlot] >= 0 )
				{
					addItem( playerEquipment[targetSlot], playerEquipmentN[targetSlot] );
				}
			}
			outStream.createFrameVarSizeWord( 34 );
			outStream.writeWord( 1688 );
			outStream.writeByte( targetSlot );
			outStream.writeWord( wearID + 1 );
			if ( wearAmount > 254 )
			{
				outStream.writeByte( 255 );
				outStream.writeDWord( wearAmount );
			}
			else
			{
				outStream.writeByte( wearAmount ); // amount
			}
			outStream.endFrameVarSizeWord();
			playerEquipment[targetSlot] = wearID;
			playerEquipmentN[targetSlot] = wearAmount;
			if ( ( targetSlot == playerWeapon )
					&& ( playerEquipment[playerShield] != -1 )
					&& ( Item.itemTwoHanded[wearID] == true ) )
			{
				remove( playerEquipment[playerShield], playerShield );
			}
			if ( targetSlot == playerWeapon )
			{
				SendWeapon( wearID, getItemName( wearID ) );
				playerSE = GetStandAnim( wearID );
				playerSEW = GetWalkAnim( wearID );//gobackhere
				playerSER = GetRunAnim( wearID );
				playerSEA = 0x326;

				if ( wearID == 4747 )
				{ //Torag Hammers
					playerSEA = 0x814;
				}
				if ( wearID == 4151 )
				{ //Whip
					playerSER = 1661;
					playerSEW = 1660;
				}
				if ( wearID == 8447 )
				{ //cat toy
					playerSER = 1661;
					playerSEW = 1660;
				}
				pEmote = playerSE;
			}
			ResetBonus();
			GetBonus();
			WriteBonus();
			SendWeapon( ( playerEquipment[playerWeapon] ), GetItemName( playerEquipment[playerWeapon] ) );
			updateRequired = true;
			appearanceUpdateRequired = true;
			wearing = false;
			return true;
		}
		return false;
	}

	/* WOODCUTTING */
	public Boolean Woodcut( )
	{
		if ( randomed )
			return false;
		int WCAxe = 0;

		if ( IsCutting == true )
		{
			WCAxe = 1; // If Cutting -> Go trough loop, passby WCCheckAxe to
					   // prevent originalweapon loss, 1 to tell you got axe,
					   // no function left for WCAxe if cutting, so 1 is
					   // enough.
		}
		else
		{
			WCAxe = WCCheckAxe();
		}
		if ( WCAxe > 0 )
		{
			if ( playerLevel[playerWoodcutting] >= woodcutting[1] )
			{
				if ( freeSlots() > 0 )
				{
					if ( ( TimeHelper.CurrentTimeMillis() - lastAction >= 1000 )
							&& ( IsCutting == false ) )
					{
						lastAction = TimeHelper.CurrentTimeMillis();
						attackTimer++;
						sendMessage( "You swing your axe at the tree..." );
						attackTimer = ( int ) ( ( woodcutting[0] + 10 ) - WCAxe );
						if ( attackTimer < 1 )
						{
							attackTimer = 1;
						}
						setAnimation( 0x284 );
						IsCutting = true;
					}
					if ( ( TimeHelper.CurrentTimeMillis() - lastAction >= 1600 )
							&& ( IsCutting == true ) )
					{
						lastAction = TimeHelper.CurrentTimeMillis();
						addSkillXP( ( woodcutting[2] * woodcutting[3] * 2 ),
								playerWoodcutting );
						if ( misc.random( 100 ) == 1 )
						{
							triggerRandom();
							resetWC();
							return false;
						}
						if ( freeSlots() > 0 )
						{
							sendMessage( "You get some logs." );
							addItem( woodcutting[4], 1 );
							attackTimer += 10;
						}
						else
						{
							playerEquipment[playerWeapon] = OriginalWeapon;
							OriginalWeapon = -1;
							resetAnimation();
							IsCutting = false;
							resetWC();
						}
					}
				}
				else
				{
					// createEnemyItem(woodcutting[4]);
					sendMessage( "Not enough space in your inventory." );
					woodcutting[0] = -1;
					resetWC();
					return false;
				}
			}
			else
			{
				sendMessage( "You need " + woodcutting[1] + " "
						+ statName[playerWoodcutting] + " to cut those logs." );
				resetWC();
				return false;
			}
		}
		else
		{
			sendMessage( "Equip your axe before cutting trees!" );
			resetWC();
			return false;
		}
		return true;
	}

	public void WriteBonus( )
	{
		int offset = 0;
		String send = "";

		for ( int i = 0; i < playerBonus.Length; i++ )
		{
			if ( playerBonus[i] >= 0 )
			{
				send = BonusName[i] + ": +" + playerBonus[i];
			}
			else
			{
				send = BonusName[i] + ": -"
						+ Math.Abs( playerBonus[i] );
			}

			if ( i == 10 )
			{
				offset = 1;
			}
			if ( i == 11 )
			{
				send = "Spell Dmg:  +" + playerBonus[i] + "";
			}
			sendFrame126( send, ( 1675 + i + offset ) );
		}
		CalculateMaxHit();

		/*
		 * for (int i = 4000; i <= 7000; i++) { sendFrame126("T"+i, i);
		 * println_debug("Sended: Test"+i); }
		 */// USED FOR TESTING INTERFACE NUMBERS !
	}

	public void WriteEnergy( )
	{
		// if (playerRights < 3) {
		playerEnergy = 100;
		// }
		sendFrame126( playerEnergy + "%", 149 );
	}

	public void writeLog( String data, String file )
	{
		// used for bans/mutes/chatlogs etc. -bakatool
		try
		{
			File.AppendAllLines( "config//" + file + ".txt", new String[]
			{
				data
			} );
		}
		catch ( IOException ioe )
		{
			//ioe.printStackTrace();
		}
	}

	public void yell( String message )
	{
		foreach ( Player p in PlayerHandler.players )
		{
			if ( ( message.IndexOf( "tradereq" ) > 0 )
					|| ( message.IndexOf( "duelreq" ) > 0 ) )
				return;
			if ( ( p == null ) || !p.isActive )
				continue;
			client temp = ( client ) p;
			if ( ( temp.absX > 0 ) && ( temp.absY > 0 ) )
				if ( ( temp != null ) && !temp.disconnected && p.isActive )
					temp.sendMessage( message );
		}
	}

}
}
