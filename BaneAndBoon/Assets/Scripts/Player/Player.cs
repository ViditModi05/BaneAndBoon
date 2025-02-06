using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask movableBlock;
    public SpriteRenderer sr {  get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody2D rb {  get; private set; }
    public SwitchManager switchManager { get; private set; }

    [Header("State Machine")]
    public PlayerStateMachine stateMachine { get; private set; }

    [Header("Movement")]
    public float moveSpeed = 12f;
    public float jumpForce = 7f;
    public bool isMoving;

    [Header("Flip")]
    public int direction { get; private set; } = 1;
    private bool isFacingRight = true;

    [Header("Shadow State Settings")]
    public bool inShadowState;
    public bool isBusy { get; private set; }
    public float shadowStateTimer;
    public float shadowStateTime = 5f;

    [Header("Dash")]
    [SerializeField] private float dashCooldown;
    public bool isDashing;
    public float dashSpeed;
    public float dashDuration;
    private float dashCooldownTimer;
    public float dashDirection {  get; private set; }

    [Header("Jump Settings")]
    public int maxJumps = 2;
    private int currentJumps;

    [Header("Blocks")]
    public Transform block {  get; private set; }

    [Header("Key")]
    public bool hasKey;

    [Header("Auido Events")]
    public System.Action onMove;
    public System.Action stopMove;
    public System.Action onJump;
    public System.Action onDash;


    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerShadowState shadowState { get; private set; }
    public PlayerDashState dashState { get; private set; }  
    public PlayerShadowMoveState shadowMove  { get; private set; }
    public PlayerShadowJumpState shadowJump { get; private set; }
    public PlayerShadowAirState shadowAir {  get; private set; }
    public PlayerMoveBlockState moveBlock { get; private set; }
    public PlayerShadowWallJumpState shadowWallJump { get; private set; }
    public PlayerShadowWallSlideState shadowWallSlide { get; private set; }
    #endregion

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        switchManager = SwitchManager.instance;
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        wallJump  = new PlayerWallJumpState(this, stateMachine, "Jump");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        shadowState = new PlayerShadowState(this, stateMachine, "ShadowState");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        shadowMove = new PlayerShadowMoveState(this, stateMachine, "ShadowMove");
        shadowJump = new PlayerShadowJumpState(this, stateMachine, "ShadowJump");
        shadowAir = new PlayerShadowAirState(this, stateMachine, "ShadowJump");
        moveBlock = new PlayerMoveBlockState(this, stateMachine, "Move");
        shadowWallJump = new PlayerShadowWallJumpState(this, stateMachine, "ShadowJump");
        shadowWallSlide = new PlayerShadowWallSlideState(this, stateMachine, "ShadowWallSlide");


    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        stateMachine.Initialize(idleState);
        currentJumps = maxJumps;


    }

    private void Update()
    {
        stateMachine.currentState.UpdateState();
        dashCooldownTimer -= Time.deltaTime;
        if(transform.position.y < -10f)
        {
            Death();
        }
        DashInput();
        FlipController();
        if (isGrounded())
        {
            currentJumps = maxJumps;
        }
        if(inShadowState && shadowStateTimer != shadowStateTime)
        {
            shadowStateTimer += Time.deltaTime;
        }
    }

    public void Death()
    {
        Vector2 respawnPosition = Checkpoint_Manager.instance.GetCheckpoint();
        if (respawnPosition != Vector2.zero) 
        {
            transform.position = respawnPosition;
        }
    }

    public bool CanJump() => currentJumps > 0;
    public void UseJump() => currentJumps--;

    private void DashInput()
    {
        if(isWallDetected())
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer < 0 && !inShadowState) 
        {
            dashCooldownTimer = dashCooldown;
            dashDirection = Input.GetAxisRaw("Horizontal");
            if(dashDirection == 0)
            {
                dashDirection = direction;
            }
            stateMachine.ChangeState(dashState);
            isDashing = true;
        }
    }

    public bool isGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, ground) || Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, movableBlock);
    public bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, wallCheckDistance, ground);   
    public bool isBlockDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, Vector2.right * direction, wallCheckDistance, movableBlock);
        block = hit.transform;
        return hit;
    }

    public bool IsBlockHittingWall()
    {

        RaycastHit2D hit = Physics2D.Raycast(block.position, Vector2.right * direction, 1f , ground);
        return hit;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + +wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        direction *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if( rb.linearVelocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        if(rb.linearVelocity.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void MoveWithBlock(float blockMovement)
    {
        if (!isMoving)
        {
            rb.linearVelocity = new Vector2(blockMovement, rb.linearVelocity.y);
        }
    }
}
