using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using System.Linq;
using Mini;
namespace Mini
{
   

    
//Subnode is designed to hold a SINGLE card 
    public class SubNode 
    {
        public Card current { get; set; }
        internal int? Score { get; set; }
        public SubNode(Card card){
        current = card;
        }
      
    }
    public class Node : IEnumerable<SubNode>
    {
        public List<SubNode> currentHand = new List<SubNode>();
        public int Id { get; set; }
        public int ParentId{get; set;}
        public Node? Child { get; set; }
        public bool haveIBeenVisited;
        public List<SubNode> AddSubNode(SubNode subnode){
            currentHand.Add(subnode);
            return currentHand;
        }
        public Node(int id, int parentId, Node? child)
        {
            Id = id;
            ParentId = parentId;
            Child = child;
            //CurrentHand = currentHand;
            haveIBeenVisited = false;
        }
        public IEnumerator<SubNode> GetEnumerator()
        {
        return currentHand.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
        return GetEnumerator();
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
public class Tree : MonoBehaviour
{
    //public object 
    public GameObject tmp;
    public List<Node> tree;
    public List<Card> AIhand;

    // Start is called before the first frame update
    void Awake()
    {
       AIhand = tmp.GetComponent<HandManager>().tempList;
       tree = populateLis(50);
       takeTurn(true, tree, 0);

    /*while (currentNode != null)
    {
        Debug.Log("Node ID: " + currentNode.Id);
        currentNode = currentNode.Child;
    }*/
}

    public int? takeTurn(bool cont, List<Node> tree, int turnNumber){
        if(!cont){
        //if turn action has been decided, or player turn
        return null;
        }else{
        Node turn = findNodeFromId(tree, turnNumber);
        populatedSubNodes(AIhand.Count, turn);
        var permutation = GetAllPermutations( turn.currentHand,turn.currentHand.Count);
        foreach (var collection in permutation)
        {
            foreach (var item in collection)
            {
            



            }
        }
        cont = false;
        return 1;
    }
}



       
       //var ls = GetAllPermutations(breh.CurrentHand.ToList(), AIhand.Count);
      // Debug.Log(ls[0].Count);
    


    //Move towards enemyKing = good!
    //Destroy enemy unit = better! (priority before king move)
    //Place friendly unit = even better!
    
    public void returnOrderOfPlay(Card[] currentHand){


    }
    public void tryPlayingUnit(){

    }
    public void tryPlayingStructure(){

    }
    public void tryPlayingSpell(){

    }
    public void tryPlayingMove(){

    }
   
   
   /*
    public object boardScore(int, int){
        switch(current.Type){
        case Unit:
         
        break;
        case Structure:
        
        break;
        case Spell:
        return 
        break;
        case Move:
        return tryPlayingMove()
        break;
        default:
        Debug.Log(current.Type.toString());
        break;
    }


    }*/
    public void returnCardScore(Card current){

    }
    public void cardTree(int amountOfCards){
    //List<SubNode> 
 
    }
    public List<Node> populateLis(int endId)
    {
        int id = 1;
        List<Node> preLink = new List<Node>();
        Node root = new Node(0, -1, null);
        preLink.Add(root);
        do
        {
            Node tmp = new Node(id, id-1, null);
            preLink.Add(tmp);
            id++;
           // Debug.Log(tmp.Id);
        }
        while (preLink.Count < endId);
        return createLinks(preLink);
    }

    public List<Node> createLinks(List<Node> preLink){
    foreach (Node item in preLink){
            item.Child = findNodeFromId(preLink,(item.Id+1));
        }
        return preLink;
    }
  public Node findNodeFromId(List<Node> tree, int idToFind)
{
    if (tree == null || tree.Count == 0)
    {
        // Tree is empty
        return null;
    }
    foreach (var node in tree)
    {
        if (node.Id == idToFind)
        {
            return node;
        }
    }

    // Node with the specified ID was not found
    return null;
}
//fin
 
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
       private void populatedSubNodes(int length, Node curr)
    {
        for (var i = 0; i < length; i++)
        {
            curr.AddSubNode(new SubNode(AIhand[i]));
        }
    }
//Any No good code not written by me, because I'm too stupid goes here:
 //Literal Stolen code, arrest me officer!
static IEnumerable<IEnumerable<T>> GetAllPermutations<T>(IEnumerable<T> list, int length )
    {
        // Base case: if the length is 1, return each element as a single-element array
        if (length == 1) return list.Select(t => new T[] { t });
        // Recursive case: get permutations of length (n-1)
        return GetAllPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)), // Add elements not already in the permutation
                        (t1, t2) => t1.Concat(new T[] { t2 })); // Concatenate current element to the permutation
        }
    }  
}