using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSM_Base
{
    private FSM_Jugador fsm;

    public IdleState(FSM_Jugador fsm)
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
        if (fsm.jugadorActual.inputPlayer.seEstaMoviendo)
        {
            fsm.ChangeState(fsm.RunState);
        }

        if (fsm.jugadorActual.inputPlayer.estaMinando)
        {
            fsm.ChangeState(fsm.MineState);
            Debug.Log("jugador minando");
        }

        if (fsm.jugadorActual.inputPlayer.estaSaltando)
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
