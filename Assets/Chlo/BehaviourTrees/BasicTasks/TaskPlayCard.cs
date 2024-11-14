using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = UnityEngine.Vector2;


public class TaskPlayCard : BTNode
{

	private EnemyContainer _enemyContainer;
	private Card ChosenCard;
	public float waitCounter;
	private float _waitTime;

	private Transform _target;
	private Card selectedCard;
	private int reps;

	public Transform _transform;
	private bool waitingForPreviousNode;
	public HandManager handManager;

	public TaskPlayCard(Transform target, Transform unit, EnemyContainer enemyContainer, float waitTime)
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
			ChosenCard = _enemyContainer.CardToPlay;

			foreach (Card c in ourLs)
			{
				//int field;
				bool canBePlayed = false;
				if (c is AttackCard attackCard)
				{
					_enemyContainer.Target = GridGambitUtil.FindNearestTarget(_enemyContainer.transform, false).First().transform;
					if (attackCard.range >
					    Vector2.Distance(_target.position, _enemyContainer.gameObject.transform.position))
						canBePlayed = true;
					ChosenCard = attackCard;		

				}
				else if (c is SupportCard supportCard)
				{
					_enemyContainer.Target = GridGambitUtil.FindNearestTarget(_enemyContainer.transform, true).First().transform;
					if (supportCard.range >
					    Vector2.Distance(_target.position, _enemyContainer.gameObject.transform.position))
						canBePlayed = true;
					ChosenCard = supportCard;		
				} if (canBePlayed) break;
			}
			
		
			
		
			state = NodeState.SUCCESS;
			return state;
		}
		state = NodeState.RUNNING;
		return state;
	}
}

