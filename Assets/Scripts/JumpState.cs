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
        Debug.Log("Estoy en Jump");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}
