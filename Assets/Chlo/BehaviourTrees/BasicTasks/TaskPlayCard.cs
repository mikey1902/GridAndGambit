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
			ChosenCard = _enemyContainer.CardToPlay;
			handManager = GridGambitUtil.GetHandManager();

			//    _transform.gameObject.SetActive(false);

			///    Debug.Log(ChosenCard.Typing);
			if (ChosenCard is AttackCard attackCard)
			{
				Debug.Log(attackCard.damage);
				_target.gameObject.GetComponent<simpleSetup>().HP -= attackCard.damage;
			}
			else if (ChosenCard is SupportCard supportCard)
			{
				Debug.Log(supportCard.supportAmount);
				_target.gameObject.GetComponent<simpleSetup>().HP += supportCard.supportAmount;
			}
			// Debug.Log(stc.supportAmount); 
			else
			{
				//Debug.Log(ChosenCard.cardName);
			}

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

