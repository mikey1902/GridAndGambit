using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour
{
	private SpriteRenderer cellSpriteRenderer;
	private SpriteRenderer highlightSpriteRenderer;
	private GridManager gridManager;
	private GameObject cellHighlight;
	public GameObject objectInCell;
	public bool cellOccupied = false;
	public bool cellMoveHighlighted = false;
	public bool hoveringCell = false;
	public Vector2 gridIndex;

	public Color originalColor;
	public Color highlightColor = Color.cyan;
	public Color occupiedColor = Color.yellow;
	public Color placeableColor = Color.green;
	public Color notPlaceableColor = Color.red;
	public Color MoveableColor = Color.magenta;

	public Vector2 GridIndex
	{
		get
		{
			return gridIndex;
		}
		set
		{
			gridIndex = value;
		}
	}

	void Awake()
	{
		cellSpriteRenderer = GetComponent<SpriteRenderer>();
		gridManager = FindObjectOfType<GridManager>();


		cellHighlight = gameObject.transform.GetChild(0).gameObject;
		highlightSpriteRenderer = cellHighlight.GetComponent<SpriteRenderer>();
	}

	void OnMouseEnter()
	{
		cellHighlight.gameObject.SetActive(true);

		if (!GameManager.Instance.playingCard && !GameManager.Instance.playingMove && cellMoveHighlighted)
		{
			highlightSpriteRenderer.color = placeableColor;
			hoveringCell = true;
		}
		else if (!GameManager.Instance.playingCard && !GameManager.Instance.playingMove)
		{
			highlightSpriteRenderer.color = highlightColor;
		}
		else if (cellOccupied && GameManager.Instance.playingMove)
		{
			highlightSpriteRenderer.color = MoveableColor;
		}
		else if (cellOccupied)
		{
			highlightSpriteRenderer.color = notPlaceableColor;
		}
		else
		{
			highlightSpriteRenderer.color = placeableColor;
		}
	}

	void OnMouseExit()
	{
		cellHighlight.gameObject.SetActive(false);
		hoveringCell = false;
	}

	void OnMouseDown()
	{
		if (hoveringCell)
		{
			gridManager.moveChosen(GridIndex);
		}
	}

	public void HighlightOccupiedCell()
	{
		cellSpriteRenderer.material.color = occupiedColor;
	}
	public void HighlightMoveCell()
	{
		cellSpriteRenderer.material.color = MoveableColor;
	}

	public void DisableHighlight()
	{
		cellSpriteRenderer.material.color = originalColor;
	}
}