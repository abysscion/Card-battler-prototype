using UnityEngine;

namespace Cards
{
	public class Card : MonoBehaviour
	{
		[SerializeField] private CardDragNDropProvider dragNDropProvider;
		[SerializeField] private CardConfig config;

		public CardDragNDropProvider DragNDropProvider => dragNDropProvider;
		public CardConfig Config => config;
	}
}
