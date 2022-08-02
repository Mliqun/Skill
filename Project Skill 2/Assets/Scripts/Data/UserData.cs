using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ModelCountorl
{
    public string name;
    public string pathName;
    public bool isFlag;
    public float px;
    public float py;
    public float pz;
    public float rx;
    public float ry;
    public float rz;
    public RoleType type;
}
public enum TaskType
{
    nullType=0,
    Dg,
    Cj
}
public class Taskk
{
    public string taskName;
    public string npcName;
    public TaskType type;
    public string dialog;
    public bool isFinish;
    public float x;
    public float y;
    public float z;
    public int num;
}
public class UserData : SingleTon<UserData>
{
    public void GetSkill()
    {
        Dictionary<string, List<SkillBase>> dic = new Dictionary<string, List<SkillBase>>();
        string json = File.ReadAllText("Assets/Teddy.txt");
        List<SkillXml> skillXmls = new List<SkillXml>();
        skillXmls = JsonConvert.DeserializeObject<List<SkillXml>>(json);
        foreach (var item in skillXmls)
        {
            dic.Add(item.name, new List<SkillBase>());
            foreach (var ite in item.Skilldic)
            {
                foreach (var it in ite.Value)
                {
                    if (ite.Key.Equals("¶¯»­"))
                    {
                        
                    }
                }
            }
        }
    }
    public List<ModelCountorl> GetMonsterDatas(string mapName)
    {
       string txt=File.ReadAllText("Assets/"+mapName+".json");
        List<ModelCountorl> models = JsonConvert.DeserializeObject<List<ModelCountorl>>(txt);
        return models;
    }
    public List<Taskk> GetTask()
    {
        string txt = File.ReadAllText("Assets/" + "Task" + ".json");
        List<Taskk> taskks = JsonConvert.DeserializeObject<List<Taskk>>(txt);
        return taskks;
    }
}
