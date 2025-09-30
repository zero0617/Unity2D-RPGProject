using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;  //攻击计时器
    private float lastTimeAttacked; //最后攻击时间

    private bool comboAttackQueued; //用于控制延迟进入攻击状态的协程
    private int comboIndex = 1; //基础攻击索引
    private int comboLimit = 3; //基础攻击招式总数
    private int attackDir;  //攻击方向
    private const int FirstComboIndex = 1;  //首次攻击索引

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
        // 确保连招次数限制与玩家攻击招式数组长度一致
        if (comboLimit != player.attackVelocity.Length)
        {
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);

        //攻击方向
        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

        ApplyAttackVelocity();


    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        //玩家输入攻击指令
        if (input.Player.BasicAttack.WasReleasedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;   //连招计数++
        lastTimeAttacked = Time.time;   //记录本次最后攻击时间
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)  // 如果有排队的连招
        {
            anim.SetBool(animaBoolName, false); // 结束当前攻击动画
            player.EnterAttackStateWithDelay(); // 延迟进入下一次攻击
        }
        else
            StateMachine.ChangeState(player.idleState);
    }

    //排队下一次攻击
    private void QueueNextAttack()
    {
        if(comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
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
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
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
