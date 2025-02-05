using UnityEngine;

public class PlayerState 
{
    [Header("StateMachine")]
    protected PlayerStateMachine stateMachine;
    protected Player player; 
    private string animBoolName;
    protected float stateTimer;

    [Header("Movement")]
    protected float xInput;
    protected float yInput;

    [Header("Components")]
    protected Rigidbody2D rb;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void EnterState()
    {
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void ExitState()
    {
        player.animator.SetBool(animBoolName, false);
    }

    public virtual void UpdateState()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yVelocity", rb.linearVelocity.y);
        stateTimer -= Time.deltaTime;    
    }
}
