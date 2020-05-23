using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class NPCList
	{
		public Int32 npcCombat;
		public Int32 npcHealth;
		public Int32 npcId;
		public String npcName;
		public Int32 npcRespawn = 60;

		public NPCList( Int32 _npcId )
		{
			npcId = _npcId;
		}
	}
}
