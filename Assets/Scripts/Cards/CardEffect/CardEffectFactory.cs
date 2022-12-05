namespace Cards
{
	public static class CardEffectFactory
	{
		public static bool TryCreateEffectByConfig(CardEffectConfig config, out CardEffect effect)
		{
			switch (config.EffectType)
			{
				case CardEffectType.StatChange:
					effect = new CardEffectChangeCreatureStat(config);
					return true;
				case CardEffectType.Heal:
					effect = new CardEffectHeal(config);
					return true;
				case CardEffectType.HealthDamage:
					effect = new CardEffectDealHealthDamage(config);
					return true;
				case CardEffectType.Shield:
					effect = new CardEffectShield(config);
					return true;
				case CardEffectType.Poison:
					effect = new CardEffectApplyPoison(config);
					return true;
				default:
					effect = null;
					return false;
			}
		}
	}
}
