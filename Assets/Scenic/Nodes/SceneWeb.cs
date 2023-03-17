using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Scenic
{
    [CreateAssetMenu()]
    public class SceneWeb : ScriptableObject
    {
        public Node rootNode;

        public List<Node> nodes = new();

        public Node CreateNode(System.Type type)
        {
            Node node =ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}
