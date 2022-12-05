using System.Collections.Generic;
using Creatures;
using UnityEngine;

namespace Cards
{
	public class CardEffectChangeCreatureStat : CardEffect
	{
		private HashSet<CreatureStatModifier> _statModifiers;
		private CardEffectType _effectType;
		private int _duration;

		public override CardEffectType Type => _effectType;

		public override int TurnsDuration => _duration;

		public CardEffectChangeCreatureStat(CardEffectConfig config)
		{
			_statModifiers = new HashSet<CreatureStatModifier>();
			_duration = config.TurnsDuration;
			_effectType = config.EffectType;
			foreach (var statModifier in config.GetStatModifiers())
				_statModifiers.Add(statModifier);
		}

		public override void ProcessCardEffect(Creature target)
		{
			var stats = target.Stats;
			foreach (var modifier in _statModifiers)
			{
				var statValue = modifier.value;
				switch (modifier.type)
				{
					case CreatureStatType.MaxHealth:
						stats.AddValue(CreatureStatType.MaxHealth, statValue);
						var newMaxHp = stats[CreatureStatType.MaxHealth];
						var currentHp = stats[CreatureStatType.Health];
						stats.SetValue(CreatureStatType.Health, currentHp > newMaxHp ? newMaxHp : currentHp);
						break;
					case CreatureStatType.Health:
						var missingHealth = stats[CreatureStatType.MaxHealth] - stats[CreatureStatType.Health];
						stats.AddValue(CreatureStatType.Health, Mathf.Clamp(statValue, statValue, missingHealth));
						break;
					case CreatureStatType.Shield:
						stats.AddValue(CreatureStatType.Shield, statValue);
						break;
					default:
						break;
				}
			}
		}
	}
}
