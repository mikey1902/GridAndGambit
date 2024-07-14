using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GridGambitProd;
using System.Linq;
using rnd=System.Random;
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
    public class represent{
    public foundUnit found {get; set;}
    public int currentScore {get; set;}
    public represent(foundUnit foundUn, int intialScore){
    found = foundUn;
    currentScore = intialScore;
    }
    
    public void addRep(int score){
    currentScore += score;
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
       tree = populateLis(40);  
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
        List<foundUnit> listOfFriendly = new List<foundUnit>();
        List<foundUnit> listOfUnfriendly = new List<foundUnit>();

       
       // var permutation = GetPermutations(turn.currentHand, Mathf.Min(6, turn.currentHand.Count));
       var permutation = PermutationHelper.GetRandomPermutations(turn.currentHand, Mathf.Min(6, turn.currentHand.Count), 25);
        foreach (var collection in permutation)
        {
            int combinedPlayScore = 0;
            foreach (SubNode item in collection)
            {
                
            switch(item.current.cardType){
            case Card.CardType.Spell:
            item.addScore(tryPlayingSpell(listOfUnfriendly));
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
             combinedPlayScore += item.Score;
            }
        Debug.Log(combinedPlayScore);
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
    
    public int tryPlayingSpell(List<foundUnit> theirs){
        //init
    List<represent> possibleTargets = new List<represent>();
    foreach(var item in theirs){
    if (!item.IsOurs){
    possibleTargets.Add(new represent(item,0));}
    }
    //Fill score 
    foreach(var item in possibleTargets){ 
    //Destroy PriorityTarget
    switch(item.found.Type){
        case "Pawn":
        item.addRep(1);
        break;
        case "Bishop":
        item.addRep(3);
        break;
    }
    } 
    return possibleTargets.OrderByDescending(item => item.currentScore).FirstOrDefault().currentScore;
    }

    public int tryPlayingMove(/*List<foundUnit> ours, Card a*/){
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
        List<foundUnit>[] units = new List<foundUnit>[2];
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
        foreach(foundUnit item in ret){
        if(!item.IsOurs){
        units[1].Add(item);
        }else{
        units[0].Add(item);
        }    
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


      
}
//Any No good code not written by me, because I'm too stupid goes here:
 //Literal Stolen code, arrest me officer!
    public class PermutationHelper{
     
     public static rnd rng = new rnd();  
     public static IEnumerable<IEnumerable<T>> GetRandomPermutations<T>(IEnumerable<T> list, int length, int sampleSize)
    {
          
        var permutations = GetAllPermutations(list, length).ToList();
        return permutations.OrderBy(x => rng.Next()).Take(sampleSize);
    }
 public static IEnumerable<IEnumerable<T>> GetAllPermutations<T>(IEnumerable<T> list, int length){
           if (length == 1)
            return list.Select(t => new T[] { t });

        return GetAllPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new T[] { t2 }));
    }
     public static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
    {
        if (length == 0)
            return new[] { new T[0] };

        return list.SelectMany((item, index) =>
            GetCombinations(list.Skip(index + 1), length - 1).Select(subcomb => new[] { item }.Concat(subcomb)));
        }

    }
}
