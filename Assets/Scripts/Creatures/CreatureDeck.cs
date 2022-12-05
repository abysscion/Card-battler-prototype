using System.Collections.Generic;
using Cards;
using CoreGameplay;
using UnityEngine;

namespace Creatures
{
	public class CreatureDeck
	{
		private CardConfigsContainer _cardsConfigs;
		private Creature _creature;
		private Card _spawnedCard;
		private GameTeamType _team;

		public event System.Action CardUsed;

		public Creature Creature => _creature;

		public CreatureDeck(Creature creature, CardConfigsContainer configsContainer, GameTeamType team)
		{
			_cardsConfigs = configsContainer;
			_creature = creature;
			_team = team;

			_creature.Died += OnCreatureDided;
			GameController.TurnStarted += OnTurnStarted;
			GameController.TurnEnded += OnTurnEnded;
		}

		private void OnCreatureDided()
		{
			GameController.TurnStarted -= OnTurnStarted;
			GameController.TurnEnded -= OnTurnEnded;
		}

		private void OnTurnStarted(GameTeamType team)
		{
			if (_team != team)
				return;

			var prefabToSpawn = _cardsConfigs.GetRandomCardConfig().Prefab;

			_spawnedCard = Object.Instantiate(prefabToSpawn, _creature.CardSpawnPoint.position, Quaternion.identity);
			_spawnedCard.transform.SetParent(_creature.transform);
			_spawnedCard.DragNDropProvider.DraggedOverCreature += OnDragOverCreature;
		}

		private void OnTurnEnded(GameTeamType team)
		{
			if (_spawnedCard)
				Object.Destroy(_spawnedCard.gameObject);
		}

		private void OnDragOverCreature(Creature targetCreature)
		{
			var cardConfig = _spawnedCard.Config;
			var cardDraggedOverCorrectCreature = cardConfig.TargetType switch
			{
				CardTargetType.Self => _creature == targetCreature,
				CardTargetType.Ally => _creature.Team == targetCreature.Team,
				CardTargetType.Enemy => targetCreature.Team != _creature.Team,
				_ => throw new System.NotImplementedException()
			};
			if (cardDraggedOverCorrectCreature)
			{
				ApplyCardEffects(targetCreature, cardConfig.GetCardEffectConfigs());
				Object.Destroy(_spawnedCard.gameObject);
				CardUsed?.Invoke();
			}
		}

		private void ApplyCardEffects(Creature targetCreature, ICollection<CardEffectConfig> effectConfigs)
		{
			foreach (var effectConfig in effectConfigs)
			{
				if (!CardEffectFactory.TryCreateEffectByConfig(effectConfig, out var effect))
					continue;

				switch (effectConfig.TargetType)
				{
					case CardEffectTargetType.Self:
						_creature.EffectsController.TryApplyEffect(effect);
						break;
					case CardEffectTargetType.Other:
						targetCreature.EffectsController.TryApplyEffect(effect);
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
		}
	}
}
