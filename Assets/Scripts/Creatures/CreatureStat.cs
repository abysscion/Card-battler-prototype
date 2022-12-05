namespace Creatures
{
	public class CreatureStat
	{
		public CreatureStatType type;

		private float _value;

		/// <summary> T0 refers to a new value after change </summary>
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
