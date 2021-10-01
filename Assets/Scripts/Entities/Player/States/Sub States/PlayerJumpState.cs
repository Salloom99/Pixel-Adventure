using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using FiniteStateMachine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
        amountOfJumpsLeft = movement.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        movement.ApplyGroundLinearDrag(1);
        movement.Jump();
        isAbilityDone = true;
        amountOfJumpsLeft--;

        if(!isGrounded && !isTouchingWall)
            player.Anim.SetBool("doubleJump",true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("doubleJump",false);
    }



    public bool CanJump() => amountOfJumpsLeft > 0;

    public void Resetjumps() => amountOfJumpsLeft = movement.AmountOfJumps;

    public void UseJump() => amountOfJumpsLeft--;
}
