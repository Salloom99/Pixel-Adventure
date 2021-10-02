using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreStuff;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public int FacingDirection { get; private set; }
    public bool CanSetVelocity { get; set; }
    public Vector2 CurrentVelocity { get; private set; }
    public float surfaceFriction { get; private set; }

    #region getters
    public float WallJumpTime { get=> data.wallJumpTime; }
    public float CoyoteTime { get => data.coyoteTime; }
    public int AmountOfJumps { get => data.amountOfJumps; }
    #endregion

    [SerializeField] private MovementData data;
    private Vector2 workspace;
    private Vector2 VelZero = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Functions

    public void SetVelocityZero()
    {
        workspace = Vector2.zero;        
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void OldSetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        if (CanSetVelocity)
        {
            // RB.velocity = workspace;
            RB.velocity = Vector2.SmoothDamp(CurrentVelocity,workspace,ref VelZero,0.15f/* data.movementSmooth */);
            // CurrentVelocity = RB.velocity;
            CurrentVelocity = workspace;
        }        
    }

    public void SetVelocityX(float xInput)
    {
        workspace.Set(data.moveAccel*xInput, 0f);
        RB.AddForce(workspace);

        if(Mathf.Abs(RB.velocity.x) > data.maxMoveVel)
            RB.velocity = new Vector2(Mathf.Sign(RB.velocity.x) * data.maxMoveVel,RB.velocity.y);

        CurrentVelocity = RB.velocity;   
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        if(CurrentVelocity.y < velocity)
            SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }        
    }

    public void ApplyGroundLinearDrag(int xInput)
    {
        if(xInput == 0)
            RB.velocity = new Vector2(RB.velocity.x*(1-data.groundLinearDrag),RB.velocity.y);
        
    }

    public void ApplyAirLinearDrag(int xInput)
    {
        if(xInput == 0)
            RB.velocity = new Vector2(RB.velocity.x*(1-data.airLinearDrag),RB.velocity.y);
    }

    public void Jump()
    {
        workspace.Set(RB.velocity.x,data.jumpForce);
        RB.velocity = workspace;
        //RB.AddForce(Vector2.up * data.jumpForce,ForceMode2D.Impulse);
    }

    public void AddVelocityUp(float velocity)
    {
        RB.gravityScale = data.normalMultiplier;
        workspace.Set(RB.velocity.x,velocity);
        RB.velocity = workspace;
    }

    public void WallJump(int target)
    {
        workspace.Set(data.wallJumpAngle.x*target,data.wallJumpAngle.y);
        RB.velocity = workspace*data.jumpForce;/* Vector2.zero; */
        // RB.AddForce(workspace * data.jumpForce,ForceMode2D.Impulse);
    }

    public void SetFallMultiplier(FallMultiplier value)
    {
        switch(value)
        {
            case FallMultiplier.LowJump:
                RB.gravityScale = data.lowJumpMultiplier;
                break;
            case FallMultiplier.Fall:
                RB.gravityScale = data.fallMultiplier;
                break;
            default:
                RB.gravityScale = data.normalMultiplier;
                break;
        }

    }

    public void setGravity(float value)
    {
        RB.gravityScale = value;
    }

    public void Slide()
    {
        RB.velocity = Vector2.down*data.wallSlideVelocity;
    }

    public float getDeadZone()=> data.deadZone;

    public void SetFriction(float value)
    {
        surfaceFriction = value;
    }

    public void OldCheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void OldFlip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (Mathf.Sign(CurrentVelocity.x) != FacingDirection && Mathf.Abs(CurrentVelocity.x) > data.deadZone)
            Flip();
    }

    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion


    public enum FallMultiplier{
        LowJump,
        Fall,
        Normal
    }
}
