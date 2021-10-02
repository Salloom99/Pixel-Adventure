using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    private float spriteBlinkingTimer = 0.0f;
    private float spriteBlinkingMiniDuration = 0.1f;
    private float spriteBlinkingTotalTimer = 0.0f;
    private float spriteBlinkingTotalDuration = 1.0f;

    private Rigidbody2D PartRB;

    private void Start() {
        PartRB = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if(gameObject.activeSelf && PartRB.velocity.x ==0)
        {
            Invoke("DestroyThis",1f);
            Invoke("Blinking",0.1f);
        }
           
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PartRB.freezeRotation = false;   
    }

    private void DestroyThis()
    {
         Destroy(gameObject);
    }

    private void Blinking()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if(spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
             spriteBlinkingTotalTimer = 0.0f;
             this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   // according to 
                      //your sprite
             return;
          }
     
     spriteBlinkingTimer += Time.deltaTime;
     if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
     {
         spriteBlinkingTimer = 0.0f;
         if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
             this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;  //make changes
         } else {
             this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   //make changes
         }
    }
    }
}
