using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using Misc;
using Decorations;

[CustomEditor(typeof(Decoration))]
public class DecorationEditor : Editor
{
    Decoration Decoration { get; set; }

    private void OnEnable()
    {
        this.Decoration = this.target as Decoration;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

        GUI.enabled = !(this.Decoration?.Data == null ||
            this.Decoration.HitBox == null);

        if (GUILayout.Button("Load Data Stage"))
        {
            this.Decoration.UpdateStage();
            EditorUtility.SetDirty(this.Decoration);
        }

        GUI.enabled = true;
    }
}
