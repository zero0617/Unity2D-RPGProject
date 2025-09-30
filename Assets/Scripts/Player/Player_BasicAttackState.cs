using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;  //������ʱ��
    private float lastTimeAttacked; //��󹥻�ʱ��

    private bool comboAttackQueued; //���ڿ����ӳٽ��빥��״̬��Э��
    private int comboIndex = 1; //������������
    private int comboLimit = 3; //����������ʽ����
    private int attackDir;  //��������
    private const int FirstComboIndex = 1;  //�״ι�������

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
        // ȷ�����д�����������ҹ�����ʽ���鳤��һ��
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

        //��������
        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

        ApplyAttackVelocity();


    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        //������빥��ָ��
        if (input.Player.BasicAttack.WasReleasedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;   //���м���++
        lastTimeAttacked = Time.time;   //��¼������󹥻�ʱ��
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)  // ������Ŷӵ�����
        {
            anim.SetBool(animaBoolName, false); // ������ǰ��������
            player.EnterAttackStateWithDelay(); // �ӳٽ�����һ�ι���
        }
        else
            StateMachine.ChangeState(player.idleState);
    }

    //�Ŷ���һ�ι���
    private void QueueNextAttack()
    {
        if(comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
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
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
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
