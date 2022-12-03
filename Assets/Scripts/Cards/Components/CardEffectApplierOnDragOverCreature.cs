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
			var allEffects = config.GetCardEffects();
			foreach (var effect in allEffects)
				creature.EffectsController.TryApplyEffect(effect);
		}
	}
}
