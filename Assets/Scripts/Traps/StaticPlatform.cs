using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatform : MonoBehaviour
{
    private bool goingToP1;
    private bool goingToP2;

    private bool waiting;

    private Animator Anim;

    private Transform point1;
    private Transform point2;

    [SerializeField] private TrapsData data;

    private void Awake() 
    {
        Anim = GetComponent<Animator>();
        point1 = transform.parent.GetChild(0);
        point2 = transform.parent.GetChild(1);
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.position.y >transform.position.y)
            other.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.transform.parent = null;
    }

    private void FixedUpdate() 
    {
        if(goingToP1 && (transform.localPosition.y > point1.localPosition.y || transform.localPosition.x > point1.localPosition.x))
        {
            transform.Translate(direction(point2,point1)*data.platformSpeed);
            Anim.SetBool("movingToP1",true);
            Anim.SetBool("movingToP2",false);
            goingToP2 = false;
        }
            
        if(goingToP2 && (transform.localPosition.y < point2.localPosition.y || transform.localPosition.x < point2.localPosition.x))
        {
            transform.Translate(direction(point1,point2)*data.platformSpeed);
            Anim.SetBool("movingToP2",true);
            Anim.SetBool("movingToP1",false);
            goingToP1 = false;
        }
        if(transform.localPosition.y >= point1.localPosition.y && transform.localPosition.x <= point1.localPosition.x)
        {
            if(!waiting)
                {
                    Anim.SetBool("movingToP2",false);
                    Anim.SetBool("movingToP1",false);
                    waiting = true;
                    Invoke("SetGoingToP2",data.platformWaitTime);
                }
        }
        if(transform.localPosition.y <= point2.localPosition.y && transform.localPosition.x >= point2.localPosition.x)
        {
            if(!waiting)
                {
                    Anim.SetBool("movingToP2",false);
                    Anim.SetBool("movingToP1",false);
                    waiting = true;
                    Invoke("SetGoingToP1",data.platformWaitTime);
                }
        }
        
    }

    private void SetGoingToP1()
    {
        goingToP1 = true;
        waiting = false;
    }
    private void SetGoingToP2()
    {
        goingToP2 = true;
        waiting = false;
    }

    private Vector2 direction(Transform point1,Transform point2)
    {
        return new Vector2(point2.localPosition.x-point1.localPosition.x,point2.localPosition.y-point1.localPosition.y).normalized;
    }
}
