using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if(player.isBlockDetected() && player.isGrounded() && !player.IsBlockHittingWall() && xInput != 0 && !player.inShadowState)
        {
            stateMachine.ChangeState(player.moveBlock);
        }
        if(Input.GetKeyDown(KeyCode.Space) && player.isGrounded())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if(!player.isGrounded())
        {
            stateMachine.ChangeState(player.airState);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && player.isGrounded() && !player.inShadowState && !player.isBusy && !player.isDashing && shadowStateSwitchTimer < 0)
        {
            shadowStateSwitchTimer = shadowStateDelay;
            player.inShadowState = true;
            player.StartCoroutine("BusyFor", .1f);
            player.switchManager.Invoke("StartSwitch", 0);
            stateMachine.ChangeState(player.shadowState);
        }
    }
}
