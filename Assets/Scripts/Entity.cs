using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected StateMachine stateMachine;  //状态机
    public Animator anim { get; private set; }
    public Rigidbody2D rb;

    public bool facingRight = true; //是否朝右
    public int facingDir { get; private set; } = 1; //水平射线方向（1：右  -1: 左）




    //碰撞细节
    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance; //到地面的距离
    [SerializeField] private float wallCheckDistance;   //到墙壁距离
    [SerializeField] private LayerMask whatIsGround;    //是否有地面属性
    [SerializeField] private Transform primaryWallCheck;    //上半墙壁检查
    [SerializeField] private Transform secondaryWallCheck;    //下半墙壁检查


    public bool groundDetected { get; private set; } //是否在地面
    public bool wallDetected { get; private set; }  //是否为墙壁

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //创造一个状态机实例对象
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



    //修改人物线速度
    public void SetVelocity(float xVelocity, float yVelocity)
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
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
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
