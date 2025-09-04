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

        //��������ڵ��棬������״̬�������״̬
        if (player.groundDetected)
            StateMachine.ChangeState(player.idleState);

        //�����������ǽ�ڣ�������״̬���뻬ǽ״̬
        if (player.wallDetected)
            StateMachine.ChangeState(player.wallSlideState);
    }
}
