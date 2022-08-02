using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GzState : FSMState
{
    public 
    Transform player;
    public GzState(FSMSystem fsm):base(fsm)
    {
        id = StateID.gz;
        player = GameObject.Find("Teddy").transform;
    }
    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(player);
        npc.transform.Translate(Vector3.forward *Time.deltaTime*2);
    }

    public override void Reson(GameObject npc)
    {
        if (Vector3.Distance(npc.transform.position,player.position)>5)
        {
            fsm.ZhTransition(Transition.LostPlayer);
        }
    }
}
