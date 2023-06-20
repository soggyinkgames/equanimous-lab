using UnityEngine;
using UnityEditor;

// An example window you can customise and open in the unity editor by clicking on
// the dropdown Window/Example
public class ExampleWindow : EditorWindow
{
    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    }
    void OnGUI()
    {
        // window code

    }
}