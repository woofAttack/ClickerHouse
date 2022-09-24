#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityEditor;
using UnityEditor.SceneManagement;


[CustomEditor(typeof(Prefab_Part))]

public class EditorPart : Editor
{

    public Prefab_Part t;
    SerializedObject serializedBase;

    private bool Foldout1, Foldout2, Foldout3, Foldout4, Foldout5, Foldout6, Foldout7;

    public void OnEnable()
    {
        t = (Prefab_Part)target;
        serializedBase = new SerializedObject(t);
    }

    private void OpenVertical()
    {
        GUILayout.BeginVertical(GUI.skin.button);
        EditorGUILayout.Space(2);
    }

    private void CloseVertical()
    {
        EditorGUILayout.Space(2);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space(2);
    }

    public override void OnInspectorGUI()
    {

        if (Foldout1 = EditorGUILayout.Foldout(Foldout1, "Наименование и описание"))
        {
            OpenVertical();

            GUILayout.BeginHorizontal();
            t.MainSprite = (Sprite)EditorGUILayout.ObjectField(t.MainSprite, typeof(Sprite), false, GUILayout.Height(80), GUILayout.Width(80));

            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("Наименование части:");
            t.NameOfPart = EditorGUILayout.TextField(t.NameOfPart);

            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("LVL:", GUILayout.Width(40));
            EditorGUILayout.LabelField("Тип:", GUILayout.Width(80));
            EditorGUILayout.LabelField("Редкость:");

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            t.LevelOfPart = EditorGUILayout.IntField(t.LevelOfPart, GUILayout.Width(40));
            t.TypeOfPart = (Prefab_Part.TypePart)EditorGUILayout.EnumPopup(t.TypeOfPart, GUILayout.Width(80));
            t.RarityOfPart = (Prefab_Part.Rarity)EditorGUILayout.EnumPopup(t.RarityOfPart);

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            CloseVertical();
        }

        if (Foldout2 = EditorGUILayout.Foldout(Foldout2, "Статы"))
        {
            int countMax = Prefab_Part.CountBonus;

            if (t.MainStats == null || t.MainStats.Count != countMax)
            {
                t.MainStats = new List<Stat>();

                for (int i = 0; i < countMax; i++)
                {
                    t.MainStats.Add(new Stat((Prefab_Part.Bonus)i));
                }

                EditorGUILayout.LabelField(string.Format("{0} <> {1}", t.MainStats.Count, countMax));
            }

            if (t.SubStats == null || t.SubStats.Count != countMax)
            {
                t.SubStats = new List<Stat>();

                for (int i = 0; i < countMax; i++)
                {
                    t.SubStats.Add(new Stat((Prefab_Part.Bonus)i));
                }

                EditorGUILayout.LabelField(string.Format("{0} <> {1}", t.SubStats.Count, countMax));
            }



            OpenVertical();

            EditorGUILayout.LabelField("Главные статы: ");

            if (t.MainStats.Count == countMax)
            {
                for (int i = 0; i < countMax; i += 2)
                {
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(t.MainStats[i].Bonus.ToString());
                    EditorGUILayout.LabelField(t.MainStats[i + 1].Bonus.ToString());

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                    t.MainStats[i].Value = EditorGUILayout.FloatField(t.MainStats[i].Value);
                    t.MainStats[i + 1].Value = EditorGUILayout.FloatField(t.MainStats[i + 1].Value);

                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField(string.Format("{0} != {1}", t.MainStats.Count, countMax));
            }

            CloseVertical();

            OpenVertical();

            EditorGUILayout.LabelField("Доп. статы: ");

            if (t.SubStats.Count == countMax)
            {
                for (int i = 0; i < countMax; i += 2)
                {
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(t.SubStats[i].Bonus.ToString() + " в %");
                    EditorGUILayout.LabelField(t.SubStats[i + 1].Bonus.ToString() + " в %");

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                    t.SubStats[i].Value = EditorGUILayout.FloatField(t.SubStats[i].Value);
                    t.SubStats[i + 1].Value = EditorGUILayout.FloatField(t.SubStats[i + 1].Value);

                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField(string.Format("{0} != {1}", t.SubStats.Count, countMax));
            }

            CloseVertical();
        }

        if (Foldout3 = EditorGUILayout.Foldout(Foldout3, "Градации"))
        {
            if (t.Parts == null)
            {
                t.Parts = new List<OnePart>();
            }

            if (t.Parts.Count > 0)
            {
                for (int i = 0; i < t.Parts.Count; i++)
                {
                    OpenVertical();

                    GUILayout.BeginVertical();

                    GUILayout.BeginHorizontal();
                    t.Parts[i].Sprite = (Sprite)EditorGUILayout.ObjectField(t.Parts[i].Sprite, typeof(Sprite), false, GUILayout.Height(80), GUILayout.Width(80));

                    GUILayout.BeginVertical();

                    EditorGUILayout.LabelField("Стоимость: ");
                    t.Parts[i].CountToBuild = EditorGUILayout.FloatField(t.Parts[i].CountToBuild);

                    EditorGUILayout.LabelField("Мод в %: ");
                    t.Parts[i].Diff = EditorGUILayout.FloatField(t.Parts[i].Diff);

                    GUILayout.EndVertical();

                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Убрать"))
                    {
                        t.Parts.RemoveAt(i);
                        continue;
                    }

                    GUILayout.EndVertical();

                    CloseVertical();
                }
            }

            if (GUILayout.Button("Добавить"))
            {
                t.Parts.Add(new OnePart());
            }
        }

        if (GUI.changed)
        {
            Debug.Log("Swaping");
            Undo.RecordObject(t, "Nani");
            EditorUtility.SetDirty(t);
        }

    }


}

#endif
