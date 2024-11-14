using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;

public class BattleManager : MonoBehaviour
{
    public void AttackCardEffect(AttackCard cardData, GameObject target)
	{
		UnitStats unit = target.GetComponent<UnitStats>();
		unit.TakeDamage(cardData.damage);
	}
	public void MoveCardEffect(MoveCard cardData, GameObject target)
	{

	}
	public void SupportCardEffect(SupportCard cardData, GameObject target)
	{

	}
}
