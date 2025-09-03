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

        //创造一个状态机实例对象
        stateMachine = new StateMachine();

        //创建一个输入实例对象
        input = new PlayerInputSet();

        //创造一个空闲状态实例对象
        idleState = new Player_IdleState(this, stateMachine, "idle");

        //创建一个移动状态实例对象
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

    //修改人物线速度
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

    // 人物翻转
    public void Fild()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    
}
