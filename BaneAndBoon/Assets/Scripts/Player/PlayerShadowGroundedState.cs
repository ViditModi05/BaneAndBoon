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

        if (Input.GetKeyDown(KeyCode.Tab) && player.isGrounded() && player.inShadowState && !player.isBusy && shadowStateSwitchTimer < 0)
        {
            shadowStateSwitchTimer = shadowStateDelay;
            player.shadowStateTimer = 0;
            player.inShadowState = false;
            player.StartCoroutine("BusyFor", .1f);
            player.switchManager.Invoke("SwitchfromShadowtoLight", 0);
            stateMachine.ChangeState(player.idleState);
        }
    }
}
