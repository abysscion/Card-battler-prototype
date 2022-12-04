using System;
using CoreGameplay;
using UnityEngine;

namespace Creatures
{
	public class Creature : MonoBehaviour
	{
		[SerializeField] private CreatureConfig config;
		[SerializeField] private Transform cardSpawnPoint;
		[SerializeField] private GameTeamType team;

		private CreatureEffectsController _effectsController;
		private CreatureStatsContainer _stats;

		public event Action Died;

		public CreatureEffectsController EffectsController => _effectsController;
		public CreatureStatsContainer Stats => _stats;
		public Transform CardSpawnPoint => cardSpawnPoint;
		public GameTeamType Team => team;

		private void Awake()
		{
			_stats = new CreatureStatsContainer(config);
			_effectsController = new CreatureEffectsController(this);
			Stats.AddSubscriberToValueChanged(CreatureStatType.Health, OnHealthChanged);
		}

		private void OnHealthChanged(float value)
		{
			if (value <= 0)
				Died?.Invoke();
		}
	}
}
