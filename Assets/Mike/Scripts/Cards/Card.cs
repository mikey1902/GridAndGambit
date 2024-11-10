using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rand = System.Random;
namespace GridGambitProd
{
    public class Card : ScriptableObject
    {

        public string cardName;
        public CardType cardType;
        public Sprite cardSprite;
        public string cardText;
        public bool hasExtraEffect;

        public enum CardType
        {
            Attack,
            Move,
            Support,
        }
    }

    public class GridGambitUtil : MonoBehaviour
    {


        //Concatinates all the pools()
        public static List<Card> ReturnCardPool(bool doesShuffle, params string[] poolNames)
        {
            var rnd = new Rand();
            List<Card> currentPool = new List<Card>();

            if (poolNames.Length <= 1) {
                foreach (string item in poolNames) {
                    currentPool = Enumerable.Union(currentPool, Resources.LoadAll<Card>(item)).ToList();
                }
            } else {
                currentPool = Resources.LoadAll<Card>(poolNames[0]).ToList();
            }
            if (doesShuffle) return currentPool.OrderBy(item => rnd.Next()).ToList();
            return currentPool;
        }



    }





}
