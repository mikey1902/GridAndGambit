using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

public class BattleManager : MonoBehaviour
{
	private GridManager gridManager;
	private HandManager handManager;

	void Awake()
	{
		gridManager = FindObjectOfType<GridManager>();
		handManager = FindObjectOfType<HandManager>();
	}

	public void AttackCardEffect(AttackCard cardData, GameObject target)
	{
		UnitStats unit = target.GetComponent<UnitStats>();
		unit.TakeDamage(cardData.damage);
	}
	public void MoveCardEffect(MoveCard cardData, GameObject target, Vector2 targetCellPos)
	{
		gridManager.moveableObject = target;
		switch (cardData.moveType)
		{
			case MoveCard.MoveType.Orthogonal:
				gridManager.OrthogonalMovement(targetCellPos, cardData.moveDistance);
				break;

			case MoveCard.MoveType.Diagonal:
				gridManager.DiagonalMovement(targetCellPos, cardData.moveDistance);
				break;

			case MoveCard.MoveType.LShape:
				gridManager.LShapeMovement(targetCellPos, cardData.moveDistance);
				break;
		}
	}
	public void SupportCardEffect(SupportCard cardData, GameObject target)
	{
		UnitStats unit = target.GetComponent<UnitStats>();
		unit.Heal(cardData.supportAmount);
	}
}
