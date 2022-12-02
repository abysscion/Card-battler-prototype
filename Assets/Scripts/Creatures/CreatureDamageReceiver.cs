using System;

namespace Creatures
{
	public class CreatureDamageReceiver
	{
		private Creature _host;
		private Action<int> _setHealthMethod;

		public CreatureDamageReceiver(Creature creature, Action<int> setHealthMethod)
		{
			_setHealthMethod = setHealthMethod;
			_host = creature;
		}

		public bool TryReceiveDamage(int value)
		{
			_setHealthMethod.Invoke(_host.Health - value);
			return true;
		}
	}
}
