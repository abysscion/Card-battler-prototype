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

		public override void Initialize()
		{

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
