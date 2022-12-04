using System;
using System.Collections.Generic;
using Cards;
using CoreGameplay;
using UnityEngine;

namespace Creatures
{
	public class CreaturesController : MonoBehaviour
	{
		[SerializeField] private CardConfigsContainer availableCards;
		[SerializeField] private List<Creature> creatures;
		[SerializeField] private GameTeamType team;

		private Dictionary<Creature, CreatureDeck> _creatureToCreaturesDecksDic;
		private HashSet<Creature> _actedCreatures;

		public event Action<GameTeamType> AllCreaturesActed;
		public event Action<GameTeamType> AllCreaturesDied;

		public GameTeamType Team => team;

		private void Start()
		{
			_creatureToCreaturesDecksDic = new Dictionary<Creature, CreatureDeck>();
			_actedCreatures = new HashSet<Creature>();

			for (int i = 0; i < creatures.Count; i++)
			{
				if (!TryDisableInappropriateTeamCreature(creatures[i]))
				{
					var creatureDeck = new CreatureDeck(creatures[i], availableCards, team);
					_creatureToCreaturesDecksDic.Add(creatures[i], creatureDeck);
					creatureDeck.CardUsed += () => OnCreatureDeckUsedCard(creatureDeck);
					creatures[i].Died += () => OnCreatureDied(creatures[i]);
				}
				else
				{
					creatures.Remove(creatures[i]);
					Debug.LogWarning($"{creatures[i].name} was disabled due to mismatching team.");
				}
			}

			GameController.Instance.TurnStarted += OnTurnStarted;
			GameController.Instance.TurnEnded += OnTurnEnded;
		}

		private void OnDestroy()
		{
			GameController.Instance.TurnStarted -= OnTurnStarted;
			GameController.Instance.TurnEnded -= OnTurnEnded;
		}

		private void OnTurnStarted(GameTeamType teamTurn)
		{
			if (teamTurn != team)
				return;
		}

		private void OnTurnEnded(GameTeamType teamTurn)
		{
			if (teamTurn != team)
				return;

			_actedCreatures.Clear();
		}

		private void OnCreatureDied(Creature creature)
		{
			creatures.Remove(creature);
			Destroy(creature.gameObject);
			if (creatures.Count <= 0)
				AllCreaturesDied?.Invoke(team);
		}

		private void OnCreatureDeckUsedCard(CreatureDeck creatureDeck)
		{
			_actedCreatures.Add(creatureDeck.Creature);
			if (_actedCreatures.Count == _creatureToCreaturesDecksDic.Keys.Count)
				AllCreaturesActed?.Invoke(team);
		}

		private bool TryDisableInappropriateTeamCreature(Creature creature)
		{
			if (creature.Team != Team)
			{
				creature.gameObject.SetActive(false);
				return true;
			}
			return false;
		}
	}
}
