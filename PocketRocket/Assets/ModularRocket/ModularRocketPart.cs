using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets.ModularRocket
{
    /// <summary>
    /// A delegate method to be used in the parts changed event.
    /// </summary>
    /// <param name="partsRemoved">A list of parts removed from the current part.</param>
    /// <param name="partsAdded">A list of parts added to the current part.</param>
    public delegate void PartsListChangedEventHandler(List<ModularRocketPart> partsRemoved, List<ModularRocketPart> partsAdded);

    /// <summary>
    /// A class representing a single part added to a vehicle
    /// </summary>
    public class ModularRocketPart : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The list of connected parts to this part.
        /// </summary>
        public List<ModularRocketPart> RocketParts;

        /// <summary>
        /// A property indicating whether or not this is the root part of the vehicle
        /// </summary>
        public bool IsRootRocketPart = false;

        /// <summary>
        /// A referenc to the root rocket part
        /// </summary>
        public ModularRocketPart RootPart;

        /// <summary>
        /// Event that occurs when parts are added to or removed from this rocket part.
        /// </summary>
        public event PartsListChangedEventHandler PartListChanged;

        #endregion

        #region Private fields

        /// <summary>
        /// Field indicating whether or not this part has been initialized
        /// </summary>
        private bool isInitialized = false;

        /// <summary>
        /// A field indicating whether or not this rocket part is currently enabled.
        /// </summary>
        private bool isEnabled = true;

        /// <summary>
        /// The private event handler for when parts are added or removed.
        /// </summary>
        private PartsListChangedEventHandler partsChangedHandler;

        #endregion

        // Use this for initialization
        void Start()
        {
            if (this.IsRootRocketPart)
            {
                this.RootPart = this;
                this.InitializeParts();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Returns a boolean indicating whether or not the part is currently enabled.
        /// </summary>
        /// <returns>True if enabled, false otherwise</returns>
        public bool IsEnabled()
        {
            return this.isEnabled;
        }

        /// <summary>
        /// Sets the current enabled state
        /// </summary>
        /// <param name="enabled">True if enabled, false otherwise</param>
        public void SetEnabledState(bool enabled)
        {
            this.isEnabled = enabled;
            foreach(var part in this.RocketParts)
            {
                part.SetEnabledState(enabled);
            }
        }

        /// <summary>
        /// Gets the attached parts to this part. Initializes list of parts if has not been initialized.
        /// </summary>
        /// <returns></returns>
        public List<ModularRocketPart> GetAttachedModularParts()
        {
            if (this.isInitialized == false)
            {
                this.InitializeParts();
                this.isInitialized = true;
            }

            return this.RocketParts;
        }

        /// <summary>
        /// Event handler for when a 2D joint breaks
        /// </summary>
        /// <param name="joint">The broken joint.</param>
        public void OnJointBreak2D(Joint2D joint)
        {
            var connectedParts = this.GetPartsConnectedToJoint(joint);
            this.RemoveParts(connectedParts);
        }

        /// <summary>
        /// Initialization for the current part.
        /// </summary>
        public void InitializeParts()
        {
            this.partsChangedHandler = new PartsListChangedEventHandler(this.UpdatePartsList);
            this.RocketParts = new List<ModularRocketPart>();

            var joints = this.GetComponents<FixedJoint2D>();
            foreach (var joint in joints)
            {
                var parts = this.GetPartsConnectedToJoint(joint);
                this.AddParts(parts);
            }
        }

        /// <summary>
        /// A Method used to add a list of <see cref="ModularRocketPart"/> to the current part.
        /// </summary>
        /// <param name="parts">The parts to add</param>
        public void AddParts(List<ModularRocketPart> parts)
        {
            var partsAdded = new List<ModularRocketPart>();
            foreach (var part in parts)
            {
                if (this.RocketParts.Contains(part) == false)
                {
                    part.RootPart = this.RootPart;
                    this.RocketParts.Add(part);

                    if (part.IsEnabled() == false)
                    {
                        part.SetEnabledState(true);
                    }

                    if (part.IsRootRocketPart == true)
                    {
                        part.IsRootRocketPart = false;
                    }

                    partsAdded.Add(part);
                    partsAdded.AddRange(part.GetAttachedModularParts());
                    part.PartListChanged += this.partsChangedHandler;
                }
            }

            if (this.PartListChanged != null)
            {
                this.PartListChanged(new List<ModularRocketPart>(), partsAdded);
            }
        }

        /// <summary>
        /// A method used to remove a list of parts from the current part.
        /// </summary>
        /// <param name="parts">The parts to remove</param>
        public void RemoveParts(List<ModularRocketPart> parts)
        {
            var partsRemoved = new List<ModularRocketPart>();
            foreach (var part in parts)
            {
                if (this.RocketParts.Contains(part) == true)
                {
                    this.RocketParts.Remove(part);

                    if (part.IsEnabled() == true)
                    {
                        part.SetEnabledState(false);
                    }

                    partsRemoved.Add(part);
                    partsRemoved.AddRange(part.GetAttachedModularParts());
                    part.PartListChanged -= this.partsChangedHandler;
                }
            }

            if (this.PartListChanged != null)
            {
                this.PartListChanged(partsRemoved, new List<ModularRocketPart>());
            }

        }

        /// <summary>
        /// Adds parts to be added, removes those to be removed
        /// </summary>
        /// <param name="partsRemoved">The parts to be removed</param>
        /// <param name="partsAdded">The parts to be added</param>
        private void UpdatePartsList(List<ModularRocketPart> partsRemoved, List<ModularRocketPart> partsAdded)
        {
            foreach (var removed in partsRemoved)
            {
                if (this.RocketParts.Contains(removed))
                {
                    this.RocketParts.Remove(removed);
                }
            }

            this.RocketParts.AddRange(partsAdded);

            if (this.PartListChanged != null)
            {
                this.PartListChanged(partsRemoved, partsAdded);
            }

        }

        /// <summary>
        /// Given a 2D joint, retreives the modular rocket parts connected to that joint.
        /// </summary>
        /// <param name="joint">The 2D joint</param>
        /// <returns>A list of <see cref="ModularRocketPart"/></returns>
        private List<ModularRocketPart> GetPartsConnectedToJoint(Joint2D joint)
        {
            var connectedParts = new List<ModularRocketPart>();

            var part = joint.connectedBody.gameObject.GetComponent<ModularRocketPart>();
            if (part != null)
            {
                connectedParts.AddRange(part.GetAttachedModularParts());
                connectedParts.Add(part);
            }

            return connectedParts;
        }
        
 
    }

}
