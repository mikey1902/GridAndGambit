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
	public TMP_Text nameText;
	public TMP_Text cardText;

	//public Image displayImage;
	//im dumb im dumb im dumb fix the images im dumb
	public GameObject pawnImage;
	public GameObject bishopImage;
	public GameObject rookImage;
	public GameObject knightImage;

	//Attack cards
	public GameObject attackCardImage;
	public TMP_Text damageText;
	public TMP_Text rangeText;

	//Move cards
	public GameObject moveCardImage;
	public TMP_Text moveDistText;

	//Support cards
	public GameObject supportCardImage;
	public TMP_Text suppAmountText;

	public void UpdateCard()
	{
		//all cards
		nameText.text = cardData.cardName;

		//displayImage.sprite = cardData.cardSprite;

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

		switch (cardData.imageType)
		{
			case Card.ImageType.Pawn:
				pawnImage.SetActive(true);
				break;

			case Card.ImageType.Bishop:
				bishopImage.SetActive(true);
				break;

			case Card.ImageType.Rook:
				rookImage.SetActive(true);
				break;

			case Card.ImageType.Knight:
				knightImage.SetActive(true);
				break;
		}
	}

	private void UpdateAttackCard(AttackCard attackCard)
	{
		attackCardImage.SetActive(true);
		damageText.text = attackCard.damage.ToString();
		rangeText.text = attackCard.range.ToString();
	}

	private void UpdateMoveCard(MoveCard moveCard)
	{
		moveCardImage.SetActive(true);
		moveDistText.text = moveCard.moveDistance.ToString();
	}

	private void UpdateSupportCard(SupportCard supportCard)
	{
		supportCardImage.SetActive(true);
		suppAmountText.text = supportCard.supportAmount.ToString();
		rangeText.text = supportCard.range.ToString();
	}
}
