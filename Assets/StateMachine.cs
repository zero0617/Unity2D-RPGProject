using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//状态机
public class StateMachine
{
    //定义EntityState类型的变量，外部可读（get），但只能在内部修改（private set）
    public EntityState currentState { get; private set; }


    //初始化状态
    public void Initialize(EntityState startState)
    {
        //设置当前状态为起始状态
        currentState = startState;

        //调用当前状态方法
        currentState.Enter();
    }


    //修改状态
    public void ChangeState(EntityState newState)
    {
        //退出当前状态
        currentState.Exit();

        //更新当前状态
        currentState = newState;

        //调用当前状态方法
        currentState.Enter();
    }

    //更新状态
    public void UpdataActionState()
    {
        currentState.Update();
    }
}
