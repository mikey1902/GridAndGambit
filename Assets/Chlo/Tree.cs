using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGambitProd;
using System.Linq;
namespace Mini
{
    
//Subnode is designed to hold a SINGLE card 
    public class SubNode 
    {
        public Card current { get; set; }
        public int Score { get; set; }
        public SubNode(Card card){
        current = card;
        }
           public void addScore(int score)
        {
        if (Score == null)
        {
            Score += score;
        }
        else
        {
            Score = score;
        }
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
}
void Start(){
    takeTurn(true, tree, 0);

}

    public int? takeTurn(bool cont, List<Node> tree, int turnNumber){
        if(!cont){
        //if turn action has been decided, or player turn
        return null;
        }else{
        Node turn = findNodeFromId(tree, turnNumber);
        turn = populatedSubNodes(AIhand.Count, turn);
        List<foundUnit> foundUnits = findUnits();
        //Bad list
        var permutation = GetAllPermutations(turn.currentHand, Mathf.Min(6, turn.currentHand.Count));
        foreach (var collection in permutation)
        {
            int b = 0;
            foreach (SubNode item in collection)
            {
                
             switch(item.current.cardType){
            case Card.CardType.Spell:
            item.addScore(tryPlayingSpell());
            break;
            case Card.CardType.Move:
            item.addScore(tryPlayingMove());
            break;
            case Card.CardType.Unit:
            item.addScore(tryPlayingUnit());
            break;
            default:

            break;
                }
                b = b+ item.Score;
            }
        Debug.Log(b);
        }
        
        cont = false;     
    }
    return 1;
}

    //Move towards enemyKing = good!
    //Destroy enemy unit = better! (priority before king move)
    //Place friendly unit = even better!
    
    public int tryPlayingUnit(){
    //King needs to keep track of 'spawn cells'
    return 3;
    }
    /*public void tryPlayingStructure(){
    

    }*/
    public int tryPlayingSpell(){
        return 2;
    }
    public int tryPlayingMove(){
    return 1;
    }
   
   
   
    public int boardScore(int a){
        return 1;
    }



    
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
 
    public List<foundUnit> findUnits()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Unit");
        GameObject[] tmp2 = GameObject.FindGameObjectsWithTag("Structure");
        tmp.Concat(tmp2);
        List<foundUnit> ret = new List<foundUnit>();
        foreach (GameObject obj in tmp)
        {   
            var scr = obj.GetComponent<gridInteg>();
            foundUnit current = new foundUnit(obj.name, scr.gcord, scr.isOurs);
            ret.Add(current);
        }
        return ret;
    }
       private Node populatedSubNodes(int length, Node curr){
        for (var i = 0; i < length; i++)
        {
            curr.AddSubNode(new SubNode(AIhand[i]));
        }
        return curr;
    }
//Any No good code not written by me, because I'm too stupid goes here:
 //Literal Stolen code, arrest me officer!

       public static IEnumerable<IEnumerable<T>> GetAllPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1)
            return list.Select(t => new T[] { t });

        return GetAllPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}
}
    