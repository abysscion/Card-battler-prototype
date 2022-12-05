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
		[SerializeField] private List<CardEffectType> dispellableEffectTypes;
		[SerializeField] private Sprite icon;
		[SerializeField] private CardEffectTargetType targetType;
		[SerializeField] private CardEffectType effectType;
		[SerializeField] private float value;
		[SerializeField] private bool shouldBeProcessedOnAdd;
		[SerializeField] private int turnsDuration;

		public Sprite Icon => icon;
		public CardEffectTargetType TargetType => targetType;
		public CardEffectType EffectType => effectType;
		public float Value => value;
		public bool ShouldBeProcessedOnAdd => shouldBeProcessedOnAdd;
		public int TurnsDuration => turnsDuration;

		public CreatureStatModifier[] GetStatModifiers()
		{
			var result = new CreatureStatModifier[statModifiers.Count];
			statModifiers.CopyTo(result);
			return result;
		}

		public CardEffectType[] GetDispellableEffectTypes()
		{
			var result = new CardEffectType[dispellableEffectTypes.Count];
			dispellableEffectTypes.CopyTo(result);
			return result;
		}
	}
}
