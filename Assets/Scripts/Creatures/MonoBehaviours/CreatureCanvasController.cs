using System.Collections.Generic;
using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Creatures
{
	public class CreatureCanvasController : MonoBehaviour
	{
		[SerializeField] private CardEffectUIStatusIcon statusIconPrefab;
		[SerializeField] private LayoutGroup iconsGroup;
		[SerializeField] private Creature creature;
		[SerializeField] private TMP_Text healthText;
		[SerializeField] private TMP_Text shieldText;
		[SerializeField] private Image healthFillImage;
		[SerializeField] private Image shieldFillImage;

		private Dictionary<CardActiveEffect, CardEffectUIStatusIcon> _effectToSpawnedIconItem;

		private void Start()
		{
			_effectToSpawnedIconItem = new Dictionary<CardActiveEffect, CardEffectUIStatusIcon>();
			UpdateView();
			creature.EffectsController.ActiveEffectRemoved += OnActiveEffectRemoved;
			creature.EffectsController.ActiveEffectAdded += OnActiveEffectAdded;
			creature.Stats.AnyStatChanged += OnStatChanged;
		}

		private void OnDestroy()
		{
			creature.EffectsController.ActiveEffectRemoved -= OnActiveEffectRemoved;
			creature.EffectsController.ActiveEffectAdded -= OnActiveEffectAdded;
			creature.Stats.AnyStatChanged -= OnStatChanged;
		}

		private void OnStatChanged(CreatureStatType _, float __) => UpdateView();

		private void OnActiveEffectAdded(CardActiveEffect addedEffect)
		{
			if (_effectToSpawnedIconItem.ContainsKey(addedEffect))
				return;

			var spawnedItem = Instantiate(statusIconPrefab, iconsGroup.transform);

			spawnedItem.Initialize(addedEffect);
			_effectToSpawnedIconItem.Add(addedEffect, spawnedItem);
		}

		private void OnActiveEffectRemoved(CardActiveEffect removedEffect)
		{
			if (_effectToSpawnedIconItem.TryGetValue(removedEffect, out var spawnedItem))
			{
				_effectToSpawnedIconItem.Remove(removedEffect);
				Destroy(spawnedItem.gameObject);
			}
		}

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
