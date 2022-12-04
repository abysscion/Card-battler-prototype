using System;
using System.Collections.Generic;

namespace Creatures
{
	[System.Serializable]
	public class CreatureStatsContainer
	{
		private Dictionary<CreatureStatType, CreatureStat> _statTypeToStatDic;

		public event Action<CreatureStatType, float> AnyStatChanged;

		public float this[CreatureStatType statType]
		{
			get => _statTypeToStatDic[statType].Value;
		}

		public CreatureStatsContainer(CreatureConfig config)
		{
			var allStats = Enum.GetValues(typeof(CreatureStatType));

			_statTypeToStatDic = new Dictionary<CreatureStatType, CreatureStat>(allStats.Length);
			foreach (int statTypeValue in allStats)
			{
				if (statTypeValue < 0)
					continue;

				var newStatType = (CreatureStatType)statTypeValue;
				var newStat = new CreatureStat(newStatType, 0f);

				_statTypeToStatDic.Add(newStat.type, newStat);
				newStat.ValueChanged += (changedValue) => AnyStatChanged?.Invoke(newStatType, newStat.Value);
			}

			//it's possible to autofill stats container through config
			// if config will contain stats as a parsable container
			_statTypeToStatDic[CreatureStatType.MaxHealth].Value = config.HealthMax;
			_statTypeToStatDic[CreatureStatType.Health].Value = config.HealthMax;
		}

		public float GetValue(CreatureStatType type)
		{
			return _statTypeToStatDic[type].Value;
		}

		//way to strict stat set method access?
		public void SetValue(CreatureStatType type, float value)
		{
			_statTypeToStatDic[type].Value = value;
		}

		public void AddValue(CreatureStatType type, float value)
		{
			_statTypeToStatDic[type].Value += value;
		}

		public void AddSubscriberToValueChanged(CreatureStatType type, Action<float> OnChanged)
		{
			_statTypeToStatDic[type].ValueChanged += OnChanged;
		}

		public void RemoveSubscriberFromValueChanged(CreatureStatType type, Action<float> OnChanged)
		{
			_statTypeToStatDic[type].ValueChanged -= OnChanged;
		}
	}
}
