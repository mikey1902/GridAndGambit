using GridGambitProd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

	public Card cardData;

	public Image cardImage;
	public Image[] typeImages;
	public TMP_Text nameText;

	private Color[] typeColors =
	{
		Color.green, Color.cyan, Color.red, Color.blue
	};

	void Update()
	{
		UpdateCard();
	}

	public void UpdateCard()
	{
		cardImage.color = typeColors[(int)cardData.cardType];

		nameText.text = cardData.cardName;

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
	}
}
