using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//状态实体
public abstract class PlayerState : EntiryState
{
    // 声明一个受保护的 StateMachine 类型的字段 StateMachine。
    // 该字段将持有对状态机的引用，以便状态可以控制状态转换或访问共享上下文。
    protected PlayerInputSet input;
    protected Player player;



    // 定义类的构造函数，它接受一个 StateMachine 类型的参数 stateMachine。
    public PlayerState(Player player, StateMachine stateMachine, string animaBoolName) : base(stateMachine,animaBoolName)
    {
        // 将传入的 stateMachine 参数赋值给当前实例的 StateMachine 字段。
        // 这样，每个 PlayerState 实例都知道它属于哪个状态机。
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

    // 当前状态方法

    //是否能冲刺
    private bool canDash()
    {
        //爬墙不能冲刺
        if (player.wallDetected)
            return false;

        //不能连续冲刺
        if (StateMachine.currentState == player.dashState)
            return false;


        return true;
    }

}
