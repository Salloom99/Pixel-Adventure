using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isExitingState)
            return;
            
        movement.Slide();

        player.Anim.SetFloat("yVelocity",core.Movement.CurrentVelocity.y);

        // if(grabInput && yInput == 0)
        //     stateMachine.ChangeState(player.WallGrabState);

        if(jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        
        
    }
}
