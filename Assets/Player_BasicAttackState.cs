using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;  //攻击计时器
    private const int FirstComboIndex = 1;  //首次攻击索引
    private int comboIndex = 1; //基础攻击索引
    private int comboLimit = 3; //基础攻击招式总数
    private float lastTimeAttacked; //最后攻击时间

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetComboIndexNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();


    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (triggerCalled)
            StateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;   //连招计数++
        lastTimeAttacked = Time.time;   //记录本次最后攻击时间
    }


    //攻击时禁止移动
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime; 
         
        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.velocity.y);
    }


    //攻击前小幅度移动
    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * player.facingDir, attackVelocity.y);
    }

    //攻击连招切换
    private void ResetComboIndexNeeded()
    {
        //超过重置时间没有攻击，重置攻击连招
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        //连击数超过最大连击数，重置攻击连招
        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }
}
