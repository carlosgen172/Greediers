using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FSM_Base
{

    private FSM_Jugador fsm;

    public JumpState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }
    public override void Enter()
    {
        if (fsm.animator == null) return;
        AnimationManager.Instance.ChangeAnimation("Jump", fsm.animator);
        Debug.Log("jugador saltando");
    }

    public override void Update()
    {
        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo)
        {
            fsm.ChangeState(fsm.IdleState);
        }
    }

    public override void Exit()
    {
        if (fsm.animator != null)
            fsm.animator.StopPlayback();
    }
}
