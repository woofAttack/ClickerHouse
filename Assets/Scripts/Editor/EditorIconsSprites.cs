using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(IconsStat))]
public class EditorIconsSprites : Editor
{
    public IconsStat t;
    SerializedObject serializedBase;


    public void OnEnable()
    {
        t = (IconsStat)target;
        serializedBase = new SerializedObject(t);
    }

    public override void OnInspectorGUI()
    {
        int imax = Prefab_Part.CountBonus;

        if (t.SpritesOfIcon == null)
        {
            for (int i = 0; i < imax; i++)
            {
                t.SpritesOfIcon = new List<Sprite>(imax);
            }
        }

        if (t.SpritesOfIcon.Count == Prefab_Part.CountBonus)
        {
            for (int i = 0; i < imax; i += 2)
            {
                GUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(typeof(Prefab_Part.Bonus).GetEnumNames()[i]);
                EditorGUILayout.LabelField(typeof(Prefab_Part.Bonus).GetEnumNames()[i + 1]);

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                t.SpritesOfIcon[i] = (Sprite)EditorGUILayout.ObjectField(t.SpritesOfIcon[i], typeof(Sprite), false);
                t.SpritesOfIcon[i + 1] = (Sprite)EditorGUILayout.ObjectField(t.SpritesOfIcon[i + 1], typeof(Sprite), false);

                GUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField(string.Format("{0} != {1}", t.SpritesOfIcon.Count, Prefab_Part.CountBonus));
        }





            if (GUI.changed)
        {
            Debug.Log("Swaping");
            Undo.RecordObject(t, "Nani");
            EditorUtility.SetDirty(t);
        }
    }



}
