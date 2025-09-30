using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundeState : PlayerState
{
    public Player_GroundeState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Update()
    {
        base.Update();


        //����y���ٶ�С��0����������״̬
        if (rb.velocity.y < 0 && player.groundDetected == false)
            StateMachine.ChangeState(player.fallState);

        //��Ծ���룬������Ծ״̬
        if (input.Player.Jump.WasPressedThisFrame())
            StateMachine.ChangeState(player.jumpState);

        //�������룬���빥��״̬
        if (input.Player.BasicAttack.WasReleasedThisFrame())
            StateMachine.ChangeState(player.basicAttackState);
    }
}
