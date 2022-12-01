using UnityEngine;

namespace Creature
{
	public class Creature : MonoBehaviour
	{
		[SerializeField] private CreatureConfig config;

		private int _health;

		public System.Action HealthChanged;
		public System.Action Died;

		public int HealthMax => config.HealthMax;

		public int Health
		{
			get => _health;
			set
			{
				_health = value;
				HealthChanged?.Invoke();
				if (_health <= 0)
				{
					Died?.Invoke();
					Destroy(gameObject);
				}
			}
		}

		private void Awake()
		{
			_health = config.HealthMax;
		}
	}
}
