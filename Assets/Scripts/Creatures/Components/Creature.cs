using System;
using UnityEngine;

namespace Creatures
{
	public class Creature : MonoBehaviour
	{
		[SerializeField] private CreatureConfig config;
		[SerializeField] private Transform cardSpawnPoint;

		private CreatureEffectsController _effectsController;
		private CreatureStatsContainer _stats;

		public event Action Died;

		public CreatureEffectsController EffectsController => _effectsController;
		public CreatureStatsContainer Stats => _stats;
		public Transform CardSpawnPoint => cardSpawnPoint;

		private void Awake()
		{
			_stats = new CreatureStatsContainer(config, out var ChangeStatMethod);
			_effectsController = new CreatureEffectsController(this, ChangeStatMethod);
			Stats.AddSubscriberToValueChanged(CreatureStatType.Health, OnHealthChanged);
		}

		private void OnHealthChanged(float value)
		{
			if (value <= 0)
			{
				Died?.Invoke();
				Destroy(gameObject);
			}
		}
	}
}
