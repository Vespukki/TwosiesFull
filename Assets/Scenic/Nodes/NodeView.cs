using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scenic
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Node node;
        public NodeView(Node _node)
        {
            this.node = _node;
            this.title = _node.name;
        }
    }
}
