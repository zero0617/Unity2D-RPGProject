using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//状态实体
public abstract class EntityState 
{
    // 声明一个受保护的 StateMachine 类型的字段 StateMachine。
    // 该字段将持有对状态机的引用，以便状态可以控制状态转换或访问共享上下文。
    protected StateMachine StateMachine;

    protected string animaBoolName;

    protected Player player;

    protected Animator anim;

    protected Rigidbody2D rb;

    protected PlayerInputSet input;


    // 定义类的构造函数，它接受一个 StateMachine 类型的参数 stateMachine。
    public EntityState(Player player, StateMachine stateMachine, string animaBoolName)
    {
        // 将传入的 stateMachine 参数赋值给当前实例的 StateMachine 字段。
        // 这样，每个 EntityState 实例都知道它属于哪个状态机。
        this.player = player;
        this.StateMachine = stateMachine;
        this.animaBoolName = animaBoolName;


        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    // 当前状态方法
    public virtual void Enter()
    {
        anim.SetBool(animaBoolName, true);
    }

    // 更新
    public virtual void Update()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    // 退出状态
    public virtual void Exit()
    {
        anim.SetBool(animaBoolName, false);
    }
}
