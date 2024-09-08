using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractForestGenerator), true)]
public class RandomForestGeneratorEditor : Editor
{
    AbstractForestGenerator generator;

    private void Awake()
    {
        generator = (AbstractForestGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Forest"))
        {
            generator.GenerateForest();
        }
    }
}
