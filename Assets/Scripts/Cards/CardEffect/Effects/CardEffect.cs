using System;
using Creatures;
using UnityEngine;

namespace Cards
{
	[Serializable]
	public abstract class CardEffect
	{
		public abstract Sprite Icon { get; }
		public abstract CardEffectType Type { get; }
		public abstract bool ShouldBeProcessedOnAdd { get; }
		public abstract int TurnsDuration { get; }

		public abstract void ProcessCardEffect(Creature target);
		public abstract void Dispose();
	}
}
