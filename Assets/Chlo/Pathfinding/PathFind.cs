 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class pathFind : MonoBehaviour
{
    public AreaSelect AreaSelect;
    public GameObject pl;
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
    private Vector3 newPosition;
   // public Animator Ani;
    private Coroutine mvCoroutine;
    public List<Vector3> pubDirs;
    private void Awake()
    {
      
        gridOb = GameObject.Find("ChessGrid");
    }
    //TESTING SWITCH CASE AND FILTER CODE GOES HERE UNTIL BEN MAKES TURN SYSTEM
    //e.g 
    /*
    switch(item.GetComponent<SCRIPTNAME>().string){
    case "Charger":

    startFunc(x, z);
    break;
    }
    */
    /// <summary>
    /// jank if statement for each offset position, throw it in a function  and forgor about it 
    /// 
    /// <returns></returns>


    //This is essentially the function that calls everything else, if you are going to make the enemy do anything special -  
    //I'd make a COPY of this and go from there - lots of things use this.
    public List<GameObject> startFunc(Vector3 TargetPos, Vector3 currentPosition, GameObject item, int stepLength, string TYPE)
    {
            Vector3 reroll(Vector3 TargetPos) {
            System.Random rand = new System.Random();
            x = rand.Next(-2, 2);
            z = rand.Next(-2, 2);
            return new Vector3(x, 0, z) + TargetPos;
            }
        pubDirs = new List<Vector3>();
        nextMove = new List<GameObject>();  

    switch(TYPE){
    case "SD":           
        var ret = returnPoint(reroll(TargetPos));
        int n = 0;
        while (ret == null)
        {
            n += 1;
            ret = returnPoint(reroll(TargetPos));
            if (n > 10) break;
        }
        
        StartCoroutine(moveWithDelay(ret.transform, item));
        break;
        
        case "SP":
        for (var i = 0; i < stepLength; i++)
        {
            if (i >= 1)
            {      
                nextMove.Add(tryEMoving(TargetPos, newPosition, item));
            }
            else
            {       
                nextMove.Add(tryEMoving(TargetPos, currentPosition, item));
            }
        }
            break;
            default:
            Debug.Log("Game don't work"); 
            break;
        }  
        GameObject nxtMv = nextMove[nextMove.Count - 1];
        nxtMv.GetComponent<Node>().occupied = true;
        if (mvCoroutine != null)
        {
            StopCoroutine(mvCoroutine);
        }
        mvCoroutine = StartCoroutine(sMoveObject(nextMove, item, 1.0f));
        return nextMove;
    }


 

    public void moveObject(Transform trnf, GameObject item)
    {
        item.transform.position = trnf.position;
        ps.enemyMoves = 0;

    }


    public string deconS(Vector3 vector){
        string conv(float a){
        return Convert.ToString(a);
        }
        return conv(vector.x) + conv(vector.z);
    }



    public IEnumerator moveWithDelay(Transform trnf, GameObject item){
        yield return new WaitForSeconds(1.0f);
        item.transform.position = trnf.position;
        ps.enemyMoves = 0;
    }




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
            item.transform.position = new Vector3(Mathf.Lerp(origPos.x, target.position.x, t), 0, Mathf.Lerp(origPos.z, target.position.z, t));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }    
        yield return new WaitForSeconds(1.0f);
        item.transform.position = target.position;        
    }
    ps.enemyMoves = 0;
}

    public GameObject tryEMoving(Vector3 goal, Vector3 currentPosition, GameObject Actor)
    {
        var PGC = new List<GameObject>();
        GC = gridOb.GetComponent<createGrid>().nlist;
        var gCord = Actor.GetComponent<gridInteg>().Gcord;
        Vector3 right = new Vector3(1, 0, 0);
        Vector3 left = new Vector3(-1, 0, 0);
        Vector3 backward = new Vector3(0, 0, -1);
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3[] dir = { right, left, backward, forward };
        List<Vector3> dirs = dir.ToList();
        List<Vector3> tmpDirs = procDirs(dirs, GC, gCord); 
        Vector3 shortSightedChoice = tmpDirs.OrderBy(vec => Vector3.Distance(vec + gCord, goal)).First();
        pubDirs.Add(shortSightedChoice);
        newPosition = currentPosition + shortSightedChoice;
       return  returnPoint(newPosition);
   
}

private GameObject? returnPoint(Vector3 pos){
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

    private List<Vector3> procDirs(List<Vector3> dirs, List<Transform> GC, Vector3 cP)
    {
        List<Vector3> tempDir = new List<Vector3>();
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
    } 
 
    /// <summary>
    /// VESTIGAL FUNKY CODE THAT LINKS TOLD OUTDATED SCRIPTS
    /// 


    public List<GameObject> tryMoving(Vector3 xy, GameObject Gamer)
    {
        List<GameObject> returnCub(Vector3 xy)
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
        Vector3 reroll(Vector3 gcord)
        {
            System.Random rand = new System.Random();
            x = rand.Next(-2, 2);
            z = rand.Next(-2, 2);
            var a = new Vector3(x, 0, z) + gcord;
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
}

     



     
