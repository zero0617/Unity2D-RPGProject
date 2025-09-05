using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : EntityState
{
    public Player_DashState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    private float originalGravityScale;

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();
        player.SetVelocity(player.dashSpeed * player.facingDir, 0);

        if (stateTimer < 0)
        {
            if (player.groundDetected)
            {
                StateMachine.ChangeState(player.idleState);
            }
            else
            {
                StateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);

        rb.gravityScale = originalGravityScale;
    }

    private void CancelDashIfNeeded()
    {
        if (player.wallDetected)
        {
            if (player.groundDetected)
                StateMachine.ChangeState(player.idleState);
            else
                StateMachine.ChangeState(player.wallSlideState);
        }
    }
}
