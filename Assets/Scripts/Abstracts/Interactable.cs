using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour,IInteractable
{
    private Collider2D collider2d;
    protected Rigidbody2D playerRB;

    protected CameraShake cameraShake;
    protected bool shake;
    private Vector2 center;

    private float width;
    private float height;

    protected virtual void Awake() 
    {
        collider2d = GetComponent<Collider2D>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    private void Start() 
    {
        width = collider2d.bounds.size.x;
        height = collider2d.bounds.size.y;

        center = new Vector2(collider2d.bounds.center.x,collider2d.bounds.center.y);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && CheckInteraction(other.transform.position))
            Interact(other);
            
    }

    public virtual void Interact(Collision2D other)
    {
        playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        if(shake)
            StartCoroutine(cameraShake.Shake());
    }

    public virtual bool CheckInteraction(Vector2 interactor)
    {
        return IfAbove(interactor)||IfBelow(interactor);
    }

    public bool IfAbove(Vector2 interactor)
    {
        return Mathf.Abs(center.x-interactor.x) <= width && interactor.y-center.y >= height;
    }

    public bool IfBelow(Vector2 interactor)
    {
        return Mathf.Abs(center.x-interactor.x) <= width && center.y-interactor.y >= 0;
    }

    

    
}
