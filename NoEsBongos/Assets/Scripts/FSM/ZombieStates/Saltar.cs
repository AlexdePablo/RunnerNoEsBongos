using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saltar : State
{
    GameObject gameObject;
    private ControlesPlayer m_PlayerControls;
    private MovimientoZombie m_Zombie;
    string m_Animation;
    [SerializeField]
    private LayerMask layerMask;

    public Saltar(FSM fsm, string animation)
     : base(fsm)
    {
        m_Animation = animation;
    }

    public override void Init()
    {
        m_PlayerControls = new ControlesPlayer();
        m_PlayerControls.Enable();
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
    }
    public override void Update()
    {
        base.Update();
        if (CheckGround())
        {
            m_FSM.ChangeState<IdleState>();
        }

    }
    public bool CheckGround()
    {
        //transform.GetComponent<SphereCollider>().radius * 1.1f


        bool raycast = Physics.Raycast(m_FSM.Owner.transform.position, -m_FSM.Owner.transform.up, m_FSM.Owner.GetComponent<CapsuleCollider>().radius * 1.2f);
        //Debug.DrawRay(m_FSM.Owner.transform.position * 0.5f, -m_FSM.Owner.transform.up * 1.2f, Color.red, 3f);
        return raycast;
    }
    public override void Exit()
    {
        
        m_PlayerControls.Player.Disable();
    }

}
