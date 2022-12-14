using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : State
{
    string m_Animation;
    private ControlesPlayer m_PlayerControls;
    public IdleState(FSM fsm, string animation) : base(fsm)
    {
        m_Animation = animation;
    }

    public override void Init()
    {

        m_PlayerControls = new ControlesPlayer();
        m_PlayerControls.Player.Salto.started += YoDigoSaltaConmigo;
      //  m_PlayerControls.Player.Movimiento.started += Movimiento;
       // m_PlayerControls.Player.Movimiento.canceled += StopMovement;
        m_PlayerControls.Enable();
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
    }
    private void YoDigoSaltaConmigo(InputAction.CallbackContext obj)
    {
        m_FSM.ChangeState<Saltar>();
    }
    /* private void Movimiento(InputAction.CallbackContext obj)
     {
         m_FSM.ChangeState<CaminarLento>();
     }
     private void StopMovement(InputAction.CallbackContext obj)
     {
         m_FSM.ChangeState<IdleState>();
     }*/

    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        m_PlayerControls.Player.Salto.started -= YoDigoSaltaConmigo;
       // m_PlayerControls.Player.Movimiento.started -= Movimiento;
        //m_PlayerControls.Player.Movimiento.canceled -= StopMovement;
        m_PlayerControls.Player.Disable();
    }

}
