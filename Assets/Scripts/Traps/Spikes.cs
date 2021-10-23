using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    protected CameraShake cameraShake;
    private Player player;

    protected virtual void Awake() 
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    protected void OnCollisionEnter2D(Collision2D other) 
    {
        StartCoroutine(cameraShake.Shake());
        other.gameObject.GetComponent<Player>().Die();

    }

    
}
