using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
            StateMachine.ChangeState(player.moveState);
    }
}
