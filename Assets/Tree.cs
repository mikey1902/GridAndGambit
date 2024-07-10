using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini{


 private class foundUnit
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

    private class SubNode 
    {
    public ScriptableObject current { get; set; }
    internal int Score { get; set; }

    }

public class Node {
        public int id { get; set; }
        public int ParentId { get; set; }
        public Node? child { get; set; }

        public SubNode[] currentHand { get; set; }
        internal bool haveIBeenVisited;
        internal int W;
        internal int L;
        internal int COST;
        internal Node()
        {
            COST = W - L;
        }
    }
public class MiniMa : MonoBehaviour
{

   
    public GameObject tmp;
    public ScriptableObject[] AIhand;    
    // Start is called before the first frame update



public List<Node> populateLis(int endId){
int id = 0
List<Node> preLink = new List<Node>();
Node root = new Node(){-1, -2, null, populatedSubNodes(new SubNode[AIhand.length])};
preLink.Add(root);
do {
Node tmp = new Node(){id, id-1, null, populatedSubNodes(new SubNode[AIhand.length])};
pop.Add(tmp);
id++;
}
while (pop.Count < length);
foreach(var nd in preLink){
if (nd.id != -1){
   nd.child = findNodeFromId(nd.id+1);
}

} 



}

private Node findNodeFromId(List<Node> tree, int id){
Node currentNode = tree[0];
while(currentNode.localId != id){
currentNode = currentNode.child;
}
return currentNode;
}

/*
private Node createNewNode(int id, int cardAmount){
return new Node{
        id = id,
        children = new SubNode[cardAmount],


}
}*/

private SubNode[] populatedSubNodes(SubNode[] emptySubs){
for(var i = 0; i < length;i++){
    emptySubs[i] = new SubNode(AIhand[i]);
}
}
private List<GameObject> findUnits(){
List<GameObject> tmp = GameObject.FindObjectsWithTag("Unit");
List<foundUnit> ret = new List<foundUnit>();
foreach(GameObject obj in tmp) {
var scr = obj.GetComponent<gridInteg>();
foundUnit current = new foundUnit(obj.name, scr.Gcord, scr.isOurs);
ret.Add(current);
}
return tmp;
}

private Node findNodeFromId(List<Node> tree, int id){
Node currentNode = tree[0];
while(currentNode.localId != id){
currentNode = currentNode.child;
}
return currentNode;
}


    









    // Update is called once per frame
 
}




}
