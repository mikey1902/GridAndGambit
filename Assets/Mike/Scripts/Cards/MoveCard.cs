using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

[CreateAssetMenu(fileName = "New Move Card", menuName = "Card/Move")]
public class MoveCard : Card
{
	public int moveDistance;
	public MoveType moveType;

	public enum MoveType
	{
		Orthogonal,
		Diagonal,
		LShape,
	}
}