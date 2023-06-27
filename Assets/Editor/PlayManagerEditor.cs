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
        public VisualTreeAsset m_UXML;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            m_UXML.CloneTree(root);

            // Draw the default inspectror
            var foldout = new Foldout() 
            {
                viewDataKey = "TestingCustomInspectorFoldout", //giving it a viewdatakey will let the foldout retain state between domain reloads, closing/opening windows, otherise it will be recreated with the default state expanded.
                text = "Full Inspector"
            };
            InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
            root.Add(foldout);
            return root;
        }
    }

#endif
}
