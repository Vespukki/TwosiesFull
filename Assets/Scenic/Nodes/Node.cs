using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenic
{
    public class Node : ScriptableObject
    {
        public string guid;
        public List<Node> connections = new();
        public Vector2 position;
    }
}
