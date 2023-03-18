using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

namespace Scenic
{
    public struct DoorPort
    {
        public DoorObject door;
        public Port port;

        public DoorPort(DoorObject door, Port port)
        {
            this.door = door;
            this.port = port;
        }
    }
}
