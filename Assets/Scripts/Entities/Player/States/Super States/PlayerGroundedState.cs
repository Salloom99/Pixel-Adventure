using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingWallBack;
    private bool isTouchingLedge;
    private bool dashInput;

    public PlayerGroundedState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
        isTouchingWallBack = core.CollisionSenses.WallBack;
        // isTouchingLedge = core.CollisionSenses.LedgeHorizontal;
        // isTouchingCeiling = core.CollisionSenses.Ceiling;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.Resetjumps();
        player.Anim.SetFloat("yVelocity",0);
        player.Anim.SetBool("doubleJump",false);
        // player.DashState.ResetCanDash();

        movement.SetFallMultiplier(Movement.FallMultiplier.Normal);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        // if(xInput ==0)
        //     movement.ApplyDrag(Time.deltaTime);
        

        // if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        // {
        //     stateMachine.ChangeState(player.PrimaryAttackState);
        // }
        // else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        // {
        //     stateMachine.ChangeState(player.SecondaryAttackState);
        // }
        if (JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        // }else if(isTouchingWall && grabInput && isTouchingLedge)
        // {
        //     stateMachine.ChangeState(player.WallGrabState);
        // }
        // else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        // {
        //     stateMachine.ChangeState(player.DashState);
        // }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        movement.ApplyGroundLinearDrag(xInput);
    }

}
