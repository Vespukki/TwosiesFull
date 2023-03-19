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

        /// <summary>
        /// creates connection to b on a
        /// </summary>
        public void AddConnection(Node node, DoorElement a, DoorElement b)
        {
            node.connections.Add(new(a.output,b.input));
        } 
        public void AddConnection(Node node, Connection connection)
        {
            node.connections.Add(connection);
        }

        /// <summary>
        /// removes connection to b from a
        /// </summary>
        public void RemoveConnection(Node node, DoorElement a, DoorElement b)
        {
            node.connections.Remove(new(a.output,b.input));
        }

        public void RemoveConnection(Node node, Connection connection)
        {
            node.connections.Remove(connection);

        }

        public List<Connection> GetConnections(Node node)
        {
            return node.connections;
        }
    }
}
