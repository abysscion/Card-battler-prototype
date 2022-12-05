using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	[CreateAssetMenu(fileName = "NewCardConfigsContainer", menuName = "Configs/Create card configs container")]
	public class CardConfigsContainer : ScriptableObject
	{
		[SerializeField] private List<CardConfig> cards = new List<CardConfig>();

		public CardConfig[] GetAllCardConfigs()
		{
			var result = new CardConfig[cards.Count];
			cards.CopyTo(result);
			return result;
		}

		public CardConfig GetRandomCardConfig()
		{
			return cards[Random.Range(0, cards.Count)];
		}
	}
}
