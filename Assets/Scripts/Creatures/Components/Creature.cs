using UnityEngine;

namespace Creatures
{
	public class Creature : MonoBehaviour
	{
		[SerializeField] private CreatureConfig config;

		private CreatureDamageReceiver _damageReceiver;
		private CreatureStatsContainer _stats;

		public System.Action Died;
		private System.Action<CreatureStatType, float> ChangeStat;

		public CreatureDamageReceiver DamageReceiver => _damageReceiver;
		public CreatureStatsContainer Stats => _stats;
		public float HealthMax => _stats.GetStatValue(CreatureStatType.HealthMax);
		public float Health => _stats.GetStatValue(CreatureStatType.Health);

		private void Awake()
		{
			_stats = new CreatureStatsContainer(config, out ChangeStat);
			_damageReceiver = new CreatureDamageReceiver(this, SetHealth);
		}

		private void SetHealth(float newValue)
		{
			ChangeStat(CreatureStatType.Health, Mathf.Clamp(newValue, 0f, HealthMax));
			if (Health <= 0)
			{
				Died?.Invoke();
				Destroy(gameObject);
			}
		}
	}
}
