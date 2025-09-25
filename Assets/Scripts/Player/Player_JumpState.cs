using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //使人物可跳跃
        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        //如果人物y轴速度小于0，则进入下落状态
        if (rb.velocity.y < 0 && StateMachine.currentState != player.jumpAttackState)
            StateMachine.ChangeState(player.fallState);
    }
}
