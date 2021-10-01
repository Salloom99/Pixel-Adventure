using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreStuff;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    // public Transform LedgeCheckHorizontal
    // {
    //     get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
    //     private set => ledgeCheckHorizontal = value;
    // }
    // public Transform LedgeCheckVertical
    // {
    //     get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
    //     private set => ledgeCheckVertical = value;
    // }
    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    //[SerializeField] private Transform ledgeCheckHorizontal;
    //[SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    private BoxCollider2D boxCollider;
    #endregion

    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool Ground
    {
        get => Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.extents,0f,Vector2.down,groundCheckRadius,whatIsGround);
    }

    public bool WallFront
    {
        get => Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.extents,0f,Vector2.right*core.Movement.FacingDirection,wallCheckDistance,whatIsWall);
    }

    public bool WallBack
    {
       get => Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.extents,0f,-Vector2.right*core.Movement.FacingDirection,wallCheckDistance,whatIsWall);
    }

    // public bool LedgeHorizontal
    // {
    //     get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    // }

    // public bool LedgeVertical
    // {
    //     get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    // }

    

    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponentInParent<BoxCollider2D>();
    }

    // private void OnDrawGizmos() {
    //     Gizmos.DrawLine()
    // }
}
