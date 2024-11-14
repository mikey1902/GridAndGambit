using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GridGambitProd;
using static GridGambitProd.GridGambitUtil;
using BehaviourTree;
using Unity.VisualScripting;
using Vector2 = UnityEngine.Vector2;



public class TaskMove : BTNode
{
    private bool waitingForPreviousNode;
    private List<moveOb> pathLis;
    private List<Card> currentPool;
    private EnemyContainer _enemyContainer;
    private float waitTime;
    private bool waitForPreviousNode;
    private bool waitingForMove;
    private GridManager _gridManager;
    private Transform _target;
    private float _waitTime;
    private float waitCounter;
    private bool notHadTurn;
    class moveOb
    {
        public GameObject Cell;
        public float Score;

    }

public TaskMove(EnemyContainer enemyContainer, int distance, GridManager gridManager, Transform target, float waitTime)
    {
        _enemyContainer = enemyContainer;
        _gridManager = gridManager;
        gridManager.movingUnit = true;
        _target = target;
        pathLis = new List<moveOb>();
        _waitTime = waitTime;
        waitingForPreviousNode = true;
        notHadTurn = true;
        _gridManager.moveableObject = _enemyContainer.gameObject;

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
            if (notHadTurn)
            {
                Vector2 ourPosition = _enemyContainer.gameObject.transform.position;
                int x = Mathf.FloorToInt(ourPosition.x + 3.5f);
                int y = Mathf.FloorToInt(ourPosition.y + 3.5f);
                Vector2 localPos = new Vector2(x, y);
                _gridManager.moveableObject = _enemyContainer.gameObject;
                
                MoveSetup(_gridManager, localPos, 1,
                    Unit.UnitMoveType.Orthogonal);
                //Figure Out Path
                foreach (GameObject cell in _gridManager.highlightedCells)
                {
                    moveOb mv = new moveOb();
                    mv.Score = Vector2.Distance(_target.position, cell.transform.position);
                    mv.Cell = cell;
                    Debug.Log(cell.GetComponent<GridCell>().GridIndex + " " + mv.Score);
                    if (!cell.GetComponent<GridCell>().cellOccupied) pathLis.Add(mv);
                }
                //Order by the best path - via Distance To Target
                pathLis = pathLis.OrderBy(item => item.Score).ToList();
                _gridManager.moveChosen(pathLis.First().Cell.gameObject.GetComponent<GridCell>().GridIndex);
                
                
                _gridManager.moveChose = true;
                waitingForMove = _gridManager.movingUnit;
                //Complete Turn
                notHadTurn = false;
                state = NodeState.SUCCESS;
                return state;
            }

        }

        state = NodeState.FAILURE;
        return state;
    }
}