using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correr : State
{
    string m_Animation;
    public Correr(FSM fsm, string animation)
     : base(fsm)
    {
        m_Animation = animation;
    }

    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_FSM.ChangeState<Saltar>();
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //   m_FSM.ChangeState<CoroutineExampleState>();
            return;
        }
    }
}
