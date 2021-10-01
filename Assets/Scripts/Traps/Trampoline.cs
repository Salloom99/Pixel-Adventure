using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private TrapsData data;

    private CameraShake cameraShake;
    private CoreStuff.Core core;

    private void Awake() {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GetComponent<Animator>().Play("Jump");
        StartCoroutine(cameraShake.Shake());
        core = other.GetComponentInChildren<CoreStuff.Core>();
        core.Movement.AddVelocityUp(data.trampolineJumpForce);
    }
   



    

}
