using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    FSMSystem fsm;
    // Start is called before the first frame update
    void Start()
    {
        fsm = new FSMSystem();

        FSMState xlState = new XlState(fsm);
        xlState.AddTransition(Transition.SeePlayer, StateID.gz);

        FSMState gzState = new GzState(fsm);
        gzState.AddTransition(Transition.LostPlayer,StateID.xl);

        fsm.AddState(xlState);
        fsm.AddState(gzState);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Update(gameObject);
    }
}
