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
            player.Death();
        }
        if (player.isWallDetected())
        {
            stateMachine.ChangeState(player.shadowWallSlide);
        }
        if (xInput != 0)
        {
            rb.linearVelocity = new Vector2(player.moveSpeed * .8f * xInput, rb.linearVelocity.y);
        }
    }
}
