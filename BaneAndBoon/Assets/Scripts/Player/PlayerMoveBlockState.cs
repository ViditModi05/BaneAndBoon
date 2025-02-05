using UnityEngine;

public class PlayerMoveBlockState : PlayerState
{
    public PlayerMoveBlockState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.IsBlockHittingWall())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        rb.linearVelocity = new Vector2(xInput * player.moveSpeed, rb.linearVelocity.y);

        if (player.isBlockDetected())
        {
            Vector3 blockMove = new Vector3(1, 0, 0);
            Rigidbody2D blockRb = player.block.GetComponent<Rigidbody2D>();
            blockRb.linearVelocity = new Vector2(xInput, 0);

        }

        if (!player.isBlockDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }


}
