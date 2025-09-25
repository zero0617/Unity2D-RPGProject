using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//״̬��
public class StateMachine
{
    //����EntityState���͵ı������ⲿ�ɶ���get������ֻ�����ڲ��޸ģ�private set��
    public EntityState currentState { get; private set; }


    //��ʼ��״̬
    public void Initialize(EntityState startState)
    {
        //���õ�ǰ״̬Ϊ��ʼ״̬
        currentState = startState;

        //���õ�ǰ״̬����
        currentState.Enter();
    }


    //�޸�״̬
    public void ChangeState(EntityState newState)
    {
        //�˳���ǰ״̬
        currentState.Exit();

        //���µ�ǰ״̬
        currentState = newState;

        //���õ�ǰ״̬����
        currentState.Enter();
    }

    //����״̬
    public void UpdataActionState()
    {
        currentState.Update();
    }
}
