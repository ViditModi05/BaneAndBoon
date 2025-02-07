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
        if (player.onJump != null)
        {
            player.onJump();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKeyDown(KeyCode.Tab) && !player.isGrounded() && player.inShadowState && !player.isBusy && shadowStateSwitchTimer < 0)
        {
            shadowStateSwitchTimer = shadowStateDelay;
            player.shadowStateTimer = 0;
            player.inShadowState = false;
            player.StartCoroutine("BusyFor", .1f);
            player.switchManager.Invoke("SwitchfromShadowtoLight", 0);
            stateMachine.ChangeState(player.idleState);
        }
        if (player.shadowStateTime <= player.shadowStateTimer)
        {
            player.shadowStateTimer = 0;
            shadowStateSwitchTimer = shadowStateDelay;
            player.inShadowState = false;
            player.StartCoroutine("BusyFor", .1f);
            player.switchManager.Invoke("SwitchfromShadowtoLight", 0);
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0)
        {
            rb.linearVelocity = new Vector2(player.moveSpeed * .8f * xInput, rb.linearVelocity.y);
        }
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.shadowAir);
        }
    }
}
