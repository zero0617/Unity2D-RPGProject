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

        //�������û�д���ǽ�ڣ��ӻ�ǽ״̬��������״̬
        if (player.wallDetected == false)
            StateMachine.ChangeState(player.fallState);

        //���ﻬǽ���ף�ת��������״̬
        if (player.groundDetected)
        {
            StateMachine.ChangeState(player.idleState);

            if(player.facingDir != player.moveInput.x)
                player.Fild();

        }

        //��ǽʱ����Ծ���룬���뻬ǽ��Ծ״̬
        if (input.Player.Jump.WasPressedThisFrame())
            StateMachine.ChangeState(player.wallJumpState);
    }


    //��ǽ�����ٶȼ���
    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.velocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.velocity.y * player.wallSlideSlowMultipLier);
    }
}
