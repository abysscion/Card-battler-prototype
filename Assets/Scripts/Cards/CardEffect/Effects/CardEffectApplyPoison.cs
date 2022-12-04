using System;
using Creatures;

namespace Cards
{
	[Serializable]
	public class CardEffectApplyPoison : CardEffect
	{
		public override int TurnsDuration => throw new NotImplementedException();

		public override void ProcessCardEffect(Creature target)
		{
			throw new NotImplementedException();
		}
	}
}
