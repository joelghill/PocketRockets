using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets.Levels
{
    public class BaseLevelGoalList : MonoBehaviour, IGoalCollection
    {
        private ILevelGoal currentGoal;

        public event CurrentGoalUpdatedEventHandler CurrentGoalUpdated;

        public List<ILevelGoal> Targets;

        public ILevelGoal GetCurrentGoal()
        {
            return this.currentGoal;
        }

        public List<ILevelGoal> GetTargetsMet()
        {
            throw new NotImplementedException();
        }

        public List<ILevelGoal> GetTargetsPending()
        {
            throw new NotImplementedException();
        }

        public bool MinimumStandardsMet()
        {
            throw new NotImplementedException();
        }

        public void OnMinimumStandardsMet()
        {
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        List<GameObject> IGoalCollection.GetTargetsMet()
        {
            throw new NotImplementedException();
        }

        List<GameObject> IGoalCollection.GetTargetsPending()
        {
            throw new NotImplementedException();
        }
    }
}

