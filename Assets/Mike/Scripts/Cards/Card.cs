using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridGambitProd
{
	public class Card : ScriptableObject 
	{

		public string cardName;
		public CardType cardType;
		public Sprite cardSprite;
		public string cardText;

		public enum CardType
		{
			Unit,
			Structure,
			Spell,
			Move
		}
	}
}
