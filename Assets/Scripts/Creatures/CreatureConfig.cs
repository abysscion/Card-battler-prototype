using UnityEngine;

namespace Creatures
{
	[CreateAssetMenu(fileName = "NewCreatureConfig", menuName = "Configs/Create creature config")]
	public class CreatureConfig : ScriptableObject
	{
		//possible adjustment: create container with CreatureStat items and validate in OnValidate() or through CustomEditor
		[SerializeField] private int healthMax = 30;

		public int HealthMax => healthMax;
	}
}
