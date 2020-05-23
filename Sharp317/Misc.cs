using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sharp317
{
	public class misc
	{
		public static Int32[] buttons_smelting = { 15147, 15146, 10247, 9110, 15151,
			15150, 15149, 15148, 15155, 15154, 15153, 15152, 15159, 15158,
			15157, 15156, 15163, 15162, 15161, 15160, 29017, 29016, 24253,
			16062, 29022, 29020, 29019, 29018, 29026, 29025, 29024, 29023 };
		private static Char[] decodeBuf = new Char[4096];
		public static SByte[] directionDeltaX = new SByte[] { 0, 1, 1, 1, 0, -1, -1, -1 };
		public static SByte[] directionDeltaY = new SByte[] { 1, 1, 0, -1, -1, -1, 0, 1 };
		public static Char[] playerNameXlateTable = new Char[] { '_', 'a', 'b', 'c',
			'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
			'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2',
			'3', '4', '5', '6', '7', '8', '9' };
		public static String[] rules = { "Offensive language", "Item scamming",
			"Password scamming", "Bug abuse (includes noclip)",
			"Sharp317 staff impersonation", "Monster luring or abuse",
			"Macroing", "Item Duplication",
			"Encouraging others to break rules", "Misuse of yell channel",
			"Advertising / website", "Possible duped items" };

		public static Int32[] smelt_bars = { 2349, 2351, 2355, 2353, 2357, 2359,
			2361, 2363 };

		public static Int32[] smelt_frame = { 2405, 2406, 2407, 2409, 2410, 2411,
			2412, 2413 };
		public static Int32[] times = { 1, 3, 3, 1, 5, 1, 0, 0, 0, 1, 1, 0, 0 };
		// translates our direction convention to the one used in the protocol
		public static Byte[] xlateDirectionToClient = new Byte[] { 1, 2, 4, 7, 6,
			5, 3, 0 };
		public static Char[] xlateTable = new Char[]{ ' ', 'e', 't', 'a', 'o', 'i', 'h', 'n',
			's', 'r', 'd', 'l', 'u', 'm', 'w', 'c', 'y', 'f', 'g', 'p', 'b',
			'v', 'k', 'x', 'j', 'q', 'z', '0', '1', '2', '3', '4', '5', '6',
			'7', '8', '9', ' ', '!', '?', '.', ',', ':', ';', '(', ')', '-',
			'&', '*', '\\', '\'', '@', '#', '+', '=', '\u0243', '$', '%', '"',
			'[', ']' };

		public static Int32 getCurrentHP( Int32 i, Int32 i1, Int32 i2 )
		{
			var x = ( Double ) i / ( Double ) i1;
			return ( Int32 ) Math.Round( x * i2 );
		}

		public enum Direction : SByte
		{
			None = -1,
			NorthWest = 0,
			North = 1,
			NorthEast = 2,
			West = 3,
			East = 4,
			SouthWest = 5,
			South = 6,
			SouthEast = 7
		}

		public static Int32 direction( Int32 srcX, Int32 srcY, Int32 destX, Int32 destY )
		{	
			Int32 dx = destX - srcX, dy = destY - srcY;
			// a lot of cases that have to be considered here ... is there a more
			// sophisticated (and quick!) way?
			if ( dx < 0 )
			{
				if ( dy < 0 )
				{
					if ( dx < dy )
						return 11;
					else if ( dx > dy )
						return 9;
					else
						return 10; // dx == dy
				}
				else if ( dy > 0 )
				{
					if ( -dx < dy )
						return 15;
					else if ( -dx > dy )
						return 13;
					else
						return 14; // -dx == dy
				}
				else
				{ // dy == 0
					return 12;
				}
			}
			else if ( dx > 0 )
			{
				if ( dy < 0 )
				{
					if ( dx < -dy )
						return 7;
					else if ( dx > -dy )
						return 5;
					else
						return 6; // dx == -dy
				}
				else if ( dy > 0 )
				{
					if ( dx < dy )
						return 1;
					else if ( dx > dy )
						return 3;
					else
						return 2; // dx == dy
				}
				else
				{ // dy == 0
					return 4;
				}
			}
			else
			{ // dx == 0
				if ( dy < 0 )
				{
					return 8;
				}
				else if ( dy > 0 )
				{
					return 0;
				}
				else
				{ // dy == 0
					return -1; // src and dest are the same
				}
			}
		}

		public static String format( Int32 num )
		{
			return num.ToString( new NumberFormatInfo() );
		}

		public static String Hex( Byte[] data )
		{
			return Hex( data, 0, data.Length );
		}

		public static String Hex( Byte[] data, Int32 offset, Int32 len )
		{
			String temp = "";
			for ( var cntr = 0; cntr < len; cntr++ )
			{
				var num = data[offset + cntr] & 0xFF;
				String myStr;
				if ( num < 16 )
					myStr = "0";
				else
					myStr = "";
				temp += myStr + num.ToString( "X4" ) + " ";
			}
			return temp.ToUpper().Trim();
		}

		public static Int32 HexToInt( Byte[] data, Int32 offset, Int32 len )
		{
			var temp = 0;
			var i = 1000;
			for ( var cntr = 0; cntr < len; cntr++ )
			{
				var num = ( data[offset + cntr] & 0xFF ) * i;
				temp += num;
				if ( i > 1 )
					i = i / 1000;
			}
			return temp;
		}

		public static String longToPlayerName( Int64 l )
		{
			var i = 0;
			Char[] ac = new Char[12];
			while ( l != 0L )
			{
				var l1 = l;
				l /= 37L;
				ac[11 - i++] = playerNameXlateTable[( Int32 ) ( l1 - l * 37L )];
			}
			return new String( ac, 12 - i, i );
		}

		public static String optimizeText( String text )
		{
			Char[] buf = text.ToCharArray();
			Boolean endMarker = true; // marks the end of a sentence to make the
									  // next char capital
			for ( var i = 0; i < buf.Length; i++ )
			{
				var c = buf[i];
				if ( endMarker && ( c >= 'a' ) && ( c <= 'z' ) )
				{
					buf[i] = Char.ToUpper( buf[i] ); // transform lower case into upper case
					endMarker = false;
				}
				if ( ( c == '.' ) || ( c == '!' ) || ( c == '?' ) )
					endMarker = true;
			}
			return new String( buf, 0, buf.Length );
		}

		public static Int64 playerNameToInt64( String s )
		{
			var l = 0L;
			for ( var i = 0; ( i < s.Length ) && ( i < 12 ); i++ )
			{
				var c = s[i];
				l *= 37L;
				if ( ( c >= 'A' ) && ( c <= 'Z' ) )
					l += ( 1 + c ) - 65;
				else if ( ( c >= 'a' ) && ( c <= 'z' ) )
					l += ( 1 + c ) - 97;
				else if ( ( c >= '0' ) && ( c <= '9' ) )
					l += ( 27 + c ) - 48;
			}
			while ( ( l % 37L == 0L ) && ( l != 0L ) )
				l /= 37L;
			return l;
		}

		public static Int64 playerNameToLong( String s )
		{
			var l = 0L;
			for ( var i = 0; ( i < s.Length ) && ( i < 12 ); i++ )
			{
				var c = s[i];
				l *= 37L;
				if ( ( c >= 'A' ) && ( c <= 'Z' ) )
				{
					l += ( 1 + c ) - 65;
				}
				else if ( ( c >= 'a' ) && ( c <= 'z' ) )
				{
					l += ( 1 + c ) - 97;
				}
				else if ( ( c >= '0' ) && ( c <= '9' ) )
				{
					l += ( 27 + c ) - 48;
				}
			}
			for ( ; ( l % 37L == 0L ) && ( l != 0L ); l /= 37L )
				;
			return l;
		}

		public static void print( String str )
		{
			Console.WriteLine( str );
		}

		public static void print_debug( String str )
		{
			Console.WriteLine( str ); // comment this line out if you want
									 // to get rid of debug messages
		}

		public static void println( String str )
		{
			Console.WriteLine( str );
		}

		public static void println_debug( String str )
		{
			Console.WriteLine( str );
		}

		public static Int32 random( Int32 range )
		{ // 0 till range (range INCLUDED)
			return ( Int32 ) ( MathHelper.Random() * ( range + 1 ) );
		}

		public static Int32 random2( Int32 range )
		{ // 1 till range
			return ( Int32 ) ( ( MathHelper.Random() * range ) + 1 );
		}

		public static Int32 random3( Int32 range )
		{ // 0 till range
			return ( Int32 ) ( MathHelper.Random() * range );
		}

		public static Int32 random4( Int32 range )
		{ // 0 till range (range INCLUDED)
			return ( Int32 ) ( MathHelper.Random() * ( range + 1 ) );
		}

		public static void textPack( Byte[] packedData, String text )
		{
			if ( text.Length > 80 )
				text = text.Substring( 0, 80 );
			text = text.ToLower();

			var carryOverNibble = -1;
			var ofs = 0;
			for ( var idx = 0; idx < text.Length; idx++ )
			{
				var c = text[idx];
				var tableIdx = 0;
				for ( var i = 0; i < xlateTable.Length; i++ )
				{
					if ( c == xlateTable[i] )
					{
						tableIdx = i;
						break;
					}
				}
				if ( tableIdx > 12 )
					tableIdx += 195;
				if ( carryOverNibble == -1 )
				{
					if ( tableIdx < 13 )
						carryOverNibble = tableIdx;
					else
						packedData[ofs++] = ( Byte ) ( tableIdx );
				}
				else if ( tableIdx < 13 )
				{
					packedData[ofs++] = ( Byte ) ( ( carryOverNibble << 4 ) + tableIdx );
					carryOverNibble = -1;
				}
				else
				{
					packedData[ofs++] = ( Byte ) ( ( carryOverNibble << 4 ) + ( tableIdx >> 4 ) );
					carryOverNibble = tableIdx & 0xf;
				}
			}

			if ( carryOverNibble != -1 )
				packedData[ofs++] = ( Byte ) ( carryOverNibble << 4 );
		}

		public static String textUnpack( Byte[] packedData, Int32 size )
		{
			Int32 idx = 0, highNibble = -1;
			for ( var i = 0; i < size * 2; i++ )
			{
				var val = packedData[i / 2] >> ( 4 - 4 * ( i % 2 ) ) & 0xf;
				if ( highNibble == -1 )
				{
					if ( val < 13 )
						decodeBuf[idx++] = xlateTable[val];
					else
						highNibble = val;
				}
				else
				{
					decodeBuf[idx++] = xlateTable[( ( highNibble << 4 ) + val ) - 195];
					highNibble = -1;
				}
			}

			return new String( decodeBuf, 0, idx );
		}
	}

}
