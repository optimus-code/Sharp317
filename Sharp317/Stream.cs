using Reinterpret.Net;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Sharp317
{
	public class stream
	{
		public static Int32[] bitMaskOut = new Int32[32];

		private static Int32 frameStackSize = 10;

		static stream( )
		{
			for ( var i = 0; i < 32; i++ )
				bitMaskOut[i] = ( 1 << i ) - 1;
		}

		public Int32 bitPosition = 0;

		public Byte[] buffer = null;

		public Int32 currentOffset = 0;

		private Int32[] frameStack = new Int32[frameStackSize];

		private Int32 frameStackPtr = -1;

		public ISAACRandomGenerator packetEncryption = null;

		public stream( )
		{
		}

		public stream( Byte[] abyte0 )
		{
			buffer = abyte0;
			currentOffset = 0;
		}

		public void createFrame( Int32 id )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( id /*+ packetEncryption.getNextKey()*/ );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void createFrameVarSize( Int32 id )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( id /*+ packetEncryption.getNextKey()*/ );
				buffer[currentOffset++] = 0;
				if ( frameStackPtr >= frameStackSize - 1 )
				{
					throw new Exception( "Stack overflow" );
				}
				else
					frameStack[++frameStackPtr] = currentOffset;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void createFrameVarSizeWord( Int32 id )
		{ // creates a variable sized
			try
			{                                           // frame
				buffer[currentOffset++] = ( Byte ) ( id /*+ packetEncryption.getNextKey()*/ );
				writeWord( 0 ); // place holder for size word
				if ( frameStackPtr >= frameStackSize - 1 )
				{
					throw new Exception( "Stack overflow" );
				}
				else
					frameStack[++frameStackPtr] = currentOffset;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void endFrameVarSize( )
		{
			try
			{
				if ( frameStackPtr < 0 )
					throw new Exception( "Stack empty" );
				else
					writeFrameSize( currentOffset - frameStack[frameStackPtr--] );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void endFrameVarSizeWord( )
		{
			try
			{
				if ( frameStackPtr < 0 )
					throw new Exception( "Stack empty" );
				else
					writeFrameSizeWord( currentOffset - frameStack[frameStackPtr--] );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void finishBitAccess( )
		{
			try
			{
				currentOffset = ( bitPosition + 7 ) / 8;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void initBitAccess( )
		{
			try
			{
				bitPosition = currentOffset * 8;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void readBytes( Byte[] abyte0, Int32 i, Int32 j )
		{
			try
			{
				for ( var k = j; k < j + i; k++ )
					abyte0[k] = buffer[currentOffset++];

			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void readBytes_reverse( Byte[] abyte0, Int32 i, Int32 j )
		{
			try
			{
				for ( var k = ( j + i ) - 1; k >= j; k-- )
				{
					abyte0[k] = buffer[currentOffset++];

				}
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void readBytes_reverseA( Byte[] abyte0, Int32 i, Int32 j )
		{
			try
			{
				for ( var k = ( j + i ) - 1; k >= j; k-- )
					abyte0[k] = ( Byte ) ( buffer[currentOffset++] - 128 );

			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public Int32 readDWord( )
		{
			currentOffset += 4;
			return ( ( buffer[currentOffset - 4] & 0xff ) << 24 ) + ( ( buffer[currentOffset - 3] & 0xff ) << 16 ) + ( ( buffer[currentOffset - 2] & 0xff ) << 8 ) + ( buffer[currentOffset - 1] & 0xff );
		}

		public int readShort( )
		{
			currentOffset += 2;
			int i = ( ( buffer[currentOffset - 2] & 0xff ) << 8 ) + ( buffer[currentOffset - 1] & 0xff );
			if ( i > 32767 )
				i -= 0x10000;
			return i;
		}
		public int readShortA( )
		{
			currentOffset += 2;
			int i = ( ( buffer[currentOffset - 2] & 0xff ) << 8 ) + ( buffer[currentOffset - 1] - 128 & 0xff );
			if ( i > 32767 )
				i -= 0x10000;
			return i;
		}

		public int readSignedLEShortA( )
		{
			currentOffset += 2;
			int j = ( ( buffer[currentOffset - 1] & 0xff ) << 8 ) + ( buffer[currentOffset - 2] - 128 & 0xff );
			if ( j > 32767 )
				j -= 0x10000;
			return j;
		}

		public Int32 readDWord_v1( )
		{
			currentOffset += 4;
			return ( ( buffer[currentOffset - 2] & 0xff ) << 24 )
					+ ( ( buffer[currentOffset - 1] & 0xff ) << 16 )
					+ ( ( buffer[currentOffset - 4] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 3] & 0xff );
		}

		public Int32 readDWord_v2( )
		{
			currentOffset += 4;
			return ( ( buffer[currentOffset - 3] & 0xff ) << 24 )
					+ ( ( buffer[currentOffset - 4] & 0xff ) << 16 )
					+ ( ( buffer[currentOffset - 1] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 2] & 0xff );
		}

		public Int64 readQWord( )
		{
			var l = readDWord() & 0xffffffffL;
			var l1 = readDWord() & 0xffffffffL;
			return ( l << 32 ) + l1;
		}

		public SByte readSignedByte( )
		{
			return ( SByte ) buffer[currentOffset++];
		}

		public Byte readSignedByteA( )
		{
			return ( Byte ) ( buffer[currentOffset++] - 128 );
		}

		public Byte readSignedByteC( )
		{
			return ( Byte ) ( -buffer[currentOffset++] );
		}

		public Byte readSignedByteS( )
		{
			return ( Byte ) ( 128 - buffer[currentOffset++] );
		}

		public Int32 readSignedWord( )
		{
			currentOffset += 2;
			var i = ( ( buffer[currentOffset - 2] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 1] & 0xff );
			if ( i > 32767 )
			{
				i -= 0x10000;
			}
			return i;
		}

		public Int32 readSignedWordA( )
		{
			currentOffset += 2;
			var i = ( ( buffer[currentOffset - 2] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 1] - 128 & 0xff );
			if ( i > 32767 )
			{
				i -= 0x10000;
			}
			return i;
		}

		public Int32 readSignedWordBigEndian( )
		{
			currentOffset += 2;
			var i = ( ( buffer[currentOffset - 1] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 2] & 0xff );
			if ( i > 32767 )
			{
				i -= 0x10000;
			}
			return i;
		}

		public Int32 readSignedWordBigEndianA( )
		{
			currentOffset += 2;
			var i = ( ( buffer[currentOffset - 1] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 2] - 128 & 0xff );
			if ( i > 32767 )
				i -= 0x10000;
			return i;
		}

		public String readString( )
		{
			int i = currentOffset;

			while ( currentOffset + 1 < buffer.Length && buffer[currentOffset++] != 10 )
				;

			return Encoding.ASCII.GetString( buffer, i, currentOffset - i - 1 ).Trim();
		}

		public Int32 readUnsignedByte( )
		{
			return buffer[currentOffset++] & 0xff;
		}

		public Int32 readUnsignedByteA( )
		{
			return buffer[currentOffset++] - 128 & 0xff;
		}

		public Int32 readUnsignedByteC( )
		{
			return -buffer[currentOffset++] & 0xff;
		}

		public Int32 readUnsignedByteS( )
		{
			return 128 - buffer[currentOffset++] & 0xff;
		}

		public Int32 readUnsignedWord( )
		{
			currentOffset += 2;
			return ( ( buffer[currentOffset - 2] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 1] & 0xff );
		}

		public Int32 readUnsignedWordA( )
		{
			currentOffset += 2;
			return ( ( buffer[currentOffset - 2] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 1] - 128 & 0xff );
		}

		public Int32 readUnsignedWordBigEndian( )
		{
			currentOffset += 2;
			return ( ( buffer[currentOffset - 1] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 2] & 0xff );
		}

		public Int32 readUnsignedWordBigEndianA( )
		{
			currentOffset += 2;
			return ( ( buffer[currentOffset - 1] & 0xff ) << 8 )
					+ ( buffer[currentOffset - 2] - 128 & 0xff );
		}

		public void write3Byte( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i >> 16 );
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
				buffer[currentOffset++] = ( Byte ) i;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeBits( Int32 numBits, Int32 value )
		{
			try
			{
				var bytePos = bitPosition >> 3;
				var bitOffset = 8 - ( bitPosition & 7 );
				var val = 0;
				bitPosition += numBits;

				for ( ; numBits > bitOffset; bitOffset = 8 )
				{
					//val = buffer[bytePos] & ~bitMaskOut[bitOffset];

					buffer[bytePos] &= ( Byte ) ~bitMaskOut[bitOffset];

					// mask out the desired area
					buffer[bytePos++] |= ( Byte ) ( ( value >> ( numBits - bitOffset ) ) & bitMaskOut[bitOffset] );

					numBits -= bitOffset;
				}
				if ( numBits == bitOffset )
				{
					buffer[bytePos] &= ( Byte ) ~bitMaskOut[bitOffset];
					buffer[bytePos] |= ( Byte ) ( value & bitMaskOut[bitOffset] );
				}
				else
				{
					buffer[bytePos] &= ( Byte ) ~( bitMaskOut[numBits] << ( bitOffset - numBits ) );
					buffer[bytePos] |= ( Byte ) ( ( value & bitMaskOut[numBits] ) << ( bitOffset - numBits ) );
				}
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeByte( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) i;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeByteA( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i + 128 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeByteC( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( -i );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeBytes( Byte[] abyte0, Int32 i, Int32 j )
		{
			try
			{
				for ( var k = j; k < j + i; k++ )
					buffer[currentOffset++] = abyte0[k];

			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeByteS( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( 128 - i );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeBytes_reverse( Byte[] abyte0, Int32 i, Int32 j )
		{
			try
			{
				for ( var k = ( j + i ) - 1; k >= j; k-- )
					buffer[currentOffset++] = abyte0[k];

			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeBytes_reverseA( Byte[] abyte0, Int32 i, Int32 j )
		{
			try
			{
				for ( var k = ( j + i ) - 1; k >= j; k-- )
					buffer[currentOffset++] = ( Byte ) ( abyte0[k] + 128 );

			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeDWord( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i >> 24 );
				buffer[currentOffset++] = ( Byte ) ( i >> 16 );
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
				buffer[currentOffset++] = ( Byte ) i;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeDWord_v1( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
				buffer[currentOffset++] = ( Byte ) i;
				buffer[currentOffset++] = ( Byte ) ( i >> 24 );
				buffer[currentOffset++] = ( Byte ) ( i >> 16 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeDWord_v2( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i >> 16 );
				buffer[currentOffset++] = ( Byte ) ( i >> 24 );
				buffer[currentOffset++] = ( Byte ) i;
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeDWordBigEndian( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) i;
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
				buffer[currentOffset++] = ( Byte ) ( i >> 16 );
				buffer[currentOffset++] = ( Byte ) ( i >> 24 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeFrameSize( Int32 i )
		{
			try
			{
				buffer[currentOffset - i - 1] = ( Byte ) i;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeFrameSizeWord( Int32 i )
		{
			try
			{
				buffer[currentOffset - i - 2] = ( Byte ) ( i >> 8 );
				buffer[currentOffset - i - 1] = ( Byte ) i;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeQWord( Int64 l )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( Int32 ) ( l >> 56 );
				buffer[currentOffset++] = ( Byte ) ( Int32 ) ( l >> 48 );
				buffer[currentOffset++] = ( Byte ) ( Int32 ) ( l >> 40 );
				buffer[currentOffset++] = ( Byte ) ( Int32 ) ( l >> 32 );
				buffer[currentOffset++] = ( Byte ) ( Int32 ) ( l >> 24 );
				buffer[currentOffset++] = ( Byte ) ( Int32 ) ( l >> 16 );
				buffer[currentOffset++] = ( Byte ) ( Int32 ) ( l >> 8 );
				buffer[currentOffset++] = ( Byte ) ( Int32 ) l;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeString( String s )
		{
			try
			{
				System.Buffer.BlockCopy( s.Reinterpret( Encoding.ASCII ), 0, buffer, currentOffset, s.Length );
				currentOffset += s.Length;

				//var data = Encoding.Convert( Encoding.UTF8, Encoding.ASCII, Encoding.UTF8.GetBytes( s ) );
				//IntPtr unmanagedPointer = Marshal.AllocHGlobal( data.Length );
				//Marshal.Copy( data, 0, unmanagedPointer, data.Length );
				//// Call unmanaged code
				//Marshal.FreeHGlobal( unmanagedPointer );

				//Marshal.Copy( unmanagedPointer, buffer, 0, s.Length );

				//Encoding.ASCII.GetBytes( s.Rein, 0, s.Length, buffer, currentOffset );
				//currentOffset += s.Length;
				buffer[currentOffset++] = 10;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeWord( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
				buffer[currentOffset++] = ( Byte ) i;
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeWordA( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
				buffer[currentOffset++] = ( Byte ) ( i + 128 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeWordBigEndian( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) i;
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeWordBigEndian_dup( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) i;
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}

		public void writeWordBigEndianA( Int32 i )
		{
			try
			{
				buffer[currentOffset++] = ( Byte ) ( i + 128 );
				buffer[currentOffset++] = ( Byte ) ( i >> 8 );
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}
		}
	}
}
