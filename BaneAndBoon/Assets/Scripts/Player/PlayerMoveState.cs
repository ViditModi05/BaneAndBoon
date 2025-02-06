using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        player.isMoving = true;
        if (player.onMove != null)
        {
            player.onMove();
        }

    }

    public override void ExitState()
    {
        base.ExitState();
        player.isMoving = false;
        if(player.stopMove != null)
        {
            player.stopMove();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.linearVelocity = new Vector2(xInput * player.moveSpeed, rb.linearVelocity.y);


        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }


    }
}
