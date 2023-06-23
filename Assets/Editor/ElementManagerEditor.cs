using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace SoggyInkGames.Equanimous.Lab.Manager
{
    // The #if true directive is a special case of the #if directive. It always evaluates to true, so the code inside the #if true block will always be compiled.
    #if true

    [CustomPropertyDrawer(typeof(ElementManager))]
    public class ElementManagerEditor : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property);
        }
    }

#endif
}