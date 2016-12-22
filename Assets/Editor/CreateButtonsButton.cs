using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateButtons))]
public class CreateButtonsButton : Editor
{
    public override void OnInspectorGUI()
    {
        CreateButtons myScript = (CreateButtons)target;

        if (GUILayout.Button("Build Buttons"))
        {
            myScript.BuildObject();
        }

        DrawDefaultInspector();
    }
}