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

        //创造一个状态机实例对象
        stateMachine = new StateMachine();

        //创建一个输入对象
        input = new PlayerInputSet();

        //创造一个空闲状态对象
        idleState = new Player_IdleState(this, stateMachine, "idle");

        //创建一个移动状态变量
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
