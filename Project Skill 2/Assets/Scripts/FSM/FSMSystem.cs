using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem 
{
    public Dictionary<StateID, FSMState> states = new Dictionary<StateID, FSMState>();
    private StateID currStateID;
    private FSMState currState;

    public void Update(GameObject npc)
    {
        currState.Act(npc);
        currState.Reson(npc);
    }
    public void AddState(FSMState s)
    {
        if (currState==null)
        {
            currState = s;
            currStateID = s.id;
        }
        if (states.ContainsKey(s.id))
        {
            return;
        }
        states.Add(s.id, s);
    }
    public void DeleteState(StateID id)
    {
        if (states.ContainsKey(id)==false)
        {
            return;
        }
        states.Remove(id);
    }
    public void ZhTransition(Transition tran)
    {
        StateID id = currState.GetStateID(tran);
        if (states.ContainsKey(id)==false)
        {
            return;
        }
        FSMState state = states[id];
        currState.OnEnd();
        currState = state;
        currStateID = id;
        currState.OnBegin();
    }
}
