using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	[CreateAssetMenu(fileName = "NewCardConfig", menuName = "Configs/Create card config")]
	public class CardConfig : ScriptableObject
	{
		[SerializeField] private List<CardEffect> effects = new List<CardEffect>();
		[SerializeField] private CardTargetType targetType;
		[SerializeField] private Card prefab;
		[SerializeField] private CardType type;

		public CardTargetType TargetType => targetType;
		public CardType Type => type;
		public Card Prefab => prefab;

		public CardEffect[] GetCardEffects()
		{
			var result = new CardEffect[effects.Count];
			effects.CopyTo(result);
			return result;
		}
	}
}
