using UnityEngine;

public class PlayerShadowAirState : PlayerState
{
    public PlayerShadowAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (player.isGrounded())
        {
            stateMachine.ChangeState(player.shadowState);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && player.inShadowState)
        {
            player.inShadowState = false;
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0)
        {
            rb.linearVelocity = new Vector2(player.moveSpeed * .8f * xInput, rb.linearVelocity.y);
        }
    }
}
