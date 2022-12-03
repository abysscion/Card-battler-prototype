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

			healthText.text = $"{Mathf.CeilToInt(healthValue)}/{Mathf.CeilToInt(maxHealthValue)}";
			healthFillImage.fillAmount = healthValue / maxHealthValue;
		}
	}
}
