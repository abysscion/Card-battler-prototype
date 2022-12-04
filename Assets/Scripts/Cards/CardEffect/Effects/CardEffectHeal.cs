using Creatures;
using UnityEngine;

namespace Cards
{
	public class CardEffectHeal : CardEffect
	{
		private float _healValue;
		private int _duration;

		public override int TurnsDuration => _duration;

		public CardEffectHeal(CardEffectConfig config)
		{
			_healValue = config.Value;
			_duration = config.TurnsDuration;
		}

		public override void ProcessCardEffect(Creature targetCreature)
		{
			var stats = targetCreature.Stats;
			var missingHealth = stats[CreatureStatType.MaxHealth] - stats[CreatureStatType.Health];
			stats.AddValue(CreatureStatType.Health, Mathf.Clamp(_healValue, 0f, missingHealth));
		}
	}
}
