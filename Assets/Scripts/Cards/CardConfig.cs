using System;
using System.Collections.Generic;
using Creatures;
using UnityEngine;

namespace Cards
{
	[CreateAssetMenu(fileName = "NewCardConfig", menuName = "Configs/Create card config")]
	public class CardConfig : ScriptableObject
	{
		[SerializeField] private CardType type;
		[SerializeField] private List<CardStatModifier> modifiers = new List<CardStatModifier>();

		public CardType Type => type;

		public CardStatModifier[] GetStatModifiers()
		{
			var result = new CardStatModifier[modifiers.Count];
			modifiers.CopyTo(result);
			return result;
		}

		[Serializable]
		public class CardStatModifier
		{
			[SerializeField] private CreatureStatType type;
			[SerializeField] private float value;
			[SerializeField] private int duration;

			public CreatureStatType Type => type;
			public float Value => value;
			public int Duration => duration;
		}
	}
}
