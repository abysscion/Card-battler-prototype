using System;
using System.Collections.Generic;
using Cards;
using static Creatures.CreatureStatsContainer;

namespace Creatures
{
	public class CreatureEffectsController
	{
		private HashSet<ActiveEffect> _activeEffects;
		private ChangeStat _changeStatMethod;
		private Creature _host;

		public CreatureEffectsController(Creature creature, ChangeStat statChanger)
		{
			_changeStatMethod = statChanger;
			_host = creature;
			_activeEffects = new HashSet<ActiveEffect>();
		}

		public bool TryApplyEffect(CardEffect effect)
		{
			if (effect.Duration == 0)
				ApplyInstantEffect(effect.Type, effect.Value);
			else if (effect.Duration > 0)
				_activeEffects.Add(new ActiveEffect(effect, effect.Duration));
			else
				return false;
			return true;
		}

		//TODO: factory of stat changers? base for common and specific for special
		private void ApplyInstantEffect(CreatureStatType type, float value)
		{
			switch (type)
			{
				case CreatureStatType.MaxHealth:
				case CreatureStatType.MaxEnergyShield:
				case CreatureStatType.EnergyShield:
					ChangeStatCommonly(type, value); break;
				case CreatureStatType.Health:
					ChangeHealthStat(type, value); break;
				default:
					break;
			}
		}

		private void ChangeHealthStat(CreatureStatType type, float value)
		{
			var stats = _host.Stats;
			if (value >= 0)
			{
				var missingHealth = stats.GetValue(CreatureStatType.MaxHealth) - stats.GetValue(CreatureStatType.Health);
				ChangeStatCommonly(type, Math.Clamp(value, 0f, missingHealth));
			}
			else if (stats.GetValue(CreatureStatType.EnergyShield) <= 0)
				ChangeStatCommonly(type, value);
			else
			{
				var curES = stats.GetValue(CreatureStatType.EnergyShield);
				var esAndDamageDelta = curES + value;

				if (esAndDamageDelta > 0)
					ChangeStatCommonly(CreatureStatType.EnergyShield, value);
				else
				{
					ChangeStatCommonly(CreatureStatType.EnergyShield, -curES);
					ChangeStatCommonly(CreatureStatType.Health, esAndDamageDelta);
				}
			}
		}

		private void ChangeStatCommonly(CreatureStatType type, float value)
		{
			_changeStatMethod.Invoke(type, _host.Stats.GetValue(type) + value);
		}

		private class ActiveEffect
		{
			public CardEffect effect;
			public int turnsLeft;

			public ActiveEffect(CardEffect effect, int turnsLeft)
			{
				this.effect = effect;
				this.turnsLeft = turnsLeft;
			}
		}
	}
}
