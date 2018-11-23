using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Missile explosion. Applys knockback force to enemy on explosion.
/// </summary>
	[RequireComponent (typeof(Collider2D))]
	public class MissileExplosion : MonoBehaviour
	{
		public float PushBackForce = 10f;

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.CompareTag ("Enemy")) {
				var otherRigidbody = other.GetComponent<Rigidbody2D> ();
			
				if (otherRigidbody) {
					var heading = other.transform.position - transform.position;
					var distance = heading.magnitude;
					var dir = heading / distance;
				
					otherRigidbody.AddForce (dir * PushBackForce);
				
				}
			}
		}
	}
}
