using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

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
            m_UXML.CloneTree(root); // instanciates visual elements

            var choices = new List<string> { "Textures (T_)","Materials (M_)","Signed Distance Field (SDF_)","Visual Effects (VFX_)","Shader (SH_)","Shader Graph (SHG_)","Particle System (PS_)" };
            var prefixField = root.Q<DropdownField>("prefix");
            prefixField.choices = choices;

            var suffixChoices = new List<string> { "_E", "_I", "_A", "_F" };
            var suffixField = root.Q<DropdownField>("suffix");
            suffixField.choices = suffixChoices;


            prefixField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                if(evt.newValue == choices[0] && prefixField != null){
                    suffixField.choices = new List<string> { "_D", "_Normal", "_Roughness", "_AlphaOpacity", "_AmbientOcclusion", "_Bump", "_Emissive", "_Mask", "_Specular", "_Particle"};
                }
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
