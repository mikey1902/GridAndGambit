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
	public Image[] typeImages;
	public TMP_Text nameText;
	public TMP_Text cardText;

	public GameObject unitValues;
	public GameObject spellValues;
	public GameObject unitLabel;
	public GameObject spellLabel;

	//unit cards

	//spell cards
	public TMP_Text damageText;

	private Color[] typeColors =
	{
		Color.green, Color.cyan, Color.red, Color.blue
	};

	public void UpdateCard()
	{

		//all cards
		cardImage.color = typeColors[(int)cardData.cardType];
		nameText.text = cardData.cardName;
		displayImage.sprite = cardData.cardSprite;
		cardText.text = cardData.cardText;

		//Enables the type symbols depending on the type of the card
		switch (cardData.cardType) 
		{
			case Card.CardType.Unit:
				typeImages[0].gameObject.SetActive(true);
				break;

			case Card.CardType.Structure:
				typeImages[1].gameObject.SetActive(true);
				break;

			case Card.CardType.Spell:
				typeImages[2].gameObject.SetActive(true);
				break;

			case Card.CardType.Move:
				typeImages[3].gameObject.SetActive(true);
				break;
		}

		//dependant card changes
		//if (cardData is UnitCard unitCard)
		//{
		//	UpdateUnitCard(unitCard);
		//}
		//else if (cardData is SpellCard spellCard)
		//{
		//	UpdateSpellCard(spellCard);
		//}
	}

	//private void UpdateUnitCard(UnitCard unitCard)
	//{
	//	spellValues.SetActive(false);
	//	unitValues.SetActive(true);
	//	unitLabel.SetActive(true);
	//}

	//private void UpdateSpellCard(SpellCard unitCard)
	//{
	//	unitValues.SetActive(false);
	//	spellValues.SetActive(true);
	//	spellLabel.SetActive(true);

	//	damageText.text = unitCard.damage.ToString();
	//}
}
