using UnityEngine;

namespace Cards
{
	public class CardBase : MonoBehaviour
	{
		[SerializeField] private CardDragNDropHelper dragNDropHelper;
		[SerializeField] private CardConfig config;
	}
}
