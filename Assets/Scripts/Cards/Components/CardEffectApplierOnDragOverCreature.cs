using Creatures;

namespace Cards
{
	public class CardEffectApplierOnDragOverCreature : CardBase
	{
		private void Start()
		{
			dragNDropProvider.DraggedOverCreature += OnDragOverCreature;
		}

		private void OnDragOverCreature(Creature creature)
		{
			var allEffects = config.GetCardEffects();
			foreach (var effect in allEffects)
				creature.EffectsController.TryApplyEffect(effect);
		}
	}
}
