using Creatures;
using UnityEngine;

namespace Cards
{
	public class CardEffectApplyPoison : CardEffect
	{
		private Sprite _icon;
		private CardEffectType _effectType;
		private float _damageValue;
		private bool _shouldBeProcessedOnAdd;
		private int _duration;

		public override Sprite Icon => _icon;
		public override CardEffectType Type => _effectType;
		public override bool ShouldBeProcessedOnAdd => _shouldBeProcessedOnAdd;
		public override int TurnsDuration => _duration;

		public CardEffectApplyPoison(CardEffectConfig config)
		{
			_shouldBeProcessedOnAdd = config.ShouldBeProcessedOnAdd;
			_effectType = config.EffectType;
			_damageValue = config.Value;
			_duration = config.TurnsDuration;
			_icon = config.Icon;
		}

		public override void ProcessCardEffect(Creature targetCreature)
		{
			targetCreature.Stats.AddValue(CreatureStatType.Health, -_damageValue);
		}

		public override void Dispose() { }
	}
}
