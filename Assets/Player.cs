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
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }


    //攻击细节
    [Header("Attack details")]
    public Vector2[] attackVelocity;  //攻击向量
    public float attackVelocityDuration = .1f;  //攻击前冲持续时间
    public float comboResetTime = 1;    //攻击间隔时间（如果超过这个时间没有继续攻击，连招计数就会重置）
    private Coroutine queuedArrackCo;


    //移动细节
    [Header("Movement details")]
    public float moveSpeed; //移动速度
    public bool facingRight = true; //是否朝右
    public int facingDir { get; private set; } = 1; //水平射线方向（1：右  -1: 左）
    public float jumpForce = 5; //跳跃高度
    public Vector2 wallJumpForce; //滑墙时跳跃的方向


    [Range(0, 1)]
    public float wallSlideSlowMultipLier = .7f; //下滑阻力

    [Range(0,1)]
    public float inAirMoveMultipLier = .7f; //空中跳跃阻力

    [Space]
    public float dashDuration = .25f; //冲刺持续时间
    public float dashSpeed = 20; //冲刺速度


    //碰撞细节
    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance; //到地面的距离
    [SerializeField] private float wallCheckDistance;   //到墙壁距离
    [SerializeField] private LayerMask whatIsGround;    //是否有地面属性
    [SerializeField] private Transform primaryWallCheck;    //上半墙壁检查
    [SerializeField] private Transform secondaryWallCheck;    //下半墙壁检查


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

        //创建一个基础攻击状态对象
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");

        //创建一个跳跃攻击状态对象
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
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

    //延迟进入攻击状态
    public void EnterAttackStateWithDelay()
    {
        // 如果已有延迟攻击协程正在运行，则停止它
        if (queuedArrackCo != null)
            StopCoroutine(queuedArrackCo);

        // 启动新的延迟攻击协程并保存引用
        queuedArrackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    //实现延迟进入攻击状态的逻辑
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        // 等待直到当前帧结束
        yield return new WaitForEndOfFrame();

        // 切换到基本攻击状态
        stateMachine.ChangeState(basicAttackState);
    }

    //修改人物线速度
    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        handleFild(xVelocity);
    }

    //动画触发器
    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
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
        wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                    && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    //人物射线
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));

    }

}
