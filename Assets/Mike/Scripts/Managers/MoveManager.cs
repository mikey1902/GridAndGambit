using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
	//make instance bools & singleton later
	//private bool isMoving = false;
	//private bool isActing = false;

	private LayerMask unitLayerMask;
	private LayerMask gridLayerMask;
	private GridManager gridManager;

	void Awake()
	{
		unitLayerMask = LayerMask.GetMask("Units");
		gridLayerMask = LayerMask.GetMask("Grid");

		gridManager = FindObjectOfType<GridManager>();
	}

	void Update()
	{
		if(GameManager.Instance.gameState == GameState.PLAYER)
		{
			PlayerAction();
		}
	}

	private void PlayerAction()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, unitLayerMask);
			if (hit.collider != null && hit.collider.GetComponent<PlayerUnit>().discoverReady == true)
			{
				PlayerUnit clickedUnit = hit.collider.GetComponent<PlayerUnit>();
				clickedUnit.DiscoverSetup();
			}
		}
		if (Input.GetMouseButtonUp(1) && gridManager.movingUnit == false)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, unitLayerMask);
			RaycastHit2D hit2 = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, gridLayerMask);

			if (hit.collider != null && hit.collider.GetComponent<PlayerUnit>().moveReady == true)
			{
				PlayerUnit clickedUnit = hit.collider.GetComponent<PlayerUnit>();
				GridCell clickedCell = hit2.collider.GetComponent<GridCell>();

				gridManager.moveableObject = clickedUnit.gameObject;
				clickedUnit.MoveSetup(clickedCell.gridIndex);
			}
		}
	
	}
}
