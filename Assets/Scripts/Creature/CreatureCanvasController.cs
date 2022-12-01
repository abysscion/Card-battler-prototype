using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Creature
{
	public class CreatureCanvasController : MonoBehaviour
	{
		[SerializeField] private Creature creature;
		[SerializeField] private TMP_Text healthText;
		[SerializeField] private Image healthFillImage;


		private void Start()
		{
			creature.HealthChanged += UpdateView;
			UpdateView();
		}

		private void OnDestroy()
		{
			creature.HealthChanged -= UpdateView;
		}

		private void UpdateView()
		{
			healthText.text = $"{creature.Health}/{creature.HealthMax}";
			healthFillImage.fillAmount = (float) creature.Health / creature.HealthMax;
		}
	}
}
