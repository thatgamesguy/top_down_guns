using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Disables gameobject.
/// </summary>
	public class Disable : MonoBehaviour
	{
		/// <summary>
		/// Executes disable. Called by animation.
		/// </summary>
		public void Execute ()
		{
			gameObject.SetActive (false);
		}
	}
}

