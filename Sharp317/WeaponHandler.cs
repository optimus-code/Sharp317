using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{

	public class WeaponHandler
	{
		/*Weapon Timers are based off of rs's knowledge bases speed bars. There are 10 sections, subtract 2 from 20 per bar,
		and then an extra 2 at the end.
		Example: [+|+|+|+|+|+| | | | ] 6 full bars out of 10, subtract 12 from 20, another 2, and u get 6 for the speed :)*/

		public Int32 WeaponSpeed = 10;

		public Int32 SendWeapon( String WeaponName, Int32 FightType )
		{
			WeaponName = WeaponName.Replace( "_", " " ).Trim();

			if ( WeaponName.Contains( "Unarmed" ) )
			{
				if ( FightType == 2 )
				{
					return 423;
				}
				else
				{
					return 422;
				}
			}

			else if ( WeaponName.Contains( "Dragon dagger" ) )
			{
				if ( FightType == 2 )
				{
					return 401;
				}
				else
				{
					return 401;
				}
			}

			else if ( WeaponName.Contains( "dagger" ) || WeaponName.Contains( "pickaxe" ) )
			{
				if ( FightType == 2 )
				{
					return 451;
				}
				else
				{
					return 412;
				}
			}

			else if ( WeaponName.Contains( "sword" ) && !WeaponName.Contains( "god" ) && !WeaponName.Contains( "2h" ) || WeaponName.Contains( "mace" ) || WeaponName.Contains( "longsword" ) && !WeaponName.Contains( "2h" ) || WeaponName.Contains( "scimitar" ) )
			{
				if ( FightType == 3 )
				{
					return 412;
				}
				else
				{
					return 451;
				}
			}

			else if ( WeaponName.Contains( "axe" ) && !WeaponName.Contains( "greataxe" ) || WeaponName.Contains( "battleaxe" ) )
			{
				return 1833;
			}

			else if ( WeaponName.Contains( "halberd" ) || WeaponName.Contains( "spear" ) && !WeaponName.Contains( "Guthans" ) )
			{
				if ( FightType == 2 )
				{
					return 440;
				}
				else
				{
					return 412;
				}
			}

			else if ( WeaponName.Contains( "anchor" ) )
			{
				return 2661;
			}

			else if ( WeaponName.Contains( "2h" ) )
			{
				if ( FightType == 3 )
				{
					return 406;
				}
				else
				{
					return 407;
				}
			}

			if ( WeaponName.Contains( "godsword" ) ) // godswords
			{
				return 2890;
			}

			else if ( WeaponName.Contains( "Tzhaar-ket-om" ) )
			{
				return 2661;
			}

			else if ( WeaponName.Contains( "Granite maul" ) )
			{
				return 1665;
			}

			else if ( WeaponName.Contains( "greataxe" ) )
			{
				if ( FightType == 3 )
				{
					return 2066;
				}
				else
				{
					return 2067;
				}
			}

			else if ( WeaponName.Contains( "flail" ) )
			{
				return 2062;
			}

			else if ( WeaponName.Contains( "whip" ) )
			{
				return 1658;
			}

			else if ( WeaponName.Contains( "Mouse" ) )
			{
				return 1658;
			}

			else if ( WeaponName.Contains( "spear" ) && WeaponName.Contains( "Guthans" ) )
			{
				if ( FightType == 3 )
				{
					return 2080;
				}
				else
				{
					return 2081;
				}
			}

			else if ( WeaponName.Contains( "toktz-xil-ul" ) )
			{
				return 1060;
			}

			else if ( WeaponName.Contains( "hammers" ) )
			{
				return 2068;
			}

			else
			{
				return 451;
			}

		}

		public Int32 GetWeaponSpeed( String WeaponName )
		{
			WeaponName = WeaponName.Replace( "_", " " ).Trim();

			if ( WeaponName.Contains( "Unarmed" ) )
			{
				return 5;
			}

			else if ( WeaponName.Contains( "Dragon dagger" ) )
			{
				return 6;
			}
			else if ( WeaponName.Contains( "Magic shortbow" ) )
			{
				return 4;
			}
			else if ( WeaponName.Contains( "Magic longbow" ) )
			{
				return 8;
			}
			else if ( WeaponName.Contains( "dagger" ) || WeaponName.Contains( "pickaxe" ) )
			{
				return 5;
			}

			else if ( WeaponName.Contains( "sword" ) && !WeaponName.Contains( "2h" ) && !WeaponName.Contains( "god" ) )
			{
				return 5;
			}

			else if ( WeaponName.Contains( "mace" ) )
			{
				return 6;
			}

			else if ( WeaponName.Contains( "longsword" ) && !WeaponName.Contains( "2h" ) && !WeaponName.Contains( "god" ) )
			{
				return 6;
			}

			else if ( WeaponName.Contains( "scimitar" ) )
			{
				return 4;
			}

			else if ( WeaponName.Contains( "axe" ) && !WeaponName.Contains( "greataxe" ) )
			{
				return 10;
			}

			else if ( WeaponName.Contains( "battleaxe" ) )
			{
				return 10;
			}


			if ( WeaponName.Contains( "godsword" ) ) // godswords
			{
				return 7;
			}

			else if ( WeaponName.Contains( "halberd" ) )
			{
				return 10;
			}

			else if ( WeaponName.Contains( "spear" ) && !WeaponName.Contains( "Guthans" ) )
			{
				return 7;
			}

			else if ( WeaponName.Contains( "2h" ) )
			{
				return 10;
			}

			else if ( WeaponName.Contains( "Tzhaar-ket-om" ) )
			{
				return 10;
			}

			else if ( WeaponName.Contains( "Granite maul" ) )
			{
				return 10;
			}

			else if ( WeaponName.Contains( "greataxe" ) )
			{
				return 11;
			}

			else if ( WeaponName.Contains( "flail" ) )
			{
				return 7;
			}

			else if ( WeaponName.Contains( "whip" ) )
			{
				return 5;
			}

			else if ( WeaponName.Contains( "warhammer" ) )
			{
				return 8;
			}

			else if ( WeaponName.Contains( "Mouse" ) )
			{
				return 5;
			}
			if ( WeaponName.Contains( "god" ) ) // godswords
			{
				return 8;
			}
			else if ( WeaponName.Contains( "spear" ) && WeaponName.Contains( "Guthans" ) )
			{
				return 7;
			}

			else if ( WeaponName.Contains( "hammers" ) )
			{
				return 8;
			}

			else if ( WeaponName.Contains( "staff" ) )
			{
				return 6;
			}

			else if ( WeaponName.Contains( "ancient" ) )
			{
				return 6;
			}

			else if ( WeaponName.Contains( "shortbow" ) )
			{
				return 5;
			}

			else if ( WeaponName.Contains( "Seercull" ) )
			{
				return 5;
			}

			else if ( WeaponName.Contains( "Karils crossbow" ) )
			{
				return 6;
			}

			else if ( WeaponName.Contains( "dart" ) )
			{
				return 4;
			}

			else if ( WeaponName.Contains( "knife" ) )
			{
				return 4;
			}

			else if ( WeaponName.Contains( "thrownaxe" ) )
			{
				return 6;
			}

			else if ( WeaponName.Contains( "javelin" ) )
			{
				return 8;
			}

			else if ( WeaponName.Contains( "crystal" ) )
			{
				return 6;
			}

			else
			{
				return 10;
			}

		}

	}
}
