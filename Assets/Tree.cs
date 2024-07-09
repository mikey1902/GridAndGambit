using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini{
public class Node {
        public int localId { get; set; }
        public double realId { get; set; }
        public int ParentId { get; set; }
        public Node?[] children { get; set; }
        public ScriptableObject currentCard { get; set; }
        internal bool haveIBeenVisited;
        internal int W;
        internal int L;
        internal int COST;
        internal Node()
        {
            COST = W - L;
        }
    }
public class Tree : MonoBehaviour
{
    public GameObject tmp;
    public ScriptableObject[] AIhand;    
    // Start is called before the first frame update


/*
public List<Node> populateLis(int length){
List<Node> pop = new List<Node>();

for(var i = 0; i < length; i++){




}





}

private Node createNewNode(int id, int cardAmount){
return new Node{
        localId = id,
        children = new Node[cardAmount],
}
}*/


    









    // Update is called once per frame
    void Update()
    {

    }
}




}
