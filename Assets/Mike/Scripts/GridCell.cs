using UnityEngine;


public class GridCell : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	public GameObject objectInCell;
	public bool cellOccupied = false;
	public Vector2 gridIndex;

	public Color originalColor;

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
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (cellOccupied == true)
		{
			spriteRenderer.material.color = Color.yellow;
		}
	}

	void OnMouseEnter()
	{
		spriteRenderer.material.color = Color.cyan;
	}

	void OnMouseExit()
	{
		spriteRenderer.material.color = originalColor;
	}
}
