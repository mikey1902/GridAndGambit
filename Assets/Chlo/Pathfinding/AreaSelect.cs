using System.Collections.Generic;
using UnityEngine;


public class AreaSelect : MonoBehaviour
{
    private Vector3[] a;
    private GameObject[] b;
    private Vector3[] pat;
    private int[,] definePattern;
    private GameObject chessObj;
    public createGrid createGrid;
    public GameObject fodder;

    void Awake()
    {
        chessObj = GameObject.Find("/ChessGrid");
        createGrid = chessObj.GetComponent<createGrid>();
    }
    public void call(int pattern, Vector3 orig)
    {
        a = returnDamageGroup(pattern, orig);
        b = new GameObject[a.Length];
        for (var i = 0; i < a.Length; i++)
        {
            b[i] = GetNodeFromVec(a[i], createGrid.nlist);
            if (b[i] != null)
            {
                b[i] = GetNodeFromVec(a[i], createGrid.nlist);
                GameObject show = Instantiate(fodder);
                show.transform.position = b[i].transform.position;
            }
        }
    }
      



    public GameObject? GetNodeFromVec(Vector3 xy, List<Transform> GC)
    {
        for (int i = 0; i < GC.Count; i++)
        {
            var ND = GC[i].gameObject.GetComponent<Node>();
            if (!ND.walkable){
            if (ND.Gcord == xy )
            {
                return ND.gameObject;
            }
        }
    }
    return null;
}


//Failed experiment that came close - flipping patterns, will do everything manually for this project 
/*
    public Vector3[] flpPattern(int pattern, Vector3 Origin)
    {
        Vector3[] ret = returnDamageGroup(pattern, Origin);
        var modRet = new Vector3[ret.Length];
        var z = 0;
        foreach (var item in ret)
        {
            modRet[z] = new Vector3(createGrid.wid - item.x -1f, item.y, item.z);   
            z++;
        }
        return modRet;
    }
*/ 
    public Vector3[] transformTemplate(int[,] patternTemplate, Vector3 Origin)
    {
        Vector3 qV(int xOffset, int zOffset)
        {
            return new Vector3(Origin.x + xOffset, 0, Origin.z + zOffset);
        }
        pat = new Vector3[patternTemplate.GetLength(0)];
        for (int i = 0; i < patternTemplate.GetLength(0); i++)
        {
            Vector3 tmp = qV(patternTemplate[i, 0], patternTemplate[i, 1]);
            if (tmp != null)
            {
                pat[i] = qV(patternTemplate[i, 0], patternTemplate[i, 1]);
            }
        }
        return pat;
    }
    public Vector3[] returnDamageGroup(int pattern, Vector3 Origin)
    {
        switch (pattern)
        {
            case 0:
                definePattern = new int[,] { { 0, 1 }, { 1, 0 }, { -1, 0 }, { 0, -1 }, { 0, 0 } };
                break;
            case 1:
                definePattern = new int[,] { { 3, -1 }, { -2, 1 }, { 0, 0 }, { 1, -2 }, { 1, -3 } };
                break;

            case 2:
                definePattern = new int[,] { { 1, -1 }, { -1, 2 }, { 3, -2 }, { 4, -3 }, { 1, -4 } };
                break;

            case 3:
                definePattern = new int[,] { { 1, 1 }, { 1, 0 }, { 0, 0 }, { 0, -1 }, { -1, -1 } };

                break;
            case 4:
                definePattern = new int[,] { { 0, 1 }, { 1, 0 }, { -1, 1 }, { 1, 1 }, { -1, -1 }, { 1, -1 }, { -1, 0 }, { 0, -1 }, { 0, 0 } };

                break;
            case 5:

              definePattern = new int[,] {{1, 1},{0, 1},{0, 0},{-1, 0},{-1, -1}};
            break;
            case 6:
            definePattern = new int[,] {{-1, -1},{0, -1},{0, 0},{1, 0},{1, 1}};
            break;
        }
        return transformTemplate(definePattern, Origin);
    }


}



