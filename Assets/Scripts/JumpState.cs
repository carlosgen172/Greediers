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
    }

    public override void Update()
    {
        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando && fsm.jugadorActual.inputPlayer.estaQuieto && !fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.IdleState);
        }

        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando && !fsm.jugadorActual.inputPlayer.estaQuieto && !fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.RunState);
        }

        if (fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.MomiaState);
        }
    }

    public override void Exit()
    {

    }
}
