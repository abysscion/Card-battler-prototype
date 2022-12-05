using System.Collections.Generic;
using Cards;
using CoreGameplay;
using static Cards.CardActiveEffect;

namespace Creatures
{
	public class CreatureEffectsController
	{
		private List<ActiveEffectWrapper> _activeEffectsWrappers;
		private Creature _host;

		public event System.Action<CardActiveEffect> ActiveEffectAdded;
		public event System.Action<CardActiveEffect> ActiveEffectRemoved;

		public CreatureEffectsController(Creature creature)
		{
			_activeEffectsWrappers = new List<ActiveEffectWrapper>();
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

				_activeEffectsWrappers.Add(effectWrapper);
				ActiveEffectAdded?.Invoke(activeEffect);
			}
			else
				return false;
			return true;
		}

		public bool TryDispelEffects(CardEffectType effectTypeToDispell)
		{
			var dispelledAtLeastOneEffect = false;

			for (int i = 0; i < _activeEffectsWrappers.Count; i++)
			{
				if (_activeEffectsWrappers[i].activeEffect.Effect.Type != effectTypeToDispell)
					continue;

				dispelledAtLeastOneEffect = true;
				ActiveEffectRemoved?.Invoke(_activeEffectsWrappers[i].activeEffect);
				_activeEffectsWrappers.RemoveAt(i);
				i--;
			}

			return dispelledAtLeastOneEffect;
		}

		public CardActiveEffect[] GetActiveEffects()
		{
			var result = new CardActiveEffect[_activeEffectsWrappers.Count];

			for (int i = 0; i < _activeEffectsWrappers.Count; i++)
				result[i] = _activeEffectsWrappers[i].activeEffect;
			return result;
		}

		private void OnTurnEnded(GameTeamType teamTurn)
		{
			if (_host.Team != teamTurn)
				return;

			for (var i = 0; i < _activeEffectsWrappers.Count; i++)
			{
				var activeEffect = _activeEffectsWrappers[i].activeEffect;

				activeEffect.Effect.ProcessCardEffect(_host);
				_activeEffectsWrappers[i].turnsCountSetter.Invoke(activeEffect.TurnsRemaining - 1);
				if (activeEffect.TurnsRemaining > 0)
					continue;

				ActiveEffectRemoved?.Invoke(_activeEffectsWrappers[i].activeEffect);
				_activeEffectsWrappers.RemoveAt(i);
				i--;
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
