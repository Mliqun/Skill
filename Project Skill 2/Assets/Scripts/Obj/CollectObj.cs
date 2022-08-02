using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObj : ObjBase
{
    Collect_Info _Info;
    public CollectObj(Obj_Info info)
    {
        _Info = new Collect_Info();
        name = info.name;
        modelPath = info.name;
        localPos = info.pos;
        localRos = info.ros;
        type = info.type;
        base.Created();
    }
}
