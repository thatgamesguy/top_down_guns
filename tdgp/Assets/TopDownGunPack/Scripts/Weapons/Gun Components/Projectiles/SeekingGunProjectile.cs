using UnityEngine;
using System.Collections;

namespace TDGP
{
	/// <summary>
	/// A projectile that seeks towards its target.
	/// </summary>
	[RequireComponent (typeof(Rigidbody2D))]
	public class SeekingGunProjectile : GunProjectile
	{	
		/// <summary>
		/// Damage on collision.
		/// </summary>
		public float Damage = 10f;

		/// <summary>
		/// Projectiles velocity.
		/// </summary>
		public float Velocity = 40f;

		/// <summary>
		/// Animation on collision with enemy.
		/// </summary>
		public GameObject DamageAnimation;

		private Transform target;
		private LineRenderer linerenderer;
		private bool foundTarget = false;
		private Rigidbody2D _rigidbody2D;
		
		public override void Awake ()
		{
			base.Awake ();

			if (!DamageAnimation) {
				Debug.Log ("No damage animation selected for projectile: no animation will be played on impact");
			}

			_rigidbody2D = GetComponent<Rigidbody2D> ();
		}

		
		void OnDisable ()
		{
			foundTarget = false;
		}

		/// <summary>
		/// Returns projectile if it hits wall or enemy. Instantiates damage animation and damages object (if enemy).
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

		/// <summary>
		/// Rotates towards closest target.
		/// </summary>
		public override void Update ()
		{
			base.Update ();
			
			if (foundTarget && target != null) {			
				Vector3 dir = target.position - transform.position;
				float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			} else if (!foundTarget) {
				target = GetNearestObject ("Enemy");
			}
			
			_rigidbody2D.AddForce (transform.up * Velocity);
			
		} 
		
		
		public Vector2 GetForce ()
		{
			return target.position - transform.position;
		}

		private Transform GetNearestObject (string tag)
		{
			var objs = GameObject.FindGameObjectsWithTag (tag);

			Transform closest = null;

			float closestDistance = float.MaxValue;

			foreach (var obj in objs) {

				var heading = obj.transform.position - transform.position;
				var distance = heading.magnitude;

				if (distance < closestDistance && IsTargetInFront (obj.transform)) {
					closestDistance = distance;
					closest = obj.transform;
					foundTarget = true;
				}
			}

			return closest;
		}
		
		private bool IsTargetInFront (Transform target)
		{
			var heading = target.position - transform.position;
		
			var dot = Vector2.Dot (heading, transform.up);

			return dot > 1.2f; 
		}

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
