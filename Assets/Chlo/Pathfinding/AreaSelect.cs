using System.Collections.Generic;
using UnityEngine;
public class AreaSelect : MonoBehaviour
{
    private Vector2[] a;
    private GameObject[] b;

    public GameObject fodder;
    private Vector2[] pat;
    private int[,] definePattern;
    public GameObject chessObj;
    public GridManager createGrid;

    public GameObject chessUnit;
	// public GameObject fodder;

	void Awake()
	{
        createGrid = gameObject.GetComponent<GridManager>();
	}
	void Start()
    {
        
       // call(chessUnit.GetComponent<gridInteg>().gcord, new Vector2(1, 0), "S", 3);

        
            //GetNodeFromVec(new Vector2(0, 1), createGrid.nlist));
    }

    void Update(){

            if(Input.GetKeyDown("space")){
            call(chessUnit.GetComponent<gridInteg>().gcord, new Vector2(1, -1), "S", 5);

            }
    }
    public GameObject[] call(Vector2 orig, Vector2 orient, string type, int len)
    {
        a = returnDamageGroup(type, orig, orient, len);
        Debug.Log(a[1]);
        b = new GameObject[a.Length];
        for (var i = 0; i < a.Length; i++)
        {
            //Debug.Log(a[i]);
            b[i] = GetNodeFromVec(a[i], createGrid.cellTransforms);
            if (b[i] != null)
            {
                b[i] = GetNodeFromVec(a[i], createGrid.cellTransforms);
                GameObject show = Instantiate(fodder);
                show.transform.position = b[i].transform.position;
            }
        }
        return b;
    }

    public GameObject? GetNodeFromVec(Vector2 xy, List<Transform> GC)
    {
        for (int i = 0; i < GC.Count; i++)
        {
            var ND = GC[i].gameObject.GetComponent<GridCell>();
            if (!ND.cellOccupied)
            if (ND.GridIndex == xy)
            {
                return ND.gameObject;
            }
        }
        return null;
    }


    //Failed experiment that came close - flipping patterns, will do everything manually for this project 
    /*
        public Vector2[] flpPattern(int pattern, Vector2 Origin)
        {
            Vector2[] ret = returnDamageGroup(pattern, Origin);
            var modRet = new Vector2[ret.Length];
            var z = 0;
            foreach (var item in ret)
            {
                modRet[z] = new Vector2(createGrid.wid - item.x -1f, item.y, item.z);   
                z++;
            }
            return modRet;
        }
    */
    public Vector2[] transformTemplate(int[,] patternTemplate, Vector2 Origin)
    {
        Vector2 qV(int xOffset, int yOffset)
        {
            return new Vector2(Origin.x + xOffset, Origin.y + yOffset);
        }
        pat = new Vector2[patternTemplate.GetLength(0)];
        for (int i = 0; i < patternTemplate.GetLength(0); i++)
        {
            Vector2 tmp = qV(patternTemplate[i, 0], patternTemplate[i, 1]);
            if (tmp != null)
            {
                pat[i] = qV(patternTemplate[i, 0], patternTemplate[i, 1]);
            }
        }
        return pat;
    }
    public string parseVector(Vector2 Orient)
    {
        return Orient.ToString();
    }

    public Vector2[] returnDamageGroup(string pattern, Vector2 Origin, Vector2 Orient, int len)
    {
        int[,] populateLine(Vector2 orient, int len)
        {
            int[,] currentPattern = new int[len, 2];
            for (var i = 0; i < len; i++)
            {
                currentPattern[i, 0] = (int)(orient.x * i);  // Set the x coordinate
                currentPattern[i, 1] = (int)(orient.y * i);  // Set the y coordinate
            }

            return currentPattern;
        }
        switch (pattern)
        {
            case "S":
                definePattern = populateLine(Orient, len);
                break;
            case "D":
                break;
        }
        return transformTemplate(definePattern, Origin);
    }
}
