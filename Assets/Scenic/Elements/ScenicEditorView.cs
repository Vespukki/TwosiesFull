using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;

namespace Scenic
{
    public class ScenicEditorView : GraphView
    {
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

        internal void PopulateView(SceneWeb web)
        {
            this.web = web;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            web.nodes.ForEach(n => CreateNodeView(n));
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

        void CreateNodeView(Node node)
        {
            NodeView nodeView = new(node);
            AddElement(nodeView);
        }
    }
}
