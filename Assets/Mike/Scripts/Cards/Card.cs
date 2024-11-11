using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public struct CardInfo
    {
        public int Score; 
        public Card GenCard; 
        public int Typing;
    }
    public class GridGambitUtil : MonoBehaviour
    {
       public static string[] ReturnModifiedDirectoryArr(string[] items, string directoryModification)
        {
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new string(directoryModification + items[i]);
            }
            return items;
        }
        //Concatinates all the pools()
        public static List<Card> ReturnCardPool(bool doesShuffle, params string[] poolNames)
        {
            var rnd = new Rand();
            List<Card> currentPool = new List<Card>();
            Debug.Log(poolNames.Length);
            if (poolNames.Length > 1) {
                foreach (string item in poolNames) {
                    currentPool = Enumerable.Concat(currentPool, Resources.LoadAll<Card>(item)).ToList();
                    Debug.Log(item);
                }
            } else {
                currentPool = Resources.LoadAll<Card>(poolNames[0]).ToList();
            }
            if (doesShuffle) return currentPool.OrderBy(item => rnd.Next()).ToList();
            return currentPool;
        }
      



    }





}
