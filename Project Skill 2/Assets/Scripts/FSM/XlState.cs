using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XlState : FSMState
{
    List<Transform> paths = new List<Transform>();
    Transform path;
    Transform player;
    int index = 0;
    public XlState(FSMSystem fsm):base(fsm)
    {
        id = StateID.xl;
        path = GameObject.Find("Path").transform;
        Transform[] ss= path.GetComponentsInChildren<Transform>();
        foreach (var item in ss)
        {
            if (item!=path)
            {
                paths.Add(item);
            }
        }
        player = GameObject.Find("Teddy").transform;

    }
    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(paths[index]);
        npc.transform.Translate(Vector3.forward * Time.deltaTime * 2);
        if (Vector3.Distance(npc.transform.position,paths[index].position)<1)
        {
            index++;
            index = index % paths.Count;
        }
    }

    public override void Reson(GameObject npc)
    {
        if (Vector3.Distance(player.position,npc.transform.position)<3)
        {
            fsm.ZhTransition(Transition.SeePlayer);
        }
    }
}
