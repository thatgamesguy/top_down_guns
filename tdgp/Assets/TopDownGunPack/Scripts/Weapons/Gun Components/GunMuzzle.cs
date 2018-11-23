using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Handles gun muzzle flash.
/// </summary>
	[RequireComponent (typeof(Animator))]
	public class GunMuzzle : GunComponent
	{
		/// <summary>
		/// The flash animation speed.
		/// </summary>
		public float FlashAnimationSpeed = 1f;

		private Animator animator;

		void Awake ()
		{
			animator = GetComponent<Animator> ();
			animator.speed = FlashAnimationSpeed;

			gameObject.SetActive (false);
		}

		/// <summary>
		/// Raised when weapon picked up.
		/// </summary>
		public override void OnPickup ()
		{

		}

		/// <summary>
		/// Raised when weapon dropped. Disables gameobject.
		/// </summary>
		public override void OnDrop ()
		{
			gameObject.SetActive (false);
		}
	}
}
