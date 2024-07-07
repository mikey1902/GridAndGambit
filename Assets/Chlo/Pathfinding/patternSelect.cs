using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Randoms = System.Random;
using Random = UnityEngine.Random;
public class patternSelect : MonoBehaviour
{

    private gridInteg gridInteg;
    public GameObject tbs;
    [SerializeField] private GameObject pl;
    [SerializeField] private Vector3 plC;
    private GameObject pathManager;
    public string enemyType;
    private int[] patterns;
    public int enMoves;
    private AreaSelect AreaSelect;
    public pathFind pathFind;
    private List<GameObject> pth;
    public float Initiative;
    public bool isMoving;
    public int enemyMoves;
    private bool playerTurn;
   // private Animator ani;
    /*  public int[] allowedPatterns
      {
          get
          {
              return patterns;
          }
          set
          {
              patterns = value;
          }
      }
  */


    void Start()
    {   


        
        tbs = GameObject.Find("EventSystem");
       // playerTurn = tbs.GetComponent<TurnbaseSystem>().playersTurn;
        gridInteg = gameObject.GetComponent<gridInteg>();
      //  isMoving = false;
        pathManager = GameObject.Find("PathManager");
        pathFind = pathManager.GetComponent<pathFind>();
        AreaSelect = pathManager.GetComponent<AreaSelect>();
     //   ani = transform.GetChild(0).GetComponent<Animator>();
        /*
 
*/
    }
    void Update()
    {
      //  pl = FindObjectOfType<newPlayerMovement>().gameObject;
        plC = pl.GetComponent<gridInteg>().gcord;
    }

    private void performAttack(Vector2 orient, int length)
    {   
    AreaSelect.call(patterns[Random.Range(0, patterns.Length)], pl.GetComponent<gridInteg>().gcord);
    }
    public void ultizeCard(ScriptableObject card, Vector2 orient)
    {
        bool returnRand(int min, int max)
        {
            Randoms rand = new System.Random();
            return Convert.ToBoolean(rand.Next(min, max));
        }
       /* switch (card.nameProperty)
        {
            case "Sheep":
                pathFind.startFunc(plC, gameObject.GetComponent<gridInteg>().Gcord, gameObject, 4, "SP");                                                   //                                                                                                                                           // Debug.Log("PLAYER TARGET: " + plC);
                performAttack(card, orient);
                break;


            case "SD":
               // pathFind.startFunc(plC, gameObject.GetComponent<gridInteg>().Gcord, gameObject, 1, "SD");
                performAttack(card, orient);
                
                break;
    }*/
    }

    private IEnumerator delay(){
        //ani.SetBool("teleport", true);
        new WaitForSeconds(1.0f);
        pathFind.startFunc(plC, gameObject.GetComponent<gridInteg>().Gcord, gameObject, 1, "SD");
        //ani.SetBool("teleport", false);
        yield return new WaitForEndOfFrame();

    }
   
}
