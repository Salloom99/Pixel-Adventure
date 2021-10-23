using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreStuff;
using FiniteStateMachine;

public abstract class Entity : MonoBehaviour
{
    public FSM StateMachine { get; protected set; }

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; protected set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public new ParticleSystem particleSystem {get; private set;}
    #endregion

    public Data Data;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Anim = GetComponentInChildren<Animator>();
        MovementCollider = GetComponent<BoxCollider2D>();
        RB = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
}
