using Creatures;
using UnityEngine;

namespace Cards
{
	public class CardEffectHeal : CardEffect
	{
		private CardEffectType[] _dispellableTypes;
		private CardEffectType _effectType;
		private bool _shouldBeProcessedOnAdd;
		private float _healValue;
		private int _duration;

		public override CardEffectType Type => _effectType;
		public override bool ShouldBeProcessedOnAdd => _shouldBeProcessedOnAdd;
		public override int TurnsDuration => _duration;

		public CardEffectHeal(CardEffectConfig config)
		{
			_dispellableTypes = config.GetDispellableEffectTypes();
			_shouldBeProcessedOnAdd = config.ShouldBeProcessedOnAdd;
			_effectType = config.EffectType;
			_healValue = config.Value;
			_duration = config.TurnsDuration;
		}

		public override void ProcessCardEffect(Creature targetCreature)
		{
			var stats = targetCreature.Stats;
			var missingHealth = stats[CreatureStatType.MaxHealth] - stats[CreatureStatType.Health];

			foreach (var dispellableType in _dispellableTypes)
				targetCreature.EffectsController.TryDispelEffectsOfType(dispellableType);

			stats.AddValue(CreatureStatType.Health, Mathf.Clamp(_healValue, 0f, missingHealth));
		}
	}
}
