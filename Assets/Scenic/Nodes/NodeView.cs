using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

namespace Scenic
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> OnNodeSelected;
        public Node node;
        public Port input;
        public Port output;
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
            input = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            if (input != null)
            {
                input.portName = "";
                inputContainer.Add(input);
            }

            output = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

            if (output != null)
            {
                output.portName = "";
                outputContainer.Add(output);
            }
        }

        public void NodeUpdate()
        {
            node.name = node.doorName;
            node.oldName = node.doorName;
            string assetPath = AssetDatabase.GetAssetPath(node);
            Debug.Log(assetPath);
            AssetDatabase.RenameAsset(assetPath + "/" + node.oldName, node.doorName);
            AssetDatabase.SaveAssets();
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
}
