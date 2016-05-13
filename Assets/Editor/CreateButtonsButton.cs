using UnityEngine;
using UnityEditor;
using Assets.Scripts.Builder;

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