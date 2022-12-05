using System;
using Creatures;

namespace Cards
{
	[Serializable]
	public abstract class CardEffect
	{
		public abstract CardEffectType Type { get; }
		public abstract bool ShouldBeProcessedOnAdd { get; }
		public abstract int TurnsDuration { get; }
		public abstract void ProcessCardEffect(Creature target);
	}
}
