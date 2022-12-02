using UnityEngine;

namespace Creatures
{
	[CreateAssetMenu(fileName = "NewCreatureConfig", menuName = "Configs/Create creature config")]
	public class CreatureConfig : ScriptableObject
	{
		[SerializeField] private int healthMax = 30;

		public int HealthMax => healthMax;
	}
}
