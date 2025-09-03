using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpState : EntityState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0)
            StateMachine.ChangeState(player.fallState);
    }
}
