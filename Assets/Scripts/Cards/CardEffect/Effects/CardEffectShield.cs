using Creatures;

namespace Cards
{
	public class CardEffectShield : CardEffect
	{
		private CardEffectType _effectType;
		private Creature _host;
		private float _initialCapacity;
		private bool _shouldBeProcessedOnAdd;
		private bool _processedOnAdd;
		private int _duration;

		public override CardEffectType Type => _effectType;
		public override bool ShouldBeProcessedOnAdd => _shouldBeProcessedOnAdd;
		public override int TurnsDuration => _duration;
		public float RemainingCapacity => _initialCapacity;

		public CardEffectShield(CardEffectConfig config)
		{
			_effectType = config.EffectType;
			_initialCapacity = config.Value;
			_duration = config.TurnsDuration;
			_shouldBeProcessedOnAdd = config.ShouldBeProcessedOnAdd;
		}

		~CardEffectShield()
		{
			if (_host && _host.Stats != null)
				_host.Stats.RemoveSubscriberFromValueChanged(CreatureStatType.Shield, OnShieldDeplete);
			_host.Stats.SetValue(CreatureStatType.Shield, 0f);
		}

		public override void ProcessCardEffect(Creature targetCreature)
		{
			ProcessFirstTimeIfNeeded(targetCreature);
		}

		private void ProcessFirstTimeIfNeeded(Creature targetCreature)
		{
			if (_processedOnAdd)
				return;

			_processedOnAdd = true;
			_host = targetCreature;
			_host.EffectsController.DispelAllEffectsOfTypeExceptGiven(CardEffectType.Shield, this);
			_host.Stats.AddSubscriberToValueChanged(CreatureStatType.Shield, OnShieldDeplete);
			_host.Stats.SetValue(CreatureStatType.Shield, _initialCapacity);
		}

		private void OnShieldDeplete(float newValue)
		{
			if (newValue <= 0)
				_host.EffectsController.TryDispelEffectsOfType(CardEffectType.Shield);
		}
	}
}
