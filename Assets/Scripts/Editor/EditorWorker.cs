using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorWorker : EditorWindow
{
    public Transform TrfGroup;
    public Material MaterialRed;
    public Material MaterialGreen;
    public Material MaterialBlue;



    public GameObject TargetObj;
    public Transform RootParent;
    public Vector3 StartPoint;
    public int CreateCount;
    public int RowCount;
    public float RowInterval;

    [MenuItem("DCubeLab/ECS/EditorWindow")]
    public static void OpenExportWindow()
    {
        EditorWindow.GetWindow<EditorWorker>(false, "EditorWorker", true).Show();
    }

    private void OnGUI()
    {
        TrfGroup = EditorGUILayout.ObjectField("TrfGroup", TrfGroup, typeof(Transform), true) as Transform;
        MaterialRed = EditorGUILayout.ObjectField("MaterialRed", MaterialRed, typeof(Material), true) as Material;
        MaterialGreen = EditorGUILayout.ObjectField("MaterialGreen", MaterialGreen, typeof(Material), true) as Material;
        MaterialBlue = EditorGUILayout.ObjectField("MaterialBlue", MaterialBlue, typeof(Material), true) as Material;

        TargetObj = EditorGUILayout.ObjectField("TargetObj", TargetObj, typeof(GameObject), true) as GameObject;
        RootParent = EditorGUILayout.ObjectField("RootParent", RootParent, typeof(Transform), true) as Transform;
        StartPoint = EditorGUILayout.Vector3Field("StartPoint", StartPoint);
        CreateCount = EditorGUILayout.IntField("CreateCount", CreateCount);
        RowCount = EditorGUILayout.IntField("RowCount", RowCount);
        RowInterval = EditorGUILayout.FloatField("RowInterval", RowInterval);
        if (GUILayout.Button("작업 시작!"))
        {
            Debug.LogWarning("작업 시작!");
            OnDoWork(TrfGroup, MaterialRed, MaterialGreen, MaterialBlue);
            Debug.LogWarning("작업 종료!");
        }
    }

    void OnDoWork(Transform group, Material red, Material green, Material blue)
    {
        var renders = group.GetComponentsInChildren<MeshRenderer>(true);

        for (int i = 0; i < renders.Length; i++)
        {
            if (i % 3 == 1)
                renders[i].material = red;
            else if (i % 3 == 2)
                renders[i].material = green;
            else
                renders[i].material = blue;
        }
    }


    void OnDoWork(GameObject target, Transform root)
    {
        int index_X = 0;
        int index_Z = 0;
        GameObject obj;
        Vector3 point;
        for (int i = 0; i < CreateCount; i++)
        {
            obj = Instantiate(target, root);
            point = StartPoint;
            point.x += index_X * RowInterval;
            point.z += index_Z * RowInterval;

            index_X++;
            if (index_X >= RowCount)
            {
                index_X = 0;
                index_Z++;
            }
            obj.transform.position = point;
        }
    }
}
