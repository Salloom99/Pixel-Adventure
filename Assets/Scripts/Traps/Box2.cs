using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2 : Box
{
    
    protected override void Awake()
    {
        Hits = 5;
        friutsNum = 5;
        base.Awake();

    }

    public override void Interact(Collision2D other)
    {
        base.Interact(other);

        ThrowFruit();
    }

    public override void Break()
    {
        breakable.Break();
    }

    private void ThrowFruit()
    {
        GameObject[] fruits = fruitsData.fruits;
        GameObject fruit = Instantiate(fruits[Random.Range(0,8)],new Vector3(transform.position.x+Random.Range(-.5f,.5f),transform.position.y),Quaternion.identity,transform.parent.transform);
        fruit.GetComponent<Fruit>().SetIstantiated();
        fruit.GetComponent<Rigidbody2D>().velocity =Vector2.right*Random.Range(-1f,1f)*Random.Range(10,20);
    }
    
}
