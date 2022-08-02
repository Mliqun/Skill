using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerObj : ObjBase
{
    public Player player;
    player_Info _Info;
    public PlayerObj(player_Info Info)
    {
        _Info = Info;
        modelPath = Info.modelPath;
        name = Info.name;
        localPos = Info.pos;
        localRos = Info.ros;
        type = Info.type;
        base.Created();
    }
    public override void OnCreater()
    {
        if (obj!=null)
        {
            obj.AddComponent<Xt>();
            obj.GetComponent<Xt>().Init(_Info.hp, _Info.name);
            player=obj.AddComponent<Player>();
            player.Init2();
            Debug.Log(obj.GetComponent<Player>().skillslist.Count + "***1");
        }
    }
    public void SetMdd(Vector3 v3)
    {
        if (!World.Ins.isYg)
        {
            if (obj.gameObject.GetComponent<Animator>())
            {
                obj.gameObject.GetComponent<Animator>().SetBool("run1",true);
                obj.GetComponent<NavMeshAgent>().SetDestination(v3);

                obj.GetComponent<Player>().isMove = true;
            }
        }
    }
    
}
