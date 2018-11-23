using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Missile trail. Uses line renderer to create a trail effect for the missile.
/// </summary>
	public class MissileTrail : MonoBehaviour
	{
		/// <summary>
		/// The number of points on line.
		/// </summary>
		public int numOfPoints = 10;

		/// <summary>
		/// The update speed.
		/// </summary>
		public float updateSpeed = 0.25f;

		/// <summary>
		/// The turn speed.
		/// </summary>
		public float turnSpeed = 0.25f;

		/// <summary>
		/// The spread of the line.
		/// </summary>
		public float spread = 0.2f;
	
		private LineRenderer line;
		private Transform tr;
		private Vector3[] positions;
		private Vector3[] directions;
		private int i;
		private float timeSinceUpdate = 0f;
		private Material lineMaterial;
		private float lineSegment = 0f;
		private int currentNumberOfPoints = 2;
		private bool allPointsRequired = false;
	
		private Vector3 tempVec;

		void Start ()
		{
			tr = transform;
			line = GetComponent<LineRenderer> ();
			lineMaterial = line.material;
		
		
			positions = new Vector3[numOfPoints];
			directions = new Vector3[numOfPoints];
		
			line.positionCount = currentNumberOfPoints;
		
			for (i = 0; i < currentNumberOfPoints; i++) {
				tempVec = getSmokeVec ();
				directions [i] = tempVec;
				positions [i] = tr.position;
				line.SetPosition (i, positions [i]);
			}
		}

		private Vector3 getSmokeVec ()
		{
			Vector3 smokeVec;
			smokeVec.x = Random.Range (-1f, 1f);
			smokeVec.y = Random.Range (0f, 1f);
			smokeVec.z = Random.Range (-1f, 1f);
			smokeVec.Normalize ();
			smokeVec *= spread;
			smokeVec.y += turnSpeed;
			return smokeVec;
		}
	
		void Update ()
		{
		
			timeSinceUpdate += Time.deltaTime; 

			if (timeSinceUpdate > updateSpeed) {
				timeSinceUpdate -= updateSpeed;

				if (!allPointsRequired) {
					currentNumberOfPoints++;
					line.positionCount = currentNumberOfPoints;
					tempVec = getSmokeVec ();
					directions [0] = tempVec;
					positions [0] = tr.position;
					line.SetPosition (0, positions [0]);
				}
			
				if (!allPointsRequired && (currentNumberOfPoints == numOfPoints)) {
					allPointsRequired = true;
				}

				for (i = currentNumberOfPoints - 1; i > 0; i--) {
					tempVec = positions [i - 1];
					positions [i] = tempVec;
					tempVec = directions [i - 1];
					directions [i] = tempVec;
				}
				tempVec = getSmokeVec ();
				directions [0] = tempVec; 
			}

			for (i = 1; i < currentNumberOfPoints; i++) {
				tempVec = positions [i];
				tempVec += directions [i] * Time.deltaTime;
				positions [i] = tempVec;
			
				line.SetPosition (i, positions [i]);
			}
			positions [0] = tr.position; 
			line.SetPosition (0, tr.position);

			if (allPointsRequired) {
				lineMaterial.mainTextureOffset = new Vector2 (lineSegment * (timeSinceUpdate / updateSpeed),
																lineMaterial.mainTextureOffset.y);
			}
		}

		/// <summary>
		/// Updates line based on projectiles movement.
		/// </summary>
		public void OnFire ()
		{
			if (!line)
				return;
	
			currentNumberOfPoints = 2;
			timeSinceUpdate = 0f;
			allPointsRequired = false;
		
			line.positionCount = currentNumberOfPoints;
		
			for (i = 0; i < currentNumberOfPoints; i++) {
				tempVec = getSmokeVec ();
				directions [i] = tempVec;
				positions [i] = tr.position;
				line.SetPosition (i, positions [i]);
			}
		}
	
		void OnDisable ()
		{
		
		}
	}
}
