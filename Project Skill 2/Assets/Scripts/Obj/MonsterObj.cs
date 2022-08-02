using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObj : ObjBase
{
    Monster_Info info;
    public MonsterObj(Obj_Info _Info)
    {
        info = new Monster_Info();
        info.hp = 100;
        info.modelPath = _Info.modelPath;
        info.name = _Info.name;
        info.pos = _Info.pos;
        info.ros = _Info.ros;
        info.type = _Info.type;
        
        modelPath = _Info.modelPath;
        name = _Info.name;
        localPos = _Info.pos;
        localRos = _Info.ros;
        type = _Info.type;
        base.Created();
    }
    public override void OnCreater()
    {
        if (obj!=null)
        {
            obj.AddComponent<Xt>();
            obj.GetComponent<Xt>().Init(info.hp, info.name);
        }
    }
}
