using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;

using BehaviourTree;
using Vector2 = System.Numerics.Vector2;


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

	public TaskPlayCard(Transform target, Transform unit, Card selectCard, EnemyContainer enemyContainer, float waitTime)
	{
		//  selectedCards = unit.gameObject.GetComponent<EnemyContainer>().discoverChoices;
		_enemyContainer = enemyContainer;
		ChosenCard = selectCard;
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
				bool canBePlayed = false; 
				if (c is AttackCard attackCard)
				{
					_target = GridGambitProd.GridGambitUtil.FindNearestTarget(_enemyContainer.transform, false);

					canBePlayed = true;
					//	_target.gameObject.GetComponent<simpleSetup>().HP -= attackCard.damage;
				}
				else if (c is SupportCard supportCard)
				{
					_target = GridGambitProd.GridGambitUtil.FindNearestTarget(_enemyContainer.transform, false);
	
					
					//	_target.gameObject.GetComponent<simpleSetup>().HP += supportCard.supportAmount;
				}


				if (canBePlayed) break;
				
			}

			ChosenCard = _enemyContainer.CardToPlay;
			//    _transform.gameObject.SetActive(false);

			///    Debug.Log(ChosenCard.Typing);
			/*	if (ChosenCard is AttackCard attackCard)
			{
				Debug.Log(attackCard.damage);
				_target.gameObject.GetComponent<simpleSetup>().HP -= attackCard.damage;
			}
			else if (ChosenCard is SupportCard supportCard)
			{
				Debug.Log(supportCard.supportAmount);
				_target.gameObject.GetComponent<simpleSetup>().HP += supportCard.supportAmount;
			}*/
			// Debug.Log(stc.supportAmount); 
			

			/*  case 2: //Move
				//  trg.HP -= mtc.damage; 
				  MoveCard mtc = (MoveCard)ChosenCard.smpCard; 
				  Debug.Log(mtc.moveDistance); 
				  break;*/
			state = NodeState.SUCCESS;
			return state;
		}
		state = NodeState.RUNNING;
		return state;
	}
}

