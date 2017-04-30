using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PocketRockets.Levels
{
    /// <summary>
    /// Delegate used in the event that the current goal has been updated
    /// </summary>
    /// <param name="goalCollectionState">The current goal collection</param>
    public delegate void CurrentGoalUpdatedEventHandler(IGoalCollection goalCollectionState);

    /// <summary>
    /// Interface used when defining a class representing a 
    /// collection of goals required to a achieve a winning state of play.
    /// </summary>
    public interface IGoalCollection
    {
        /// <summary>
        /// Gets the currently active goal
        /// </summary>
        /// <returns>The currently active goal.</returns>
        ILevelGoal GetCurrentGoal();

        /// <summary>
        /// Event to be fired when the current goal is updated
        /// </summary>
        event CurrentGoalUpdatedEventHandler CurrentGoalUpdated;

        /// <summary>
        /// Method to retrieve the list of goals that have been met in this stage.
        /// </summary>
        /// <returns>A list of <see cref="ILevelGoal"/></returns>
        List<GameObject> GetTargetsMet();

        /// <summary>
        /// Method to retrieve the list of goals that have not been met in this stage.
        /// </summary>
        /// <returns>A list of <see cref="ILevelGoal"/></returns>
        List<GameObject> GetTargetsPending();

        /// <summary>
        /// Gets a value indicating whether or not the minimun standards
        /// for victory have been met.
        /// </summary>
        /// <returns>True if the collection is in a winning state, false otherwise.</returns>
        bool MinimumStandardsMet();

        /// <summary>
        /// A method to be called when the minimum standards for victory are met
        /// </summary>
        void OnMinimumStandardsMet();
    }
}
