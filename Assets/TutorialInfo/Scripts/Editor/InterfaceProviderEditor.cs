using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using Misc;

//[CustomEditor(typeof(InterfaceProvider))]
//public class ItemEditor : Editor
//{
//    InterfaceProvider InterfaceProvider { get; set; }

//    private void OnEnable()
//    {
//        this.InterfaceProvider = this.target as InterfaceProvider;
//    }

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();


//        this.ItemData = (ItemData)EditorGUILayout.ObjectField("Item", this.ItemData, typeof(ItemData), true);

//        EditorGUILayout.LabelField("");
//        EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

//        GUI.enabled = this.Item?.Settings != null;

//        if (GUILayout.Button("Apply Settings"))
//        {
//            this.Item.ApplySettings();
//            EditorUtility.SetDirty(this.Item);
//        }

//        GUI.enabled = true;
//    }
//}
