using GridGambitProd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
	//all cards
	public Card cardData;

	public Image cardImage;
	public Image displayImage;
	public TMP_Text nameText;
	public TMP_Text cardText;

	public GameObject AttackValues;
	public GameObject RangeValues;

	//Attack cards
	public TMP_Text damageText;
	public TMP_Text rangeText;

	//Move cards
	public TMP_Text moveDistText;

	//Support cards
	public TMP_Text suppAmountText;

	private Color[] typeColors =
	{
		Color.red, Color.green, Color.cyan
	};

	public void UpdateCard()
	{

		//all cards
		cardImage.color = typeColors[(int)cardData.cardType];
		nameText.text = cardData.cardName;
		displayImage.sprite = cardData.cardSprite;
		cardText.text = cardData.cardText;

		//dependant card changes
		if (cardData is AttackCard attackCard)
		{
			UpdateAttackCard(attackCard);
		}
		else if (cardData is MoveCard moveCard)
		{
			UpdateMoveCard(moveCard);
		}
		else if (cardData is SupportCard supportCard)
		{
			UpdateSupportCard(supportCard);
		}
	}

	private void UpdateAttackCard(AttackCard attackCard)
	{
		AttackValues.SetActive(true);
		RangeValues.SetActive(true);

		damageText.text = attackCard.damage.ToString();
		rangeText.text = attackCard.range.ToString();
	}

	private void UpdateMoveCard(MoveCard moveCard)
	{
		AttackValues.SetActive(false);
		RangeValues.SetActive(true);

		moveDistText.text = moveCard.moveDistance.ToString();
	}

	private void UpdateSupportCard(SupportCard supportCard)
	{
		AttackValues.SetActive(false);
		RangeValues.SetActive(true);

		suppAmountText.text = supportCard.supportAmount.ToString();
	}
}
