using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

public class PlayerUnit : MonoBehaviour
{
	public List<Card> CardsInResources = new List<Card>();
	public string CDTypeFolderName;
	public UnitMoveType unitMoveType;
	public int moveDistance = 2;
	private GridManager gridManager;
	private DrawManager drawManager;

	public bool discoverReady = true;
	public bool moveReady = true;

	public enum UnitMoveType
	{
		Orthogonal =0,
		Diagonal =1,
		LShape=2,
	}

	void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
		drawManager = GetComponent<DrawManager>();

	}
	void Start()
	{
		//load cards from resources folder
		List<Card> cards = GridGambitUtil.ReturnCardPool(true, CDTypeFolderName);
		CardsInResources.AddRange(cards);
		drawManager.CreateUnitDeck(CardsInResources);
	}

	public void DiscoverSetup()
	{
		drawManager.FirstHandSetup(3);
		discoverReady = false;
	}
	public bool MoveSetup(Vector2 cellPos)
	{
		moveReady = false;
		switch (unitMoveType)
		{
			case UnitMoveType.Orthogonal:
				gridManager.OrthogonalMovement(cellPos, moveDistance);
				return true;

			case UnitMoveType.Diagonal:
				gridManager.DiagonalMovement(cellPos, moveDistance);
				return true;

			case UnitMoveType.LShape:
				gridManager.LShapeMovement(cellPos, moveDistance);
				return true;
		}
		return false;
	}
}
