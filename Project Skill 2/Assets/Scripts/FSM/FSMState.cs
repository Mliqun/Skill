using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
    nullType=-1,
    SeePlayer,
    LostPlayer
}
public enum StateID
{
    NullState=-1,
    gz,
    xl
}
public abstract class FSMState 
{
    public StateID id;
    public Dictionary<Transition, StateID> dic = new Dictionary<Transition, StateID>();
    public FSMSystem fsm;
    public FSMState(FSMSystem _fsm)
    {
        fsm = _fsm;
    }
    public void AddTransition(Transition tran,StateID stateID)
    {
        if (tran==Transition.nullType)
        {
            return;
        }
        if (tran==Transition.nullType)
        {
            return;
        }
        if (dic.ContainsKey(tran))
        {
            return;
        }
        dic.Add(tran, stateID);
    }
    public void DeleteTransition(Transition tran)
    {
        if (dic.ContainsKey(tran)==false)
        {
            return;
        }
        dic.Remove(tran);
    }
    public StateID GetStateID(Transition tran)
    {
        if (dic.ContainsKey(tran))
        {
            return dic[tran];
        }
        return StateID.NullState;
    }
    public virtual void OnBegin() { }
    public virtual void OnEnd() { }
    public abstract void Act(GameObject npc);
    public abstract void Reson(GameObject npc);

}
