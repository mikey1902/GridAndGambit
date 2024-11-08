using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class BTNode
    {
        protected NodeState state;

        private Dictionary<string, object> dataContext = new Dictionary<string,object>();
        public BTNode parent;
        protected List<BTNode> children = new List<BTNode>();
        public BTNode(List<BTNode> children)
        {
            foreach (var node in children)
            {
                AttachChild(node);
            }
        }
        private void AttachChild(BTNode node)
        {
        node.parent = this;
        children.Add(node);
        }
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetKey(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
                return value;

            BTNode node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }
        
        
        public bool ClearData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
            {
                dataContext.Remove(key);
                return true;
            }
            BTNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared) return true;
                
                node = node.parent;
            }
            return false;
        }
        
    }
}
