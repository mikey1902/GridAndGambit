using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using JetBrains.Annotations;
using Vector2 = UnityEngine.Vector2;


public class TaskPlayCard : BTNode
{

	private EnemyContainer _enemyContainer;
	private Card ChosenCard;
	public float waitCounter;
	private float _waitTime;

	private bool canBePlayed;
	private Transform _target;
	private Card selectedCard;
	private int reps;
	private Transform targ;
	public Transform _transform;
	private bool waitingForPreviousNode;

	public TaskPlayCard([CanBeNull] Transform target, Transform unit, EnemyContainer enemyContainer, float waitTime)
	{
		//  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
		_enemyContainer = enemyContainer;
		waitCounter = 0f;
		_waitTime = waitTime;
		_transform = unit;
		waitingForPreviousNode = true;
		_target = target;
	}


	public override NodeState Evaluate()
	{
		if (waitingForPreviousNode)
		{

			//code for delay goes here
			waitCounter += Time.deltaTime;
			if (waitCounter >= _waitTime)
				waitingForPreviousNode = false;
		}
		else
		{
			List<Card> ourLs = _enemyContainer.discoverChoices;

			foreach (Card c in ourLs)
			{
				//int field;
				 canBePlayed = false;
				if (c is AttackCard attackCard)
				{
					targ = GridGambitUtil.FindNearestTarget(_enemyContainer.gameObject.transform, false).First()
						.transform;
					if (attackCard.range > Vector2.Distance(targ.position, _enemyContainer.gameObject.transform.position)- _enemyContainer.MoveAmount)
						canBePlayed = true;
				}
				else if (c is SupportCard supportCard)
				{
					if (!supportCard.canPlayOnSelf)
					{
						targ = GridGambitUtil.FindNearestTarget(_enemyContainer.gameObject.transform, true).ElementAt(1).transform;
						
						float dst = Vector2.Distance(targ.position, _enemyContainer.gameObject.transform.position) -
						            _enemyContainer.MoveAmount;
						if (supportCard.range > dst)
						{
							canBePlayed = true;
						}
					}
					else if (supportCard.canPlayOnSelf)
					{
						targ = _enemyContainer.gameObject.transform;
						canBePlayed = true;
					}
				}

				if (canBePlayed)
				{
					_enemyContainer.discoverCard = c;
					_enemyContainer.Target = targ;
					state = NodeState.SUCCESS;
					return state;
				}
			}
			
			
					
		
		}
		
	state = NodeState.FAILURE;
	return state;
}
}
	


