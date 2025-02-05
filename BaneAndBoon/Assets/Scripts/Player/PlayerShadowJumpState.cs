using UnityEngine;

public class PlayerShadowJumpState : PlayerState
{
    public PlayerShadowJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.shadowAir);
        }
    }
}
