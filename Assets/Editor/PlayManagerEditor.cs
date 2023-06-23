using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace SoggyInkGames.Equanimous.Lab.Managers
{
// The #if true directive is a special case of the #if directive. It always evaluates to true, so the code inside the #if true block will always be compiled.
#if true

    [CustomEditor(typeof(PlayManager))]
    public class PlayManagerEditor : Editor
    {
        // public VisualTreeAsset m_UXML;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            // Draw the default inspectror

            var foldout = new Foldout() { viewDataKey = "TestingCustomInspectorFoldout", text = "Full Inspector"};
            InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
            root.Add(foldout);
            return root;
        }
    }

#endif
}
