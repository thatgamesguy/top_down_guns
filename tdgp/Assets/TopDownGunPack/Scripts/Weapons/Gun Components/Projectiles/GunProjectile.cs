using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// The abstract base class for all gun projectiles. Responsible for handling collision, applying damage,
/// and pooling projectile when max time alive has been reached.
/// </summary>
	[RequireComponent (typeof(Rigidbody2D))]
	public abstract class GunProjectile : GunComponent
	{
		/// <summary>
		/// The maximum time projectile can be alive. The projectile is pooled when this time has been reached.
		/// </summary>
		public float MaxTimeAlive = 2f;

		/// <summary>
		/// If true, the projectile will be pooled when a collision with an enemy occurs.
		/// </summary>
		public bool DestroyOnEnemyImpact = true;

		private GunClip owner;

		/// <summary>
		/// The owner of this projectile.
		/// </summary>
		/// <value>The owner.</value>
		public GunClip Owner {
			set {
				owner = value;
			}
		}
	
		private float currentTimeAlive;

		public virtual void Awake ()
		{
			gameObject.SetActive (false);
		}

		void OnEnable ()
		{
			currentTimeAlive = 0f;
		}

		/// <summary>
		/// Returns the projectile when/if maximum tile alive is reached.
		/// </summary>
		public virtual void Update ()
		{
			currentTimeAlive += Time.deltaTime;
			if (currentTimeAlive >= MaxTimeAlive) {
				ReturnProjectile ();
			}

		}

		/// <summary>
		/// Returns projectile if it hits wall.
		/// </summary>
		/// <param name="other">Other.</param>
		public virtual void OnTriggerEnter2D (Collider2D other)
		{
			if (other.CompareTag ("Wall")) {
				ReturnProjectile ();
			}
		}

		protected void ApplyDamage (Collider2D other, float damage)
		{
			var health = other.GetComponent<Health> ();
		
			if (!health) {
				Debug.LogError ("Enemy should have health script attached");
				return;
			}
		
			health.OnHit (damage);
		}

		protected void InitDamageAnimation (Collider2D other, GameObject animation)
		{
			var dir = transform.up.normalized;
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		
			Instantiate (animation, transform.position, Quaternion.AngleAxis (angle, Vector3.forward));
		}

		/// <summary>
		/// Gets the status of the gun in case it has been removed from scene/disabled.
		/// </summary>
		/// <returns><c>true</c>, if owner not null and gun object active.</returns>
		private bool GunActive ()
		{
			return owner != null && owner.gameObject.activeInHierarchy;
		}

		protected void ReturnProjectile ()
		{
			if (GunActive ()) {
				owner.PoolObject (gameObject);
			} else {
				Destroy (gameObject);
			}
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

