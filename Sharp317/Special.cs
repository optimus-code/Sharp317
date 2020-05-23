﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class special
	{
		public void loginSpec( client c, int item )
		{
			switch ( item )
			{
				case 1215:
				case 1231:
				case 1249:
				case 1263:
				case 1305:
				case 1377:
				case 1434:
				case 3204:
				case 4587:
				case 5680:
				case 5698:
				case 5716:
				case 5730:
				case 6739:
				case 7158:
				case 859:
				case 861:
				case 4827:
				case 35:
				case 667:
				case 746:
				case 2402:
				case 8100:
				case 4151:
				case 4153:
					c.l33thax( 12323 );
					c.l33thax( 7574 );
					c.l33thax( 7599 );
					c.l33thax( 7549 );
					c.l33thax( 8493 );
					c.l33thax( 7499 );
					break;

				default:
					c.removeSpec( 12323 );
					c.removeSpec( 7574 );
					c.removeSpec( 7599 );
					c.removeSpec( 7549 );
					c.removeSpec( 8493 );
					c.removeSpec( 7499 );
					break;
			}
		}

		public void needSpecial( client c, int item, int target )
		{
			if ( target != 3 )
			{
				return;
			}
			switch ( item )
			{
				case 1215:
				case 1231:
				case 1249:
				case 1263:
				case 1305:
				case 1377:
				case 1434:
				case 3204:
				case 4587:
				case 5680:
				case 5698:
				case 5716:
				case 5730:
				case 6739:
				case 7158:
				case 859:
				case 861:
				case 4827:
				case 35:
				case 667:
				case 746:
				case 2402:
				case 8100:
				case 4151:
				case 4153:
					c.l33thax( 12323 );
					c.l33thax( 7574 );
					c.l33thax( 7599 );
					c.l33thax( 7549 );
					c.l33thax( 8493 );
					c.l33thax( 7499 );
					break;

				default:
					c.removeSpec( 12323 );
					c.removeSpec( 7574 );
					c.removeSpec( 7599 );
					c.removeSpec( 7549 );
					c.removeSpec( 8493 );
					c.removeSpec( 7499 );
					break;
			}
		}
		public void specialAttacks( int ID )
		{
			client c = ( client ) PlayerHandler.players[ID];
			if ( c.specialAmount >= 0 && c.specialAmount <= 24 )
			{
				c.sendFrame126( "S P E C I A L  A T T A C K", 12335 );
				c.fsBar( 0, 0, 12325 );
				c.fsBar( 0, 0, 12326 );
				c.fsBar( 0, 0, 12327 );
				c.fsBar( 0, 0, 12328 );
				c.fsBar( 0, 0, 12329 );
				c.fsBar( 0, 0, 12330 );
				c.fsBar( 0, 0, 12331 );
				c.fsBar( 0, 0, 12332 );
				c.fsBar( 0, 0, 12333 );
				c.fsBar( 0, 0, 12334 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E @bla@C I A L  A T T A C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 0, 0, 12328 );
				c.fsBar( 0, 0, 12329 );
				c.fsBar( 0, 0, 12330 );
				c.fsBar( 0, 0, 12331 );
				c.fsBar( 0, 0, 12332 );
				c.fsBar( 0, 0, 12333 );
				c.fsBar( 0, 0, 12334 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L@bla@  A T T A C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 500, 0, 12328 );
				c.fsBar( 500, 0, 12329 );
				c.fsBar( 0, 0, 12330 );
				c.fsBar( 0, 0, 12331 );
				c.fsBar( 0, 0, 12332 );
				c.fsBar( 0, 0, 12333 );
				c.fsBar( 0, 0, 12334 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A @bla@C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 500, 0, 12328 );
				c.fsBar( 500, 0, 12329 );
				c.fsBar( 500, 0, 12330 );
				c.fsBar( 500, 0, 12331 );
				c.fsBar( 500, 0, 12332 );
				c.fsBar( 0, 0, 12333 );
				c.fsBar( 0, 0, 12334 );
			}
			if ( c.specialAmount >= 100 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 500, 0, 12328 );
				c.fsBar( 500, 0, 12329 );
				c.fsBar( 500, 0, 12330 );
				c.fsBar( 500, 0, 12331 );
				c.fsBar( 500, 0, 12332 );
				c.fsBar( 500, 0, 12333 );
				c.fsBar( 500, 0, 12334 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E @bla@C I A L  A T T A C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 0, 0, 12328 );
				c.fsBar( 0, 0, 12329 );
				c.fsBar( 0, 0, 12330 );
				c.fsBar( 0, 0, 12331 );
				c.fsBar( 0, 0, 12332 );
				c.fsBar( 0, 0, 12333 );
				c.fsBar( 0, 0, 12334 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L@bla@  A T T A C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 500, 0, 12328 );
				c.fsBar( 500, 0, 12329 );
				c.fsBar( 0, 0, 12330 );
				c.fsBar( 0, 0, 12331 );
				c.fsBar( 0, 0, 12332 );
				c.fsBar( 0, 0, 12333 );
				c.fsBar( 0, 0, 12334 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A @bla@C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 500, 0, 12328 );
				c.fsBar( 500, 0, 12329 );
				c.fsBar( 500, 0, 12330 );
				c.fsBar( 500, 0, 12331 );
				c.fsBar( 500, 0, 12332 );
				c.fsBar( 0, 0, 12333 );
				c.fsBar( 0, 0, 12334 );
			}
			if ( c.specialAmount >= 100 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A C K", 12335 );
				c.fsBar( 500, 0, 12325 );
				c.fsBar( 500, 0, 12326 );
				c.fsBar( 500, 0, 12327 );
				c.fsBar( 500, 0, 12328 );
				c.fsBar( 500, 0, 12329 );
				c.fsBar( 500, 0, 12330 );
				c.fsBar( 500, 0, 12331 );
				c.fsBar( 500, 0, 12332 );
				c.fsBar( 500, 0, 12333 );
				c.fsBar( 500, 0, 12334 );
			}
		}

		public void specialAttacks2( int ID )
		{
			client c = ( client ) PlayerHandler.players[ID];
			if ( c.specialAmount >= 0 && c.specialAmount <= 24 )
			{
				c.sendFrame126( "S P E C I A L  A T T A C K", 7586 );
				c.fsBar( 0, 0, 7576 );
				c.fsBar( 0, 0, 7577 );
				c.fsBar( 0, 0, 7578 );
				c.fsBar( 0, 0, 7579 );
				c.fsBar( 0, 0, 7580 );
				c.fsBar( 0, 0, 7581 );
				c.fsBar( 0, 0, 7582 );
				c.fsBar( 0, 0, 7583 );
				c.fsBar( 0, 0, 7584 );
				c.fsBar( 0, 0, 7585 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E @bla@C I A L  A T T A C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 0, 0, 7579 );
				c.fsBar( 0, 0, 7580 );
				c.fsBar( 0, 0, 7581 );
				c.fsBar( 0, 0, 7582 );
				c.fsBar( 0, 0, 7583 );
				c.fsBar( 0, 0, 7584 );
				c.fsBar( 0, 0, 7585 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L@bla@  A T T A C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 500, 0, 7579 );
				c.fsBar( 500, 0, 7580 );
				c.fsBar( 0, 0, 7581 );
				c.fsBar( 0, 0, 7582 );
				c.fsBar( 0, 0, 7583 );
				c.fsBar( 0, 0, 7584 );
				c.fsBar( 0, 0, 7585 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A @bla@C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 500, 0, 7579 );
				c.fsBar( 500, 0, 7580 );
				c.fsBar( 500, 0, 7581 );
				c.fsBar( 500, 0, 7582 );
				c.fsBar( 500, 0, 7583 );
				c.fsBar( 0, 0, 7584 );
				c.fsBar( 0, 0, 7585 );
			}
			if ( c.specialAmount >= 100 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 500, 0, 7579 );
				c.fsBar( 500, 0, 7580 );
				c.fsBar( 500, 0, 7581 );
				c.fsBar( 500, 0, 7582 );
				c.fsBar( 500, 0, 7583 );
				c.fsBar( 500, 0, 7584 );
				c.fsBar( 500, 0, 7585 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E @bla@C I A L  A T T A C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 0, 0, 7579 );
				c.fsBar( 0, 0, 7580 );
				c.fsBar( 0, 0, 7581 );
				c.fsBar( 0, 0, 7582 );
				c.fsBar( 0, 0, 7583 );
				c.fsBar( 0, 0, 7584 );
				c.fsBar( 0, 0, 7585 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L@bla@  A T T A C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 500, 0, 7579 );
				c.fsBar( 500, 0, 7580 );
				c.fsBar( 0, 0, 7581 );
				c.fsBar( 0, 0, 7582 );
				c.fsBar( 0, 0, 7583 );
				c.fsBar( 0, 0, 7584 );
				c.fsBar( 0, 0, 7585 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A @bla@C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 500, 0, 7579 );
				c.fsBar( 500, 0, 7580 );
				c.fsBar( 500, 0, 7581 );
				c.fsBar( 500, 0, 7582 );
				c.fsBar( 500, 0, 7583 );
				c.fsBar( 0, 0, 7584 );
				c.fsBar( 0, 0, 7585 );
			}
			if ( c.specialAmount >= 100 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A C K", 7586 );
				c.fsBar( 500, 0, 7576 );
				c.fsBar( 500, 0, 7577 );
				c.fsBar( 500, 0, 7578 );
				c.fsBar( 500, 0, 7579 );
				c.fsBar( 500, 0, 7580 );
				c.fsBar( 500, 0, 7581 );
				c.fsBar( 500, 0, 7582 );
				c.fsBar( 500, 0, 7583 );
				c.fsBar( 500, 0, 7584 );
				c.fsBar( 500, 0, 7585 );
			}
		}

		public void specialAttacks3( int ID )
		{
			client c = ( client ) PlayerHandler.players[ID];
			if ( c.specialAmount >= 0 && c.specialAmount <= 24 )
			{
				c.sendFrame126( "S P E C I A L  A T T A C K", 7611 );
				c.fsBar( 0, 0, 7601 );
				c.fsBar( 0, 0, 7602 );
				c.fsBar( 0, 0, 7603 );
				c.fsBar( 0, 0, 7604 );
				c.fsBar( 0, 0, 7605 );
				c.fsBar( 0, 0, 7606 );
				c.fsBar( 0, 0, 7607 );
				c.fsBar( 0, 0, 7608 );
				c.fsBar( 0, 0, 7609 );
				c.fsBar( 0, 0, 7610 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E @bla@C I A L  A T T A C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 0, 0, 7604 );
				c.fsBar( 0, 0, 7605 );
				c.fsBar( 0, 0, 7606 );
				c.fsBar( 0, 0, 7607 );
				c.fsBar( 0, 0, 7608 );
				c.fsBar( 0, 0, 7609 );
				c.fsBar( 0, 0, 7610 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L@bla@  A T T A C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 500, 0, 7604 );
				c.fsBar( 500, 0, 7605 );
				c.fsBar( 0, 0, 7606 );
				c.fsBar( 0, 0, 7607 );
				c.fsBar( 0, 0, 7608 );
				c.fsBar( 0, 0, 7609 );
				c.fsBar( 0, 0, 7610 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A @bla@C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 500, 0, 7604 );
				c.fsBar( 500, 0, 7605 );
				c.fsBar( 500, 0, 7606 );
				c.fsBar( 500, 0, 7607 );
				c.fsBar( 500, 0, 7608 );
				c.fsBar( 0, 0, 7609 );
				c.fsBar( 0, 0, 7610 );
			}
			if ( c.specialAmount >= 100 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 500, 0, 7604 );
				c.fsBar( 500, 0, 7605 );
				c.fsBar( 500, 0, 7606 );
				c.fsBar( 500, 0, 7607 );
				c.fsBar( 500, 0, 7608 );
				c.fsBar( 500, 0, 7609 );
				c.fsBar( 500, 0, 7610 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E @bla@C I A L  A T T A C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 0, 0, 7604 );
				c.fsBar( 0, 0, 7605 );
				c.fsBar( 0, 0, 7606 );
				c.fsBar( 0, 0, 7607 );
				c.fsBar( 0, 0, 7608 );
				c.fsBar( 0, 0, 7609 );
				c.fsBar( 0, 0, 7610 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L@bla@  A T T A C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 500, 0, 7604 );
				c.fsBar( 500, 0, 7605 );
				c.fsBar( 0, 0, 7606 );
				c.fsBar( 0, 0, 7607 );
				c.fsBar( 0, 0, 7608 );
				c.fsBar( 0, 0, 7609 );
				c.fsBar( 0, 0, 7610 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A @bla@C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 500, 0, 7604 );
				c.fsBar( 500, 0, 7605 );
				c.fsBar( 500, 0, 7606 );
				c.fsBar( 500, 0, 7607 );
				c.fsBar( 500, 0, 7608 );
				c.fsBar( 0, 0, 7609 );
				c.fsBar( 0, 0, 7610 );
			}
			if ( c.specialAmount >= 100 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A C K", 7611 );
				c.fsBar( 500, 0, 7601 );
				c.fsBar( 500, 0, 7602 );
				c.fsBar( 500, 0, 7603 );
				c.fsBar( 500, 0, 7604 );
				c.fsBar( 500, 0, 7605 );
				c.fsBar( 500, 0, 7606 );
				c.fsBar( 500, 0, 7607 );
				c.fsBar( 500, 0, 7608 );
				c.fsBar( 500, 0, 7609 );
				c.fsBar( 500, 0, 7610 );
			}
		}

		public void specialAttacks4( int ID )
		{
			client c = ( client ) PlayerHandler.players[ID];
			if ( c.specialAmount >= 0 && c.specialAmount <= 24 )
			{
				c.sendFrame126( "S P E C I A L  A T T A C K", 7561 );
				c.fsBar( 0, 0, 7551 );
				c.fsBar( 0, 0, 7552 );
				c.fsBar( 0, 0, 7553 );
				c.fsBar( 0, 0, 7554 );
				c.fsBar( 0, 0, 7555 );
				c.fsBar( 0, 0, 7556 );
				c.fsBar( 0, 0, 7557 );
				c.fsBar( 0, 0, 7558 );
				c.fsBar( 0, 0, 7559 );
				c.fsBar( 0, 0, 7560 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E @bla@C I A L  A T T A C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 0, 0, 7554 );
				c.fsBar( 0, 0, 7555 );
				c.fsBar( 0, 0, 7556 );
				c.fsBar( 0, 0, 7557 );
				c.fsBar( 0, 0, 7558 );
				c.fsBar( 0, 0, 7559 );
				c.fsBar( 0, 0, 7560 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L@bla@  A T T A C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 500, 0, 7554 );
				c.fsBar( 500, 0, 7555 );
				c.fsBar( 0, 0, 7556 );
				c.fsBar( 0, 0, 7557 );
				c.fsBar( 0, 0, 7558 );
				c.fsBar( 0, 0, 7559 );
				c.fsBar( 0, 0, 7560 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A @bla@C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 500, 0, 7554 );
				c.fsBar( 500, 0, 7555 );
				c.fsBar( 500, 0, 7556 );
				c.fsBar( 500, 0, 7557 );
				c.fsBar( 500, 0, 7558 );
				c.fsBar( 0, 0, 7559 );
				c.fsBar( 0, 0, 7560 );
			}
			if ( c.specialAmount >= 100 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 500, 0, 7554 );
				c.fsBar( 500, 0, 7555 );
				c.fsBar( 500, 0, 7556 );
				c.fsBar( 500, 0, 7557 );
				c.fsBar( 500, 0, 7558 );
				c.fsBar( 500, 0, 7559 );
				c.fsBar( 500, 0, 7560 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E @bla@C I A L  A T T A C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 0, 0, 7554 );
				c.fsBar( 0, 0, 7555 );
				c.fsBar( 0, 0, 7556 );
				c.fsBar( 0, 0, 7557 );
				c.fsBar( 0, 0, 7558 );
				c.fsBar( 0, 0, 7559 );
				c.fsBar( 0, 0, 7560 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L@bla@  A T T A C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 500, 0, 7554 );
				c.fsBar( 500, 0, 7555 );
				c.fsBar( 0, 0, 7556 );
				c.fsBar( 0, 0, 7557 );
				c.fsBar( 0, 0, 7558 );
				c.fsBar( 0, 0, 7559 );
				c.fsBar( 0, 0, 7560 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A @bla@C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 500, 0, 7554 );
				c.fsBar( 500, 0, 7555 );
				c.fsBar( 500, 0, 7556 );
				c.fsBar( 500, 0, 7557 );
				c.fsBar( 500, 0, 7558 );
				c.fsBar( 0, 0, 7559 );
				c.fsBar( 0, 0, 7560 );
			}
			if ( c.specialAmount >= 100 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A C K", 7561 );
				c.fsBar( 500, 0, 7551 );
				c.fsBar( 500, 0, 7552 );
				c.fsBar( 500, 0, 7553 );
				c.fsBar( 500, 0, 7554 );
				c.fsBar( 500, 0, 7555 );
				c.fsBar( 500, 0, 7556 );
				c.fsBar( 500, 0, 7557 );
				c.fsBar( 500, 0, 7558 );
				c.fsBar( 500, 0, 7559 );
				c.fsBar( 500, 0, 7560 );
			}
		}

		public void specialAttacks5( int ID )
		{
			client c = ( client ) PlayerHandler.players[ID];
			if ( c.specialAmount >= 0 && c.specialAmount <= 24 )
			{
				c.sendFrame126( "S P E C I A L  A T T A C K", 8505 );
				c.fsBar( 0, 0, 8495 );
				c.fsBar( 0, 0, 8496 );
				c.fsBar( 0, 0, 8497 );
				c.fsBar( 0, 0, 8498 );
				c.fsBar( 0, 0, 8499 );
				c.fsBar( 0, 0, 8500 );
				c.fsBar( 0, 0, 8501 );
				c.fsBar( 0, 0, 8502 );
				c.fsBar( 0, 0, 8503 );
				c.fsBar( 0, 0, 8504 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E @bla@C I A L  A T T A C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 0, 0, 8498 );
				c.fsBar( 0, 0, 8499 );
				c.fsBar( 0, 0, 8500 );
				c.fsBar( 0, 0, 8501 );
				c.fsBar( 0, 0, 8502 );
				c.fsBar( 0, 0, 8503 );
				c.fsBar( 0, 0, 8504 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L@bla@  A T T A C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 500, 0, 8498 );
				c.fsBar( 500, 0, 8499 );
				c.fsBar( 0, 0, 8500 );
				c.fsBar( 0, 0, 8501 );
				c.fsBar( 0, 0, 8502 );
				c.fsBar( 0, 0, 8503 );
				c.fsBar( 0, 0, 8504 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A @bla@C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 500, 0, 8498 );
				c.fsBar( 500, 0, 8499 );
				c.fsBar( 500, 0, 8500 );
				c.fsBar( 500, 0, 8501 );
				c.fsBar( 500, 0, 8502 );
				c.fsBar( 0, 0, 8503 );
				c.fsBar( 0, 0, 8504 );
			}
			if ( c.specialAmount >= 100 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 500, 0, 8498 );
				c.fsBar( 500, 0, 8499 );
				c.fsBar( 500, 0, 8500 );
				c.fsBar( 500, 0, 8501 );
				c.fsBar( 500, 0, 8502 );
				c.fsBar( 500, 0, 8503 );
				c.fsBar( 500, 0, 8504 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E @bla@C I A L  A T T A C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 0, 0, 8498 );
				c.fsBar( 0, 0, 8499 );
				c.fsBar( 0, 0, 8500 );
				c.fsBar( 0, 0, 8501 );
				c.fsBar( 0, 0, 8502 );
				c.fsBar( 0, 0, 8503 );
				c.fsBar( 0, 0, 8504 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L@bla@  A T T A C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 500, 0, 8498 );
				c.fsBar( 500, 0, 8499 );
				c.fsBar( 0, 0, 8500 );
				c.fsBar( 0, 0, 8501 );
				c.fsBar( 0, 0, 8502 );
				c.fsBar( 0, 0, 8503 );
				c.fsBar( 0, 0, 8504 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A @bla@C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 500, 0, 8498 );
				c.fsBar( 500, 0, 8499 );
				c.fsBar( 500, 0, 8500 );
				c.fsBar( 500, 0, 8501 );
				c.fsBar( 500, 0, 8502 );
				c.fsBar( 0, 0, 8503 );
				c.fsBar( 0, 0, 8504 );
			}
			if ( c.specialAmount >= 100 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A C K", 8505 );
				c.fsBar( 500, 0, 8495 );
				c.fsBar( 500, 0, 8496 );
				c.fsBar( 500, 0, 8497 );
				c.fsBar( 500, 0, 8498 );
				c.fsBar( 500, 0, 8499 );
				c.fsBar( 500, 0, 8500 );
				c.fsBar( 500, 0, 8501 );
				c.fsBar( 500, 0, 8502 );
				c.fsBar( 500, 0, 8503 );
				c.fsBar( 500, 0, 8504 );
			}
		}

		public void specialAttacks6( int ID )
		{
			client c = ( client ) PlayerHandler.players[ID];
			if ( c.specialAmount >= 0 && c.specialAmount <= 24 )
			{
				c.sendFrame126( "S P E C I A L  A T T A C K", 7511 );
				c.fsBar( 0, 0, 7501 );
				c.fsBar( 0, 0, 7502 );
				c.fsBar( 0, 0, 7503 );
				c.fsBar( 0, 0, 7504 );
				c.fsBar( 0, 0, 7505 );
				c.fsBar( 0, 0, 7506 );
				c.fsBar( 0, 0, 7507 );
				c.fsBar( 0, 0, 7508 );
				c.fsBar( 0, 0, 7509 );
				c.fsBar( 0, 0, 7510 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E @bla@C I A L  A T T A C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 0, 0, 7504 );
				c.fsBar( 0, 0, 7505 );
				c.fsBar( 0, 0, 7506 );
				c.fsBar( 0, 0, 7507 );
				c.fsBar( 0, 0, 7508 );
				c.fsBar( 0, 0, 7509 );
				c.fsBar( 0, 0, 7510 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L@bla@  A T T A C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 500, 0, 7504 );
				c.fsBar( 500, 0, 7505 );
				c.fsBar( 0, 0, 7506 );
				c.fsBar( 0, 0, 7507 );
				c.fsBar( 0, 0, 7508 );
				c.fsBar( 0, 0, 7509 );
				c.fsBar( 0, 0, 7510 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A @bla@C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 500, 0, 7504 );
				c.fsBar( 500, 0, 7505 );
				c.fsBar( 500, 0, 7506 );
				c.fsBar( 500, 0, 7507 );
				c.fsBar( 500, 0, 7508 );
				c.fsBar( 0, 0, 7509 );
				c.fsBar( 0, 0, 7510 );
			}
			if ( c.specialAmount >= 100 && c.specOn == false )
			{
				c.sendFrame126( "@bla@S P E C I A L  A T T A C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 500, 0, 7504 );
				c.fsBar( 500, 0, 7505 );
				c.fsBar( 500, 0, 7506 );
				c.fsBar( 500, 0, 7507 );
				c.fsBar( 500, 0, 7508 );
				c.fsBar( 500, 0, 7509 );
				c.fsBar( 500, 0, 7510 );
			}
			if ( c.specialAmount >= 25 && c.specialAmount <= 49 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E @bla@C I A L  A T T A C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 0, 0, 7504 );
				c.fsBar( 0, 0, 7505 );
				c.fsBar( 0, 0, 7506 );
				c.fsBar( 0, 0, 7507 );
				c.fsBar( 0, 0, 7508 );
				c.fsBar( 0, 0, 7509 );
				c.fsBar( 0, 0, 7510 );
			}
			if ( c.specialAmount >= 50 && c.specialAmount <= 74 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L@bla@  A T T A C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 500, 0, 7504 );
				c.fsBar( 500, 0, 7505 );
				c.fsBar( 0, 0, 7506 );
				c.fsBar( 0, 0, 7507 );
				c.fsBar( 0, 0, 7508 );
				c.fsBar( 0, 0, 7509 );
				c.fsBar( 0, 0, 7510 );
			}
			if ( c.specialAmount >= 75 && c.specialAmount <= 99 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A @bla@C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 500, 0, 7504 );
				c.fsBar( 500, 0, 7505 );
				c.fsBar( 500, 0, 7506 );
				c.fsBar( 500, 0, 7507 );
				c.fsBar( 500, 0, 7508 );
				c.fsBar( 0, 0, 7509 );
				c.fsBar( 0, 0, 7510 );
			}
			if ( c.specialAmount >= 100 && c.specOn == true )
			{
				c.sendFrame126( "@yel@S P E C I A L  A T T A C K", 7511 );
				c.fsBar( 500, 0, 7501 );
				c.fsBar( 500, 0, 7502 );
				c.fsBar( 500, 0, 7503 );
				c.fsBar( 500, 0, 7504 );
				c.fsBar( 500, 0, 7505 );
				c.fsBar( 500, 0, 7506 );
				c.fsBar( 500, 0, 7507 );
				c.fsBar( 500, 0, 7508 );
				c.fsBar( 500, 0, 7509 );
				c.fsBar( 500, 0, 7510 );
			}
		}

	}//ends special attack handler
}
