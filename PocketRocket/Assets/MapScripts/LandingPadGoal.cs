using PocketRockets.ModularRocket;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets.Levels
{
    public class LandingPadGoal : MonoBehaviour, ILevelGoal
    {
        private bool targetReached;
        private Collider2D collider;

        public Color successColor;

        public event TargetReachedEventHandler TargetReached;

        public bool IsTargetReached()
        {
            return this.targetReached;
        }

        public void OnTargetReached()
        {
            var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = this.successColor;
        }

        public void SetTargetState(bool isTargetReached)
        {
            this.targetReached = isTargetReached;
        }

        // Use this for initialization
        void Start()
        {
            this.collider = this.gameObject.GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ModularRocketPart part = collision.gameObject.GetComponent<ModularRocketPart>();
            if (part != null)
            {
                this.targetReached = true;

                if(this.TargetReached != null)
                {
                    this.TargetReached(this.gameObject, collision.gameObject);
                }
            }
        }

        public Vector3 CurrentPosition()
        {
            var renderer = this.gameObject.GetComponent<Renderer>();
            return renderer.bounds.center;
        }
    }
}

