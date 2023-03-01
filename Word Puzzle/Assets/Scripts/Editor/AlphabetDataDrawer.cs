using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
[CustomEditor(typeof(AlphabetData))]
[System.Serializable]

public class AlphabetDataDrawer : Editor
{
    private ReorderableList AlphabetPlainList;
    private ReorderableList AlphabetNormalList;
    private ReorderableList AlphabetHighlightedList;
    private ReorderableList AlphabetWrongList;

    private void OnEnable()
    {
        InitiateReoderableList(ref AlphabetPlainList, "AlphabetPlain", "Alphabet Plain");
        InitiateReoderableList(ref AlphabetNormalList, "AlphabetNormal", "Alphabet Normal");
        InitiateReoderableList(ref AlphabetHighlightedList, "AlphabetHighlighted", "Alphabet Highlighted");
        InitiateReoderableList(ref AlphabetWrongList, "AlphabetWrong", "Alphabet Wrong");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        AlphabetPlainList.DoLayoutList();
        AlphabetNormalList.DoLayoutList();
        AlphabetHighlightedList.DoLayoutList();
        AlphabetWrongList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

    }

    private void InitiateReoderableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };
        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("letter"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("image"), GUIContent.none);
        };
    }
}
