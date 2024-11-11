using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

[CreateAssetMenu(fileName = "New Attack Card", menuName = "Card/Attack")]
public class AttackCard : Card
{
	public int damage;
	public int range;
}
