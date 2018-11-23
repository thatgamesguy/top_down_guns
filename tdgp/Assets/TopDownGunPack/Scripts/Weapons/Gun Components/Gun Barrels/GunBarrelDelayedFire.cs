using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Gun barrel with delayed fire. Currently used by the chain gun to produce erratic firing.
/// </summary>
	public class GunBarrelDelayedFire : GunBarrel
	{
		/// <summary>
		/// The minimum delay between the fire button pressed and shooting a projecile.
		/// </summary>
		public float MinFireDelay = 0.05f;

		/// <summary>
		/// The maximum delay between the fire button pressed and shooting a projecile.
		/// </summary>
		public float MaxFireDelay = 0.15f;

		/// <summary>
		/// Requests bullet from gun clip and if returned, fires bullet based on barrels rotation and min and max fire delay.
		/// </summary>
		public override void OnFire ()
		{
			Invoke ("FireBarrel", Random.Range (MinFireDelay, MaxFireDelay));
		}

		private void FireBarrel ()
		{
			base.OnFire ();
		}
	}
}
