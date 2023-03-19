using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;
using System.Linq;

namespace Scenic
{
    public class ScenicEditorView : GraphView
    {
        public Action<NodeView> OnNodeSelected;
        public new class UxmlFactory : UxmlFactory<ScenicEditorView, GraphView.UxmlTraits> { }

        SceneWeb web;
        
        public ScenicEditorView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scenic/ScenicEditor.uss");
            styleSheets.Add(styleSheet);
        }

        NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }

        internal void PopulateView(SceneWeb web)
        {
            this.web = web;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            //creates node views
            web.nodes.ForEach(n => CreateNodeView(n));

            //creates edges
            web.nodes.ForEach(n =>
            {
                n.connections.ForEach(c =>
                {
                    Debug.Log(c.a);
                    Debug.Log(c.b);
                    Edge edge = c.a.ConnectTo(c.b);

                    if(edge != null)
                    {
                        AddElement(edge);
                    }
                });
            });
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    if (elem is NodeView nodeView)
                    {
                        web.DeleteNode(nodeView.node);
                    }

                    if (elem is Edge edge)
                    {

                        NodeView aView = edge.output.node as NodeView;
                        //NodeView bView = edge.input.node as NodeView;

                        List<Connection> tempConnections = new(aView.node.connections);

                        tempConnections.ForEach(c =>
                        {
                            if(c.a == edge.output && c.b == edge.input)
                            {
                                web.RemoveConnection(aView.node, c);
                            }
                        });
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView aView = edge.output.node as NodeView;

                    //web.AddConnection(aView.node, new(edge.output, edge.input));

                    aView.node.connections.ForEach(c =>
                    {
                        if(true)
                        {
                            if (c.a == edge.output && c.b == edge.input)
                            {
                                web.AddConnection(aView.node, c);
                            }
                        }
                    });
                });
            }

            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //base.BuildContextualMenu(evt);
            {
                var types = TypeCache.GetTypesDerivedFrom<Node>();

                Vector2 pos = evt.mousePosition;

                evt.menu.AppendAction($"[Node] node", (a) => CreateNode(typeof(Node), pos));
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type, pos));
                }

            }
        }

        private void CreateNode(Type type, Vector2 position)
        {
            Node node = web.CreateNode(type);
            node.position = position;

            CreateNodeView(node);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }


        void CreateNodeView(Node node)
        {
            NodeView nodeView = new(node);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }
    }
}
