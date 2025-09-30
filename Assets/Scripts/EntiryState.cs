using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntiryState 
{
    protected StateMachine StateMachine;
    protected string animaBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected float stateTimer; //定时器
    protected bool triggerCalled;   //触发调用

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

    // 更新
    public virtual void Update()
    {
        //计时器
        stateTimer -= Time.deltaTime;

    }


    // 退出状态
    public virtual void Exit()
    {
        anim.SetBool(animaBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}
