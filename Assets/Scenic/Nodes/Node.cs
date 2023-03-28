using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenic
{
    public abstract class Node : ScriptableObject
    {
        public Action<Node> OnNodeUpdate;

        public string guid;
        [HideInInspector] public List<Node> connections = new();
        [HideInInspector] public Vector2 position;

        public string doorName;

        [HideInInspector] public string oldName; 
    }
}
