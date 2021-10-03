using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreStuff;

public class Surface : MonoBehaviour
{
    [SerializeField] private float drag;
    [SerializeField] private float endDrag;

    private Core core;

    private void OnCollisionEnter2D(Collision2D other) {
        core = other.gameObject.GetComponentInChildren<Core>();


        core.Movement.SetDrag(drag);
        core.Movement.SetEndDrag(endDrag);

    }

    private void OnCollisionExit2D(Collision2D other) {
        core.Movement.ResetDrag();
    }
}
