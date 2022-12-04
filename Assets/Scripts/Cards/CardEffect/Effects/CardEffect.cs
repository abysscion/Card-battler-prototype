using System;
using Creatures;

namespace Cards
{
	[Serializable]
	public abstract class CardEffect
	{
		public abstract int TurnsDuration { get; }
		public abstract void ProcessCardEffect(Creature target);
	}
}
