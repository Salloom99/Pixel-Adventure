using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable,IBreakable
{

    private int Hits = 0;

    [SerializeField] private int maxHits = 1;
    [SerializeField] private int friutsNum = 3;

    [SerializeField] private GameObject[] parts;
    [SerializeField] private FruitsData fruitsData;
    [SerializeField] private TrapsData trapsData;
    private List<GameObject> summonedFruits;


    protected override void OnCollisionEnter2D(Collision2D other)
    {   
        if(other.gameObject.CompareTag("Player"))
        {
            playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        }
            
        base.OnCollisionEnter2D(other);
    }

    public void Break()
    {
        GameObject[] fruits = fruitsData.fruits;

        summonedFruits = new List<GameObject>();
        for (int i = 0; i < friutsNum; i++)
        {
            GameObject fruit = Instantiate(fruits[Random.Range(0,8)],transform.position,Quaternion.identity,transform.parent.transform);
            fruit.GetComponent<Fruit>().SetIstantiated();
            summonedFruits.Add(fruit);
        }
        this.gameObject.SetActive(false);
        this.gameObject.transform.parent.GetChild(0).gameObject.SetActive(true);

        foreach(GameObject part in parts)
        {
            Rigidbody2D PartRB = part.GetComponent<Rigidbody2D>();
            Vector2 direction = (this.transform.localPosition-part.transform.localPosition).normalized;
            PartRB.velocity = direction*trapsData.explosionVelocity;
        }
        for(int i=0;i<summonedFruits.Count;i++)
            summonedFruits[i].GetComponent<Rigidbody2D>().velocity =Vector2.right*Random.Range(-1,1)*Random.Range(5,20);
    
    }

    public override void Interact(Collision2D other)
    {
        shake = true;
        base.Interact(other);
        
        Hits++;
        GetComponent<Animator>().Play("Hit");

        if(IfBelow(other.transform.position))
            playerRB.velocity = new Vector2(playerRB.velocity.x,0);
        else if (IfAbove(other.transform.position))
            playerRB.velocity = new Vector2(playerRB.velocity.x,trapsData.bounceVelocity);

        if(Hits >= maxHits)
            Invoke("Break",0.2f);
    }

    public override bool CheckInteraction(Vector2 interactor)
    {
        return/*  Mathf.Abs(playerRB.velocity.y) > 5f && */(IfAbove(interactor) || IfBelow(interactor));
    }
}
