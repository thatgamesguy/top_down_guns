using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TDGP
{
/// <summary>
/// Abstract base class for all gun triggers. Responsibility: Handling player input.
/// </summary>
	public abstract class GunTrigger : GunComponent
	{
		public enum TRIGGER_KEY_MAPPING
		{
			PRIMARY,
			SECONDARY
		}

		/// <summary>
		/// The key mapping. Primary = right mouse click, secondary = left mouse click.
		/// </summary>
		public TRIGGER_KEY_MAPPING KeyMapping;

		public enum SHOOT_TYPE
		{
			CLICK,
			HOLD
		}

		/// <summary>
		/// The shoot type i.e. click (the player has to keep clicking to shoot) or hold (the player 
		/// can hold the mouse button down to continue to fire)
		/// </summary>
		public SHOOT_TYPE ShootType;

		/// <summary>
		/// The delay between projectiles.
		/// </summary>
		public float DelayBetweenProjectiles = 0.1f;

		protected string buttonMapping;
		protected bool inUse = false;
		protected List<GunBarrel> barrels;

		/// <summary>
		/// Sets button mapping and retrieves list of barrels attached to gun.
		/// </summary>
		public virtual void Awake ()
		{
			buttonMapping = (KeyMapping == TRIGGER_KEY_MAPPING.PRIMARY) ? "ShootMain" : "ShootSecondary";

			GetBarrels ();
		}

		/// <summary>
		/// Handles the click input.
		/// </summary>
		public abstract void HandleClickInput ();

		/// <summary>
		/// Handles the hold input.
		/// </summary>
		public abstract void HandleHoldInput ();

		/// <summary>
		/// Called by gun component on parent. Sets in use to true i.e. start listening for player input.
		/// </summary>
		public override void OnPickup ()
		{
			inUse = true;
		}

		/// <summary>
		/// Called by gun component on parent. Sets in use to false i.e. stop listening for player input.
		/// </summary>
		public override void OnDrop ()
		{
			inUse = false;
		}

		private void GetBarrels ()
		{

			barrels = new List<GunBarrel> ();

			foreach (Transform sibling in transform.parent) {
				if (sibling.CompareTag ("GunBarrel")) {
					var barrel = sibling.GetComponent<GunBarrel> ();

					if (!barrel) {
						Debug.LogError ("Barrel objects should have GunBarrel script attached");
					} else {
						barrels.Add (barrel);
					}
				}
			}

			if (barrels.Count == 0) {
				Debug.LogError ("Weapon requires at least one barrel with tag 'GunBarrel'");
			} 

	
		}
	}
}
