using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// A standard gun projectile.
/// </summary>
	public class StandardGunProjectile : GunProjectile
	{
		/// <summary>
		/// Damage on projectile hit.
		/// </summary>
		public float Damage;

		/// <summary>
		/// The damage animation prefab.
		/// </summary>
		public GameObject DamageAnimation;

		public override void Awake ()
		{
			if (!DamageAnimation) {
				Debug.Log ("No damage animation selected for projectile: no animation will be played on impact");
			}
		
			base.Awake ();
		}

		/// <summary>
		/// Returns projectile if it hits wall. Applys damage if hits enemy.
		/// </summary>
		/// <param name="other">Other.</param>
		public override void OnTriggerEnter2D (Collider2D other)
		{
			base.OnTriggerEnter2D (other);
		
			if (other.CompareTag ("Enemy")) {
				ApplyDamage (other, Damage);
			
				if (DamageAnimation)
					InitDamageAnimation (other, DamageAnimation);
			
				if (DestroyOnEnemyImpact)
					ReturnProjectile ();
			} 
		}
	}
}
