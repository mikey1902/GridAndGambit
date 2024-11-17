using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Rand = System.Random;
using UnitMoveType = PlayerUnit.UnitMoveType;
namespace GridGambitProd
{
    public class Card : ScriptableObject
    {

        public string cardName;
        public CardType cardType;
        public ImageType imageType;
        public Sprite cardSprite;
        public string cardText;
        public bool hasExtraEffect;
        public float cardScore;
        public bool canPlayOnSelf;

        public enum CardType
        {
            Attack,
            Move,
            Support,
        }

        public enum ImageType
        {
            Pawn,
            Rook,
            Bishop,
            Knight
        }
    }



    public class GridGambitUtil : MonoBehaviour
    {

        /// <summary>
        /// MIKE USE THIS METHOD!! it'll save ur life someday
        ///
        /// 
        /// </summary>
        /// <param name="doesShuffle"></param>
        /// <param name="poolNames"></param>
        /// <returns></returns>
        public static List<Card> ReturnCardPool(bool doesShuffle, params string[] poolNames)
        {
            var rnd = new Rand();
            List<Card> currentPool = new List<Card>();
            Debug.Log(poolNames.Length);
            if (poolNames.Length > 1)
            {
                foreach (string item in poolNames)
                {
                    currentPool = Enumerable.Concat(currentPool, Resources.LoadAll<Card>(item)).ToList();
                    Debug.Log(item);
                }
            }
            else
            {
                currentPool = Resources.LoadAll<Card>(poolNames[0]).ToList();
            }

            if (doesShuffle) return currentPool.OrderBy(item => rnd.Next()).ToList();
            return currentPool;
        }

        public static List<GameObject> FindNearestTarget(Transform self, bool friendly)
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Unit");
            IEnumerable<GameObject> nearestUnfriendly =
                from item in allObjects where item.name != "baddie" select item;
            IEnumerable<GameObject> nearestFriends =
                from item in allObjects where item.name == "baddie" select item;
            if (friendly && nearestFriends.ToList().Count > 0)
            {
                return nearestFriends.ToList().OrderBy(item => Vector2.Distance(item.transform.position, self.position))
                    .ToList();
            }else if (!friendly && nearestUnfriendly.ToList().Count > 0){
                return nearestUnfriendly.ToList()
                    .OrderBy(item => Vector2.Distance(item.transform.position, self.position)).ToList(); 
            }
            else
            {
                return new List<GameObject>();
            }
        }
        
        
    public static string[] ReturnModifiedDirectoryArr(string[] items, string directoryModification)
        {
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new string(directoryModification + items[i]);
            }
            return items;
        }
        //Concatinates all the pools()


        public static HandManager GetHandManager()
		{
            HandManager handManager;
            handManager = FindObjectOfType<HandManager>();
            return handManager;
        }
        public static bool MoveSetup(GridManager gridManager, Vector2 cellPos, int moveDistance, UnitMoveType unitMoveType)
        {
            switch (unitMoveType)
            {
                case UnitMoveType.Orthogonal:
                    gridManager.OrthogonalMovement(cellPos, moveDistance);
                    return true;

                case UnitMoveType.Diagonal:
                    gridManager.DiagonalMovement(cellPos, moveDistance);
                    return true;

                case UnitMoveType.LShape:
                    gridManager.LShapeMovement(cellPos, moveDistance);
                    return true;
            }
            return false;
        }
    }





    
    
    
    
    
    
}


