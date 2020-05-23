using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class MD5 : IDisposable
	{
		public static void main( String[] args )
		{
			Console.WriteLine( new MD5( args[0] ).compute() );
		}

		private String inStr;

		private System.Security.Cryptography.MD5 md5;

		/**
		 * Constructs the MD5 object and sets the string whose MD5 is to be
		 * computed.
		 * 
		 * @param inStr
		 *            the <code>String</code> whose MD5 is to be computed
		 */
		public MD5( String inStr )
		{
			this.inStr = inStr;
			try
			{
				md5 = System.Security.Cryptography.MD5.Create();
			}
			catch ( Exception e )
			{
				Console.WriteLine( e.ToString() );
			}
		}

		/**
		 * Computes the MD5 fingerprint of a string.
		 * 
		 * @return the MD5 digest of the input <code>String</code>
		 */
		public String compute( )
		{
			byte[] inputBytes = System.Text.Encoding.Unicode.GetBytes( inStr );
			byte[] hashBytes = md5.ComputeHash( inputBytes );

			// Convert the byte array to hexadecimal string
			StringBuilder sb = new StringBuilder();
			for ( int i = 0; i < hashBytes.Length; i++ )
			{
				sb.Append( hashBytes[i].ToString( "X2" ) );
			}			

			return sb.ToString();
		}

		public void Dispose( )
		{
			md5.Dispose();
		}
	}
}
