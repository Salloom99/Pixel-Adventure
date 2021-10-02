using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{

    public PlayerIdleState(Player entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //core.Movement.SetVelocityX(0f);

        // if(core.Movement.CurrentVelocity.x>playerData.movementVelocity)
        //     core.Movement.SetVelocityX(playerData.movementVelocity);
        
        // if (!isExitingState)
        //     return;

        bool cantMove = isTouchingWall && xInput == movement.FacingDirection;

        if(xInput !=0 && !cantMove)
            stateMachine.ChangeState(player.MoveState);
    }
}
