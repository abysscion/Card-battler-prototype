using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	[CreateAssetMenu(fileName = "NewCardConfig", menuName = "Configs/Create card config")]
	public class CardConfig : ScriptableObject
	{
		[SerializeField] private CardType type;
		[SerializeField] private GameObject prefab;
		[SerializeField] private List<CardEffect> effects = new List<CardEffect>();

		public CardType Type => type;
		public GameObject Prefab => prefab;

		public CardEffect[] GetCardEffects()
		{
			var result = new CardEffect[effects.Count];
			effects.CopyTo(result);
			return result;
		}
	}
}
