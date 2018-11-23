using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Adds a laser sight to a weapon. Responsible for updating the lasers scale.
/// </summary>
	[RequireComponent (typeof(LineRenderer))]
	public class GunLaserSight : GunComponent
	{
		/// <summary>
		/// Total range of laser when no obstructions present.
		/// </summary>
		public float Range = 5f;
	
		private LayerMask mask;
		private LineRenderer lineRenderer;

		public void Awake ()
		{
			mask = ~(1 << LayerMask.NameToLayer ("Default")); // Anything but default mask.
		
			lineRenderer = GetComponent<LineRenderer> ();
		
			if (!lineRenderer) {
				Debug.LogError ("Please ensure laser script is attached to an object with a line renderer");
			}
		
			lineRenderer.SetPosition (1, new Vector3 (0, Range, 0));

			lineRenderer.enabled = false;
		}

		void Update ()
		{
			if (lineRenderer.enabled) {
				var hit = Physics2D.Raycast (transform.position, transform.up, Range, mask);
		
		
				if (hit.collider) {
					lineRenderer.SetPosition (1, new Vector3 (0, hit.distance, 0));
				} else {
					lineRenderer.SetPosition (1, new Vector3 (0, Range, 0));
				}
			}
		
		}

		/// <summary>
		/// Raised when weapon picked up. Enables line renderer.
		/// </summary>
		public override void OnPickup ()
		{
			lineRenderer.enabled = true;
		}

		/// <summary>
		/// Raised when weapon dropped. Disables line renderer.
		/// </summary>
		public override void OnDrop ()
		{
			lineRenderer.enabled = false;
		}
	}
}
