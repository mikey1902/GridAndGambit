using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using System.Linq;
using Mini;
namespace Mini
{
   
    public class SubNode
    {
        public Card current { get; set; }
        internal int? Score { get; set; }

        public SubNode(Card card){
        current = card;
        }

    }

    public class Node
    {
        public int Id { get; set; }
        public int ParentId
        {
            get; set;}
        public Node? Child { get; set; }

        public SubNode[] CurrentHand { get; set; }
        public bool haveIBeenVisited;
        /* internal int W;
         internal int L;
         internal int COST;*/
        public Node(int id, int parentId, Node? child, SubNode[] currentHand)
        {
            Id = id;
            ParentId = parentId;
            Child = child;
            CurrentHand = currentHand;
            haveIBeenVisited = false;
        }

    }

 public class foundUnit
 {
    public string Type { get; set; }
    public Vector2 Pos { get; set; }
    public bool IsOurs { get; set; }
    public foundUnit(string type, Vector2 pos, bool isOurs)
    {
    Type = type;
     Pos = pos;
    IsOurs = isOurs;
    }
 }
public class MiniMa : MonoBehaviour
{
    /*
    public foundUnit foundUnit(int id, Vector2 gcord, isOurs){
    internal int Id = id;
    internal bool IsOurs = isOurs;
    internal Vector2 Gcord = gcord;

    }*/

    //class foundUnit Mini.foundUnit();

    public GameObject tmp;
    public List<Node> tree;
    public List<Card> AIhand;

    // Start is called before the first frame update
    void Awake()
    {
       tree = populateLis(50);
      // AIhand = tmp.GetComponent<cardDisplay>().cardData;
    }
    public List<Node> populateLis(int endId)
    {
        int id = 0;
        List<Mini.Node> preLink = new List<Node>();
        Node root = new Node(-1, -2, null, populatedSubNodes(AIhand.Count));
        preLink.Add(root);
        do
        {
            Node tmp = new Node(id, id - 1, null, populatedSubNodes(AIhand.Count) );
            preLink.Add(tmp);
            id++;
        }
        while (preLink.Count < endId);
        foreach (var nd in preLink)
        {
            if (nd.Id != -1)
            {
                nd.Child = findNodeFromId(preLink, nd.Id + 1);
            }
        }
        return preLink;
    }

    private Node findNodeFromId(List<Node> tree, int idToFind)
    {
        Node currentNode = tree[0];
        while (currentNode.Id != idToFind)
        {
            currentNode = currentNode.Child;
        }
        return currentNode;
    }
//fin
    private SubNode[] populatedSubNodes(int length)
    {
        SubNode[] newSubs = new SubNode[length];
        for (var i = 0; i < length; i++)
        {
            
            SubNode tmp = new SubNode(AIhand[i]);
            newSubs[i] = tmp;
            //[i] = new SubNode(AIhand[i]);
        }
        return newSubs;
    }
    private List<foundUnit> findUnits()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Unit");
        List<foundUnit> ret = new List<foundUnit>();
        foreach (GameObject obj in tmp)
        {   
            var scr = obj.GetComponent<gridInteg>();
            foundUnit current = new foundUnit(obj.name, scr.gcord, scr.isOurs);
            ret.Add(current);
        }
        return ret;
    }

}
}



    
