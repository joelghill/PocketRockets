using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PocketRockets.Levels
{
    public class LandingPadCollection : MonoBehaviour, IGoalCollection
    {
        public List<GameObject> LevelGoals;
        public GameObject TargetToReach;

        public event CurrentGoalUpdatedEventHandler CurrentGoalUpdated;

        private int currentIndex;
        private List<GameObject> targetsReached;
        private List<GameObject> targetsRemaining;


        public ILevelGoal GetCurrentGoal()
        {
            if(this.LevelGoals != null && this.currentIndex < this.LevelGoals.Count())
            {
                var currentObject = this.LevelGoals[this.currentIndex];
                return currentObject.GetComponent<ILevelGoal>();
            }
            else
            {
                return null;
            }
        }

        public List<GameObject> GetTargetsMet()
        {
            return this.targetsReached;
        }

        public List<GameObject> GetTargetsPending()
        {
            return this.targetsRemaining;
        }

        public bool MinimumStandardsMet()
        {
            return this.GetCurrentGoal().Equals(this.TargetToReach);
        }

        public void OnMinimumStandardsMet()
        {
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {
            this.currentIndex = 0;

            if(this.TargetToReach == null)
            {
                this.TargetToReach = this.LevelGoals.LastOrDefault();
            }

            foreach(var goalObject in this.LevelGoals)
            {
                var goal = goalObject.GetComponent<ILevelGoal>();
                goal.TargetReached += this.Goal_TargetReached;
            }

            this.targetsRemaining = new List<GameObject>(this.LevelGoals);
            this.targetsReached = new List<GameObject>();
        }

        private void Goal_TargetReached(GameObject target, GameObject player)
        {
            var goal = target.GetComponent<ILevelGoal>();
            if(goal != null && this.GetCurrentGoal().Equals(goal))
            {
                goal.OnTargetReached();
                this.targetsRemaining.Remove(target);
                this.targetsReached.Add(target);
                goal.TargetReached -= this.Goal_TargetReached;
                if(this.currentIndex < this.LevelGoals.Count)
                {
                    this.currentIndex++;
                    if (this.CurrentGoalUpdated != null)
                    {
                        this.CurrentGoalUpdated(this);
                    }
                }
            }
    }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

