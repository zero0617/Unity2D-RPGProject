using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

        //如果人物在地面，由下落状态进入空闲状态
        if (player.groundDetected)
            StateMachine.ChangeState(player.idleState);

        //如果人物碰到墙壁，由下落状态进入滑墙状态
        if (player.wallDetected)
            StateMachine.ChangeState(player.wallSlideState);
    }
}
