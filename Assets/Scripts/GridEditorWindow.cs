using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridEditorWindow : EditorWindow
{
    private ObstacleData obstacleData;
    private bool[] gridState = new bool[100]; 

    [MenuItem("Tools/Grid Editor")]
    public static void ShowWindow()
    {
        GetWindow<GridEditorWindow>("Grid Editor");
    }

    void OnEnable()
    {
        if (obstacleData != null)
        {
            gridState = obstacleData.obstacleGrid;
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Toggle tiles to place/remove obstacles", EditorStyles.boldLabel);

        if (GUILayout.Button("Load Obstacle Data"))
        {
            string path = EditorUtility.OpenFilePanel("Select Obstacle Data", "Assets", "asset");
            if (!string.IsNullOrEmpty(path))
            {
                path = "Assets" + path.Substring(Application.dataPath.Length);
                obstacleData = AssetDatabase.LoadAssetAtPath<ObstacleData>(path);
                if (obstacleData != null)
                {
                    gridState = obstacleData.obstacleGrid;
                }
            }
        }

        if (obstacleData == null)
        {
            EditorGUILayout.HelpBox("No ObstacleData loaded.", MessageType.Warning);
            return;
        }

        for (int y = 0; y < 10; y++)
        {
            // making toggles for grid chunks for 10x10 grid
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;
                bool currentState = gridState[index];
                bool newState = GUILayout.Toggle(currentState, "");

                if (newState != currentState)
                {
                    gridState[index] = newState;//updating toggles
                    obstacleData.obstacleGrid = gridState;
                    EditorUtility.SetDirty(obstacleData);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save Obstacle Data"))
        {
            EditorUtility.SetDirty(obstacleData);//saving obstacle data object
            AssetDatabase.SaveAssets();
        }
    }
}
