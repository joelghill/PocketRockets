using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketRockets;
using System;

namespace PocketRockets.ModularRocket
{

    public class ModularRocket2DBounds : MonoBehaviour, IBoundingBox2d {

        public Bounds RocketBounds;

        private ModularRocketPart rocketPart;

        // Use this for initialization
        void Start()
        {
            this.rocketPart = this.gameObject.GetComponent<ModularRocketPart>();
        }

        private void CalculateLocalBounds()
        {
            this.RocketBounds = new Bounds(transform.position, Vector3.one);

            foreach (var rocketPart in this.rocketPart.GetAttachedModularParts())
            {
                var renderer = rocketPart.gameObject.GetComponent<Renderer>();
                this.RocketBounds.Encapsulate(renderer.bounds);
            }
        }

        // Update is called once per frame
        void Update()
        {
            this.CalculateLocalBounds();
        }

        public float GetBottomEdge()
        {
            return this.RocketBounds.center.y - this.RocketBounds.extents.y;
        }

        public float GetLeftEdge()
        {
            return this.RocketBounds.center.x - this.RocketBounds.extents.x;
        }

        public float GetRightEdge()
        {
            return this.RocketBounds.center.x + this.RocketBounds.extents.x;
        }

        public float GetTopEdge()
        {
            return this.RocketBounds.center.y + this.RocketBounds.extents.y;
        }

        public BoundingEdges GetBoundingEdges()
        {
            return new BoundingEdges()
            {
                Left = this.GetLeftEdge(),
                Right = this.GetRightEdge(),
                Top = this.GetTopEdge(),
                Bottom = this.GetBottomEdge()
            };
        }
    }
}
