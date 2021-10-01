using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/PlayerData")]
public class PlayerData : Data {

    [Header("Move State")]
    public float movementVelocity = 10f;
    public float movementSmooth = 0.15f;

    [Header("Jump State")]
    public float jumpForce = 1200f;
    public int amountOfJumps = 1;

    [Header("Wall Jump State")]
    public float wallJumpForce = 1200;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    // public float inAirMovementVelocity = 5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    // [Header("Wall Climb State")]
    // public float wallClimbVelocity = 3f;
    
    // [Header("Dash State")]
    // public float dashCooldown = 0.5f;
    // public float maxHoldTime = 1f;
    // public float holdTimeScale = 0.25f;
    // public float dashTime = 0.2f;
    // public float dashVelocity = 30f;
    // public float drag = 10f;
    // public float dashEndYMultiplier = 0.2f;
    // public float distBetweenAfterImages = 0.5f;


}