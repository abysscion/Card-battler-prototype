using System;
using Creatures;
using UnityEngine;

namespace Cards
{
	[Serializable]
	public class CardEffect
	{
		[SerializeField] private CreatureStatType type;
		[SerializeField] private float value;
		[SerializeField] private int duration;

		public CreatureStatType Type => type;
		public float Value => value;
		public int Duration => duration;
	}
}
