using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public static class TimeHelper
	{
		private static readonly DateTime Jan1st1970 = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );

		public static long CurrentTimeMillis( )
		{
			return ( long ) ( DateTime.UtcNow - Jan1st1970 ).TotalMilliseconds;
		}
	}
}
