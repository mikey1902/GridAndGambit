using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

[CreateAssetMenu(fileName = "New Move Card", menuName = "Card/Move")]
public class MoveCard : Card
{
	public MoveType moveType;
	public int moveDistance;

	public enum MoveType
	{
		Orthogonal,
		Diagonal,
		LShape,
	}
}