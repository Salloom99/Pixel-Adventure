using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/MovementData")]
public class MovementData : ScriptableObject 
{
    [Header("Move State")]
    public float maxMoveVel = 10f;
    public float moveAccel = 1f;
    public float groundLinearDrag = 1f;
    public float normalLinearDrag = 0f;
    public float sandLinearDrag = 1f;
    public float mudLinearDrag = 1f;
    public float iceLinearDrag = 1f;
    public float deadZone = 0.05f;
    public float movementSmooth = 0.15f;

    [Header("Jump State")]
    public float jumpForce = 1200f;
    public int amountOfJumps = 1;

    [Header("Wall Jump State")]
    public float wallJumpForce = 1200;
    public float wallJumpTime = 0.2f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float normalMultiplier = 6f;
    public float fallMultiplier = 8f;
    public float lowJumpMultiplier = 5f;
    public float airLinearDrag = 1f;
    public float extremeAirLinearDrag = 1f;
    // public float inAirMovementVelocity = 5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;    
}