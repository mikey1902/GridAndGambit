using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class createGrid : MonoBehaviour
{
    public Vector2[,] matrix;
    public int maxX;


    public float magicX, magicZ;
    public int maxZ;
    public List<Transform> nlist;

    public List<Transform> nlist2;
    public GameObject blnk;
    private Node node;

 void Start()
    {
       createNodeGrid(maxX, maxZ);
    }

    private int round(float num)
    {
    return (int)Mathf.Floor(num);
    }
   void createNodeGrid(int len, int wid)
    {
        matrix = new Vector2[len, wid];
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < wid; j++)
            {
                matrix[i, j] = new Vector2(i, j);
                GameObject currentObject = Instantiate(blnk, new Vector2(transform.position.x,transform.position.y) - matrix[i, j], transform.rotation, this.transform);
                node = currentObject.GetComponent<Node>();
                node.Gcord = new Vector2(round(matrix[i, j].x), round(matrix[i, j].y));  
                if (i % 2 == 0){
                currentObject.GetComponent<Renderer>().material.color = j % 2 == 0 ? Color.black : Color.white;
                } else {
                currentObject.GetComponent<Renderer>().material.color = j % 2 == 0 ? Color.white : Color.black;
                }
                 nlist.Add(currentObject.transform);    
            }
        }
    }
}
