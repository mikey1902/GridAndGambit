using UnityEngine;


public class Node : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	public GameObject objectInCell;
	public bool cellOccupied = false;
	public Vector2 gridIndex;
	public Vector2 gcord;

	public Color originalColor;

	public Vector2 Gcord
	{
		get
		{
			return gcord;
		}
		set
		{
			gcord = value;
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
