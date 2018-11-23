using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Enables zombie movement when finished spawning.
/// </summary>
	public class EnableParentMovement : MonoBehaviour
	{
		private EnemyMovement movement;

		/// <summary>
		/// Enables movement. Called by animation.
		/// </summary>
		public void EnableMovement ()
		{
			if (!movement)
				movement = transform.parent.GetComponent<EnemyMovement> ();

			if (movement) {
				movement.CanMove = true;
			} else {
				Debug.LogError ("Parent transform should have 'EnemyMovement' script");
			}
		}
	}
}
