using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public bool isDestroyed = false;
	public bool hasActed = false;
	private GridManager gridManager;

	void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
	}
	public void DestroyUnit()
	{
		isDestroyed = true;
		if (isDestroyed)
		{
			Destroy(gameObject);
		}
	}
}
