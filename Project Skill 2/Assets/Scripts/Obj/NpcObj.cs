using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcObj : ObjBase
{
    Monster_Info _Info;
    public NpcObj(Obj_Info info)
    {
        _Info = new Monster_Info();
        name = info.name;
        modelPath = info.name;
        localPos = info.pos;
        localRos = info.ros;
        type = info.type;
        base.Created();
    }
}
