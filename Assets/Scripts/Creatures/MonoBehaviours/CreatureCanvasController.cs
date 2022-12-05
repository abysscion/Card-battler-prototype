using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Creatures
{
	public class CreatureCanvasController : MonoBehaviour
	{
		[SerializeField] private Creature creature;
		[SerializeField] private TMP_Text healthText;
		[SerializeField] private Image healthFillImage;
		[SerializeField] private Image shieldFillImage;
		[SerializeField] private TMP_Text shieldText;

		private void Start()
		{
			creature.Stats.AnyStatChanged += OnStatChanged;
			UpdateView();
		}

		private void OnDestroy()
		{
			creature.Stats.AnyStatChanged -= OnStatChanged;
		}

		private void OnStatChanged(CreatureStatType _, float __) => UpdateView();

		private void UpdateView()
		{
			var maxHealthValue = creature.Stats.GetValue(CreatureStatType.MaxHealth);
			var healthValue = creature.Stats.GetValue(CreatureStatType.Health);
			var shieldValue = creature.Stats.GetValue(CreatureStatType.Shield);

			healthText.text = $"{Mathf.CeilToInt(healthValue)}/{Mathf.CeilToInt(maxHealthValue)}";
			healthFillImage.fillAmount = healthValue / maxHealthValue;

			if (shieldValue <= 0)
			{
				shieldText.gameObject.SetActive(false);
				shieldFillImage.gameObject.SetActive(false);
			}
			else
			{
				shieldText.gameObject.SetActive(true);
				shieldFillImage.gameObject.SetActive(true);
				shieldText.text = $"({Mathf.CeilToInt(shieldValue)})";
				shieldFillImage.fillAmount = shieldValue / maxHealthValue;
			}
		}
	}
}
