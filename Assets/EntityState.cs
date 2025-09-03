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
    }

    // ����
    public virtual void Update()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    // �˳�״̬
    public virtual void Exit()
    {
        anim.SetBool(animaBoolName, false);
    }
}
