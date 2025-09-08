using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GenerateAttackVelocity();


    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();


        if (triggerCalled)
            StateMachine.ChangeState(player.idleState);
    }


    //攻击时水平速度为0
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime; 
         
        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.velocity.y);
    }


    //攻击时小幅度移动
    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
    }
}
