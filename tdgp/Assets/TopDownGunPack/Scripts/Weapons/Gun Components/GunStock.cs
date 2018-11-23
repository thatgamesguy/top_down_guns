using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Responsibility: Providing a knock back force when weapon fired.
/// </summary>
	public class GunStock : GunComponent
	{
		/// <summary>
		/// The knock back force applied to the player on weapon fire.
		/// </summary>
		public float KnockBackForce = 50f;

		/// <summary>
		/// Raised when weapon picked up.
		/// </summary>
		public override void OnPickup ()
		{

		}

		/// <summary>
		/// Raised when weapon dropped.
		/// </summary>
		public override void OnDrop ()
		{

		}
	}
}
