using UnityEngine;

namespace Cards
{
	public abstract class CardBase : MonoBehaviour
	{
		[SerializeField] protected CardDragNDropProvider dragNDropProvider;
		[SerializeField] protected CardConfig config;
	}
}
