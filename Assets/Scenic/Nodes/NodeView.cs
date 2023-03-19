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

        public List<DoorElement> doors = new();
        public NodeView(Node _node)
        {
            node = _node;
            title = _node.name;
            viewDataKey = _node.guid;
            style.left = node.position.x;
            style.top = node.position.y;
            style.backgroundColor = new(new Color(.4f, .4f, .4f, .8f));

            CreatePorts();
        }

        private void CreatePorts()
        {
            DoorElement holder = new(this);
            contentContainer.Add(holder);
            doors.Add(holder);
/*
            DoorElement holder2 = new(this);
            contentContainer.Add(holder2);
            doors.Add(holder2);*/
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
