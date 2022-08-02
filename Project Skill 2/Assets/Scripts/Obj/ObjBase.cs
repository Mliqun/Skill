using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBase 
{
    public string name;
    public GameObject obj;
    public string modelPath;
    public Vector3 localPos;
    public Vector3 localRos;
    public RoleType type;
    public virtual void Created()
    {
        obj = GameObject.Instantiate(Resources.Load<GameObject>(modelPath));
        obj.transform.position = localPos;
        obj.transform.eulerAngles = localRos;
        obj.name = modelPath;
        if (type==RoleType.Player||type==RoleType.Monster)
        {
            World.Ins.MyMap.GetComponent<WorldMap>().GetPlayer(obj);
            OnCreater();
        }
        
    }
    public virtual void OnCreater()
    {

    }
    public virtual void SetDh(string name)
    {
        obj.GetComponent<Player>().DoPlay(name);
    }
}
