using System;
using System.Collections.Generic;
using Cards;
using CoreGameplay;
using UnityEngine;

namespace Creatures
{
	public class CreaturesController : MonoBehaviour
	{
		[SerializeField] private GameTeamType team;
		[SerializeField] private CardConfigsContainer availableCards;
		[SerializeField] private List<Creature> creatures;

		public event Action AllCreaturesActed;

		private void Start()
		{
			GameController.Instance.TurnStarted += OnTurnStarted;
			foreach (var creature in creatures)
				creature.Died += () => { creatures.Remove(creature); };
		}

		private void OnDestroy()
		{
			GameController.Instance.TurnStarted -= OnTurnStarted;
		}

		private void OnTurnStarted(GameTeamType teamTurn)
		{
			if (teamTurn != team)
				return;
		}
	}
}
