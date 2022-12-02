using UnityEngine;

namespace Cards
{
	public abstract class CardBase : MonoBehaviour
	{
		[SerializeField] protected CardDragNDropHelper dragNDropHelper;
		[SerializeField] protected CardConfig config;
	}
}
