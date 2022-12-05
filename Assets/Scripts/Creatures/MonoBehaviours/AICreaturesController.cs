using System.Collections;
using CoreGameplay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures
{
	public class AICreaturesController : CreaturesController
	{
		protected override void Start()
		{
			base.Start();

			GameController.GameInitialized += OnGameInitialized;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			GameController.GameInitialized -= OnGameInitialized;
			GameController.TurnStarted -= OnTurnStarted;
			GameController.TurnEnded -= OnTurnEnded;
		}

		private void OnGameInitialized()
		{
			GameController.TurnStarted += OnTurnStarted;
			GameController.TurnEnded += OnTurnEnded;
			if (GameController.Instance.CurrentTeamTurn == Team)
				OnTurnStarted(Team);
		}

		private void OnTurnStarted(GameTeamType teamTurn)
		{
			if (teamTurn == Team)
				StartCoroutine(AIAttackCoroutine());
		}

		private void OnTurnEnded(GameTeamType teamTurn)
		{
			if (teamTurn == Team)
				StopAllCoroutines();
		}

		private IEnumerator AIAttackCoroutine()
		{
			var gcInst = GameController.Instance;
			var creaturesAbleToAct = gcInst.AICreatures;

			for (var i = 0; i < creaturesAbleToAct.Length; i++)
			{
				if (actedCreatures.Contains(creaturesAbleToAct[i]))
					continue;
				if (gcInst.PlayerCreatures.Length == 0)
					break;

				var actingCreatureDeck = creatureToCreaturesDecksDic[creaturesAbleToAct[i]] as AICreatureDeck;
				var targetEnemy = gcInst.PlayerCreatures[Random.Range(0, gcInst.PlayerCreatures.Length)];
				var card = actingCreatureDeck.SpawnedCard;
				var timeToAttack = 0.75f;
				var elapsedTime = 0f;
				var cardTf = card.transform;
				var cardInitialPos = card.transform.position;

				while (elapsedTime < timeToAttack)
				{
					cardTf.position = Vector3.Lerp(cardInitialPos, targetEnemy.transform.position, elapsedTime / timeToAttack);
					elapsedTime += Time.deltaTime;
					yield return null;
				}

				actingCreatureDeck.PretendDeckCardIsOverEnemyCreature(targetEnemy);
				yield return new WaitForSeconds(0.5f);
			}
		}
	}
}
