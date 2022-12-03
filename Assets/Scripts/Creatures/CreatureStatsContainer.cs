using System;
using System.Collections.Generic;

namespace Creatures
{
	[System.Serializable]
	public class CreatureStatsContainer
	{
		private Dictionary<CreatureStatType, CreatureStat> _statTypeToValueDic;

		public delegate void ChangeStat(CreatureStatType type, float value);

		public event Action<CreatureStatType, float> AnyStatChanged;

		public CreatureStatsContainer(CreatureConfig config, out ChangeStat statChanger)
		{
			var allStats = Enum.GetValues(typeof(CreatureStatType));

			_statTypeToValueDic = new Dictionary<CreatureStatType, CreatureStat>(allStats.Length);
			foreach (int statTypeValue in allStats)
			{
				if (statTypeValue < 0)
					continue;

				var newStatType = (CreatureStatType)statTypeValue;
				var newStat = new CreatureStat(newStatType, 0f);

				_statTypeToValueDic.Add(newStat.type, newStat);
				newStat.ValueChanged += (changedValue) => AnyStatChanged?.Invoke(newStatType, changedValue);
			}

			//it's possible to autofill stats container through config
			// if config will contain stats as a parsable container
			_statTypeToValueDic[CreatureStatType.MaxHealth].Value = config.HealthMax;
			_statTypeToValueDic[CreatureStatType.Health].Value = config.HealthMax;
			statChanger = SetStatValue;
		}

		public float GetValue(CreatureStatType type)
		{
			return _statTypeToValueDic[type].Value;
		}

		public void AddSubscriberToValueChanged(CreatureStatType type, Action<float> OnChanged)
		{
			_statTypeToValueDic[type].ValueChanged += OnChanged;
		}

		public void RemoveSubscriberFromValueChanged(CreatureStatType type, Action<float> OnChanged)
		{
			_statTypeToValueDic[type].ValueChanged -= OnChanged;
		}

		private void SetStatValue(CreatureStatType type, float value)
		{
			_statTypeToValueDic[type].Value = value;
		}
	}
}
