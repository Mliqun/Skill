using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using System;

//public enum RoleType
//{
//    nullType=-1,
//    Player,
//    Monster,
//    NPC,
//    Collect,
//    BOSS
//}
//public class ModelCountorl
//{
//    public string name;
//    public string pathName;
//    public bool isFlag;
//    public float px;
//    public float py;
//    public float pz;
//    public float rx;
//    public float ry;
//    public float rz;
//    public RoleType type;
//}
public class MapEditor : EditorWindow
{
    GameObject fu;
    public List<ModelCountorl> modelList = new List<ModelCountorl>();
    string[] xx = new string[] { "Player", "Monster", "NPC", "Collect", "BOSS" };
    public Dictionary<string, ModelCountorl> roledic = new Dictionary<string, ModelCountorl>();
    [MenuItem("Tools/地图编辑器")]
    public static void Init()
    {
        MapEditor window = EditorWindow.GetWindow<MapEditor>();
        if (window!=null)
        {
            window.Show();
        }
    }
    private void OnEnable()
    {
        fu = GameObject.Find("MonsterRoot");
        if (File.Exists("map1.json"))
        {
            modelList=JsonConvert.DeserializeObject<List<ModelCountorl>>(File.ReadAllText("map.json"));
        }
        else
        {
            for (int i = 0; i < fu.transform.childCount; i++)
            {
                Add(i);
            }
        }
    }
    public void Add(int index)
    {
        ModelCountorl model = new ModelCountorl();
        Transform temp = fu.transform.GetChild(index);
        model.px = temp.position.x;
        model.py = temp.position.y;
        model.pz = temp.position.z;
        
        model.rx = temp.rotation.x;
        model.ry = temp.rotation.y;
        model.rz = temp.rotation.z;
        model.isFlag = temp.gameObject.activeSelf;
        model.name = temp.name;
        model.pathName = temp.name;

        model.type = (RoleType)(index % 5);

        if (!roledic.ContainsKey(model.name))
        {
            roledic.Add(model.name, model);
            modelList.Add(model);
        }
    }

    private void OnGUI()
    {
       
        if (fu.transform.childCount!=modelList.Count)
        {
            modelList.Clear();
            for (int i = 0; i < fu.transform.childCount; i++)
            {
                Add(i);
            }
        }
        foreach (var item in modelList)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.name);
            bool flag = EditorGUILayout.Toggle(item.isFlag);
            if (flag!=item.isFlag)
            {
                fu.transform.Find(item.name).gameObject.SetActive(item.isFlag);
            }
            int index = EditorGUILayout.Popup((int)item.type, xx);
            if (index!=(int)item.type)
            {
                item.type = (RoleType)index;
            }
            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("保存数据"))
        {
            File.WriteAllText("Assets/map1.json", JsonConvert.SerializeObject(modelList));
            AssetDatabase.Refresh();
        }
    }

}
