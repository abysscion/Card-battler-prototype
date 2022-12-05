namespace Cards
{
	public class CardActiveEffect
	{
		public CardEffect Effect { get; private set; }
		public int TurnsRemaining { get; private set; }

		public delegate void TurnsCountSetter(int turnsCount);

		public CardActiveEffect(CardEffect effect, int turnsDuration, out TurnsCountSetter turnsLeftCountSetter)
		{
			Effect = effect;
			TurnsRemaining = turnsDuration;
			turnsLeftCountSetter = SetTurnsLeftCount;
		}

		private void SetTurnsLeftCount(int value)
		{
			TurnsRemaining = value;
		}
	}
}
