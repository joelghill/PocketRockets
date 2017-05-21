using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets.Entities.Movement
{
    public class ThrustController : MonoBehaviour
    {
        private Rigidbody2D rocketBody;

        // Use this for initialization
        void Start()
        {
            this.rocketBody = this.transform.gameObject.GetComponent<Rigidbody2D>();
        }

        public float Thrust = 30;

        public EngineInput KeyPress;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(this.GetKeyCode()) && this.rocketBody != null)
            {
                this.rocketBody.AddForce(transform.up * this.Thrust);
                return;
            }
        }

        public enum EngineInput
        {
            LeftInput,
            RightInput
        }

        private KeyCode GetKeyCode()
        {
            if (this.KeyPress == EngineInput.LeftInput)
            {
                return KeyCode.LeftArrow;
            }

            if (this.KeyPress == EngineInput.RightInput)
            {
                return KeyCode.RightArrow;
            }

            return KeyCode.None;
        }
    }
}
