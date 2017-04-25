﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif
class PrefabListEmitter : MonoBehaviour
{
    public List<EmitPrefab> list = new List<EmitPrefab>();

    bool activate;
    float Timer;
    public void Set()
    {
        activate = true;
    }
    public void Emit()
    {
        if (activate)
        {
            Timer += Time.deltaTime;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Time > Timer)
                {
                    GameObject obj = (GameObject)Instantiate(list[i].Prefab, list[i].Pos, Quaternion.Euler(list[i].Rot));
                    obj.transform.parent = transform;
                    list.RemoveAt(i);
                }
            }
        }
    }
    [Serializable]
    public class EmitPrefab
    {
        public float Time;
        public UnityEngine.Object Prefab;
        public Vector3 Pos;
        public Vector3 Rot;
    }
}
public class EmitPrefabAttribute : PropertyAttribute { }
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PrefabListEmitter))]
public class TutorialMessagesDrawer : Editor
{
    private ReorderableList RL;
    private SerializedProperty ListProp;
    private void OnEnable()
    {
        ListProp = serializedObject.FindProperty("list");
        RL = new ReorderableList(serializedObject, ListProp);
        RL.elementHeight = 80;
        RL.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "Prefabs");
        };
        RL.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = ListProp.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element);
        };
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        RL.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
[CustomPropertyDrawer(typeof(PrefabListEmitter.EmitPrefab))]
public class TutorialMessageAttribute : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            position.height = EditorGUIUtility.singleLineHeight;
            var ele1 = property.FindPropertyRelative("Time");
            var ele2 = property.FindPropertyRelative("Prefab");
            var ele3 = property.FindPropertyRelative("Pos");
            var ele4 = property.FindPropertyRelative("Rot");
            var ele1Rect = new Rect(position)
            {
                height = position.height
            };
            var ele2Rect = new Rect(position)
            {
                height = position.height,
                y = ele1Rect.y + EditorGUIUtility.singleLineHeight + 2
            };
            var ele3Rect = new Rect(position)
            {
                height = position.height * 2,
                y = ele2Rect.y + EditorGUIUtility.singleLineHeight + 2
            };
            var ele4Rect = new Rect(position)
            {
                height = position.height * 2,
                y = ele3Rect.y + EditorGUIUtility.singleLineHeight + 2
            };
            EditorGUI.PropertyField(ele1Rect, ele1);
            EditorGUI.PropertyField(ele2Rect, ele2);
            EditorGUI.PropertyField(ele3Rect, ele3);
            EditorGUI.PropertyField(ele4Rect, ele4);
        }
    }
}
#endif