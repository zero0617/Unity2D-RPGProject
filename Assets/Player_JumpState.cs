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

        //ʹ�������Ծ
        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        //�������y���ٶ�С��0�����������״̬
        if (rb.velocity.y < 0 && StateMachine.currentState != player.jumpAttackState)
            StateMachine.ChangeState(player.fallState);
    }
}
