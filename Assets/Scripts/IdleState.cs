using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if (fsm.jugadorActual.inputPlayer.seEstaMoviendo && !fsm.jugadorActual.estaMinando && fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.RunState);
        }

        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo && fsm.jugadorActual.estaMinando && fsm.jugadorActual.inputPlayer.estaQuieto && !fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.MineState);
        }

        if (!fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando && !fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.JumpState);
        }

        if(fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.MomiaState);
        }
    }

    public override void Exit()
    {
        
    }


}
