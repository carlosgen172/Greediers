using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : FSM_Base
{
    private FSM_Jugador fsm;

    public RunState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }
    public override void Enter()
    {
        if (fsm.animator == null) return;
        AnimationManager.Instance.ChangeAnimation("Run", fsm.animator);
    }

    public override void Update()
    {
        if (fsm.jugadorActual.inputPlayer.estaQuieto)
        {
            fsm.ChangeState(fsm.IdleState);
        }

        if (fsm.jugadorActual.inputPlayer.InteractPressed)
        {
            fsm.ChangeState(fsm.MineState);
            Debug.Log("jugador minando");
        }

        if (fsm.jugadorActual.inputPlayer.JumpPressed)
        {
            fsm.ChangeState(fsm.JumpState);
            Debug.Log("jugador saltando");
        }
    }

    public override void Exit()
    {
        if (fsm.animator != null)
            fsm.animator.StopPlayback();
    }
}
