using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridGambitProd
{
	[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
	public class Card : ScriptableObject 
	{

		public string cardName;
		public CardType cardType;
		public Sprite cardSprite;

		public enum CardType
		{
			Unit,
			Structure,
			Spell,
			Move
		}
	}
}
