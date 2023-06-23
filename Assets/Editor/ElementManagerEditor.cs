using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace SoggyInkGames.Equanimous.Lab.Managers
{
    // The #if true directive is a special case of the #if directive. It always evaluates to true, so the code inside the #if true block will always be compiled.
    #if true

    [CustomPropertyDrawer(typeof(ElementManager))]
    public class ElementManagerEditor : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            root.Add(new PropertyField(property.FindPropertyRelative("m_ElementColor")));
            var spawnPoint = new PropertyField(property.FindPropertyRelative("m_SpawnPoint"));
            root.Add(spawnPoint);

            var spawnInspector = new Box();
            root.Add(spawnInspector);

            spawnPoint.RegisterCallback<ChangeEvent<Object>, VisualElement>(
                SpawnChanged, spawnInspector);

            return root;
        }

        void SpawnChanged(ChangeEvent<Object> evt, VisualElement spawnInspector)
        {
            spawnInspector.Clear();

            var t = evt.newValue;
            if (t == null) return;

            spawnInspector.Add(new InspectorElement(t));
        }
    }

#endif
}