using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//״̬ʵ��
public abstract class PlayerState : EntiryState
{
    // ����һ���ܱ����� StateMachine ���͵��ֶ� StateMachine��
    // ���ֶν����ж�״̬�������ã��Ա�״̬���Կ���״̬ת������ʹ��������ġ�
    protected PlayerInputSet input;
    protected Player player;



    // ������Ĺ��캯����������һ�� StateMachine ���͵Ĳ��� stateMachine��
    public PlayerState(Player player, StateMachine stateMachine, string animaBoolName) : base(stateMachine,animaBoolName)
    {
        // ������� stateMachine ������ֵ����ǰʵ���� StateMachine �ֶΡ�
        // ������ÿ�� PlayerState ʵ����֪���������ĸ�״̬����
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (input.Player.Dash.WasPressedThisFrame() && canDash())
            StateMachine.ChangeState(player.dashState);
    }

    // ��ǰ״̬����

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

}
