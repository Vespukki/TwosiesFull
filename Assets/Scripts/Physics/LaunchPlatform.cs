using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.Physics
{
public class LaunchPlatform : MonoBehaviour
{
    [SerializeField] List<PathNode> nodes;
    private int nodeIndex = 0;

    [System.Serializable]
    private struct PathNode
    {
        public AnimationCurve curve; //curve from current node to next
        public float speed; //speed from current node to next
        public Vector3 position;
    }

    public float t = 0;

    private void FixedUpdate()
    {
        Vector3 newPos = Vector3.Lerp(CurrentNode().position, NextNode().position, CurrentNode().curve.Evaluate(t));
        Vector2 velocity = (newPos - transform.position) / Time.fixedDeltaTime;
        t += CurrentNode().speed * Time.fixedDeltaTime;

        if (t > 1)
        {
            IncrementNodeIndex();
            t = 0;
        }

        transform.Translate(velocity);
        transform.position = newPos;
    }

    private PathNode NextNode()
    {
        int newIndex = nodeIndex + 1;
        if (newIndex > nodes.Count - 1)
        {
            newIndex = 0;
        }
        return nodes[newIndex];
    }

    private PathNode CurrentNode()
    {
        return nodes[nodeIndex];
    }

    private void IncrementNodeIndex()
    {
        nodeIndex++;
        if (nodeIndex > nodes.Count - 1)
        {
            nodeIndex = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < nodes.Count; i++)
        {
            Gizmos.DrawSphere(nodes[i].position, .1f);
        }
        
    }
}
}