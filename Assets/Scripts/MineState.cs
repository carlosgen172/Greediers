using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineState : FSM_Base
{
    private FSM_Jugador fsm;

    public MineState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        if (fsm.animator == null) return;
        AnimationManager.Instance.ChangeAnimation("Idle", fsm.animator);
    }

    public override void Update()
    {
        if (fsm.jugadorActual.inputPlayer.estaQuieto)
        {
            fsm.ChangeState(fsm.IdleState);
        }

        if (fsm.jugadorActual.inputPlayer.seEstaMoviendo)
        {
            fsm.ChangeState(fsm.RunState);
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
