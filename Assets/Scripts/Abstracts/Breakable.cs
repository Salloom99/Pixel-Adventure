using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : IBreakable
{
    public Transform self;
    public Transform[] parts;
    public TrapsData trapsData;

    public Breakable(Transform transform) => self = transform;

    public virtual void Break()
    {
        self.gameObject.SetActive(false);
        self.gameObject.transform.parent.GetChild(0).gameObject.SetActive(true);

        foreach(Transform part in parts)
        {
            Rigidbody2D PartRB = part.GetComponent<Rigidbody2D>();
            Vector2 direction = (self.localPosition-part.transform.localPosition).normalized;
            PartRB.velocity = direction*trapsData.explosionVelocity;
        }
    }

    public virtual void Awake() 
    {
        parts = new Transform[self.parent.GetChild(0).childCount];
        for (int i = 0; i < parts.Length; i++)
            parts[i] = self.parent.GetChild(0).GetChild(i);
        
    }

    
}
