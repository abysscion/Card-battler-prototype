using UnityEngine;

namespace Creatures
{
	public class Creature : MonoBehaviour
	{
		[SerializeField] private CreatureConfig config;

		private CreatureDamageReceiver damageReceiver;
		private int _health;

		public System.Action HealthChanged;
		public System.Action Died;

		public CreatureDamageReceiver DamageReceiver => damageReceiver;
		public int HealthMax => config.HealthMax;
		public int Health => _health;

		private void Awake()
		{
			_health = config.HealthMax;
			damageReceiver = new CreatureDamageReceiver(this, SetHealth);
		}

		private void SetHealth(int newValue)
		{
			_health = Mathf.Clamp(newValue, 0, HealthMax);
			HealthChanged?.Invoke();
			if (_health <= 0)
			{
				Died?.Invoke();
				Destroy(gameObject);
			}
		}
	}
}
