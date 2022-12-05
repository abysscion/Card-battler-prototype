using CoreGameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Components;

namespace Creatures
{
	public class MainUICanvasController : MonoSingleton<MainUICanvasController>
	{
		[SerializeField] private TMP_Text currentTurnText;
		[SerializeField] private Button endTurnButton;

		public Button.ButtonClickedEvent EndTurnButtonClicked => endTurnButton.onClick;

		public override void Initialize()
		{
			GameController.TurnStarted += OnTurnStarted;
			OnTurnStarted(GameController.Instance.CurrentTeamTurn);
		}

		private void OnDestroy()
		{
			GameController.TurnStarted -= OnTurnStarted;
		}

		private void OnTurnStarted(GameTeamType teamTurn)
		{
			endTurnButton.gameObject.SetActive(teamTurn == GameTeamType.Player);
			currentTurnText.text = $"{teamTurn}";
		}
	}
}
