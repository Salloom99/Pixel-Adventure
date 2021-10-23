using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.CheckIfShouldFlip(xInput);
        // core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

        //player.particleSystem.Play();

        // if (!isExitingState)
        //     return;
        bool cantMove = isTouchingWall && xInput == movement.FacingDirection;

        if(xInput ==0 && Mathf.Abs(core.Movement.CurrentVelocity.x) < movement.getDeadZone() || cantMove)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        core.Movement.SetVelocityX(xInput);
    }
}
