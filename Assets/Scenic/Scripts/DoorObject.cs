using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace Scenic
{
    [CreateAssetMenu]
    public class DoorObject : ScriptableObject
    {
        public Destination destination;

        public ScenePortGroup portGroup;
    }

    [System.Serializable]
    public struct Destination
    {
        public Vector2 position;
        public Door door;
    }
}
