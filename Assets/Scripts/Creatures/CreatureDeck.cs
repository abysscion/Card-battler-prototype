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
			_creature = creature;
			_cardsConfigs = configsContainer;
			_team = team;
			GameController.Instance.TurnStarted += OnTurnStarted;
			GameController.Instance.TurnEnded += OnTurnEnded;
		}

		~CreatureDeck()
		{
			GameController.Instance.TurnStarted -= OnTurnStarted;
			GameController.Instance.TurnEnded -= OnTurnEnded;
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
				Object.Destroy(_spawnedCard.gameObject);
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
