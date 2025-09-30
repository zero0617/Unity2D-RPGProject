using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntiryState 
{
    protected StateMachine StateMachine;
    protected string animaBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected float stateTimer; //��ʱ��
    protected bool triggerCalled;   //��������

    public EntiryState(StateMachine StateMachine, string animaBoolName)
    {
        this.StateMachine = StateMachine;
        this.animaBoolName = animaBoolName;
    }

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

    }


    // �˳�״̬
    public virtual void Exit()
    {
        anim.SetBool(animaBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}
