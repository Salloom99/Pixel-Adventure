using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreStuff;

namespace FiniteStateMachine
{
    public abstract class State
    {
        protected Core core;

        protected Movement movement {get => core.Movement;}
        protected CollisionSenses collisionSenses {get => core.CollisionSenses;}

        protected Entity entity;
        protected FSM stateMachine;
        protected Data data;

        protected bool isAnimationFinished;
        protected bool isExitingState;

        protected float startTime;

        private string animBoolName;

        protected State(Entity entity,string animBoolName)
        {
            this.core = entity.Core;
            this.entity = entity;
            this.stateMachine = entity.StateMachine;
            this.data = entity.Data;
            this.animBoolName = animBoolName;
        }

        public string AnimBoolName { get => animBoolName; set => animBoolName = value; }

        public virtual void Enter()
        {
            DoChecks();
            entity.Anim.SetBool(animBoolName,true);
            startTime = Time.time;
            // Debug.Log(animBoolName);
            isAnimationFinished = false;
            isExitingState = false;
        }

        public virtual void Exit()
        {
            entity.Anim.SetBool(animBoolName,false);
            isExitingState = true;
        } 

        public virtual void LogicUpdate() {}

        public virtual void PhysicsUpdate() => DoChecks();

        public virtual void DoChecks(){}

        public virtual void AnimationTrigger() {}

        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    }
}


