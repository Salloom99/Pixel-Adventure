using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;

    protected int xInput;
    protected int yInput;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerTouchingWallState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
    }

     public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        // grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;



        if(isExitingState)
            return;

        // if(jumpInput)
        // {
        //     player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
        //     stateMachine.ChangeState(player.WallJumpState);
        // }
        if(isGrounded)
            stateMachine.ChangeState(player.IdleState);

        else if (!isTouchingWall || xInput == -core.Movement.FacingDirection) // || (xInput != core.Movement.FacingDirection && !grabInput)
        {
            stateMachine.ChangeState(player.InAirState);
        }

    }

    

}
