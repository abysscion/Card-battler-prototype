using UnityEngine;

namespace Cards
{
	[CreateAssetMenu(fileName = "NewCardConfig", menuName = "Configs/Create card config")]
	public class CardConfig : ScriptableObject
	{
		[SerializeField] private CardType type;

		public CardType Type => type;
	}
}
