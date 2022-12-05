using Cards;
using CoreGameplay;

namespace Creatures
{
	public class AICreatureDeck : CreatureDeck
	{
		public Card SpawnedCard => _spawnedCard;

		public AICreatureDeck(Creature creature, CardConfigsContainer configsContainer, GameTeamType team)
			: base(creature, configsContainer, team)
		{
		}

		public void PretendDeckCardIsOverEnemyCreature(Creature enemyCreature)
		{
			OnDragOverCreature(enemyCreature);
		}
	}
}
