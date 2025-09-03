using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;  

    public PlayerInputSet input { get; private set; }

    public Vector2 moveInput { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }


    [Header("Movement details")]
    public float moveSpeed;
    public bool facingRight = true;
    public float jumpForce = 5;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //����һ��״̬��ʵ������
        stateMachine = new StateMachine();

        //����һ������ʵ������
        input = new PlayerInputSet();

        //����һ������״̬ʵ������
        idleState = new Player_IdleState(this, stateMachine, "idle");

        //����һ���ƶ�״̬ʵ������
        moveState = new Player_MoveState(this, stateMachine, "move");

        //����һ����Ծ״̬ʵ������
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");

        //����һ������״̬ʵ������
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
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

    //�޸��������ٶ�
    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        handleFild(xVelocity);
    }



    // ���﷭ת
    public void handleFild(float xVelocity)
    {
        if (xVelocity > 0 && facingRight == false)
            Fild();
        else if (xVelocity < 0 && facingRight)
            Fild();
    }

    public void Fild()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    
}
