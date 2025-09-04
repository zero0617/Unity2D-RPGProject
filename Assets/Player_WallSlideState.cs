using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (player.wallDetected == false)
            StateMachine.ChangeState(player.fallState);

        if (player.groundDetected)
        {
            player.Fild();

            StateMachine.ChangeState(player.idleState);
        }

        if (input.Player.Jump.WasPressedThisFrame())
            StateMachine.ChangeState(player.jumpState);
    }


    //ª¨«Ωœ¬¬‰ÀŸ∂»ºıª∫
    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.velocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.velocity.y * player.wallSlideSlowMultipLier);
    }
}
