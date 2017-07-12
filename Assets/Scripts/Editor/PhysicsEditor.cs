using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PhysicsEditor : EditorWindow {


    private static GameObject character1;
    private static GameObject character2;

    private static float distance;
    private static float angle = 45;
    private static float rad;

    private static PhysicsData data;


    [MenuItem("Tools/PhysicsEditor")]
    static void Init()
    {
        GetWindow(typeof(PhysicsEditor)).Show();

    }

    private void OnGUI()
    { 

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Character 1", GUILayout.Width(126));
        character1 = EditorGUILayout.ObjectField(character1, typeof(GameObject), true, GUILayout.Width(126)) as GameObject;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Character 2", GUILayout.Width(126));
        character2 = EditorGUILayout.ObjectField(character2, typeof(GameObject), true, GUILayout.Width(126)) as GameObject;
        GUILayout.EndHorizontal();

       
        if (character1 != null && character2 != null)
        {
            distance = character2.transform.position.x - character1.transform.position.x;

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Distance", GUILayout.Width(126));
            EditorGUILayout.FloatField(distance, GUILayout.Width(126));
            GUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Throw Angle", GUILayout.Width(126));
            angle = EditorGUILayout.Slider(angle, 0, 90, GUILayout.Width(126));

            
            rad = angle * Mathf.Deg2Rad;

            float v = Mathf.Sqrt(distance * GameManager.gravity / Mathf.Sin(2 * rad));
            float vX = (v * Mathf.Cos(rad));
            float vY = (v * Mathf.Sin(rad) - GameManager.gravity * 0.02f);

            Vector2 throwVelocity = new Vector2(vX, vY);
            if(data == null)
                data = new PhysicsData();

            data.throwVelocity = throwVelocity;

            EditorGUILayout.Space();

            EditorGUILayout.Vector2Field("Throw Velocity", throwVelocity, GUILayout.Width(126));

            if(GUILayout.Button("Save Data"))
            {
                SaveToJSON(data, GameManager.physDataPath);
            }


            
        }

        

    }

    private void SaveToJSON(object data, string dataPath)
    {
        string jsonData = EditorJsonUtility.ToJson(data);

        string filePath = Application.dataPath + dataPath;
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Data Saved");
    }


}
