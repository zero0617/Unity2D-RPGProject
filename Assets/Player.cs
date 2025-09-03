using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;

    private PlayerInputSet input;

    public Vector2 moveInput { get; private set; }
    public Animator anim { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }



    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        //����һ��״̬��ʵ������
        stateMachine = new StateMachine();

        //����һ���������
        input = new PlayerInputSet();

        //����һ������״̬����
        idleState = new Player_IdleState(this, stateMachine, "idle");

        //����һ���ƶ�״̬����
        moveState = new Player_MoveState(this, stateMachine, "move");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    

    private void Update()
    {
        stateMachine.UpdataActionState();
    }


}
