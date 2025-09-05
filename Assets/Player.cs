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
    public float moveSpeed; //�ƶ��ٶ�
    public bool facingRight = true; //�Ƿ���
    public int facingDir { get; private set; } = 1; //ˮƽ���߷���1 ���ң�
    public float jumpForce = 5; //��Ծ�߶�
    public Vector2 wallJumpForce; //��ǽʱ��Ծ�ķ���


    [Range(0, 1)]
    public float wallSlideSlowMultipLier = .7f; //�»�����

    [Range(0,1)]
    public float inAirMoveMultipLier = .7f; //������Ծ����

    [Space]
    public float dashDuration = .25f; //��̳���ʱ��
    public float dashSpeed = 20; //����ٶ�

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance; //������ľ���
    [SerializeField] private float wallCheckDistance;   //��ǽ�ھ���
    [SerializeField] private LayerMask whatIsGround;    //�Ƿ��е�������

    public bool groundDetected { get; private set; } //�Ƿ��ڵ���
    public bool wallDetected { get; private set; }  //�Ƿ�Ϊǽ��

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

        //����һ����ǽ״̬ʵ������
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");

        //����һ����ǽ��Ծ״̬ʵ������
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");

        //����һ�����״̬ʵ������
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

    //�޸��������ٶ�
    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        handleFild(xVelocity);
    }



    // ���﷭ת
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

    //��������Ƿ��ڵ���
    public void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance  ,whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
    }

    //��������
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }

}
