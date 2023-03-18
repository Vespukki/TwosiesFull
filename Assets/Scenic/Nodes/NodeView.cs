using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Scenic
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> OnNodeSelected;
        public Action<Node> OnNodeRoomChange;
        public Node node;

        public List<VisualElement> doorVisualElements = new();

        public NodeView(Node _node)
        {
            node = _node;
            title = _node.name;
            viewDataKey = _node.guid;
            style.left = node.position.x;
            style.top = node.position.y;

            
            CreatePorts();
        }

        private void NodeRoomChange(Node node)
        {
            //node.connectedNodes.ForEach(n => 
           // { 
                //TODO: remove all connections from node
            //});
            CreatePorts();
        }

        private void CreatePorts()
        {
            if (node != null && node.room != null)
            {
              

                foreach (var door in node.room.doors)
                {
                    var section = new DoorContentContainer();
                    section.door = door;

                    // Add some content to the section
                    string title = "name not found";
                    if(door != null && door.destination.door != null)
                    {
                        title = door.destination.door.name;
                    }
                    section.Add(new Label(title));
                    
                    // Style the section using CSS
                    section.style.backgroundColor = new StyleColor(new Color(.4f, .4f, .4f, .65f));
                    section.style.paddingLeft = 10;
                    section.style.paddingTop = 10;
                    section.style.paddingRight = 10;
                    section.style.paddingBottom = 10;

                    // Add the section to the node's content container
                    contentContainer.Add(section);
                    node.OnRoomChange = NodeRoomChange;

                    Port inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                    door.portGroup.input = new(door, inputPort);
                    if (door.portGroup.input.port != null)
                    {
                        door.portGroup.input.port.portName = "in";
                        section.Add(door.portGroup.input.port);
                    }

                    Port outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                    door.portGroup.output = new(door,outputPort);

                    if (door.portGroup.output.port != null)
                    {
                        door.portGroup.output.port.portName = "out";
                        section.Add(door.portGroup.output.port);
                    }
                }
            }
        }


        /// <summary>
        /// connects the two doors so that they both link to each other
        /// </summary>
        private void ConnectPorts(ScenePortGroup a, ScenePortGroup b)
        {
            foreach (var connection in a.output.connections)
            {
                //connection.Re
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
        public DoorPort input;
        public DoorPort output;
    }

}
