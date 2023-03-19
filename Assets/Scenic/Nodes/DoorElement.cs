using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace Scenic
{
    public class DoorElement : GraphElement
    {
        public NodeView node;
        public DoorObject door;
        public Port input;
        public Port output;

        public DoorElement(NodeView node)
        {
            this.node = node;

            style.backgroundColor = new(new Color(.3f,.3f,.3f,.8f));


            string labelText = "door not found";
            if(door != null)
            {
                labelText = door.name;
            }
            Label label = new(labelText);
            contentContainer.Add(label);

            input = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            if (input != null)
            {
                input.portName = "in";
                this.Add(input);
            }

            output = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            if (output != null)
            {
                output.portName = "out";
                this.Add(output);
            }
        }
    }
}
