using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//״̬ʵ��
public abstract class EntityState 
{
    // ����һ���ܱ����� StateMachine ���͵��ֶ� StateMachine��
    // ���ֶν����ж�״̬�������ã��Ա�״̬���Կ���״̬ת������ʹ��������ġ�
    protected StateMachine StateMachine;

    protected string animaBoolName;

    protected Player player;

    protected Animator anim;

    protected Rigidbody2D rb;

    protected PlayerInputSet input;

    protected float stateTimer; //��ʱ��

    protected bool triggerCalled;   //��������


    // ������Ĺ��캯����������һ�� StateMachine ���͵Ĳ��� stateMachine��
    public EntityState(Player player, StateMachine stateMachine, string animaBoolName)
    {
        // ������� stateMachine ������ֵ����ǰʵ���� StateMachine �ֶΡ�
        // ������ÿ�� EntityState ʵ����֪���������ĸ�״̬����
        this.player = player;
        this.StateMachine = stateMachine;
        this.animaBoolName = animaBoolName;


        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    // ��ǰ״̬����
    public virtual void Enter()
    {
        anim.SetBool(animaBoolName, true);
        triggerCalled = false;
    }

    // ����
    public virtual void Update()
    {
        //��ʱ��
        stateTimer -= Time.deltaTime;

        anim.SetFloat("yVelocity", rb.velocity.y);

        if (input.Player.Dash.WasPressedThisFrame() && canDash())
            StateMachine.ChangeState(player.dashState);
    }


    // �˳�״̬
    public virtual void Exit()
    {
        anim.SetBool(animaBoolName, false);
    }

    //�Ƿ��ܳ��
    private bool canDash()
    {
        //��ǽ���ܳ��
        if (player.wallDetected)
            return false;

        //�����������
        if (StateMachine.currentState == player.dashState)
            return false;


        return true;
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}
