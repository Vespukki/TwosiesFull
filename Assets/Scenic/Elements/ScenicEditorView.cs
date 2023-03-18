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
                var connected = web.GetConnections(n);
                connected.ForEach(c =>
                {
                    NodeView aView = FindNodeView(n);
                    NodeView bView = FindNodeView(c);

                    Edge edge = aView.portGroup.output.ConnectTo(bView.portGroup.input);
                    AddElement(edge);
                });

            });
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if(graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    NodeView nodeView = elem as NodeView;
                    if(nodeView != null)
                    {
                        web.DeleteNode(nodeView.node);
                    }

                    Edge edge = elem as Edge;
                    if(edge != null)
                    {
                        NodeView aView = edge.output.node as NodeView;
                        NodeView bView = edge.input.node as NodeView;

                        web.RemoveConnection(aView.node, bView.node);
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView aView = edge.output.node as NodeView;
                    NodeView bView = edge.input.node as NodeView;
                    web.AddConnection(aView.node, bView.node);
                });
            }

            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //base.BuildContextualMenu(evt);
            {
                var types = TypeCache.GetTypesDerivedFrom<Node>();
                evt.menu.AppendAction($"[Node] node", (a) => CreateNode(typeof(Node)));
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }

            }
        }

        private void CreateNode(Type type)
        {
            Node node = web.CreateNode(type);
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
