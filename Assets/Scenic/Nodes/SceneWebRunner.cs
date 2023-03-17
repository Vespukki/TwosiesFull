using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenic
{
    public class SceneWebRunner : MonoBehaviour
    {
        SceneWeb web;
        private void Start()
        {
            web = ScriptableObject.CreateInstance<SceneWeb>();

            var root = ScriptableObject.CreateInstance<Node>();

            web.rootNode = root;
        }
    }
}
