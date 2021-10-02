using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable,IBreakable
{

    protected Breakable breakable;

    [SerializeField] protected FruitsData fruitsData;
    [SerializeField] private TrapsData trapsData;

    protected int Hits;
    protected int friutsNum;
   

    private Transform[] parts;
    private List<GameObject> summonedFruits;

    protected override void Awake() {
        breakable = new Breakable(transform);
        base.Awake();
        breakable.Awake();

        parts = breakable.parts;
        breakable.trapsData = trapsData;
    }


    protected override void OnCollisionEnter2D(Collision2D other)
    {   
        if(other.gameObject.CompareTag("Player"))
        {
            playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        }
            
        base.OnCollisionEnter2D(other);
    }

    public virtual void Break()
    {
        breakable.Break();
        ThrowFruits();
    
    }

    public override void Interact(Collision2D other)
    {
        shake = true;
        base.Interact(other);
        
        Hits--;
        GetComponent<Animator>().Play("Hit");

        if(IfBelow(other.transform.position))
            playerRB.velocity = new Vector2(playerRB.velocity.x,0);
        else if (IfAbove(other.transform.position))
            playerRB.velocity = new Vector2(playerRB.velocity.x,trapsData.bounceVelocity);

        if(Hits <= 0)
            Invoke("Break",0.2f);
    }

    private void ThrowFruits()
    {
        summonedFruits = new List<GameObject>();
        for (int i = 0; i < friutsNum; i++)
        {
            GameObject fruit = Instantiate(fruitsData.fruits[Random.Range(0,8)],transform.position,Quaternion.identity,transform.parent.transform);
            fruit.GetComponent<Fruit>().SetIstantiated();
            summonedFruits.Add(fruit);
        }
        for(int i=0;i<summonedFruits.Count;i++)
            summonedFruits[i].GetComponent<Rigidbody2D>().velocity =Vector2.right*Random.Range(-1,1)*Random.Range(5,20);
    }
}
