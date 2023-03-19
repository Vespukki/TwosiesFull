using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Scenic
{
    public class Node : ScriptableObject
    {
        public string guid;
        public Vector2 position;

        public List<Connection> connections = new();
    }
    
    [System.Serializable]
    public struct Connection
    {
        public Port a;
        public Port b;

        public Connection(Port a, Port b)
        {
            this.a = a;
            this.b = b;
        }
    }
}
