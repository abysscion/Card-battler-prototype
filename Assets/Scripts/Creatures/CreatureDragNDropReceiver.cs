using UnityEngine;

namespace Creatures
{
	public class CreatureDragNDropReceiver : MonoBehaviour
	{
		[SerializeField] private Creature creature;

		public Creature Creature => creature;
	}
}
