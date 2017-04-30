using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets.Levels
{
    /// <summary>
    /// Delegate to be used in the event that a target has been reached by the player
    /// </summary>
    /// <param name="target">The target sought after by the player</param>
    /// <param name="player">The current player</param>
    public delegate void TargetReachedEventHandler(GameObject target, GameObject player);

    /// <summary>
    /// Interface to be used by game objects serving as goals for the player to reach
    /// </summary>
    public interface ILevelGoal
    {
        /// <summary>
        /// Event fired when the goal has been met by the player
        /// </summary>
        event TargetReachedEventHandler TargetReached;

        /// <summary>
        /// A method to be called when the goal has been reached by the player.
        /// </summary>
        void OnTargetReached();

        /// <summary>
        /// A method used to check whether or not the current goal has been met.
        /// </summary>
        /// <returns>True if the target has been reached, false otherwise</returns>
        bool IsTargetReached();

        /// <summary>
        /// Sets the current state of the goal
        /// </summary>
        /// <param name="isTargetReached">A value indicating whether or not the target has been reached</param>
        void SetTargetState(bool isTargetReached);
    }
}

