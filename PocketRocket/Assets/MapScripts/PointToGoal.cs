using PocketRockets.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets
{
    public class PointToGoal : MonoBehaviour
    {
        public GameObject Player;
        public GameObject GoalCollectionObject;
        public float MinimumDistance = 2;

        private IBoundingBox2d playerBoundingBox;
        private IGoalCollection goalCollection;

        private Vector3 direction;
        private ILevelGoal currentGoal;

        // Use this for initialization
        void Start()
        {
            this.playerBoundingBox = Player.GetComponent<IBoundingBox2d>();
            this.goalCollection = this.GoalCollectionObject.GetComponent<IGoalCollection>();
        }

        // Update is called once per frame
        void Update()
        {
            this.UpdateCurrentGoal();

            if(this.currentGoal == null)
            {
                this.Dispose();
                return;
            }

            this.UpdateDirection();
            this.UpdatePosition();
            this.UpdateRotation();
        }

        protected virtual void UpdateCurrentGoal()
        {
            this.currentGoal = this.goalCollection.GetCurrentGoal();
        }

        protected virtual void UpdatePosition()
        {
            var playerPos = this.playerBoundingBox.GetPosition();             
            this.transform.position = playerPos + (this.MinimumDistance * this.direction);
        }

        protected virtual void UpdateDirection()
        {
            this.direction = currentGoal.CurrentPosition() - this.Player.transform.position;
            this.direction.Normalize();

        }

        protected virtual void UpdateRotation()
        {
            var angle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        protected virtual void Dispose()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
