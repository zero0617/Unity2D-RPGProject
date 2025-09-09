using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;  //������ʱ��
    private const int FirstComboIndex = 1;  //�״ι�������
    private int comboIndex = 1; //������������
    private int comboLimit = 3; //����������ʽ����
    private float lastTimeAttacked; //��󹥻�ʱ��

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
        comboIndex++;   //���м���++
        lastTimeAttacked = Time.time;   //��¼������󹥻�ʱ��
    }


    //����ʱ��ֹ�ƶ�
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime; 
         
        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.velocity.y);
    }


    //����ǰС�����ƶ�
    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * player.facingDir, attackVelocity.y);
    }

    //���������л�
    private void ResetComboIndexNeeded()
    {
        //��������ʱ��û�й��������ù�������
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        //������������������������ù�������
        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }
}
