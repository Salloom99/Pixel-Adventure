using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreStuff;

public class Platform : Interactable
{

    private bool goingToP1;
    private Rigidbody2D PlatformRB;
    private Animator Anim;

    private Transform point1;
    private Transform point2;

    [SerializeField] private TrapsData data;

    protected override void Awake() 
    {
        base.Awake();
        PlatformRB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        point1 = transform.parent.GetChild(0);
        point2 = transform.parent.GetChild(1);
    }

    public override void Interact(Collision2D other)
    {
        shake =true;
        base.Interact(other);
        goingToP1 = true;
        other.transform.parent = transform;
    }

    public override bool CheckInteraction(Vector2 interactor)
    {
        return interactor.y >transform.position.y;
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        goingToP1=false;
        other.transform.parent = null;

    }

    private void FixedUpdate() 
    {
        
        if(goingToP1 && (transform.localPosition.y < point1.localPosition.y || transform.localPosition.x > point1.localPosition.x))
        {
            //PlatformRB.velocity = direction(point2,point1)*data.platformSpeed;
            transform.Translate(direction(point2,point1)*data.platformSpeed);
            Anim.SetBool("movingToP1",true);
            Anim.SetBool("movingToP2",false);
        }
            
        else if(!goingToP1 && (transform.localPosition.y > point2.localPosition.y || transform.localPosition.x < point2.localPosition.x))
        {
            //PlatformRB.velocity = direction(point1,point2)*data.platformSpeed;
            transform.Translate(direction(point1,point2)*data.platformSpeed);
            Anim.SetBool("movingToP2",true);
            Anim.SetBool("movingToP1",false);
        }
        else
        {
            //PlatformRB.velocity = Vector2.zero;
            Anim.SetBool("movingToP1",false);
            Anim.SetBool("movingToP2",false);
        }
    }

    private Vector2 direction(Transform point1,Transform point2)
    {
        return new Vector2(point2.localPosition.x-point1.localPosition.x,point2.localPosition.y-point1.localPosition.y).normalized;
    }


}
