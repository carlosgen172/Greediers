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
        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando && fsm.jugadorActual.inputPlayer.estaQuieto)
        {
            fsm.ChangeState(fsm.IdleState);
        }

        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo && fsm.jugadorActual.estaMinando && fsm.jugadorActual.inputPlayer.estaQuieto)
        {
            fsm.ChangeState(fsm.MineState);
        }

        if (!fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando)
        {
            fsm.ChangeState(fsm.JumpState);
        }
    }

    public override void Exit()
    {
        
    }
}
