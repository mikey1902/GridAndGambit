using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

[CreateAssetMenu(fileName = "New Attack Card", menuName = "Card/Attack")]
public class AttackCard : Card
{
	public int damage { get; set; }
	public int range { get; set; }
}
