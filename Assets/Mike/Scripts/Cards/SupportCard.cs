using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

[CreateAssetMenu(fileName = "New Support Card", menuName = "Card/Support")]
public class SupportCard : Card
{
	public int supportAmount;
	public SupportType supportType;

	public enum SupportType
	{
		Buff,
		Debuff
	}
}