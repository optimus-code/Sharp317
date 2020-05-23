using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public abstract class Player
	{
		public void gfx0( int id, int delay )
		{
			mask100var1 = id;
			mask100var2 = delay;
			mask100update = true;
			updateRequired = true;
		}
		public stream inStream = null, outStream = null;
		public static client client = null;
		public static int GObjId = 0;
		public static int GObjOrient = 0;
		public static int GObjSet = 0;
		public int itemKept1, itemKept2, itemKept3, itemKept4;
		public int itemKept1Slot, itemKept2Slot, itemKept3Slot, itemKept4Slot;
		public Boolean itemSlot1, itemSlot2, itemSlot3, itemSlot4;
		public Boolean isSkulled;
		public long lastSkull;
		public Boolean keep6570;
		public int leaveDuelTimer = 0;
		public int startDuelTimer = 0;
		public Boolean hasDuelSign = false;
		public int hitID;
		public long lastTeleblock;
		public Boolean teleblocked;
		public long actionInterval;
		public Boolean secondTradeWindow = false;
		public Boolean secondDuelWindow = false;
		public int NoRange = 0;
		public int NoMelee = 0;
		public int NoMagic = 0;
		public Boolean DuelTele = false;
		public int NoSpecials = 0;
		public int FunWeapons = 0;
		public int NoForfeit = 0;
		public int NoDrinks = 0;
		public int NoFood = 0;
		public int NoPrayer = 0;
		public int NoMovement = 0;
		public int Obstacles = 0;
		public long entangleDelay, lastEntangle;
		public int EntangleDelay = 0;
		public Boolean autocasting;
		public int autocastID;
		public Boolean splash;
		public Boolean Ardougnewarning = false;
		public int delItem1, delAmount1, delItem2, delAmount2, delItem3, delAmount3 = -1;
		public Boolean showWarning = true;
		public int[] KilledBy;
		public long offTimer;
		public static int GObjType = 0;
		public static int GObjX = 0;
		public static int GObjY = 0;
		public static int id = -1; // mysql userid
		public static int localId = -1;
		public static int maxNPCListSize = NPCHandler.maxNPCSpawns;
		public static int maxPlayerListSize = PlayerHandler.maxPlayers;
		public static int walkingQueueSize = 50;
		protected static Boolean newWalkCmdIsRunning = false;
		protected static int newWalkCmdSteps = 0;
		protected static SByte[] newWalkCmdX = new SByte[walkingQueueSize];
		protected static SByte[] newWalkCmdY = new SByte[walkingQueueSize];
		protected static int numTravelBackSteps = 0;
		protected static stream playerProps;
		protected static SByte[] tmpNWCX = new SByte[walkingQueueSize];
		protected static SByte[] tmpNWCY = new SByte[walkingQueueSize];
		protected static SByte[] travelBackX = new SByte[walkingQueueSize];
		protected static SByte[] travelBackY = new SByte[walkingQueueSize];
		static Player()
		{
			playerProps = new stream(new byte[100]);
		}

		public void fsBar( int id1, int id2, int id3 )
		{
			outStream.createFrame( 70 );
			outStream.writeWord( id1 );
			outStream.writeWordBigEndian( id2 );
			outStream.writeWordBigEndian( id3 );
		}
		public int absX, absY;
		public Boolean hitUpdateRequired2;
		public int actionAmount = 0;
		public String actionName = "";
		public int actionTimer = 0;
		public int ActionType = -1;
		public Boolean AntiTradeScam = false;
			public Boolean appearanceUpdateRequired = true; // set to true if the
		public Boolean apset; // apperance set.
		public int AttackingOn = 0;
		public int attacknpc = -1;
		public int[] bankItems = new int[800];
		public int[] bankItemsN = new int[800];
		public Boolean bankNotes = false;
		public byte[] cachedPropertiesBitmap = new byte[( PlayerHandler.maxPlayers + 7 ) >> 3];
		public Boolean[] ChangeDoor = new Boolean[ObjectHandler.MaxObjects];
		protected byte[] chatText = new byte[4096];
		protected int chatTextSize = 0, chatTextEffects = 0, chatTextColor = 0;
		public Boolean chatTextUpdateRequired = false;
		public Boolean Climbing = false;
		public int ClimbStage = -1;
		public int combat = 0;
		public int combatLevel = 0;
		public String connectedFrom = "";
		public Boolean CrackerForMe = false;
		public Boolean CrackerMsg = false;
		public int currentX, currentY;
		public int deathStage = 0;
		public long deathTimer = 0;
		public Boolean debug = false;
		public int destinationID = -1;
		public int destinationRange = 1;
		public int destinationX = -1;
		public int destinationY = -1;
		public Boolean didTeleport = false;
		public int dir1 = -1, dir2 = -1;
		public int DirectionCount = 0;
		protected Boolean dirUpdate2Required = false;
		public Boolean dirUpdateRequired = false;
		public Boolean dropsitem = false;
		public int duelChatStage = -1;
		public int duelChatTimer = -1;
		public int[] duelItems = new int[28];
		public int[] duelItemsN = new int[28];
		public Boolean[] duelRule = new Boolean[28];
		public int duelStatus = -1; // 0 = Requesting duel, 1 = in duel screen, 2 =
		public int duelWith = 0;
		public int Essence;
		public Boolean fighting = false;
		public int FightType = 1;
		public Boolean[] FireDelete = new Boolean[ObjectHandler.MaxObjects];
		public int FocusPointX = -1, FocusPointY = -1;
		public Boolean following = false;
		public int freezeTimer = -1;
		// buddy list
		public long[] friends = new long[200];
		public String globalMessage = "";
		public int GObjChange = 1;
		public PlayerHandler handler = null;
		// /dueling
		public int headIcon = 1 & 1 >> 2;
		public Boolean healUpdateRequired = false;
		// relative x/y coordinates (to map region)
		// Note that mapRegionX*8+currentX yields absX
		public int heightLevel;
		// 0-3 supported by the client
		public int hitDiff = 0;
			public int hitDiff2 = 0;
		public int hits = 0, fightId = 0, npcId2 = 0;
			public Boolean hitUpdateRequired = false;
		public int hptype = 0;
		public int i = 0;
		public long[] ignores = new long[100];
		public Boolean inCombat = false;
		public Boolean initialized = false, disconnected = false, savefile = true;
		public int ip = 0;
		public Boolean isActive = false;
		public Boolean IsAttacking = false;
		public Boolean IsAttackingNPC = false;
		public Boolean IsBanking = false;
		public Boolean IsCutting = false;
		public Boolean[] IsDropped = new Boolean[ItemHandler.MaxDropItems];
		public Boolean IsDropping = false;
		public Boolean IsFireing = false;
		public Boolean[] IsFireShowed = new Boolean[ObjectHandler.MaxObjects];
		public Boolean IsGhost = false;
		public Boolean IsInCW = false;
		public Boolean IsInTz = false;
		public Boolean InWilderness = false;
		public Boolean isKicked = false;
		public Boolean IsMakingFire = false;
		public Boolean IsMining = false;
		public Boolean hasAccepted = false;
		public int AntiDupe = 0;
		public Boolean wonDuel = false;
		public Boolean isNpc;
		public Boolean IsPMLoaded = false;
		// points to (first free) slot for writing to the queue
		public Boolean isRunning = false;
		// name of the connecting client
		public Boolean isRunning2 = false;
		public Boolean IsShopping = false;
		public int IsSnowing = 0;
		protected Boolean IsStair = false;
		public Boolean IsUsingSkill = false;
		public int[] killers = new int[PlayerHandler.maxPlayers];
		public long lastCombat = 0;
		// implemented Constants (changed Constants to interface) -bakatool
		public long lastDeath = 0, lastAction = 0, lastClick = 0;
		public long lastPacket = 0, deathTime = 0;
		public int m4001 = 0;
		public int m4002 = 0;
		public int m4003 = 0;
		public int m4004 = 0;
		public int m4005 = 0;
		public int m4006 = 0;
		public int m4007 = 0;
		// set to true if char did teleport in this cycle
		public Boolean mapRegionDidChange = false;
		public int mapRegionX, mapRegionY;
		protected Boolean mask100update = false;
		public int mask100var1 = 0;
		public int mask100var2 = 0;
		protected Boolean mask1update = false;
		public int mask1var = 0;
		protected Boolean animationUpdateRequired = false;
		protected Boolean mask400update = false;
		public int maxItemAmount = /* 2000000000 */999999999;
		public String md5pass = "", playerSalt = "";
		public Boolean[] MustDelete = new Boolean[ItemHandler.MaxDropItems];
		public int playerEmotionReq = -1;
		public int playerEmotionDelay = 0;
		public int MyShopID = 0;
		protected int NewHP = 135;
		public Boolean newhptype = false;

		public int NpcDialogue = 0;

		public Boolean NpcDialogueSend = false;

		public int npcId;

		// bit at position npcId is set to 1 in case player is currently in
		// playerList
		public byte[] npcInListBitmap = new byte[( NPCHandler.maxNPCSpawns + 7 ) >> 3];
		public NPC[] npcList = new NPC[maxNPCListSize];

		public int npcListSize = 0;
		public int NpcTalkTo = 0;
		public int NpcWanneTalk = 0;
		public int OptionObject = -1;

		public int[] otherDuelItems = new int[28];
		public int[] otherDuelItemsN = new int[28];
		// Default appearance
		public int pArms = 31;
		public int pBeard = 16;
		public int pFeet = 42;
		public int pFeetC = 3;
		public int pGender = 0;
		public int pHairC = 3;
		public int pHands = 33;
		public int pHead = 1;
		public int pLegs = 39;
		public int pLegsC = 2;
		public int pSkinC = 0;
		public int pTorso = 20;
		public int pTorsoC = 1;

		public int playerSE = 0x328; // SE = Standard Emotion
		public int playerSEA = 0x326; // SEA = Standard Emotion Attack
		public int playerSER = 0x338; // SER = Standard Emotion Run
		public int playerSEW = 0x333; // SEW = Standard Emotion Walking
		public int pEmote = 0x328; // this being the original standing state
		public int pWalk = 0x333; // original walking animation
		public int playerFly = 0x2261; // Flying Emotion

		public int playerallignment;
		public Boolean playerAncientMagics = true;

		public int playerBankSize = 350;

		public int playercwstatus;
		public int[] playerBonus = new int[12];

		public int playerHat = 0;
		public int playerCape = 1;
		public int playerAmulet = 2;
		public int playerWeapon = 3;
		public int playerChest = 4;
		public int playerShield = 5;
		public int playerLegs = 7;
		public int playerHands = 9;
		public int playerFeet = 10;
		public int playerRing = 12;
		public int playerArrows = 13;

		public int playerAttack = 0;
		public int playerDefence = 1;
		public int playerStrength = 2;
		public int playerHitpoints = 3;
		public int playerRanged = 4;
		public int playerPrayer = 5;
		public int playerMagic = 6;
		public int playerCooking = 7;
		public int playerWoodcutting = 8;
		public int playerFletching = 9;
		public int playerFishing = 10;
		public int playerFiremaking = 11;
		public int playerCrafting = 12;
		public int playerSmithing = 13;
		public int playerMining = 14;
		public int playerHerblore = 15;
		public int playerAgility = 16;
		public int playerThieving = 17;
		public int playerSlayer = 18;
		public int playerFarming = 19;
		public int playerRunecrafting = 20;
		public int animationRequest = -1, animationWaitCycles = 0;
		public int playerEnergy;
		public int playerEnergyGian;
		public int[] playerEquipment = new int[14];
		public int[] playerEquipmentN = new int[14];
		public int[] playerFollow = new int[PlayerHandler.maxPlayers];
		public int playerFollowID = -1;
		public int playerGameCount;
		public int playerGameTime;
		public int playerId = -1;
		// -1 denotes world is full, otherwise this is the playerId
		// corresponds to the index in Player players[]
		// bit at position playerId is set to 1 in case player is currently in
		// playerList
		public byte[] playerInListBitmap = new byte[( PlayerHandler.maxPlayers + 7 ) >> 3];
		public int playerIsMember;
		public int[] playerItems = new int[28];
		public int[] playerItemsN = new int[28];
		public String playerLastConnect;
		public int playerLastLogin;

		public int[] playerLevel = new int[25];
		public int currentHealth = 0;//playerLevel[playerHitpoints];
		public int maxHealth = 0;//playerLevel[playerHitpoints];
		public Player[] playerList = new Player[maxPlayerListSize];
		public int playerListSize = 0;
		public int[] playerLook = new int[6];

		public int playerMaxHit = 0;
		public int playerMD = -1;
		public int playerMessages;

		public String playerName = null;
		// name of the connecting client
		public String playerPass = null;
		public int playerRights;
		// 0=normal player, 1=player mod, 2=real mod, 3=admin?

		public String playerServer;

		public int playerstatus;

		public int[] playerXP = new int[25];
		public String plrText = "";
		public Boolean plrTextUpdateRequired = false;
		// direction char is going in this cycle
		public int poimiX = 0, poimiY = 0;
		public int poisonDelay = -1;
		public Boolean poisonDmg = false;
		public int poisonTimer = -1;
		public Boolean premium = false, randomed = false;

		public int Privatechat = 0;

		public int rating = 1500, matchId = -1, matchLives = 2, loginReturn = 11,
				deathNum = 0, uid = -1, playerTicks = 100;

		public Boolean RebuildNPCList = false;
		public int reducedAttack = -1;
		public Boolean startDuel = false;
		public int StrPotion = 0;
		public int StrPrayer = 0;
		public int summonedNPCS = 0;
		public Boolean takeAsNote = false;
		public String teleLoc = "";
		public Boolean teleOtherScreen = false;
		public int teleportToX = -1, teleportToY = -1; // contain absolute x/y
													   // coordinates of
													   // destination we want to
													   // teleport to
		public int teleReq = 0;
		public Boolean TradeConfirmed = false;
		public int tradeId = -1, violations = 0;
		public Boolean updateRequired = true;
		// set to true if, in general, updating for this player is required
		// i.e. this should be set to true whenever any of the other
		// XXXUpdateRequired flags are set to true
		// Important: this does NOT include chatTextUpdateRequired!
		public Boolean UpdateShop = false;
		public int viewToX = -1;
		public int viewToY = -1;
		public int[] walkingQueueX = new int[walkingQueueSize],
				walkingQueueY = new int[walkingQueueSize];
		public Boolean WalkingTo = false;
		public Boolean WannePickUp = false;

		public int WanneTrade = 0;

		public int WanneTradeWith = 0;

		public Boolean winDuel = false;

		public int wQueueReadPtr = 0;

		// points to slot for reading from queue
		public int wQueueWritePtr = 0;

		public Player( int _playerId )
		{
			playerId = _playerId;
			// playerName = "player"+playerId;
			playerRights = 0; // player rights
			lastPacket = TimeHelper.CurrentTimeMillis();
			for ( int i = 0; i < playerItems.Length; i++ )
			{
				// Setting player items
				playerItems[i] = 0;
			}
			for ( int i = 0; i < playerItemsN.Length; i++ )
			{
				// Setting Item amounts
				playerItemsN[i] = 0;
			}

			for ( int i = 0; i < playerLevel.Length; i++ )
			{
				// Setting Levels
				if ( i == 3 )
				{
					playerLevel[i] = 10;
					playerXP[i] = 1155;
					currentHealth = 10;
					maxHealth = 10;
				}
				else
				{
					playerLevel[i] = 1;
					playerXP[i] = 0;
				}
			}

			for ( int i = 0; i < playerBankSize; i++ )
			{
				// Setting bank items
				bankItems[i] = 0;
			}

			for ( int i = 0; i < playerBankSize; i++ )
			{
				// Setting bank item amounts
				bankItemsN[i] = 0;
			}

			// Setting Welcomescreen information
			var now = DateTime.Now;
			int day = now.Day;
			int month = now.Month;
			int year = now.Year;
			int calc = ( ( year * 10000 ) + ( month * 100 ) + day );
			playerLastLogin = calc;
			playerLastConnect = "";
			playerIsMember = 1;
			playerMessages = 0;

			// Setting player standard look
			playerLook[0] = 2;
			playerLook[1] = 6;
			playerLook[2] = 7;
			playerLook[3] = 10;
			playerLook[4] = 5;
			playerLook[5] = 0;

			// initial x and y coordinates of the player
			heightLevel = 0;
			// the first call to updateThisPlayerMovement() will craft the proper
			// initialization packet
			teleportToX = 2635;//new spot(VIP)
			teleportToY = 3324;//new spot(VIP)


			// client initially doesn't know those values yet
			absX = absY = -1;
			mapRegionX = mapRegionY = -1;
			currentX = currentY = 0;
			resetWalkingQueue();
		}

		// some remarks: one map region is 8x8
		// a 7-bit (i.e. 128) value thus ranges over 16 such regions
		// the active area of 104x104 is comprised of 13x13 such regions, i.e. from
		// the center region that is 6 regions in each direction (notice the magical
		// 6
		// appearing also in map region arithmetics...)

		public void addNewNPC( NPC npc, stream str, stream updateBlock )
		{
			int id = npc.npcId;
			npcInListBitmap[id >> 3] |= ( Byte ) ( 1 << ( id & 7 ) ); // set the flag
			npcList[npcListSize++] = npc;

			str.writeBits( 14, id ); // client doesn't seem to like id=0

			int z = npc.absY - absY;
			if ( z < 0 )
				z += 32;
			str.writeBits( 5, z ); // y coordinate relative to thisPlayer
			z = npc.absX - absX;
			if ( z < 0 )
				z += 32;
			str.writeBits( 5, z ); // x coordinate relative to thisPlayer

			str.writeBits( 1, 0 ); // something??
			str.writeBits( 12, npc.npcType );

			Boolean savedUpdateRequired = npc.updateRequired;
			npc.updateRequired = true;
			npc.appendNPCUpdateBlock( updateBlock );
			npc.updateRequired = savedUpdateRequired;
			str.writeBits( 1, 1 ); // update required
		}

		public void addNewPlayer( Player plr, stream str, stream updateBlock )
		{
			int id = plr.playerId;
			playerInListBitmap[id >> 3] |= ( Byte ) ( 1 << ( id & 7 ) ); // set the flag
			playerList[playerListSize++] = plr;

			str.writeBits( 11, id ); // client doesn't seem to like id=0

			// TODO: properly implement the character appearance handling
			// send this every time for now and don't make use of the cached ones in
			// client
			str.writeBits( 1, 1 ); // set to true, if player definitions follow below
			Boolean savedFlag = plr.appearanceUpdateRequired;
			Boolean savedUpdateRequired = plr.updateRequired;
			plr.appearanceUpdateRequired = true;
			plr.updateRequired = true;
			plr.appendPlayerUpdateBlock( updateBlock );
			plr.appearanceUpdateRequired = savedFlag;
			plr.updateRequired = savedUpdateRequired;

			str.writeBits( 1, 1 ); // set to true, if we want to discard the
								   // (client side) walking queue
								   // no idea what this might be useful for yet
			int z = plr.absY - absY;
			if ( z < 0 )
				z += 32;
			str.writeBits( 5, z ); // y coordinate relative to thisPlayer
			z = plr.absX - absX;
			if ( z < 0 )
				z += 32;
			str.writeBits( 5, z ); // x coordinate relative to thisPlayer
		}
		public void gfx1000( int gfx )
		{

			animationRequest = gfx;
			mask100var2 = 6553600;
			mask100update = true;
			updateRequired = true;
		}
		public void arrowPullBack( int gfx )
		{
			mask100var1 = gfx;
			mask100var2 = 6553600;
			mask100update = true;
			updateRequired = true;
		}
		public void gfx100( int gfx )
		{
			mask100var1 = gfx;
			mask100var2 = 6553600;
			mask100update = true;
			updateRequired = true;
		}
		public void specGFX( int gfx )
		{
			mask100var1 = gfx;
			mask100var2 = 6553600;
			mask100update = true;
			updateRequired = true;
		}
		public void startAnimation( int animIdx )
		{
			animationRequest = animIdx;
			animationWaitCycles = 0;
		}
		public void appendAnimationRequest( stream str )
		{
			str.writeWordBigEndian( ( animationRequest == -1 ) ? 65535 : animationRequest );
			str.writeByteC( animationWaitCycles );
		}
		public void appendEmotionUpdate( stream str )
		{
			str.writeWordBigEndian( ( playerEmotionReq == -1 ) ? 65535 : playerEmotionReq );
			str.writeByteC( playerEmotionDelay );
		}

		public void addToWalkingQueue( int x, int y )
		{
			int next = ( wQueueWritePtr + 1 ) % walkingQueueSize;
			if ( next == wQueueWritePtr )
				return;
			// walking queue full, silently discard the data
			walkingQueueX[wQueueWritePtr] = x;
			walkingQueueY[wQueueWritePtr] = y;
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine( $"ADD TO WALK QUEUE: {x}x{y}" );
			Console.ForegroundColor = ConsoleColor.Gray;
			wQueueWritePtr = next;
		}

		public void appendDirUpdate( stream str )
		{
			if ( playerMD != -1 )
			{
				/*
				 * str.writeBits(2, 1); // updateType str.writeBits(3,
				 * misc.xlateDirectionToClient[playerMD]); if (updateRequired) {
				 * str.writeBits(1, 1); // tell client there's an update block appended
				 * at the end } else { str.writeBits(1, 0); }
				 */
				str.writeWord( playerMD );
				playerMD = -1;
			}
		}

		protected void appendHitUpdate( stream str )
		{
			try
			{
				str.writeByte( hitDiff ); // What the perseon got 'hit' for
				if ( ( hitDiff > 0 ) && healUpdateRequired )
				{
					str.writeByteA( 0 );
				}
				else if ( ( hitDiff > 0 ) && ( newhptype == false ) )
				{
					str.writeByteA( 1 ); // 0: red hitting - 1: blue hitting
				}
				else if ( ( hitDiff > 0 ) && ( newhptype == true ) )
				{
					str.writeByteA( hptype ); // 0: red hitting - 1: blue hitting
				}
				else
				{
					str.writeByteA( 0 ); // 0: red hitting - 1: blue hitting
				}
				if ( !healUpdateRequired )
				{

				}
				else
				{
					currentHealth = ( currentHealth + hitDiff );
					if ( currentHealth > maxHealth )
					{
						currentHealth = maxHealth;
					}
				}
				healUpdateRequired = false;
				str.writeByteC( currentHealth ); // Their current hp, for HP bar
				str.writeByte( getLevelForXP( playerXP[playerHitpoints] ) ); // Their max hp,

				inCombat = true;
				lastCombat = TimeHelper.CurrentTimeMillis();
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}
		public Boolean hit2;
		protected void appendHitUpdate2( stream str )
		{

			try
			{

				str.writeByte( hitDiff2 ); // What the perseon got 'hit' for
				if ( ( hitDiff2 > 0 ) && healUpdateRequired )
				{
					str.writeByteA( 0 );
				}
				else if ( ( hitDiff2 > 0 ) && ( newhptype == false ) )
				{
					str.writeByteA( 1 ); // 0: red hitting - 1: blue hitting
				}
				else if ( ( hitDiff2 > 0 ) && ( newhptype == true ) )
				{
					str.writeByteA( currentHealth ); // 0: red hitting - 1: blue hitting
				}
				else
				{
					str.writeByteA( hitDiff2 ); // 0: red hitting - 1: blue hitting
				}
				if ( !healUpdateRequired )
				{

				}
				else
				{
					currentHealth = ( currentHealth + hitDiff );
					if ( currentHealth > maxHealth )
					{
						currentHealth = maxHealth;
					}
				}
				healUpdateRequired = false;
				str.writeByteC( currentHealth ); // Their current hp, for HP bar
												 //str.writeByte(getLevelForXP(playerXP[playerHitpoints])); // Their max hp,

				inCombat = true;
				lastCombat = TimeHelper.CurrentTimeMillis();
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}
		public void appendMask100Update( stream str )
		{
			str.writeWordBigEndian( mask100var1 );
			str.writeDWord( mask100var2 );
		}

		public void appendMask1Update( stream str )
		{
			str.writeWordBigEndian( mask1var );
		}

		public void appendMask400Update( stream str )
		{
			// Xerozcheez: Something to do with direction
			str.writeByteA( m4001 );
			str.writeByteA( m4002 );
			str.writeByteA( m4003 );
			str.writeByteA( m4004 );
			str.writeWordA( m4005 );
			str.writeWordBigEndianA( m4006 );
			str.writeByteA( m4007 ); // direction
		}

		protected void appendPlayerAppearance( stream str )
		{
			playerProps.currentOffset = 0;

			// TODO: yet some things to figure out on this block + properly implement
			// this
			playerProps.writeByte( pGender );
			// player sex. 0=Male and 1=Female
			playerProps.writeByte( headIcon );
			// playerStatusMask - skull, prayers etc alkup 0

			// defining the character shape - 12 slots following - 0 denotes a null
			// entry and just a byte is used
			// slot 0,8,11,1 is head part - missing additional things are beard and
			// eyepatch like things either 11 or 1
			// cape, apron, amulet... the remaining things...

			if ( isNpc == false )
			{
				if ( playerEquipment[playerHat] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerHat] );
				}
				else
				{
					playerProps.writeByte( 0 );
				}
				if ( playerEquipment[playerCape] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerCape] );
				}
				else
				{
					playerProps.writeByte( 0 );
				}
				if ( playerEquipment[playerAmulet] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerAmulet] );
				}
				else
				{
					playerProps.writeByte( 0 );
				}
				if ( playerEquipment[playerWeapon] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerWeapon] );
				}
				else
				{
					playerProps.writeByte( 0 );
				}
				if ( playerEquipment[playerChest] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerChest] );
				}
				else
				{
					playerProps.writeWord( 0x100 + pTorso );
				}
				if ( playerEquipment[playerShield] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerShield] );
				}
				else
				{
					playerProps.writeByte( 0 );
				}
				if ( !Item.isPlate( playerEquipment[playerChest] ) )
				{
					playerProps.writeWord( 0x100 + pArms );
				}
				else
				{
					playerProps.writeByte( 0 );
				}
				if ( playerEquipment[playerLegs] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerLegs] );
				}
				else
				{
					playerProps.writeWord( 0x100 + pLegs );
				}
				if ( !Item.isFullHelm( playerEquipment[playerHat] )
						&& !Item.isFullMask( playerEquipment[playerHat] ) )
				{
					playerProps.writeWord( 0x100 + pHead ); // head
				}
				else
				{
					playerProps.writeByte( 0 );
				}
				if ( playerEquipment[playerHands] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerHands] );
				}
				else
				{
					playerProps.writeWord( 0x100 + pHands );
				}
				if ( playerEquipment[playerFeet] > 1 )
				{
					playerProps.writeWord( 0x200 + playerEquipment[playerFeet] );
				}
				else
				{
					playerProps.writeWord( 0x100 + pFeet );
				}
				if ( !Item.isFullHelm( playerEquipment[playerHat] )
						&& !Item.isFullMask( playerEquipment[playerHat] )
						&& ( pGender != 1 ) )
				{
					playerProps.writeWord( 0x100 + pBeard );
				}
				else
				{
					playerProps.writeByte( 0 ); // 0 = nothing on and girl don't have beard
												// so send 0. -bakatool
				}
			}
			else
			{
				playerProps.writeWord( -1 );
				playerProps.writeWord( npcId2 );

			}
			// array of 5 bytes defining the colors
			playerProps.writeByte( pHairC ); // hair color
			playerProps.writeByte( pTorsoC ); // torso color.
			playerProps.writeByte( pLegsC ); // leg color
			playerProps.writeByte( pFeetC ); // feet color
			playerProps.writeByte( pSkinC ); // skin color (0-6)

			playerProps.writeWord( pEmote ); // standAnimIndex
			playerProps.writeWord( 0x337 ); // standTurnAnimIndex
			playerProps.writeWord( playerSEW ); // walkAnimIndex
			playerProps.writeWord( 0x334 ); // turn180AnimIndex
			playerProps.writeWord( 0x335 ); // turn90CWAnimIndex
			playerProps.writeWord( 0x336 ); // turn90CCWAnimIndex
			playerProps.writeWord( playerSER ); // runAnimIndex

			playerProps.writeQWord( misc.playerNameToInt64( playerName ) );

			// Stat fix, combat decreases when your hp or any of these skills get
			// lowerd, this fixes that problem.
			/*
			 * int att = (int)((double)(getLevelForXP(playerXP[0])) * 0.325); int def =
			 * (int)((double)(getLevelForXP(playerXP[1])) * 0.25); int str =
			 * (int)((double)(getLevelForXP(playerXP[2])) * 0.325); int hit =
			 * (int)((double)(getLevelForXP(playerXP[3])) * 0.25); int mag =
			 * (int)((double)(getLevelForXP(playerXP[4])) * 0.4875); int pra =
			 * (int)((double)(getLevelForXP(playerXP[5])) * 0.125); int ran =
			 * (int)((double)(getLevelForXP(playerXP[6])) * 0.4875);
			 */

			/*
			 * int mag = (int)((double)(getLevelForXP(playerXP[4])) * 1.5); int ran =
			 * (int)((double)(getLevelForXP(playerXP[6])) * 1.5); int attstr =
			 * (int)((double)(getLevelForXP(playerXP[0])) +
			 * (double)(getLevelForXP(playerXP[2])));
			 * 
			 * int combatLevel = 0; if (ran > attstr) { combatLevel =
			 * (int)(((double)(getLevelForXP(playerXP[1])) * 0.25) +
			 * ((double)(getLevelForXP(playerXP[3])) * 0.25) +
			 * ((double)(getLevelForXP(playerXP[5])) * 0.125) +
			 * ((double)(getLevelForXP(playerXP[6])) * 0.4875)); } else if (mag >
			 * attstr) { combatLevel = (int)(((double)(getLevelForXP(playerXP[1])) *
			 * 0.25) + ((double)(getLevelForXP(playerXP[3])) * 0.25) +
			 * ((double)(getLevelForXP(playerXP[5])) * 0.125) +
			 * ((double)(getLevelForXP(playerXP[4])) * 0.4875)); } else { combatLevel =
			 * (int)(((double)(getLevelForXP(playerXP[1])) * 0.25) +
			 * ((double)(getLevelForXP(playerXP[3])) * 0.25) +
			 * ((double)(getLevelForXP(playerXP[5])) * 0.125) +
			 * ((double)(getLevelForXP(playerXP[0])) * 0.325) +
			 * ((double)(getLevelForXP(playerXP[2])) * 0.325)); }
			 * playerProps.writeByte(combatLevel); // combat level
			 * playerProps.writeWord(0); // incase != 0, writes skill-%d
			 * 
			 * str.writeByteC(playerProps.currentOffset); // size of player appearance
			 * block str.writeBytes(playerProps.buffer, playerProps.currentOffset, 0); }
			 */

			int mag = ( int ) ( ( getLevelForXP( playerXP[4] ) ) * 1.5 );
			int ran = ( int ) ( ( getLevelForXP( playerXP[6] ) ) * 1.5 );
			int attstr = ( int ) ( ( double ) ( getLevelForXP( playerXP[0] ) ) + ( double ) ( getLevelForXP( playerXP[2] ) ) );

			combatLevel = 0;
			if ( ran > attstr )
			{
				combatLevel = ( int ) ( ( ( getLevelForXP( playerXP[1] ) ) * 0.25 )
						+ ( ( getLevelForXP( playerXP[3] ) ) * 0.25 )
						+ ( ( getLevelForXP( playerXP[5] ) ) * 0.125 ) + ( ( getLevelForXP( playerXP[6] ) ) * 0.4875 ) );
			}
			else if ( mag > attstr )
			{
				combatLevel = ( int ) ( ( ( getLevelForXP( playerXP[1] ) ) * 0.25 )
						+ ( ( getLevelForXP( playerXP[3] ) ) * 0.25 )
						+ ( ( getLevelForXP( playerXP[5] ) ) * 0.125 ) + ( ( getLevelForXP( playerXP[4] ) ) * 0.4875 ) );
			}
			else
			{
				combatLevel = ( int ) ( ( ( getLevelForXP( playerXP[1] ) ) * 0.25 )
						+ ( ( getLevelForXP( playerXP[3] ) ) * 0.25 )
						+ ( ( getLevelForXP( playerXP[5] ) ) * 0.125 )
						+ ( ( getLevelForXP( playerXP[0] ) ) * 0.325 ) + ( ( getLevelForXP( playerXP[2] ) ) * 0.325 ) );
			}
			combat = combatLevel;
			playerProps.writeByte( combatLevel );
			// combat level
			playerProps.writeWord( 0 );
			// incase != 0, writes skill-%d
			str.writeByteC( playerProps.currentOffset );
			// size of player appearance block
			str.writeBytes( playerProps.buffer, playerProps.currentOffset, 0 );
		}

		protected void appendPlayerChatText( stream str )
		{
			str.writeWordBigEndian( ( ( chatTextColor & 0xFF ) << 8 )
					+ ( chatTextEffects & 0xFF ) );
			str.writeByte( playerRights );
			str.writeByteC( chatTextSize );
			// no more than 256 bytes!!!
			str.writeBytes_reverse( chatText, chatTextSize, 0 );
		}

		public void appendPlayerUpdateBlock( stream str )
		{
			if ( !updateRequired && !chatTextUpdateRequired )
				return;
			int updateMask = 0;
			if ( mask400update )
				updateMask |= 0x400;
			if ( mask100update )
				updateMask |= 0x100;
			if ( animationRequest != -1 )
				updateMask |= 8;
			if ( chatTextUpdateRequired )
				updateMask |= 0x80;
			if ( faceNPCupdate )
				updateMask |= 1;
			if ( appearanceUpdateRequired )
				updateMask |= 0x10;
			if ( FocusPointX != -1 )
				updateMask |= 2;
			if ( hitUpdateRequired )
				updateMask |= 0x20;
			if ( hitUpdateRequired2 )
				updateMask |= 0x200;
			if ( dirUpdateRequired )
				updateMask |= 0x40;
			if ( dirUpdateRequired )
				updateMask |= 0x400;
			if ( plrTextUpdateRequired )
				updateMask |= 4;
			if ( updateMask >= 0x100 )
			{
				// byte isn't sufficient
				updateMask |= 0x40;
				// indication for the client that updateMask is stored in a word
				str.writeByte( updateMask & 0xFF );
				str.writeByte( updateMask >> 8 );
			}
			else
				str.writeByte( updateMask );
			if ( mask400update )
				appendMask400Update( str ); // Xerozcheez: Very
											// interesting update mask!
			if ( mask100update )
				appendMask100Update( str ); // Xerozcheez: Graphics on
											// player update mask
			if ( plrTextUpdateRequired )
				appendString4( str );
			if ( animationRequest != -1 )
				appendAnimationRequest( str );
			if ( chatTextUpdateRequired )
				appendPlayerChatText( str );
			if ( faceNPCupdate )
				appendFaceNPCUpdate( str );
			if ( appearanceUpdateRequired )
				appendPlayerAppearance( str );
			if ( FocusPointX != -1 )
				appendSetFocusDestination( str );
			if ( hitUpdateRequired )
				appendHitUpdate( str );
			if ( hitUpdateRequired2 ) appendHitUpdate( str );
			if ( dirUpdateRequired )
				appendDirUpdate( str );

			// TODO: add the various other update blocks

		}

		private void appendSetFocusDestination( stream str )
		{
			str.writeWordBigEndianA( FocusPointX );
			str.writeWordBigEndian( FocusPointY );
		}

		public void appendString4( stream str )
		{
			// Xerozcheez: Interesting mask, looks like to do with chat
			str.writeString( plrText );
		}

		public void applyPoisonToMe( ) // by bakatool.
		{
			poisonDmg = true;
			poisonDelay = misc.random( 60 );
			hitDiff = misc.random( 5 );
			updateRequired = true;
			hitUpdateRequired = true;
		}

		public void clearUpdateFlags( )
		{
			FocusPointX = FocusPointY = -1;
			updateRequired = false;
			animationRequest = -1;
			plrTextUpdateRequired = false;
			faceNPCupdate = false;
			faceNPC = 65535;
			chatTextUpdateRequired = false;
			appearanceUpdateRequired = false;
			hitUpdateRequired = false;
			hitUpdateRequired2 = false;
			mask100update = false;
			IsStair = false;
		}
		public void setFaceNPC( int index )
		{
			faceNPC = index;
			faceNPCupdate = true;
			updateRequired = true;
		}
		public Boolean faceNPCupdate = false;
		public int faceNPC = -1;
		public void appendFaceNPCUpdate( stream str )
		{
			str.writeWordBigEndian( faceNPC );
		}
		public void dealDamage( int amt )
		{
			( ( client ) this ).Debug( "Dealing " + amt + " damage to you (hp="
					+ currentHealth + ")" );
			currentHealth -= amt;
			if ( currentHealth <= 0 )
			{
				( ( client ) this ).Debug( "Triggering death timer" );
				currentHealth = 0;
				deathStage = 1;
				deathTimer = TimeHelper.CurrentTimeMillis();
			}
			else
			{
				/*
				 * if(amt == 0){ if (playerEquipment[playerShield] == -1){ pEmote = 404; }
				 * else { pEmote = 403; } }
				 */
			}
		}

		public void destruct( )
		{
			playerListSize = 0;
			for ( int i = 0; i < maxPlayerListSize; i++ )
				playerList[i] = null;
			npcListSize = 0;
			for ( int i = 0; i < maxNPCListSize; i++ )
				npcList[i] = null;

			absX = absY = -1;
			mapRegionX = mapRegionY = -1;
			currentX = currentY = 0;
			resetWalkingQueue();
		}

		public int getLevelForXP( int exp )
		{
			int points = 0;
			int output = 0;

			for ( int lvl = 1; lvl <= 135; lvl++ )
			{
				points += ( Int32 ) Math.Floor( lvl + 300.0 * Math.Pow( 2.0, lvl / 7.0 ) );
				output = ( int ) Math.Floor( points / 4d );
				if ( output >= exp )
					return lvl;
			}
			return 0;
		}

		public void getNextPlayerMovement( )
		{
			mapRegionDidChange = false;
			didTeleport = false;
			dir1 = dir2 = -1;

			if ( ( teleportToX != -1 ) && ( teleportToY != -1 ) )
			{
				mapRegionDidChange = true;
				if ( ( mapRegionX != -1 ) && ( mapRegionY != -1 ) )
				{
					// check, whether destination is within current map region
					int relX = teleportToX - mapRegionX * 8, relY = teleportToY
							- mapRegionY * 8;
					if ( ( relX >= 2 * 8 ) && ( relX < 11 * 8 ) && ( relY >= 2 * 8 )
							&& ( relY < 11 * 8 ) )
						mapRegionDidChange = false;
				}
				if ( mapRegionDidChange )
				{
					// after map region change the relative coordinates range between 48 -
					// 55
					mapRegionX = ( teleportToX >> 3 ) - 6;
					mapRegionY = ( teleportToY >> 3 ) - 6;

					// playerListSize = 0; // completely rebuild playerList after teleport
					// AND map region change
				}

				currentX = teleportToX - 8 * mapRegionX;
				currentY = teleportToY - 8 * mapRegionY;
				absX = teleportToX;
				absY = teleportToY;
				resetWalkingQueue();

				teleportToX = teleportToY = -1;
				didTeleport = true;
			}
			else
			{
				dir1 = getNextWalkingDirection();
				if ( dir1 == -1 )
					return;
				// standing

				if ( isRunning )
				{
					dir2 = getNextWalkingDirection();
				}

				// check, if we're required to change the map region
				int deltaX = 0, deltaY = 0;
				if ( currentX < 2 * 8 )
				{
					deltaX = 4 * 8;
					mapRegionX -= 4;
					mapRegionDidChange = true;
				}
				else if ( currentX >= 11 * 8 )
				{
					deltaX = -4 * 8;
					mapRegionX += 4;
					mapRegionDidChange = true;
				}
				if ( currentY < 2 * 8 )
				{
					deltaY = 4 * 8;
					mapRegionY -= 4;
					mapRegionDidChange = true;
				}
				else if ( currentY >= 11 * 8 )
				{
					deltaY = -4 * 8;
					mapRegionY += 4;
					mapRegionDidChange = true;
				}

				if ( mapRegionDidChange )
				{
					// have to adjust all relative coordinates
					currentX += deltaX;
					currentY += deltaY;
					for ( int i = 0; i < walkingQueueSize; i++ )
					{
						walkingQueueX[i] += deltaX;
						walkingQueueY[i] += deltaY;
					}
				}

			}
		}

		// returns 0-7 for next walking direction or -1, if we're not moving
		public int getNextWalkingDirection( )
		{
			if ( wQueueReadPtr == wQueueWritePtr )
				return -1;
			// walking queue empty
			int dir;
			do
			{
				dir = misc.direction( currentX, currentY,
						walkingQueueX[wQueueReadPtr], walkingQueueY[wQueueReadPtr] );
				if ( dir == -1 )
					wQueueReadPtr = ( wQueueReadPtr + 1 ) % walkingQueueSize;
				else if ( ( dir & 1 ) != 0 )
				{
					println_debug( "Invalid waypoint in walking queue!" );
					resetWalkingQueue();
					return -1;
				}
			} while ( ( dir == -1 ) && ( wQueueReadPtr != wQueueWritePtr ) );
			if ( dir == -1 )
				return -1;
			dir >>= 1;
			currentX += misc.directionDeltaX[dir];
			currentY += misc.directionDeltaY[dir];
			absX += misc.directionDeltaX[dir];
			absY += misc.directionDeltaY[dir];
			return dir;
		}

		public abstract void initialize( );

		// PM Stuff
		public abstract Boolean isinpm( long l );

		public void kick( )
		{
			isKicked = true;
		}

		public abstract void loadpm( long l, int world );

		public abstract void pmupdate( int pmid, int world );

		public void postProcessing( )
		{
			if ( newWalkCmdSteps > 0 )
			{
				/*
				 * int OldcurrentX = currentX; int OldcurrentY = currentY; for(i = 0; i <
				 * playerFollow.Length; i++) { if (playerFollow[i] != -1 && following ==
				 * true) { PlayerHandler.players[playerFollow[i]].newWalkCmdSteps =
				 * (newWalkCmdSteps + 1); for(int j = 0; j < newWalkCmdSteps; j++) {
				 * PlayerHandler.players[playerFollow[i]].newWalkCmdX[(j + 1)] =
				 * newWalkCmdX[j]; PlayerHandler.players[playerFollow[i]].newWalkCmdY[(j +
				 * 1)] = newWalkCmdY[j]; }
				 * PlayerHandler.players[playerFollow[i]].newWalkCmdX[0] = OldcurrentX;
				 * PlayerHandler.players[playerFollow[i]].newWalkCmdY[0] = OldcurrentY;
				 * PlayerHandler.players[playerFollow[i]].poimiX = OldcurrentX;
				 * PlayerHandler.players[playerFollow[i]].poimiY = OldcurrentY; } }
				 */

				// place this into walking queue
				// care must be taken and we can't just append this because usually the
				// starting point (clientside) of
				// this packet and the current position (serverside) do not coincide.
				// Therefore we might have to go
				// back in time in order to find a proper connecting vertex. This is
				// also the origin of the character
				// walking back and forth when there's noticeable lag and we keep on
				// seeding walk commands.
				int firstX = newWalkCmdX[0], firstY = newWalkCmdY[0]; // the point we need
																	  // to connect to
																	  // from our current
																	  // position...

				// travel backwards to find a proper connection vertex
				int lastDir = 0;
				Boolean found = false;
				numTravelBackSteps = 0;
				int ptr = wQueueReadPtr;
				int dir = misc.direction( currentX, currentY, firstX, firstY );
				if ( ( dir != -1 ) && ( ( dir & 1 ) != 0 ) )
				{
					// we can't connect first and current directly
					do
					{
						lastDir = dir;
						if ( --ptr < 0 )
							ptr = walkingQueueSize - 1;

						travelBackX[numTravelBackSteps] = ( SByte ) walkingQueueX[ptr];
						travelBackY[numTravelBackSteps++] = ( SByte ) walkingQueueY[ptr];
						dir = misc.direction( walkingQueueX[ptr],
								walkingQueueY[ptr], firstX, firstY );
						if ( lastDir != dir )
						{
							found = true;
							break;
							// either of those two, or a vertex between those is a candidate
						}

					} while ( ptr != wQueueWritePtr );
				}
				else
					found = true; // we didn't need to go back in time because the
								  // current position
								  // already can be connected to first

				if ( !found )
				{
					println_debug( "Fatal: couldn't find connection vertex! Dropping packet." );
					client temp = ( client ) this;
					temp.savegame( true );
					disconnected = true;
				}
				else
				{
					wQueueWritePtr = wQueueReadPtr;
					// discard any yet unprocessed waypoints from queue
					Console.WriteLine( "Initial addToWalkingQueue" );
					addToWalkingQueue( currentX, currentY ); // have to add this in order to
															 // keep consistency in the queue

					if ( ( dir != -1 ) && ( ( dir & 1 ) != 0 ) )
					{
						// need to place an additional waypoint which lies between
						// walkingQueue[numTravelBackSteps-2] and
						// walkingQueue[numTravelBackSteps-1] but can be connected to
						// firstX/firstY

						for ( int i = 0; i < numTravelBackSteps - 1; i++ )
						{
							Console.WriteLine( $"addToWalkingQueue: {i}" );
							addToWalkingQueue( travelBackX[i], travelBackY[i] );
						}
						int wayPointX2 = travelBackX[numTravelBackSteps - 1], wayPointY2 = travelBackY[numTravelBackSteps - 1];
						int wayPointX1, wayPointY1;
						if ( numTravelBackSteps == 1 )
						{
							wayPointX1 = currentX;
							wayPointY1 = currentY;
						}
						else
						{
							wayPointX1 = travelBackX[numTravelBackSteps - 2];
							wayPointY1 = travelBackY[numTravelBackSteps - 2];
						}
						// we're coming from wayPoint1, want to go in direction wayPoint2
						// but only so far that
						// we get a connection to first

						// the easiest, but somewhat ugly way:
						// maybe there is a better way, but it involves shitload of
						// different
						// cases so it seems like it isn't anymore
						dir = misc.direction( wayPointX1, wayPointY1, wayPointX2,
								wayPointY2 );
						if ( ( dir == -1 ) || ( ( dir & 1 ) != 0 ) )
						{
							println_debug( "Fatal: The walking queue is corrupt! wp1=("
									+ wayPointX1
									+ ", "
									+ wayPointY1
									+ "), "
									+ "wp2=("
									+ wayPointX2
									+ ", "
									+ wayPointY2
									+ ")" );
						}
						else
						{
							dir >>= 1;
							found = false;
							int x = wayPointX1, y = wayPointY1;
							while ( ( x != wayPointX2 ) || ( y != wayPointY2 ) )
							{
								x += misc.directionDeltaX[dir];
								y += misc.directionDeltaY[dir];
								if ( ( misc.direction( x, y, firstX, firstY ) & 1 ) == 0 )
								{
									found = true;
									break;
								}
							}
							if ( !found )
							{
								println_debug( "Fatal: Internal error: unable to determine connection vertex!"
										+ "  wp1=("
										+ wayPointX1
										+ ", "
										+ wayPointY1
										+ "), wp2=("
										+ wayPointX2
										+ ", "
										+ wayPointY2
										+ "), "
										+ "first=("
										+ firstX + ", " + firstY + ")" );
							}
							else
							{

								Console.WriteLine( $"addToWalkingQueue: found == true" );
								addToWalkingQueue( wayPointX1, wayPointY1 );
							}
						}
					}
					else
					{
						for ( int i = 0; i < numTravelBackSteps; i++ )
						{
							Console.WriteLine( $"addToWalkingQueue: numTravelBackSteps {i}" );
							addToWalkingQueue( travelBackX[i], travelBackY[i] );
						}
					}

					// now we can finally add those waypoints because we made sure about the
					// connection to first
					for ( int i = 0; i < newWalkCmdSteps; i++ )
					{
						Console.WriteLine( $"addToWalkingQueue: newWalkCmdSteps {i}" );
						addToWalkingQueue( newWalkCmdX[i], newWalkCmdY[i] );
					}

				}
				isRunning = newWalkCmdIsRunning || isRunning2;
				/*
				 * for(i = 0; i < playerFollow.Length; i++) { if (playerFollow[i] != -1) {
				 * PlayerHandler.players[playerFollow[i]].postProcessing(); } }
				 */
			}
		}

		public void preProcessing( )
		{
			newWalkCmdSteps = 0;
		}

		public void println( String str )
		{
			Console.WriteLine( "[player-" + playerId + "]: " + str );
		}

		public void println_debug( String str )
		{
			Console.WriteLine( "[player-" + playerId + "]: " + str );
		}

		// is being called regularily every 500ms - do any automatic player actions
		// herein
		public abstract Boolean process( );

		public void removeequipped( )
		{
			dropsitem = true;
		}

		public void resetWalkingQueue( )
		{
			wQueueReadPtr = wQueueWritePtr = 0;
			// properly initialize this to make the "travel back" algorithm work
			for ( int i = 0; i < walkingQueueSize; i++ )
			{
				walkingQueueX[i] = currentX;
				walkingQueueY[i] = currentY;
			}
		}

		public abstract void sendpm( long name, int rights, byte[] chatmessage,
				int messagesize );

		public void TurnPlayerTo( int pointX, int pointY )
		{
			FocusPointX = 2 * pointX + 1;
			FocusPointY = 2 * pointY + 1;
		}

		public abstract void update( );

		// handles anything related to character position basically walking, running
		// and standing
		// applies to only to "non-thisPlayer" characters
		public void updatePlayerMovement( stream str )
		{
			if ( dir1 == -1 )
			{
				// don't have to update the character position, because the char is just
				// standing
				if ( updateRequired || chatTextUpdateRequired )
				{
					// tell client there's an update block appended at the end
					str.writeBits( 1, 1 );
					str.writeBits( 2, 0 );
				}
				else
					str.writeBits( 1, 0 );
			}
			else if ( dir2 == -1 )
			{
				// send "walking packet"
				str.writeBits( 1, 1 );
				str.writeBits( 2, 1 );
				str.writeBits( 3, misc.xlateDirectionToClient[dir1] );
				str
						.writeBits( 1,
								( updateRequired || chatTextUpdateRequired ) ? 1 : 0 );
			}
			else
			{
				// send "running packet"
				str.writeBits( 1, 1 );
				str.writeBits( 2, 2 );
				str.writeBits( 3, misc.xlateDirectionToClient[dir1] );
				str.writeBits( 3, misc.xlateDirectionToClient[dir2] );
				str
						.writeBits( 1,
								( updateRequired || chatTextUpdateRequired ) ? 1 : 0 );
			}
		}

		// handles anything related to character position, i.e. walking,running and
		// teleportation
		// applies only to the char and the client which is playing it
		public void updateThisPlayerMovement( stream str )
		{
			if ( mapRegionDidChange )
			{
				str.createFrame( 73 );
				str.writeWordA( mapRegionX + 6 ); // for some reason the client substracts 6
												  // from those values
				str.writeWord( mapRegionY + 6 );
			}

			if ( didTeleport == true )
			{
				str.createFrameVarSizeWord( 81 );
				str.initBitAccess();
				str.writeBits( 1, 1 );
				str.writeBits( 2, 3 );
				// updateType
				str.writeBits( 2, heightLevel );
				str.writeBits( 1, 1 );
				// set to true, if discarding (clientside) walking queue
				str.writeBits( 1, ( updateRequired ) ? 1 : 0 );
				str.writeBits( 7, currentY );
				str.writeBits( 7, currentX );
				return;
			}

			if ( dir1 == -1 )
			{
				// don't have to update the character position, because we're just
				// standing
				str.createFrameVarSizeWord( 81 );
				str.initBitAccess();
				if ( updateRequired )
				{
					// tell client there's an update block appended at the end
					str.writeBits( 1, 1 );
					str.writeBits( 2, 0 );
				}
				else
				{
					str.writeBits( 1, 0 );
				}
				if ( DirectionCount < 50 )
				{
					DirectionCount++;
				}
			}
			else
			{
				DirectionCount = 0;
				str.createFrameVarSizeWord( 81 );
				str.initBitAccess();
				str.writeBits( 1, 1 );

				if ( dir2 == -1 )
				{
					// send "walking packet"
					str.writeBits( 2, 1 );
					// updateType
					str.writeBits( 3, misc.xlateDirectionToClient[dir1] );
					if ( updateRequired )
						str.writeBits( 1, 1 );
					// tell client there's an update block appended at the end
					else
						str.writeBits( 1, 0 );
				}
				else
				{
					// send "running packet"
					str.writeBits( 2, 2 );
					// updateType
					str.writeBits( 3, misc.xlateDirectionToClient[dir1] );
					str.writeBits( 3, misc.xlateDirectionToClient[dir2] );
					if ( updateRequired )
						str.writeBits( 1, 1 );
					// tell client there's an update block appended at the end
					else
						str.writeBits( 1, 0 );
					if ( playerEnergy > 0 )
					{
						playerEnergy -= 1;
					}
					else
					{
						isRunning2 = false;
					}
				}
			}

		}

		public Boolean withinDistance( NPC npc )
		{
			if ( heightLevel != npc.heightLevel )
				return false;
			if ( npc.NeedRespawn == true )
				return false;
			int deltaX = npc.absX - absX, deltaY = npc.absY - absY;
			return ( deltaX <= 15 ) && ( deltaX >= -16 ) && ( deltaY <= 15 )
					&& ( deltaY >= -16 );
		}

		// supported within the packet adding new players are coordinates relative
		// to thisPlayer
		// that are >= -16 and <= 15 (i.e. a signed 5-bit number)
		public Boolean withinDistance( Player otherPlr )
		{
			if ( heightLevel != otherPlr.heightLevel )
				return false;
			int deltaX = otherPlr.absX - absX, deltaY = otherPlr.absY - absY;
			return ( deltaX <= 15 ) && ( deltaX >= -16 ) && ( deltaY <= 15 )
					&& ( deltaY >= -16 );
		}
	}

}
