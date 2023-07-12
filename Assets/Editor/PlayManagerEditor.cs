using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using Button = UnityEngine.UIElements.Button;

namespace SoggyInkGames.Equanimous.Lab.Managers
{
    // The #if true directive is a special case of the #if directive. It always evaluates to true, so the code inside the #if true block will always be compiled.
#if true

    [CustomEditor(typeof(PlayManager))]
    public class PlayManagerEditor : Editor
    {
        public VisualTreeAsset m_UXML;
        public PlayManager m_PlayManager;
        public override VisualElement CreateInspectorGUI()
        {
            // Get a reference to the PlayManager object.
            m_PlayManager = target as PlayManager;

            var root = new VisualElement();

            m_UXML.CloneTree(root); // instanciates visual elements

            var choices = m_PlayManager.m_Prefix;
            // var choices = new List<string> { "T_", "M_", "SDF_", "VFX_", "SH_", "SHG_", "PS_", "TER_", "MESH_", "none" };

            var prefixField = root.Q<DropdownField>("prefix");
            prefixField.choices = choices;

            var suffixChoices = m_PlayManager.m_SuffixChoices;
            var suffixField = root.Q<DropdownField>("suffix");
            suffixField.choices = suffixChoices;

            var uxmlButton = root.Q<Button>("rename-asset");

            prefixField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                if (evt.newValue == choices[0] && prefixField != null)
                {
                    suffixField.choices = m_PlayManager.m_TextureSuffixChoices;
                }
                else if (evt.newValue != choices[0])
                {
                    suffixField.choices = suffixChoices;
                }
            });


            uxmlButton.RegisterCallback<MouseUpEvent>((evt) =>
            {
                string newSuffix = suffixField.value;
                string newPrefix = prefixField.value;
                m_PlayManager.RenameAsset(newSuffix, newPrefix);
            });


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
