using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Vector2 moveInput { get; private set; }
    public PlayerInputSet input { get; private set; }

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
    public float jumpForce = 5; //跳跃高度
    public Vector2 wallJumpForce; //滑墙时跳跃的方向

    [Range(0, 1)]
    public float wallSlideSlowMultipLier = .7f; //下滑阻力

    [Range(0, 1)]
    public float inAirMoveMultipLier = .7f; //空中跳跃阻力

    [Space]
    public float dashDuration = .25f; //冲刺持续时间
    public float dashSpeed = 20; //冲刺速度

    protected override void Awake()
    {
        base.Awake();
        
        input = new PlayerInputSet();   //创建一个输入实例对象       
        idleState = new Player_IdleState(this, stateMachine, "idle");   //创造一个空闲状态实例对象        
        moveState = new Player_MoveState(this, stateMachine, "move");   //创建一个移动状态实例对象       
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");   //创建一个跳跃状态实例对象       
        fallState = new Player_FallState(this, stateMachine, "jumpFall");   //创建一个下落状态实例对象       
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");    //创建一个滑墙状态实例对象       
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");   //创建一个滑墙跳跃状态实例对象   
        dashState = new Player_DashState(this, stateMachine, "dash");   //创建一个冲刺状态实例对象
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");  //创建一个基础攻击状态对象        
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack"); //创建一个跳跃攻击状态对象
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
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

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
