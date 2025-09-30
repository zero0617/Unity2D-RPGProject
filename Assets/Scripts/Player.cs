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

    //����ϸ��
    [Header("Attack details")]
    public Vector2[] attackVelocity;  //��������
    public float attackVelocityDuration = .1f;  //����ǰ�����ʱ��
    public float comboResetTime = 1;    //�������ʱ�䣨����������ʱ��û�м������������м����ͻ����ã�
    private Coroutine queuedArrackCo;


    //�ƶ�ϸ��
    [Header("Movement details")]
    public float moveSpeed; //�ƶ��ٶ�
    public float jumpForce = 5; //��Ծ�߶�
    public Vector2 wallJumpForce; //��ǽʱ��Ծ�ķ���

    [Range(0, 1)]
    public float wallSlideSlowMultipLier = .7f; //�»�����

    [Range(0, 1)]
    public float inAirMoveMultipLier = .7f; //������Ծ����

    [Space]
    public float dashDuration = .25f; //��̳���ʱ��
    public float dashSpeed = 20; //����ٶ�

    protected override void Awake()
    {
        base.Awake();
        
        input = new PlayerInputSet();   //����һ������ʵ������       
        idleState = new Player_IdleState(this, stateMachine, "idle");   //����һ������״̬ʵ������        
        moveState = new Player_MoveState(this, stateMachine, "move");   //����һ���ƶ�״̬ʵ������       
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");   //����һ����Ծ״̬ʵ������       
        fallState = new Player_FallState(this, stateMachine, "jumpFall");   //����һ������״̬ʵ������       
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");    //����һ����ǽ״̬ʵ������       
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");   //����һ����ǽ��Ծ״̬ʵ������   
        dashState = new Player_DashState(this, stateMachine, "dash");   //����һ�����״̬ʵ������
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");  //����һ����������״̬����        
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack"); //����һ����Ծ����״̬����
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    //�ӳٽ��빥��״̬
    public void EnterAttackStateWithDelay()
    {
        // ��������ӳٹ���Э���������У���ֹͣ��
        if (queuedArrackCo != null)
            StopCoroutine(queuedArrackCo);

        // �����µ��ӳٹ���Э�̲���������
        queuedArrackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    //ʵ���ӳٽ��빥��״̬���߼�
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        // �ȴ�ֱ����ǰ֡����
        yield return new WaitForEndOfFrame();

        // �л�����������״̬
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
