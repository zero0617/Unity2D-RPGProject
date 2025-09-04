using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : Player_GroundeState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        //如果有移动输入，进入移动状态
        if (player.moveInput.x != 0)
            StateMachine.ChangeState(player.moveState);
    }
}
