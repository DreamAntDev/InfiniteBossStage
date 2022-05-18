using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CommandEditor : EditorWindow
{
    static int ListCount = 20;
    static List<string> stringList;

    [MenuItem("Editor/CommandEditor")]

    static void Init()
    {
        CommandEditor window = (CommandEditor)EditorWindow.GetWindow(typeof(CommandEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("CommandEditor", EditorStyles.boldLabel);

        for (int i = 0; i < ListCount; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Command", GUILayout.Width(60));
            stringList[i] = GUILayout.TextField(stringList[i]);
            if(GUILayout.Button("Excute",GUILayout.Width(60)))
            {
                DebugCommand.CommandEditor.Excute(stringList[i]);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
    private void OnEnable()
    {
        stringList = new List<string>();
        stringList.Add("Character Damage 10");
        stringList.Add("BossKill");

        for (int i = stringList.Count-1; i < ListCount; i++)
        {
            stringList.Add("");
        }
    }

}