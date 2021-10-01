using UnityEngine;

[CreateAssetMenu(fileName = "TrapsData", menuName = "Data/TrapsData")]
public class TrapsData : ScriptableObject 
{

    [Header("Common")]
    public float bounceVelocity = 10f;

    [Header("Boxes")]
    public int box1Life = 1;
    public int box2Life = 5;
    public int box3Life = 5;
    public float explosionVelocity = 5f;

    [Header("Trampoline")]
    public float trampolineJumpForce = 2000f;

    [Header("Fan")]
    public float fanTimer = 5f;
    public float weakFanForce = 70f;
    public float MidFanForce = 83.385f;
    public float strongFanForce = 90f;

    [Header("Arrow")]
    public float ArrowForce = 1300f;

    [Header("Platform")]
    public float platformSpeed = 0.1f;
    public float platformWaitTime = 1f;
    public float fallingPlatformWaitTime = 0.5f;

    [Header("Saw")]
    public float sawSpeed = .1f;

    [Header("Fire")]
    public float fireWaitTime = .5f;
    public float fireOnTime = .5f;

    [Header("Heads")]
    public float RockHeadVelocity =10f;
    public float SpikeHeadVelocity =10f;
}