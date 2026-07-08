using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MomiaState : FSM_Base
{
    private FSM_Jugador fsm;

    public MomiaState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        if (fsm.animator == null) return;
        AnimationManager.Instance.ChangeAnimation("Momia", fsm.animator);
    }

    public override void Update()
    {
        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando && fsm.jugadorActual.inputPlayer.estaQuieto && !fsm.jugadorActual.esMomia)
        {
            fsm.ChangeState(fsm.IdleState);
        }

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
    }

    public override void Exit()
    {

    }


}
