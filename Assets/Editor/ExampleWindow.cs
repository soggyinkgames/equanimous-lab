using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using System.Runtime.Serialization;

// An example window you can customise and open in the unity editor by clicking on
// the dropdown Window/Example
// todo: move to editor folder to exclude from build files, research this info
public class ExampleWindow : EditorWindow
{
    // TEXT FIELD
    string myString = "HelloWorld";

    // COLOR PICKER
    Color color;

    // SCROLLVIEW
    Vector2 scrollPosition;
    string longString = "This is a long-ish string";

    public Texture icon;

    // BOUNDS FIELD
    float radius = 0;
    Bounds bounds;

    // OBJECT SPAWNER
    string spawnedObjectName;
    int objectID;
    float objectScale;
    float spawnRadius = 5f;
    GameObject objectToSpawn;



    [MenuItem("MyTools/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    }
    void OnGUI()
    {
        // LABEL
        GUILayout.Label("This is a label", EditorStyles.boldLabel);


        // TEXT FIELD
        myString = EditorGUILayout.TextField("Name", myString);


        // COLOR PICKER TO COLORIZE SCENE OBJECTS
        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("COLORIZE!"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Debug.Log($"selection gameobjects {obj}");
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.sharedMaterial.color = color;
                }
            }
        }


        // SCROLL SECTION WITH A CLEAR ALL BUTTON AND A HARD CODED ADD MORE TEXT BUTTON
        scrollPosition = GUILayout.BeginScrollView(
            scrollPosition, GUILayout.Width(200), GUILayout.Height(150));

        GUILayout.Label(longString);

        if (GUILayout.Button("Clear"))
            longString = "";

        GUILayout.EndScrollView();

        if (GUILayout.Button("Add More Text"))
            longString += "\nHere is another line";


        // BOUNDS FIELD EXAMPLE LETS YOU GET THE RADIUS OF A SELECTED OBJECT IN SCENE
        GUILayout.Label("Select a mesh in the Hierarchy view and click 'Capture Bounds'");
        EditorGUILayout.BeginHorizontal();
        bounds = EditorGUILayout.BoundsField("Mesh bounds:", bounds);

        if (GUILayout.Button("Capture Bounds") && Selection.activeTransform)
        {
            MeshFilter meshFilter = Selection.activeTransform.GetComponentInChildren<MeshFilter>();
            if (meshFilter)
            {
                bounds = meshFilter.sharedMesh.bounds;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Radius:", bounds.size.magnitude.ToString());
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }


        // OBJECT SPAWNER TAKES PREFABS LETS YOU NAME THEM AND ADD ID, SCALE, NAME
        GUILayout.Label("Spawn new object", EditorStyles.boldLabel);

        spawnedObjectName = EditorGUILayout.TextField("Spawned Object Name", spawnedObjectName);
        // string newBackstageItemID = System.Guid.NewGuid().ToString(); //OR
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        objectScale = EditorGUILayout.Slider("Object Scale", objectScale, 0.5f, 3f);
        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
        objectToSpawn = EditorGUILayout.ObjectField("Prefab to spawn", objectToSpawn, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObject();
        }

    }

    private void SpawnObject()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("Error: Please assign an object to be spawned.");
            return;
        }
        if (spawnedObjectName == string.Empty)
        {
            Debug.LogError("Error: Please enter a name for the object.");
            return;
        }

        Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(spawnCircle.x, 0f, spawnCircle.y);

        GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        newObject.name = spawnedObjectName + objectID;
        newObject.transform.localScale = Vector3.one * objectScale;

        objectID++;
    }


}