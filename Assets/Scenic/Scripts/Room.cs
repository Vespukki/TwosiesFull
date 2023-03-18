using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenic
{
    [CreateAssetMenu]
    public class Room : ScriptableObject
    {
        public SceneField scene;
        public string RoomName => scene.SceneName;
        public List<DoorObject> doors = new();
    }
}
