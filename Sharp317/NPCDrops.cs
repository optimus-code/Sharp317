using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class NPCDrops
	{
		public Int32 DropType;
		public Int32[] Items = new Int32[100];
		public Int32[] ItemsN = new Int32[100];
		public Int32 npcId;

		public NPCDrops( Int32 _npcId )
		{
			npcId = _npcId;
			for ( var i = 0; i < Items.Length; i++ )
			{
				Items[i] = -1;
				ItemsN[i] = 0;
			}
		}
	}

}
