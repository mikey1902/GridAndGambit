 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class pathFind : MonoBehaviour
{
    public AreaSelect AreaSelect;
    //public GameObject pl;
    public GameObject gridOb;
    private List<Transform> possMoves;
    [SerializeField] private GameObject[] enemies;
    public List<GameObject> nextMove;
    private List<Transform> GC;
    public int x, z;
    //public TurnbaseSystem tbs;
    //public TurnWatcher tw;
    public patternSelect ps;
    public gridInteg gridInteg;
    public bool enemyMoving;
    public bool enemyTurnDone;
    private Vector2 newPosition;
   // public Animator Ani;
    private Coroutine mvCoroutine;
    public List<Vector2> pubDirs;
    private void Awake()
    {
      
        gridOb = GameObject.Find("ChessGrid");
    }

    public List<GameObject> startFunc(List<GameObject> nextMove, Vector2 currentPosition, GameObject item, int length)
    {        
       /* pubDirs = new List<Vector2>();
        StartCoroutine(moveWithDelay(ret.transform, item));
        GameObject finalDestination = nextMove[nextMove.Count - 1];
        finalDestination.GetComponent<Node>().occupied = true;
        if (mvCoroutine != null)
        {
            StopCoroutine(mvCoroutine);
        }
        mvCoroutine = StartCoroutine(sMoveObject(nextMove, item, 1.0f));*/
        return nextMove;

    }
   /* public void moveObject(Transform trnf, GameObject item)
    {
        item.transform.position = trnf.position;
        ps.enemyMoves = 0;
    }*/


    public string deconS(Vector2 vector){
        string conv(float a){
        return Convert.ToString(a);
        }
        return conv(vector.x) + conv(vector.y);
    }



   /* public IEnumerator moveWithDelay(Transform trnf, GameObject item){
        yield return new WaitForSeconds(1.0f);
        item.transform.position = trnf.position;
        ps.enemyMoves = 0;
    }*/ 




    public IEnumerator sMoveObject(List<GameObject> targets, GameObject item, float speed)
    {
        for (var i = 0; i < targets.Count; i++){  
        float elapsedTime = 0;
        float timeToMove = speed;
        var origPos = item.transform.position;
        string dir = deconS(pubDirs[i]);

        GameObject Gntarget = targets[i];
        if (Gntarget == null){
        i++;
        }
       Transform target = targets[i].transform;
        while (elapsedTime < timeToMove)
        {
            float t = elapsedTime / timeToMove;
            item.transform.position = new Vector3(Mathf.Lerp(origPos.x, target.position.x, t),0, Mathf.Lerp(origPos.y, target.position.y, t));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }    
        yield return new WaitForSeconds(1.0f);
        item.transform.position = target.position;        
    }
    //ps.enemyMoves = 0;
}

  /*  public GameObject tryEMoving(Vector2 goal, Vector2 currentPosition, GameObject Actor)
    {
        var PGC = new List<GameObject>();
        GC = gridOb.GetComponent<createGrid>().nlist;
        var gCord = Actor.GetComponent<gridInteg>().Gcord;
        Vector2 right = new Vector2(1,  0);
        Vector2 left = new Vector2(-1,  0);
        Vector2 backward = new Vector2(0,  -1);
        Vector2 forward = new Vector2(0,  1);
        Vector2[] dir = { right, left, backward, forward };
        List<Vector2> dirs = dir.ToList();
        List<Vector2> tmpDirs = procDirs(dirs, GC, gCord); 
        Vector2 shortSightedChoice = tmpDirs.OrderBy(vec => Vector2.Distance(vec + gCord, goal)).First();
        pubDirs.Add(shortSightedChoice);
        newPosition = currentPosition + shortSightedChoice;
       return  returnPoint(newPosition);
   
}*/

private GameObject? returnPoint(Vector2 pos){
GC = gridOb.GetComponent<createGrid>().nlist;
for (var i = 0; i < GC.Count; i++){
            var ND = GC[i].gameObject.GetComponent<Node>();
            if (ND.Gcord == pos)
            {
            if (ND.occupied != true && ND.walkable != true)
            {
        return GC[i].gameObject;
        }
    } 
}
return null;
}
/*
    private List<Vector2> procDirs(List<Vector2> dirs, List<Transform> GC, Vector2 cP)
    {
        List<Vector2> tempDir = new List<Vector2>();
        for (var i = 0; i < dirs.Count; i++)
        {
            for (var j = 0; j < GC.Count; j++)
            {
                if ((dirs[i] + cP) == GC[j].gameObject.GetComponent<Node>().Gcord)
                {
                    if(GC[j].gameObject != null)
                    if (GC[j].gameObject.GetComponent<Node>().walkable == false && GC[j].gameObject.GetComponent<Node>().occupied == false)
                    {
                        tempDir.Add(dirs[i]);
                    }
                }
            }
        }
        return tempDir;
    } */
 
    /// <summary>
    /// VESTIGAL FUNKY CODE THAT LINKS TOLD OUTDATED SCRIPTS
    /// 


   /* public List<GameObject> tryMoving(Vector2 xy, GameObject Gamer)
    {
        List<GameObject> returnCub(Vector2 xy)
        {
            GC = Gamer.GetComponent<createGrid>().nlist;
            var PGC = new List<GameObject>();
            for (int i = 0; i < GC.Count; i++)
            {
                var ND = GC[i].gameObject.GetComponent<Node>();
                if (ND.Gcord == xy)
                {
                    if (ND.occupied != true && ND.walkable != true)
                    {
                        PGC.Add(GC[i].gameObject);
                    }
                }
            }
            return PGC;
        }
     /*   Vector2 reroll(Vector2 gcord)
        {
            System.Random rand = new System.Random();
            x = rand.Next(-2, 2);
            z = rand.Next(-2, 2);
            var a = new Vector2(x, z) + gcord;
            return a;
        }
        var ret = returnCub(xy);
        int n = 0;
        while (ret.Count <= 0)
        {
            n += 1;
            ret = returnCub(reroll(xy));
            if (n > 10) break;
        }
        return ret;
    }
    }*/
}

     



     
