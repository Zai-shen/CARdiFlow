using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ImageToggle))]
public class ImageToggleEditor : UnityEditor.UI.ToggleEditor
{
    private ImageToggle imageToggle;

    public override void OnInspectorGUI()
    {
        imageToggle = (ImageToggle)target;

        //Display custom ImageToggle values
        DisplayCustomValues();

        //Fixes custom values not being set during play mode
        EditorUtility.SetDirty(target);

        //Display standard Toggle values
        base.OnInspectorGUI();
    }

    private void DisplayCustomValues()
    {
        EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Activated sprite");
                imageToggle.activatedSprite = (Sprite)EditorGUILayout.ObjectField(imageToggle.activatedSprite, typeof(Sprite), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Background Color");
            imageToggle.backgroundOff = EditorGUILayout.ColorField("Off", imageToggle.backgroundOff);
            imageToggle.backgroundActivated = EditorGUILayout.ColorField("Activated", imageToggle.backgroundActivated);

        EditorGUILayout.EndVertical();
    }

}
