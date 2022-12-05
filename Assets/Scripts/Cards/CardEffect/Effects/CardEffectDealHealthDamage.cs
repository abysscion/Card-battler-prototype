using Creatures;

namespace Cards
{
	public class CardEffectDealHealthDamage : CardEffect
	{
		private CardEffectType _effectType;
		private float _damageValue;
		private bool _shouldBeProcessedOnAdd;
		private int _duration;

		public override CardEffectType Type => _effectType;
		public override bool ShouldBeProcessedOnAdd => _shouldBeProcessedOnAdd;
		public override int TurnsDuration => _duration;

		public CardEffectDealHealthDamage(CardEffectConfig config)
		{
			_shouldBeProcessedOnAdd = config.ShouldBeProcessedOnAdd;
			_effectType = config.EffectType;
			_damageValue = config.Value;
			_duration = config.TurnsDuration;
		}

		public override void ProcessCardEffect(Creature targetCreature)
		{
			var stats = targetCreature.Stats;

			if (stats.GetValue(CreatureStatType.Shield) <= 0)
				targetCreature.Stats.AddValue(CreatureStatType.Health, -_damageValue);
			else
			{
				var currentShield = stats[CreatureStatType.Shield];
				var shieldAfterDamage = currentShield - _damageValue;

				if (shieldAfterDamage >= 0)
					targetCreature.Stats.SetValue(CreatureStatType.Shield, shieldAfterDamage);
				else
				{
					targetCreature.Stats.SetValue(CreatureStatType.Shield, 0f);
					targetCreature.Stats.AddValue(CreatureStatType.Health, shieldAfterDamage);
				}
			}
		}
	}
}
