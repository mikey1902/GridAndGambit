using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Randoms = System.Random;
using Random = UnityEngine.Random;
public class patternSelect : MonoBehaviour
{

    private gridInteg gridInteg;
    [SerializeField] GameObject pathManager;
    public string card;
    private int[] patterns;
    private AreaSelect AreaSelect;
    //public pathFind pathFind;
    public List<GameObject> pth;
  


    void Start()
    {
        gridInteg = gameObject.GetComponent<gridInteg>();
        pathManager = GameObject.Find("PathManager");
        //pathFind = pathManager.GetComponent<pathFind>();
        AreaSelect = pathManager.GetComponent<AreaSelect>();
        
        //ultizeCard(new Vector2(0, 1));
        
    }
    void Update()
    {
            
        //performAttack(, new Vector2(0, 1), "S", 3);
        //  pl = FindObjectOfType<newPlayerMovement>().gameObject;
      //  plC = pl.GetComponent<gridInteg>().gcord;
    }

   
    public void ultizeCard(Vector2 orient)
    {
        bool returnRand(int min, int max)
        {
            Randoms rand = new System.Random();
            return Convert.ToBoolean(rand.Next(min, max));
        }
       
                pth = AreaSelect.call(gameObject.GetComponent<gridInteg>().Gcord, orient, card, 3);
                //pathFind.startFunc(plC, gameObject.GetComponent<gridInteg>().Gcord, gameObject, 4, "SP");   
        
    }

    private IEnumerator delay()
    {
        //ani.SetBool("teleport", true);
        new WaitForSeconds(1.0f);
        // pathFind.startFunc(plC, gameObject.GetComponent<gridInteg>().Gcord, gameObject, 1, "SD");
        //ani.SetBool("teleport", false);
        yield return new WaitForEndOfFrame();

    }

    void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        /*Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target.position);*/
    }
}


