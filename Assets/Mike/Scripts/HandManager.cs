using GridGambitProd;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject cardPF;
    public List<GameObject> cardsInHand = new List<GameObject>();
    public Transform handTransform;

    public CardMovement cardMovement;

    public float verticalCardSpace = 50f;
    public float horizontalCardSpace = 120f;
    public float handSpread = -10f;
    public int maxHandSize = 10;

    void Start()
    {
    }

    public void AddCard(Card cardData)
    {
        if (cardsInHand.Count < maxHandSize) {
            //Instantiate card and add it to the list
            GameObject newCard = Instantiate(cardPF, handTransform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard);

            //set the instantiated card's data
            newCard.GetComponent<CardDisplay>().cardData = cardData;
            newCard.GetComponent<CardDisplay>().UpdateCard();
            cardMovement = newCard.GetComponent<CardMovement>();
        }

        UpdateHandVisuals();
    }

    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        //Takes the first card and sets its position and rotation to avoid divide by 0 error
        if (cardCount == 1) {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++) {
            //Creates the rotation for the current card in the list and then sets it to the card
            float rotationAngle = (handSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalCardOffset = (horizontalCardSpace * (i - (cardCount - 1) / 2f));

            float positionNormalized = (2f * i / (cardCount - 1) - 1f); //Normalizes card position between -1 and 1
            float verticalCardOffset = verticalCardSpace * (1 - positionNormalized * positionNormalized);

            //sets the cards new position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalCardOffset, verticalCardOffset, 0f);
        }
    }
}
