using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class Object
	{
		public Int32 id, x, y, type;

		public Object( Int32 id, Int32 x, Int32 y, Int32 type )
		{
			this.id = id;
			this.x = x;
			this.y = y;
			this.type = type;
		}
	}
}
