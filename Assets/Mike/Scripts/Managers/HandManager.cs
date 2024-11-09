using GridGambitProd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
	public GameObject cardPF;
	public List<GameObject> cardsInHand = new List<GameObject>();
	public Transform handTransform;

	public float verticalCardSpace = 0f;
	public float horizontalCardSpace = 300f;
	public int maxHandSize;

	void Start()
	{
	}

	public void AddCard(Card cardData)
	{
		if (cardsInHand.Count < maxHandSize)
		{
			//Instantiate card and add it to the list
			GameObject newCard = Instantiate(cardPF, handTransform.position, Quaternion.identity, handTransform);
			cardsInHand.Add(newCard);

			//set the instantiated card's data
			newCard.GetComponent<CardDisplay>().cardData = cardData;
			newCard.GetComponent<CardDisplay>().UpdateCard();
		}

		UpdateHandVisuals();
	}
	public void MaxHandSizeSetup(int setMaxHandSize)
	{
		maxHandSize = setMaxHandSize;
	}

	public void UpdateHandVisuals()
	{
		int cardCount = cardsInHand.Count;

		//Takes the first card and sets its position and rotation to avoid divide by 0 error
		if (cardCount == 1)
		{
			cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
			return;
		}

		for (int i = 0; i < cardCount; i++)
		{
			float horizontalCardOffset = (horizontalCardSpace * (i - (cardCount - 1) / 2f));

			float positionNormalized = (2f * i / (cardCount - 1) - 1f); //Normalizes card position between -1 and 1
			float verticalCardOffset = verticalCardSpace * (1 - positionNormalized * positionNormalized);

			//sets the cards new position
			cardsInHand[i].transform.localPosition = new Vector3(horizontalCardOffset, verticalCardOffset, 0f);
		}
	}

	public void ClearHand()
	{
		foreach(GameObject card in cardsInHand)
		{
			Destroy(card);
		}
	}
}
