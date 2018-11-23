using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Provides burst firing.
/// </summary>
	public class GunTriggerBurst : GunTrigger
	{
		/// <summary>
		/// The time between bursts of projectiles.
		/// </summary>
		public float TimeBetweenBursts = 0.3f;

		/// <summary>
		/// The bullets per burst.
		/// </summary>
		public int BulletsPerBurst = 3;

		private bool firingBurst;
	
		public override void Awake ()
		{
			firingBurst = false;
			base.Awake ();
		}

		void Update ()
		{
			if (!inUse)
				return;
		
			if (ShootType == SHOOT_TYPE.CLICK) {
				HandleClickInput ();
			} else {
				HandleHoldInput ();
			}
		}

		/// <summary>
		/// If not already firing burst, a burst of projectiles will be fired.
		/// </summary>
		public override void HandleClickInput ()
		{
			if (Input.GetButtonDown (buttonMapping) && !firingBurst) {
				StartCoroutine (BurstFire ());
			}
		}

		/// <summary>
		/// If not already firing burst, a burst of projectiles will be fired.
		/// </summary>
		public override void HandleHoldInput ()
		{
			if (Input.GetButton (buttonMapping) && !firingBurst) {
				StartCoroutine (BurstFire ());
			}
		}

		private IEnumerator BurstFire ()
		{
			firingBurst = true;

			for (int i = 0; i < BulletsPerBurst; i++) {

				foreach (var barrel in barrels) {
					barrel.OnFire ();
				}
			
				yield return new WaitForSeconds (DelayBetweenProjectiles);
			}
		
			yield return new WaitForSeconds (TimeBetweenBursts);
		
			firingBurst = false;
		}
	}
}
