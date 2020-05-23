using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public class Character
	{
		public Int32 Height
		{
			get;
			set;
		}

		public Int32 PositionX
		{
			get;
			set;
		}

		public Int32 PositionY
		{
			get;
			set;
		}

		public Int32 Rights
		{
			get;
			set;
		}

		public Int32 StarterItems
		{
			get;
			set;
		}

		public Boolean IsMember
		{
			get;
			set;
		}

		public Boolean HasLearnedCombat
		{
			get;
			set;
		}

		public Int32 Messages
		{
			get;
			set;
		}

		public String LastConnection
		{
			get;
			set;
		}

		public Int32 LastLogin
		{
			get;
			set;
		}

		public Int32 Energy
		{
			get;
			set;
		}

		public Int32 GameTime
		{
			get;
			set;
		}

		public Int32 GameCount
		{
			get;
			set;
		}

		public Int32 Ancients
		{
			get;
			set;
		}

		public Int32 Rating
		{
			get;
			set;
		}

		public Boolean IsInJail
		{
			get;
			set;
		}

		public List<CharacterEquipment> Equipment
		{
			get;
			set;
		}

		public List<CharacterAppearance> Appearance
		{
			get;
			set;
		}

		public List<CharacterSkill> Skills
		{
			get;
			set;
		}

		public List<CharacterItem> Items
		{
			get;
			set;
		}

		public List<CharacterBankItem> Bank
		{
			get;
			set;
		}

		public List<CharacterFriend> Friends
		{
			get;
			set;
		}

		public List<CharacterIgnore> Ignores
		{
			get;
			set;
		}
	}
}
