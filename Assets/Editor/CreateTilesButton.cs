using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateTiles))]
public class CreateTilesButton : Editor
{
    public string Name;

    public override void OnInspectorGUI()
    {
        CreateTiles myScript = (CreateTiles)target;

        if (GUILayout.Button("Rebuild World"))
        {
            myScript.RebuildWorld();
        }

        if (GUILayout.Button("Save Game"))
        {
            myScript.SaveWorld();
        }
        
        DrawDefaultInspector();
    }
}