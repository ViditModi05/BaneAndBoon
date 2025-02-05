using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }
        if(yInput < 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y * .7f);
        }

        if(xInput != 0 && player.direction != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(player.isGrounded())
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(!player.isGrounded() && !player.isWallDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
  
    }
}
