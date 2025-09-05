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
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }


    [Header("Movement details")]
    public float moveSpeed; //移动速度
    public bool facingRight = true; //是否朝右
    public int facingDir { get; private set; } = 1; //水平射线方向（1 ：右）
    public float jumpForce = 5; //跳跃高度
    public Vector2 wallJumpForce; //滑墙时跳跃的方向


    [Range(0, 1)]
    public float wallSlideSlowMultipLier = .7f; //下滑阻力

    [Range(0,1)]
    public float inAirMoveMultipLier = .7f; //空中跳跃阻力

    [Space]
    public float dashDuration = .25f; //冲刺持续时间
    public float dashSpeed = 20; //冲刺速度

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance; //到地面的距离
    [SerializeField] private float wallCheckDistance;   //到墙壁距离
    [SerializeField] private LayerMask whatIsGround;    //是否有地面属性

    public bool groundDetected { get; private set; } //是否在地面
    public bool wallDetected { get; private set; }  //是否为墙壁

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

        //创建一个跳跃状态实例对象
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");

        //创建一个下落状态实例对象
        fallState = new Player_FallState(this, stateMachine, "jumpFall");

        //创建一个滑墙状态实例对象
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");

        //创建一个滑墙跳跃状态实例对象
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");

        //创建一个冲刺状态实例对象
        dashState = new Player_DashState(this, stateMachine, "dash");
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
        HandleCollisionDetection();
        stateMachine.UpdataActionState();
    }

    //修改人物线速度
    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        handleFild(xVelocity);
    }



    // 人物翻转
    private void handleFild(float xVelocity)
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
        facingDir = facingDir * -1;
    }

    //检测人物是否在地面
    public void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance  ,whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
    }

    //人物射线
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }

}
