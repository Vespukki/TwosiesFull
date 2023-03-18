using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace Scenic
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> OnNodeSelected;
        public Node node;
        public ScenePortGroup portGroup;
        public NodeView(Node _node)
        {
            node = _node;
            title = _node.name;
            viewDataKey = _node.guid;
            style.left = node.position.x;
            style.top = node.position.y;

            CreatePorts();
        }

        private void CreatePorts()
        {
            portGroup.input = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            if (portGroup.input != null)
            {
                portGroup.input.portName = "";
                inputContainer.Add(portGroup.input);
            }

            portGroup.output = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

            if (portGroup.output != null)
            {
                portGroup.output.portName = "";
                outputContainer.Add(portGroup.output);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;

        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }

    }

    public struct ScenePortGroup
    {
        public Port input;
        public Port output;
    }

}
