using System;

namespace Creatures
{
	[Serializable]
	public struct CreatureStatModifier
	{
		public CreatureStatType type;
		public float value;

		public CreatureStatModifier(CreatureStatType type, float value)
		{
			this.type = type;
			this.value = value;
		}
	}
}
