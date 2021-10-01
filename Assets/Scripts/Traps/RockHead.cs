using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreStuff;

public class RockHead : MonoBehaviour
{
    [SerializeField] private TrapsData data;
    [SerializeField] private bool Linear;
    [SerializeField] private float Speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private LayerMask WhatIsGround;
    private CameraShake cameraShake;

    
    private Animator Anim;
    private Rigidbody2D RockHeadRB;
    private Core core;
    private GameObject player;


    private bool waiting;
    private float velocity;

    

    private void Awake() 
    {
        Anim = GetComponent<Animator>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

        if(Speed == 0)
            Speed = data.RockHeadVelocity;
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.position.y >transform.position.y && other.gameObject.tag =="Player")
        {
            other.transform.parent = transform;
            core = other.gameObject.GetComponentInChildren<Core>();
        }

    }

    private void OnCollisionStay2D(Collision2D other) {
        if( direction == Vector2.up && core.CollisionSenses.Ceiling ||
            direction == Vector2.down && core.CollisionSenses.Ground ||
            direction == Vector2.left && core.Movement.FacingDirection == 1 ? core.CollisionSenses.WallFront : core.CollisionSenses.WallBack ||
            direction == Vector2.right && core.Movement.FacingDirection == 1 ? core.CollisionSenses.WallFront : core.CollisionSenses.WallBack)
            {StartCoroutine(cameraShake.Shake());
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            other.gameObject.GetComponent<Player>().enabled = false;
            Destroy(other.transform.GetChild(0).gameObject);
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x,20);
            rb.AddTorque(rb.velocity.x);
            player = other.gameObject;

        Invoke("DestroyOB",2f);}
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.transform.parent = null;
    }

    private void Update() 
    {   
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction,1f,WhatIsGround);
        if(hit)
        {
            
            transform.position = new Vector3(hit.point.x-direction.x,hit.point.y-direction.y);
           Anim.SetFloat("H",direction.x);
            Anim.SetFloat("V",direction.y);
            Anim.Play("Hit");
            ChangeDirection();
            StartCoroutine(cameraShake.Shake());
            waiting = true;


        }

            
    }

    void FixedUpdate()
    {
        if(!waiting)
            transform.Translate(direction*velocity);
        
        if(velocity <Speed)
            velocity+=0.02f;
        else
            velocity = Speed;
    }

    private void ChangeDirection()
    {
        if(Linear)
            direction = -direction;
        else
        {   if(direction.x ==0)
                direction = new Vector2(direction.y,direction.x);
            else
                direction = -new Vector2(direction.y,direction.x);
        }
        Invoke("SetWaiting",1f);
    }

    private void SetWaiting()
    {
        waiting = false;
        velocity=0;
    }

    private void DestroyOB()
    {
        Destroy(player);
    }

}
