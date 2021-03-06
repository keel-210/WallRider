﻿using UnityEngine;
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
#endif
class TagAttribute : PropertyAttribute{}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagEditor : PropertyDrawer
{
    //tagのList
    List<string> AllTags
    {
        get
        {
            return InternalEditorUtility.tags.ToList();
        }
    }
    //ドロップダウンメニューの作成
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var list = AllTags;
        var selectedIndex = list.FindIndex(item => item.Equals(property.stringValue));
        if (selectedIndex == -1)
        {
            selectedIndex = list.FindIndex(item => item.Equals(list[0]));
        }

        selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, list.ToArray());

        property.stringValue = list[selectedIndex];
    }
}
#endif
