using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PocketRockets.Entities.Interaction
{
    /// <summary>
    /// Base class to manage damage taken to an object
    /// </summary>
    public abstract class BaseDestructableEntity : MonoBehaviour
    {
        /// <summary>
        /// The current health of the object
        /// </summary>
        public int CurrentHealth;

        /// <summary>
        /// The maximum health an object can have
        /// </summary>
        public int MaxHealth;

        /// <summary>
        /// Apply a set amount of damage to an entity
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage)
        {
            this.CurrentHealth += damage;

            if(this.CurrentHealth > this.MaxHealth)
            {
                this.CurrentHealth = this.MaxHealth;
            }

            if(this.CurrentHealth < 0)
            {
                this.CurrentHealth = 0;
            }

            if(this.CurrentHealth == 0)
            {
                this.Destroy();
            }
        }

        /// <summary>
        /// Action to take when an entity has reached 0 health
        /// </summary>
        public abstract void Destroy();
    }
}
