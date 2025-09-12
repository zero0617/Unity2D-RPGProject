using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : EntityState
{

    private float originalGravityScale;
    private int dashDir;


    public Player_DashState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    
    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        dashDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();
        player.SetVelocity(player.dashSpeed * dashDir, 0);

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
