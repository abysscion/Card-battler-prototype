using System.Collections.Generic;
using Cards;
using CoreGameplay;
using static Cards.CardActiveEffect;

namespace Creatures
{
	public class CreatureEffectsController
	{
		private List<ActiveEffectWrapper> _activeEffects;
		private Creature _host;

		public event System.Action<CardActiveEffect> ActiveEffectAdded;
		public event System.Action<CardActiveEffect> ActiveEffectRemoved;

		public CreatureEffectsController(Creature creature)
		{
			_activeEffects = new List<ActiveEffectWrapper>();
			_host = creature;
			GameController.TurnEnded += OnTurnEnded;
		}

		~CreatureEffectsController()
		{
			GameController.TurnEnded -= OnTurnEnded;
		}

		public bool TryApplyEffect(CardEffect effect)
		{
			if (effect.TurnsDuration == 0)
				effect.ProcessCardEffect(_host);
			else if (effect.TurnsDuration > 0)
			{
				var activeEffect = new CardActiveEffect(effect, effect.TurnsDuration, out var turnsCountSetter);
				var effectWrapper = new ActiveEffectWrapper(activeEffect, turnsCountSetter);

				_activeEffects.Add(effectWrapper);
				ActiveEffectAdded?.Invoke(activeEffect);
			}
			else
				return false;
			return true;
		}

		public bool TryDispelEffects(CardEffectType effectTypeToDispell)
		{
			var dispelledAtLeastOneEffect = false;

			for (int i = 0; i < _activeEffects.Count; i++)
			{
				if (_activeEffects[i].activeEffect.Effect.Type != effectTypeToDispell)
					continue;

				dispelledAtLeastOneEffect = true;
				ActiveEffectRemoved?.Invoke(_activeEffects[i].activeEffect);
				_activeEffects.RemoveAt(i);
			}

			return dispelledAtLeastOneEffect;
		}

		private void OnTurnEnded(GameTeamType teamTurn)
		{
			for (var i = 0; i < _activeEffects.Count; i++)
			{
				var activeEffect = _activeEffects[i].activeEffect;

				activeEffect.Effect.ProcessCardEffect(_host);
				_activeEffects[i].turnsCountSetter.Invoke(activeEffect.TurnsRemaining - 1);
				if (activeEffect.TurnsRemaining <= 0)
					continue;

				ActiveEffectRemoved?.Invoke(_activeEffects[i].activeEffect);
				_activeEffects.RemoveAt(i);
			}
		}

		private class ActiveEffectWrapper
		{
			public CardActiveEffect activeEffect;
			public TurnsCountSetter turnsCountSetter;

			public ActiveEffectWrapper(CardActiveEffect activeEffect, TurnsCountSetter turnsCountSetter)
			{
				this.activeEffect = activeEffect;
				this.turnsCountSetter = turnsCountSetter;
			}
		}
	}
}
