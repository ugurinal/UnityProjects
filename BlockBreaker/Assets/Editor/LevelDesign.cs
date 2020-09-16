using UnityEngine;
using UnityEditor;

public class LevelDesign : EditorWindow
{

    GameObject parentObj;
    float xPos = 2.0513f;
    float yPos = 0.495f;
    int size = 0;
    Object objField = null;

    [MenuItem("Window/LevelDesign")]

    public static void ShowWindow()
    {
        GetWindow<LevelDesign>("LevelDesigner");
    }

    private void OnGUI()
    {

        parentObj = GameObject.Find("Blocks");

        xPos = EditorGUILayout.FloatField("X pos", xPos);
        yPos = EditorGUILayout.FloatField("Y pos", yPos);
        size = EditorGUILayout.IntField("How many blocks: ", size);

        objField = EditorGUILayout.ObjectField(objField, typeof(GameObject), true);

        if (GUILayout.Button("Create Horizantal"))
        {
            CreateBlockHorizontal(size);
        }

        if (GUILayout.Button("Create Vertical"))
        {
            CreateBlockVertical(size);
        }
    }

    private void CreateBlockHorizontal(int size)
    {
        float screenSize = 21.325f;
        float startPos = ((screenSize - (2.0513f * size)) / 2.0f);


        for (float i = 0; i < size; i++)
        {
            GameObject newObj = PrefabUtility.InstantiatePrefab(objField, parentObj.transform) as GameObject;

            Vector2 pos = new Vector2(startPos, yPos);
            Vector2 addPos = new Vector2(xPos * i, 0);

            newObj.transform.position = pos + addPos;


        }
    }

    private void CreateBlockVertical(int size)
    {
        float screenSize = 21.325f;
        float startPos = ((screenSize - (2.0513f * size)) / 2.0f);


        for (float i = 0; i < size; i++)
        {
            GameObject newObj = PrefabUtility.InstantiatePrefab(objField, parentObj.transform) as GameObject;

            Vector2 pos = new Vector2(startPos, yPos);
            Vector2 addPos = new Vector2(0, yPos * i);

            newObj.transform.position = pos + addPos;


        }
    }


}
