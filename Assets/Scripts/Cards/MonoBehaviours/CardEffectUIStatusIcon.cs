using Cards;
using CoreGameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardEffectUIStatusIcon : MonoBehaviour
{
	[SerializeField] private TMP_Text remainingTurnsCounterText;
	[SerializeField] private Image iconImage;

	private CardActiveEffect _activeEffect;

	public void Initialize(CardActiveEffect activeEffect)
	{
		_activeEffect = activeEffect;
		iconImage.sprite = _activeEffect.Effect?.Icon;
		remainingTurnsCounterText.text = $"{_activeEffect.TurnsRemaining}";
		GameController.TurnEnded += OnAnyTurnEnded;
	}

	private void OnDestroy()
	{
		GameController.TurnEnded -= OnAnyTurnEnded;
	}

	private void OnAnyTurnEnded(GameTeamType _)
	{
		remainingTurnsCounterText.text = $"{_activeEffect.TurnsRemaining}";
	}
}
