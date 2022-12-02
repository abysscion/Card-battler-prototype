using System;

namespace Creatures
{
	public class CreatureDamageReceiver
	{
		private Creature _host;
		private Action<float> _setHealthMethod;

		public CreatureDamageReceiver(Creature creature, Action<float> setHealthMethod)
		{
			_setHealthMethod = setHealthMethod;
			_host = creature;
		}

		public bool TryReceiveDamage(float value)
		{
			_setHealthMethod.Invoke(_host.Health - value);
			return true;
		}
	}
}
