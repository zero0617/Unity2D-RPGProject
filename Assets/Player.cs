using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;  

    private PlayerInputSet input;

    public Vector2 moveInput { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    [Header("Movement details")]
    public float moveSpeed;
    public bool facingRight = true;


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


    //

    public void handleFild(float xVelocity)
    {
        if (xVelocity > 0 && facingRight == false)
            Fild();
        else if (xVelocity < 0 && facingRight)
            Fild();
    }

    // ���﷭ת
    public void Fild()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    
}
