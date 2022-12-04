using System.Collections.Generic;
using Cards;
using CoreGameplay;

namespace Creatures
{
	public class CreatureEffectsController
	{
		private List<ActiveEffect> _activeEffects;
		private Creature _host;

		public CreatureEffectsController(Creature creature)
		{
			_host = creature;
			_activeEffects = new List<ActiveEffect>();
			GameController.Instance.TurnEnded += OnTurnEnded;
		}

		~CreatureEffectsController()
		{
			GameController.Instance.TurnEnded -= OnTurnEnded;
		}

		public bool TryApplyEffect(CardEffect effect)
		{
			if (effect.TurnsDuration == 0)
				effect.ProcessCardEffect(_host);
			else if (effect.TurnsDuration > 0)
				_activeEffects.Add(new ActiveEffect(effect, effect.TurnsDuration));
			else
				return false;
			return true;
		}

		private void OnTurnEnded(GameTeamType teamTurn)
		{
			for (var i = 0; i < _activeEffects.Count; i++)
			{
				_activeEffects[i].effect.ProcessCardEffect(_host);
				_activeEffects[i].turnsLeft--;
				if (_activeEffects[i].turnsLeft <= 0)
					_activeEffects.RemoveAt(i);
			}
		}

		private class ActiveEffect
		{
			public CardEffect effect;
			public int turnsLeft;

			public ActiveEffect(CardEffect effect, int turnsLeft)
			{
				this.effect = effect;
				this.turnsLeft = turnsLeft;
			}
		}
	}
}
