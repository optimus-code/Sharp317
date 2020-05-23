using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class ItemList
	{
		public Int32[] Bonuses = new Int32[100];
		public Double HighAlch;
		public String itemDescription;
		public Int32 itemId;
		public String itemName;
		public Double LowAlch;
		public Double ShopValue;

		public ItemList( Int32 _itemId )
		{
			itemId = _itemId;
		}
	}
}
