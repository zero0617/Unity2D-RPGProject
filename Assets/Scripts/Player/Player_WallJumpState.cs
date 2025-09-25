using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallJumpState : EntityState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.wallJumpForce = new Vector2(6, 12);

        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0)
            StateMachine.ChangeState(player.fallState);

        if (player.groundDetected)
            StateMachine.ChangeState(player.wallSlideState);
    }
}
