using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatform : MonoBehaviour
{
    private bool goingToP1;
    private bool goingToP2;

    private bool waiting;

    private Animator Anim;

    [SerializeField] private Vector2 point1;
    [SerializeField] private Vector2 point2;

    [SerializeField] private TrapsData data;

    private void Awake() 
    {
        Anim = GetComponent<Animator>();
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
        if(goingToP1 && (transform.localPosition.y > point1.y || transform.localPosition.x > point1.x))
        {
            transform.Translate(direction(point2,point1)*data.platformSpeed);
            Anim.SetBool("movingToP1",true);
            Anim.SetBool("movingToP2",false);
            goingToP2 = false;
        }
            
        if(goingToP2 && (transform.localPosition.y < point2.y || transform.localPosition.x < point2.x))
        {
            transform.Translate(direction(point1,point2)*data.platformSpeed);
            Anim.SetBool("movingToP2",true);
            Anim.SetBool("movingToP1",false);
            goingToP1 = false;
        }
        if(transform.localPosition.y >= point1.y && transform.localPosition.x <= point1.x)
        {
            if(!waiting)
                {
                    Anim.SetBool("movingToP2",false);
                    Anim.SetBool("movingToP1",false);
                    waiting = true;
                    Invoke("SetGoingToP2",data.platformWaitTime);
                }
        }
        if(transform.localPosition.y <= point2.y && transform.localPosition.x >= point2.x)
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

    private Vector2 direction(Vector2 point1,Vector2 point2)
    {
        return (point2 - point1).normalized;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(transform.parent.position+(Vector3)point2,GetComponent<Collider2D>().bounds.size);
    }
}
