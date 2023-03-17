using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


namespace Scenic
{

    public class ScenicEditor : EditorWindow
    {
        ScenicEditorView webView;
        InspectorView inspectorView;

        [MenuItem("Window/ScenicEditor")]
        public static void OpenWindow()
        {
            ScenicEditor wnd = GetWindow<ScenicEditor>();
            wnd.titleContent = new GUIContent("ScenicEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scenic/ScenicEditor.uxml");
            VisualElement labelFromUXML = visualTree.CloneTree();
            root.Add(labelFromUXML);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scenic/ScenicEditor.uss");
            root.styleSheets.Add(styleSheet);

            webView = root.Q<ScenicEditorView>();
            inspectorView = root.Q<InspectorView>();
        }

        private void OnSelectionChange()
        {
            SceneWeb web = Selection.activeObject as SceneWeb;

            if(web)
            {
                webView.PopulateView(web);
            }
        }

    }
}