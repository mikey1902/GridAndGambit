using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
	public List<GameObject> objectsOnGrid = new List<GameObject>();
	public List<Transform> cellTransforms = new List<Transform>();
	public GameObject[,] gridCells;
	public GameObject cellPrefab;
	public GameObject moveableObject;
	public List<GameObject> highlightedCells;
	public int width = 8;
	public int height = 8;

	private List<Vector2> moveCells = new List<Vector2>();
	public bool movingUnit = false;
	private Vector2 cellPosWorld;
	public bool moveChose = false;
	public Vector2 movingCell;

	void Start()
	{
		createNodeGrid();
	}
	void Update()
	{
		if (moveChose == true)
		{
			HandleMovement();
		}
	}
	void createNodeGrid()
	{
		gridCells = new GameObject[width, height];
		Vector2 gridOffset = new Vector2(width / 2.0f - 0.5f, height / 2.0f - 0.5f);

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Vector2 gridPos = new Vector2(x, y);
				Vector2 spawnPos = gridPos - gridOffset;

				GameObject cell = Instantiate(cellPrefab, spawnPos, Quaternion.identity);
				cell.transform.SetParent(transform);

				cell.GetComponent<GridCell>().GridIndex = gridPos;

				if (x % 2 == 0)
				{
					cell.GetComponent<Renderer>().material.color = y % 2 == 0 ? Color.black : Color.white;
				}
				else
				{
					cell.GetComponent<Renderer>().material.color = y % 2 == 0 ? Color.white : Color.black;
				}
				cell.GetComponent<GridCell>().originalColor = cell.GetComponent<Renderer>().material.color;

				gridCells[x, y] = cell;
				cellTransforms.Add(cell.transform);
			}
		}
	}

	public bool AddObjectToGrid(GameObject obj, Vector2 cellPos)
	{
		if (CheckIndexNull(cellPos))
		{
			//searches 2D array to find the cell
			GridCell cell = gridCells[(int)cellPos.x, (int)cellPos.y].GetComponent<GridCell>();

			if (cell.cellOccupied) return false;
			else
			{
				//spawns object and sets position to the chosen cell
				GameObject gridObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
				gridObj.transform.SetParent(transform);

				//adds the object to the list and the object var in the cell
				objectsOnGrid.Add(gridObj);
				cell.objectInCell = gridObj;
				cell.cellOccupied = true;
				cell.HighlightOccupiedCell();

				return true;
			}
		}
		else return false;
	}

	public void OrthogonalMovement(Vector2 cellPos, int moveDistance)
	{
		//CHECK FOR OBSTACLES IN THIS METHOD AT SOME POINT
		movingUnit = true;

		Vector2 spacePos;

		for (int direction = 0; direction < 4; direction++)
		{
			for (int spaces = 1; spaces < moveDistance + 1; spaces++)
			{
				switch (direction)
				{
					case 0:
						spacePos = new Vector2(cellPos.x, cellPos.y + spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 1:
						spacePos = new Vector2(cellPos.x + spaces, cellPos.y);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 2:
						spacePos = new Vector2(cellPos.x, cellPos.y - spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 3:
						spacePos = new Vector2(cellPos.x - spaces, cellPos.y);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;
				}
			}
		}

		foreach (Vector2 moveCell in moveCells)
		{
			GameObject moveableCell = SearchGrid(moveCell);
			moveableCell.GetComponent<GridCell>().HighlightMoveCell();
			highlightedCells.Add(moveableCell);
		}
		moveSelect(highlightedCells);
	}

	public void DiagonalMovement(Vector2 cellPos, int moveDistance)
	{
		movingUnit = true;

		Vector2 spacePos;

		for (int direction = 0; direction < 4; direction++)
		{
			for (int spaces = 1; spaces < moveDistance + 1; spaces++)
			{
				switch (direction)
				{
					case 0:
						spacePos = new Vector2(cellPos.x + spaces, cellPos.y + spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 1:
						spacePos = new Vector2(cellPos.x + spaces, cellPos.y - spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 2:
						spacePos = new Vector2(cellPos.x - spaces, cellPos.y - spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 3:
						spacePos = new Vector2(cellPos.x - spaces, cellPos.y + spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;
				}
			}
		}

		foreach (Vector2 moveCell in moveCells)
		{
			GameObject moveableCell = SearchGrid(moveCell);
			moveableCell.GetComponent<GridCell>().HighlightMoveCell();
			highlightedCells.Add(moveableCell);
		}
		moveSelect(highlightedCells);
	}

	public void LShapeMovement(Vector2 cellPos, int moveDistance)
	{
		
		
		movingUnit = true;

		Vector2 spacePos;

		for (int direction = 0; direction < 8; direction++)
		{
			for (int spaces = 1; spaces < moveDistance + 1; spaces++)
			{
				switch (direction)
				{
					case 0:
						spacePos = new Vector2(cellPos.x - 1, cellPos.y + 1 + spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 1:
						spacePos = new Vector2(cellPos.x + 1, cellPos.y + 1 + spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 2:
						spacePos = new Vector2(cellPos.x - 1, cellPos.y - 1 - spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 3:
						spacePos = new Vector2(cellPos.x + 1, cellPos.y - 1 - spaces);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 4:
						spacePos = new Vector2(cellPos.x + 1 + spaces, cellPos.y - 1);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 5:
						spacePos = new Vector2(cellPos.x + 1 + spaces, cellPos.y + 1);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 6:
						spacePos = new Vector2(cellPos.x - 1 - spaces, cellPos.y - 1);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;

					case 7:
						spacePos = new Vector2(cellPos.x - 1 - spaces, cellPos.y + 1);
						if (CheckIndexNull(spacePos)) moveCells.Add(spacePos);
						break;
				}
			}
		}

		foreach (Vector2 moveCell in moveCells)
		{
			GameObject moveableCell = SearchGrid(moveCell);
			moveableCell.GetComponent<GridCell>().HighlightMoveCell();
			highlightedCells.Add(moveableCell);
		}
		moveSelect(highlightedCells);
	}

	public void moveChosen(Vector2 orient)
	{
		moveChose = true;
		movingCell = orient;
		foreach (Vector2 moveCell in moveCells)
		{
			GameObject moveableCell = SearchGrid(moveCell);
			moveableCell.GetComponent<GridCell>().DisableHighlight();
		}
		moveDeSelect(highlightedCells);
		highlightedCells.Clear();
		moveCells.Clear();
		movingUnit = false;
	}

	private void moveSelect(List<GameObject> moveableCells)
	{
		foreach (GameObject moveCell in moveableCells)
		{
			moveCell.GetComponent<GridCell>().cellMoveHighlighted = true;
		}
	}
	private void moveDeSelect(List<GameObject> moveableCells)
	{
		foreach (GameObject moveCell in moveableCells)
		{
			moveCell.GetComponent<GridCell>().cellMoveHighlighted = false;
		}
	}

	private GameObject SearchGrid(Vector2 cellPos)
	{
		foreach (GameObject cell in gridCells)
		{
			if (cell.GetComponent<GridCell>().gridIndex == cellPos)
			{
				return cell;
			}
		}
		return null;
	}

	private bool CheckIndexNull(Vector2 cellPos)
	{
		if (cellPos.x >= 0 && cellPos.x < width && cellPos.y >= 0 && cellPos.y < height)
		{
			return true;
		}
		else 
			return false;
	}

	private void HandleMovement()
	{
		cellPosWorld = new Vector2(movingCell.x - (float)3.5, movingCell.y - (float)3.5);
		moveableObject.transform.position = Vector2.MoveTowards(moveableObject.transform.position, cellPosWorld, 2 * Time.deltaTime);

		if (Vector2.Distance(moveableObject.transform.position, cellPosWorld) < 0.01f)
		{
			moveChose = false;
			Debug.Log("destination reached yipee!!!");
		}
	}
}
