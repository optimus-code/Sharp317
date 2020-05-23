using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class GameItem
	{
		public Int32 id, amount;
		public Boolean stackable = false;

		public GameItem( Int32 id, Int32 amount )
		{
			if ( Item.itemStackable[id] )
			{
				stackable = true;
			}
			this.id = id;
			this.amount = amount;
		}
	}
}
