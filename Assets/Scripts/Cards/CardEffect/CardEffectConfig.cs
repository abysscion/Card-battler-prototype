using System;
using System.Collections.Generic;
using Creatures;
using UnityEngine;

namespace Cards
{
	[Serializable]
	public class CardEffectConfig
	{
		[SerializeField] private List<CreatureStatModifier> statModifiers;
		[SerializeField] private CardEffectTargetType targetType;
		[SerializeField] private CardEffectType effectType;
		[SerializeField] private float value;
		[SerializeField] private int turnsDuration;

		public CardEffectTargetType TargetType => targetType;
		public CardEffectType EffectType => effectType;
		public float Value => value;
		public int TurnsDuration => turnsDuration;

		public CreatureStatModifier[] GetStatModifiers()
		{
			var result = new CreatureStatModifier[statModifiers.Count];
			statModifiers.CopyTo(result);
			return result;
		}
	}
}
