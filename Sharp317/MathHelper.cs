using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public static class MathHelper
	{
		private static readonly Random Instance = new Random();
		public static Double Random()
		{
			return Instance.NextDouble();
		}
	}
}
