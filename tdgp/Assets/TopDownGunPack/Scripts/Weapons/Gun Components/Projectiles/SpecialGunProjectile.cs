using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Projectile of special type i.e. ice or chain lightning.
/// </summary>
	public class SpecialGunProjectile : GunProjectile
	{
		/// <summary>
		/// The damage per second.
		/// </summary>
		public float DamagePerSecond = 0.5f;

		/// <summary>
		/// The damage time.
		/// </summary>
		public float DamageTime = 1f;

		/// <summary>
		/// The type of damage to apply.
		/// </summary>
		public SPECIAL_DAMAGE_TYPE DamageType;

		/// <summary>
		/// Returns projectile if it hits wall. Applies special damage if hits enemy.
		/// </summary>
		/// <param name="other">Other.</param>
		public override void OnTriggerEnter2D (Collider2D other)
		{
			base.OnTriggerEnter2D (other);
			
			if (other.CompareTag ("Enemy")) {
				var damageController = other.GetComponent<DamageAnimationController> ();
			
				if (!damageController) {
					Debug.LogError ("Enemy should have DamageAnimationController script to apply special damage");
				} else {
					damageController.ApplyDamage (DamageType, DamagePerSecond, DamageTime);
				}
			
				ReturnProjectile ();
			}
		}
	}
}