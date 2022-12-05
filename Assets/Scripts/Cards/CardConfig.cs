using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	[CreateAssetMenu(fileName = "NewCardConfig", menuName = "Configs/Create card config")]
	public class CardConfig : ScriptableObject
	{
		[SerializeField] private List<CardEffectConfig> effectConfigs = new List<CardEffectConfig>();
		[SerializeField] private Card prefab;
		[SerializeField] private CardTargetType targetType;

		public CardTargetType TargetType => targetType;
		public Card Prefab => prefab;

		public CardEffectConfig[] GetCardEffectConfigs()
		{
			var result = new CardEffectConfig[effectConfigs.Count];
			effectConfigs.CopyTo(result);
			return result;
		}
	}
}
