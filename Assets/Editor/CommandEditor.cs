using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CommandEditor : EditorWindow
{
    static List<string> stringList;

    [MenuItem("Editor/CommandEditor")]
    static void Init()
    {
        CommandEditor window = (CommandEditor)EditorWindow.GetWindow(typeof(CommandEditor));
        stringList = new List<string>();
        for(int i=0;i<20;i++)
        {
            stringList.Add("");
        }
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("CommandEditor", EditorStyles.boldLabel);

        for (int i = 0; i < 20; i++)
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
}