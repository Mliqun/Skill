using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

public class MapEditor : EditorWindow
{
    [MenuItem("Tools/MapEditor")]
    static void Map()
    {
        MapEditor window = (MapEditor)EditorWindow.GetWindow(typeof(MapEditor));
        if (window != null)
        {
            window.Show();
        }
    }
    public List<string> Type = new List<string>() { "Normal", "Gather", "Biaoche", "NPC"};//类型集合
    Dictionary<string, modelCountrol> prefabName = new Dictionary<string, modelCountrol>();//信息字典
    public string JsonName = "";
    public void Add(GameObject Root, int index)
    {
        modelCountrol mc = new modelCountrol();//创建一个模型信息
        Transform temp = Root.transform.GetChild(index);//根据下标获取到该物体
        mc.x = temp.position.x;//赋值
        mc.y = temp.position.y;
        mc.z = temp.position.z;
        mc.tag = temp.tag;
        mc.flag = temp.gameObject.activeSelf;
        for (int i = 0; i < Type.Count; i++)
        {
            if (Type[i] == mc.tag)
            {
                mc.index = i;
                mc.type = SetType(index);
                break;
            }
        }
        mc.name = temp.name;
        prefabName[mc.name] = mc;
    }
    
    public void OnEnable()
    {
        GameObject game1 = GameObject.Find("Game1");
        GameObject game2 = GameObject.Find("Game2");
        if (game1)
        {
            JsonName = "Assets/Game1.json";
        }
        else if(game2)
        {
            JsonName = "Assets/Game2.json";
        }
    }
    private void OnGUI()
    {
        GameObject Root = GameObject.Find("Root");
        if (Root.transform.childCount != prefabName.Count)
        {
            prefabName.Clear();
            for (int i = 0; i < Root.transform.childCount; i++)
            {
                Add(Root, i);
            }
        }
        foreach (var item in prefabName.Values)
        {
            GUILayout.BeginHorizontal("");
            GUILayout.Label(item.name);
            Vector3 oldpos = new Vector3(item.x, item.y, item.z);

            Vector3 newpos = EditorGUILayout.Vector3Field("pos", oldpos);
            if (oldpos != newpos)
            {
                item.x = newpos.x;
                item.y = newpos.y;
                item.z = newpos.z;
                Root.transform.Find(item.name).position = newpos;
            }
            bool flag = EditorGUILayout.Toggle(item.flag);
            if (flag != item.flag)
            {
                item.flag = flag;
                Root.transform.Find(item.name).gameObject.SetActive(item.flag);
            }
            int index = EditorGUILayout.Popup(item.index, Type.ToArray());
            if (index != item.index)
            {
                item.index = index;
                Root.transform.Find(item.name).tag = Type[index];
                item.type=SetType(index);
            }

            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("保存"))
        {
            File.WriteAllText(JsonName, JsonConvert.SerializeObject(prefabName));
        }
        if (GUILayout.Button("读取"))
        {
            if (File.ReadAllText(JsonName) != null)
            {
                string text = File.ReadAllText(JsonName);
                Dictionary<string, modelCountrol> readlist = JsonConvert.DeserializeObject<Dictionary<string, modelCountrol>>(text);
                foreach (var item in readlist.Values)
                {
                    if (!prefabName.ContainsKey(item.name))
                    {
                        GameObject prefab = Resources.Load<GameObject>("Role/" + item.name);
                        if (prefab)
                        {
                            GameObject clone = Instantiate(prefab, Root.transform, false);
                            clone.name = item.name;
                            clone.transform.position = new Vector3(item.x, item.y, item.z);
                            clone.tag = Type[item.index];
                            clone.SetActive(item.flag);
                        }
                    }
                    else
                    {
                        Root.transform.Find(item.name).name = item.name;
                        Root.transform.Find(item.name).transform.position = new Vector3(item.x, item.y, item.z);
                        Root.transform.Find(item.name).tag = Type[item.index];
                        Root.transform.Find(item.name).gameObject.SetActive(item.flag);
                    }
                }
                prefabName.Clear();
                foreach (var item in readlist.Values)
                {
                    prefabName.Add(item.name, item);
                }
            }
        }
    }
    public MonsterType SetType(int index)
    {
        switch (index)
        {
            case 0: return MonsterType.Normal;
            case 1: return MonsterType.Gather;
            case 2: return MonsterType.Biaoche;
            case 3: return MonsterType.NPC;
        }
        return MonsterType.Null;
    }
}


