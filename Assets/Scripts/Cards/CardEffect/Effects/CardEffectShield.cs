using Creatures;
using UnityEngine;

namespace Cards
{
	public class CardEffectShield : CardEffect
	{
		private Creature _host;
		private Sprite _icon;
		private CardEffectType _effectType;
		private float _initialCapacity;
		private bool _shouldBeProcessedOnAdd;
		private bool _processedOnAdd;
		private int _duration;

		public override Sprite Icon => _icon;
		public override CardEffectType Type => _effectType;
		public override bool ShouldBeProcessedOnAdd => _shouldBeProcessedOnAdd;
		public override int TurnsDuration => _duration;

		public CardEffectShield(CardEffectConfig config)
		{
			_icon = config.Icon;
			_effectType = config.EffectType;
			_initialCapacity = config.Value;
			_duration = config.TurnsDuration;
			_shouldBeProcessedOnAdd = config.ShouldBeProcessedOnAdd;
		}

		public override void ProcessCardEffect(Creature targetCreature)
		{
			ProcessFirstTimeIfNeeded(targetCreature);
		}

		public override void Dispose()
		{
			_host.Stats.RemoveSubscriberFromValueChanged(CreatureStatType.Shield, OnShieldDeplete);
			_host.Stats.SetValue(CreatureStatType.Shield, 0f);
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
