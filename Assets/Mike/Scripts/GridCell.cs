using UnityEngine;


public class GridCell : MonoBehaviour
{
	private SpriteRenderer cellSpriteRenderer;
	private SpriteRenderer highlightSpriteRenderer;
	private GameObject cellHighlight;
	public GameObject objectInCell;
	public bool cellOccupied = false;
	public Vector2 gridIndex;

	public Color originalColor;
	public Color highlightColor = Color.cyan;
	public Color occupiedColor = Color.yellow;
	public Color placeableColor = Color.green;
	public Color notPlaceableColor = Color.red;

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

		cellHighlight = gameObject.transform.GetChild(0).gameObject;
		highlightSpriteRenderer = cellHighlight.GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (cellOccupied == true)
		{
			cellSpriteRenderer.material.color = Color.yellow;
		}
		else
		{
			cellSpriteRenderer.material.color = originalColor;
		}
	}

	void OnMouseEnter()
	{
		cellHighlight.gameObject.SetActive(true);

		if (!GameManager.Instance.playingCard)
		{
			highlightSpriteRenderer.color = highlightColor;
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
	}
}
