using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace Scenic
{
    public class Node : ScriptableObject
    {
        public delegate Node nodeDelegate();
        public Action<Node> OnRoomChange;

        [HideInInspector] public string guid;
        [HideInInspector] public Dictionary<DoorObject, DoorObject> connectedDoors = new();
        [HideInInspector] public Vector2 position;

        public Room room;
        private Room oldRoom; //for onvalidate verification

        private void OnValidate()
        {
            if(room != oldRoom)
            {
                Debug.Log("room change detected");
                OnRoomChange.Invoke(this);
                oldRoom = room;
            }

        }

        /// <summary>
        /// creates connection to b on a
        /// </summary>
        public void AddConnection(DoorObject a, DoorObject b)
        {
            connectedDoors.Add(a,b);
        }

        /// <summary>
        /// removes connection to b from a
        /// </summary>
        public void RemoveConnection(DoorObject a)
        {
            connectedDoors.Remove(a);
        }

        public Dictionary<DoorObject, DoorObject> GetConnections()
        {
            return connectedDoors;
        }

        public DoorObject PortToDoorObject(Port port)
        {
            return null;
        }
    }
}
