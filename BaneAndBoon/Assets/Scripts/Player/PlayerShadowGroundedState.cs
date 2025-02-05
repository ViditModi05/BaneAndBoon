using UnityEngine;

public class PlayerShadowGroundedState : PlayerState
{
    public PlayerShadowGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded())
        {
            stateMachine.ChangeState(player.shadowJump);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && player.inShadowState)
        {
            player.inShadowState = false;
            stateMachine.ChangeState(player.idleState);
        }
    }
}
