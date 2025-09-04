using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundeState : EntityState
{
    public Player_GroundeState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Update()
    {
        base.Update();


        //人物y轴速度小于0，进入下落状态
        if (rb.velocity.y < 0 && player.groundDetected == false)
            StateMachine.ChangeState(player.fallState);

        //有跳跃输入，进入跳跃状态
        if (input.Player.Jump.WasPressedThisFrame())
            StateMachine.ChangeState(player.jumpState);
    }
}
