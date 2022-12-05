using Creatures;

namespace Cards
{
	public class CardEffectApplyPoison : CardEffect
	{
		private CardEffectType _effectType;
		private float _damageValue;
		private int _duration;
		private bool _shouldBeProcessedOnAdd;

		public override CardEffectType Type => _effectType;
		public override bool ShouldBeProcessedOnAdd => _shouldBeProcessedOnAdd;
		public override int TurnsDuration => _duration;
		

		public CardEffectApplyPoison(CardEffectConfig config)
		{
			_shouldBeProcessedOnAdd = config.ShouldBeProcessedOnAdd;
			_effectType = config.EffectType;
			_damageValue = config.Value;
			_duration = config.TurnsDuration;
	}

		public override void ProcessCardEffect(Creature targetCreature)
		{
			targetCreature.Stats.AddValue(CreatureStatType.Health, -_damageValue);
		}

		public override void Dispose() { }
	}
}
