using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected StateMachine stateMachine;  //״̬��
    public Animator anim { get; private set; }
    public Rigidbody2D rb;

    public bool facingRight = true; //�Ƿ���
    public int facingDir { get; private set; } = 1; //ˮƽ���߷���1����  -1: ��




    //��ײϸ��
    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance; //������ľ���
    [SerializeField] private float wallCheckDistance;   //��ǽ�ھ���
    [SerializeField] private LayerMask whatIsGround;    //�Ƿ��е�������
    [SerializeField] private Transform primaryWallCheck;    //�ϰ�ǽ�ڼ��
    [SerializeField] private Transform secondaryWallCheck;    //�°�ǽ�ڼ��


    public bool groundDetected { get; private set; } //�Ƿ��ڵ���
    public bool wallDetected { get; private set; }  //�Ƿ�Ϊǽ��

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //����һ��״̬��ʵ������
        stateMachine = new StateMachine();


    }

    protected virtual void Start()
    {
        
    }

 
    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdataActionState();
    }



    //�޸��������ٶ�
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        handleFild(xVelocity);
    }

    //����������
    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
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
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                    && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    //��������
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));

    }

}
