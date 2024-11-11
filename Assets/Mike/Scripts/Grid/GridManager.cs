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
	public List<GameObject> highlightedCells = new List<GameObject>();
	public int width = 8;
	public int height = 8;
	private AreaSelect areaSelect;
	private List<Vector2> moveCells = new List<Vector2>();

	private Vector2 gmCellPos;
	private int gmMoveDistance;
	public bool movingUnit = false;

	void Awake()
	{
		areaSelect = GetComponent<AreaSelect>();
	}

	void Start()
	{
		createNodeGrid();
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
		movingUnit = true;
		gmCellPos = cellPos;
		gmMoveDistance = moveDistance;

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

	}

	public void LShapeMovement(Vector2 cellPos, int moveDistance)
	{

	}

	public void moveChosen(Vector2 orient)
	{
		Debug.Log((orient - gmCellPos).normalized);
		areaSelect.directedMove(gmCellPos,(orient - gmCellPos).normalized, "S", gmMoveDistance + 1, moveableObject);
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
}
