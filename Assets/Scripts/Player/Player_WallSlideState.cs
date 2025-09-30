using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        //如果人物没有触碰墙壁，从滑墙状态进入下落状态
        if (player.wallDetected == false)
            StateMachine.ChangeState(player.fallState);

        //人物滑墙触底，转向进入空闲状态
        if (player.groundDetected)
        {
            StateMachine.ChangeState(player.idleState);

            if(player.facingDir != player.moveInput.x)
                player.Fild();

        }

        //滑墙时有跳跃输入，进入滑墙跳跃状态
        if (input.Player.Jump.WasPressedThisFrame())
            StateMachine.ChangeState(player.wallJumpState);
    }


    //滑墙下落速度减缓
    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.velocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.velocity.y * player.wallSlideSlowMultipLier);
    }
}
