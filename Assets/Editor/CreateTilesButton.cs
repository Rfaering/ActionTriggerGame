using UnityEngine;
using UnityEditor;
using Assets.Scripts.World;

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

        DrawDefaultInspector();
    }
}