namespace Creatures
{
	public class CreatureStat
	{
		public CreatureStatType type;

		private float _value;

		public System.Action<float> ValueChanged;

		public float Value
		{
			get => _value;
			set
			{
				_value = value;
				ValueChanged?.Invoke(_value);
			}
		}

		public CreatureStat(CreatureStatType type, float value)
		{
			this.type = type;
			Value = value;
		}
	}
}
