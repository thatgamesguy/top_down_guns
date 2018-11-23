using UnityEngine;
using System.Collections;

namespace TDGP
{
    /// <summary>
    /// The gun type.
    /// </summary>
    public enum Gun_Type
    {
        ONE_HANDED,
        TWO_HANDED,
        DUAL_WIELD
    }

    /// <summary>
    /// Calls all child GunComponents OnPickup and OnDrop methods when gun is picked up and dropped respectively.
    /// Stores the guns type i.e. one handed, two handed, dual wield. This is used by the Holster script to update
    /// the players sprite.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Gun : MonoBehaviour
    {
        /// <summary>
        /// The type of the gun.
        /// </summary>
        public Gun_Type GunType;

        private GunComponent[] gunComponents;
        private Collider2D _collider2D;

        private static readonly float ON_DROP_PICKUP_DELAY = 1.5f;

        void Awake()
        {
            gunComponents = GetComponentsInChildren<GunComponent>();
            _collider2D = GetComponent<Collider2D>();
        }

        /// <summary>
        /// Calls OnPickup in all child componenets.
        /// </summary>
        public void OnPickup()
        {
            _collider2D.enabled = false;

            foreach (var component in gunComponents)
            {
                component.OnPickup();
            }
        }

        /// <summary>
        /// Calls OnDrop in all child componenets.
        /// </summary>
        public void OnDrop()
        {
            _collider2D.enabled = false;

            Invoke("EnableCollider", ON_DROP_PICKUP_DELAY);

            foreach (var component in gunComponents)
            {
                component.OnDrop();
            }
        }

        private void EnableCollider()
        {
            _collider2D.enabled = true;
        }
    }
}
