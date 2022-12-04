using Cards;
using CoreGameplay;
using UnityEngine;

namespace Creatures
{
	public class CreatureDeck
	{
		private CardConfigsContainer _cardsConfigs;
		private Creature _creature;
		private GameTeamType _team;

		public event System.Action CardUsed;

		public Creature Creature => _creature;

		public CreatureDeck(Creature creature, CardConfigsContainer configsContainer, GameTeamType team)
		{
			_creature = creature;
			_cardsConfigs = configsContainer;
			_team = team;
			GameController.Instance.TurnStarted += OnTurnStarted;
		}

		~CreatureDeck()
		{
			GameController.Instance.TurnStarted -= OnTurnStarted;
		}

		private void OnTurnStarted(GameTeamType team)
		{
			if (_team != team)
				return;

			var prefabToSpawn = _cardsConfigs.GetRandomCardConfig().Prefab;
			var spawnedCard = Object.Instantiate(prefabToSpawn, _creature.CardSpawnPoint.position, Quaternion.identity);

			spawnedCard.transform.SetParent(_creature.transform);
			spawnedCard.DragNDropProvider.DraggedOverCreature += (targetCreature) => OnDragOverCreature(targetCreature, spawnedCard);
		}

		private void OnDragOverCreature(Creature targetCreature, Card sourceCard)
		{
			var cardConfig = sourceCard.Config;
			var effectsApplied = false;
			switch (cardConfig.TargetType)
			{
				case CardTargetType.Self:
					if (targetCreature == _creature)
						effectsApplied = ApplyEffects(cardConfig.GetCardEffects(), targetCreature);
					break;
				case CardTargetType.Ally:
					if (targetCreature.Team == _creature.Team)
						effectsApplied = ApplyEffects(cardConfig.GetCardEffects(), targetCreature);
					break;
				case CardTargetType.Enemy:
					if (targetCreature.Team != _creature.Team)
						effectsApplied = ApplyEffects(cardConfig.GetCardEffects(), targetCreature);
					break;
			}

			if (effectsApplied)
			{
				Object.Destroy(sourceCard.gameObject);
				CardUsed?.Invoke();
			}
		}

		private bool ApplyEffects(CardEffect[] effects, Creature targetCreature)
		{
			foreach (var effect in effects)
				targetCreature.EffectsController.TryApplyEffect(effect);
			return true;
		}
	}
}
