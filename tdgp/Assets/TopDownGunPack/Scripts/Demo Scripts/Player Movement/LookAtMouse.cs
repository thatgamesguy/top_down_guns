using UnityEngine;
using System.Collections;

namespace TDGP.Demo
{
/// <summary>
/// Demo script. Play looks at mouse.
/// </summary>
	public class LookAtMouse : MonoBehaviour
	{
	
		void Update ()
		{
			var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			var heading = mousePos - transform.position;
			var dist = heading.magnitude;
			var dir = heading / dist;
	
			if (dist > 5.02f) {
				var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				var newRotation = Quaternion.AngleAxis (angle, Vector3.forward);

				if (Quaternion.Angle (newRotation, transform.rotation) > 3f)
					transform.rotation = newRotation;
			}
		}
	}
}
