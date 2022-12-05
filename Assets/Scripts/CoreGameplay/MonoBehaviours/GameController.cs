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

		public static event Action<GameTeamType> TurnStarted;
		public static event Action<GameTeamType> TurnEnded;
		public static event Action<GameTeamType> TeamWon;

		public GameTeamType CurrentTeamTurn { get; private set; }

		public override void Initialize()
		{
			playerCreatures.AllCreaturesActed += OnAllCreaturesActed;
			aiCreatures.AllCreaturesActed += OnAllCreaturesActed;
			playerCreatures.AllCreaturesDied += OnAllCreaturesDied;
			aiCreatures.AllCreaturesDied += OnAllCreaturesDied;
			MainUICanvasController.Instance.EndTurnButtonClicked.AddListener(OnEndTurnButtonClicked);

			StartNextTurn();
		}

		private void OnDestroy()
		{
			playerCreatures.AllCreaturesActed -= OnAllCreaturesActed;
			aiCreatures.AllCreaturesActed -= OnAllCreaturesActed;
			playerCreatures.AllCreaturesDied -= OnAllCreaturesDied;
			aiCreatures.AllCreaturesDied -= OnAllCreaturesDied;
			MainUICanvasController.Instance.EndTurnButtonClicked.RemoveListener(OnEndTurnButtonClicked);
			TurnStarted = null;
			TurnEnded = null;
			TeamWon = null;
		}

		private void OnAllCreaturesActed(GameTeamType team)
		{
			if (team == GameTeamType.AI)
				StartNextTurn();
		}

		private void OnAllCreaturesDied(GameTeamType team)
		{
			TeamWon?.Invoke(team == GameTeamType.AI ? GameTeamType.Player : GameTeamType.AI);
		}

		private void OnEndTurnButtonClicked()
		{
			if (CurrentTeamTurn == GameTeamType.Player)
				StartNextTurn();
		}

		private void StartNextTurn()
		{
			TurnEnded?.Invoke(CurrentTeamTurn);
			CurrentTeamTurn = CurrentTeamTurn == GameTeamType.AI ? GameTeamType.Player : GameTeamType.AI;
			TurnStarted?.Invoke(CurrentTeamTurn);
		}

		private void Update()
		{
			if (!Input.anyKeyDown)
				return;

			if (Input.GetKeyDown(KeyCode.R))
				SceneManager.LoadScene(0);
			if (Input.GetKeyDown(KeyCode.Space))
				OnEndTurnButtonClicked();
		}
	}
}
