using System;
using Creatures;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.Components;

namespace CoreGameplay
{
	public class GameController : MonoSingleton<GameController>
	{
		[SerializeField] private CreaturesController playerCreatures;
		[SerializeField] private CreaturesController aiCreatures;

		public event Action<GameTeamType> TurnStarted;
		public event Action<GameTeamType> TurnEnded;
		public event Action<GameTeamType> TeamWon;

		public GameTeamType CurrentTeamTurn { get; private set; }

		public override void Initialize()
		{
			playerCreatures.AllCreaturesActed += OnAllCreaturesActed;
			aiCreatures.AllCreaturesActed += OnAllCreaturesActed;
			playerCreatures.AllCreaturesDied += OnAllCreaturesDied;
			aiCreatures.AllCreaturesDied += OnAllCreaturesDied;

			StartNextTurn();
		}

		private void OnDestroy()
		{
			playerCreatures.AllCreaturesDied -= OnAllCreaturesDied;
			aiCreatures.AllCreaturesDied -= OnAllCreaturesDied;
		}

		private void OnAllCreaturesActed(GameTeamType team)
		{
			TurnEnded?.Invoke(team);
			StartNextTurn();
		}

		private void OnAllCreaturesDied(GameTeamType team)
		{
			TeamWon?.Invoke(team == GameTeamType.AI ? GameTeamType.Player : GameTeamType.AI);
		}

		private void StartNextTurn()
		{
			CurrentTeamTurn = CurrentTeamTurn == GameTeamType.AI ? GameTeamType.Player : GameTeamType.AI;
			TurnStarted?.Invoke(CurrentTeamTurn);
		}

		private void Update()
		{
			if (!Input.anyKeyDown)
				return;

			if (Input.GetKeyDown(KeyCode.R))
				SceneManager.LoadScene(0);
		}
	}
}
