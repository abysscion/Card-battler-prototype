using System;

namespace Creature
{
	public class CreatureDamageReceiver
	{
		private Creature _target;
		private Action<int> _setHealthMethod;

		public CreatureDamageReceiver(Creature creature, Action<int> setHealthMethod)
		{
			_setHealthMethod = setHealthMethod;
			_target = creature;
		}

		public bool TryReceiveDamage(int value)
		{
			_setHealthMethod.Invoke(value);
			return true;
		}
	}
}
