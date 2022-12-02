using Creatures;

namespace Cards
{
	public class CardEffectApplierOnDragOverCreature : CardBase
	{
		private void Start()
		{
			dragNDropHelper.DraggedOverCreature += OnDragOverCreature;
		}

		private void OnDragOverCreature(Creature creature)
		{
			var allEffects = config.GetStatModifiers();
			foreach (var effect in allEffects)
			{
				if (effect.Type == CreatureStatType.Health && effect.Duration == 0)
					creature.DamageReceiver.TryReceiveDamage(-effect.Value);
			}
		}
	}
}
