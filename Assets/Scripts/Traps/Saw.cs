using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Spikes
{
    [SerializeField] private Vector2 point1;
    [SerializeField] private Vector2 point2;
    public float speed;

    private void FixedUpdate() 
    {
        transform.localPosition = Vector2.Lerp(point1,point2,Mathf.Sin(Time.time*speed )*.5f+.5f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(point1+(Vector2)transform.parent.position,point2+(Vector2)transform.parent.position);
    }
}
