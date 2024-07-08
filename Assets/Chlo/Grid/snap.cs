using System.Collections.Generic;
using UnityEngine;

public class snap : MonoBehaviour
{

     public List<GameObject> allGridable;
    public LayerMask gridable;
    public float dist;
    private float distance;
    [SerializeField] private GameObject nearest;
    public Vector2 gcord;

    void Update()
    {
        allGridable = findGriable();
        var isInContact = checkForObstacle();
        gcord = gameObject.GetComponent<Node>().gcord;
            for (var i = 0; i < allGridable.Count; i++)
            {
                if (isInContact == true)
                {
                    distance = Vector3.Distance(this.transform.position, allGridable[i].transform.position);
                    if (distance < dist)
                    {
                        nearest = allGridable[i];
                    gameObject.GetComponent<Node>().cellOccupied = true;
                        nearest.GetComponent<gridInteg>().gcord = gameObject.GetComponent<Node>().gcord;
                      }
                    }
                else
                {
                gameObject.GetComponent<Node>().cellOccupied = false;
            }
        }
    }
    public bool checkForObstacle()
    {
        return Physics.CheckBox(transform.position, new Vector2(dist, dist), Quaternion.identity, gridable);
    }

    private List<GameObject> findGriable()
    {
        var s = GameObject.FindGameObjectsWithTag("Structure");
        var u = GameObject.FindGameObjectsWithTag("Unit");
        var k = GameObject.FindGameObjectsWithTag("King");
        var basis = joinTwo(s, u);
        var combinedList = new List<GameObject>();
        for (var i = 0; i < basis.Length; i++)
        {combinedList.Add(basis[i]);}
        /*combinedList.Add(k[0]);
        combinedList.Add(k[1]);*/
        return combinedList;
    }
    private GameObject[] joinTwo(GameObject[] array1, GameObject[] array2)
    {
        var newAr = new GameObject[array1.Length + array2.Length/*+2*/];
        int ar1Len = array1.Length;
        array1.CopyTo(newAr, 0); 
        array2.CopyTo(newAr, ar1Len); 
        return newAr;
    }
}