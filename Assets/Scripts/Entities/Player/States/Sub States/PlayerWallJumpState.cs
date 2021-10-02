using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int xInput;
    private int wallJumpDirection;

    public PlayerWallJumpState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        //core.Movement.SetVelocity(playerData.wallJumpVelocity,playerData.wallJumpAngle,wallJumpDirection);
        core.Movement.WallJump(wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.UseJump();
    }

  
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Anim.SetFloat("yVelocity",player.Core.Movement.CurrentVelocity.y);

        if(Time.time >= startTime + movement.WallJumpTime)
            isAbilityDone=true;
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if(isTouchingWall)
            wallJumpDirection = -core.Movement.FacingDirection;
        else
            wallJumpDirection = core.Movement.FacingDirection;
    }
}
