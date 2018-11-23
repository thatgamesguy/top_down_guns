using UnityEngine;
using System.Collections;

namespace TDGP
{
	/// <summary>
	/// Moves enemy towards player.
	/// </summary>
	[RequireComponent (typeof(Rigidbody2D))]
	public class EnemyMovement : MonoBehaviour
	{
		/// <summary>
		/// The maximum movement speed.
		/// </summary>
		public float MoveSpeed;

		private GameObject player;
	
		private bool canMove;

		/// <summary>
		/// Sets a value indicating whether this instance can move.
		/// </summary>
		/// <value><c>true</c> if this instance can move; otherwise, <c>false</c>.</value>
		public bool CanMove {
			set {
				canMove = value;
			}
		}

		void Awake ()
		{
			canMove = false;
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		/// <summary>
		/// Called by animation, tells this instance that spawning has finished.
		/// </summary>
		public void SpawnComplete ()
		{
			canMove = true;
		}

		void Update ()
		{
			if (canMove) {
				var heading = player.transform.position - transform.position;
				var distance = heading.magnitude;
				var dir = heading / distance;
			
				var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.Translate (dir * MoveSpeed * Time.deltaTime, Space.World);
			}
		}
	}
}
