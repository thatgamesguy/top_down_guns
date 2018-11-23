using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// The base class for all gun components.
/// </summary>
	public abstract class GunComponent : MonoBehaviour
	{
		/// <summary>
		/// Raised when weapon picked up.
		/// </summary>
		public abstract void OnPickup ();

		/// <summary>
		/// Raised when weapon dropped.
		/// </summary>
		public abstract void OnDrop ();
	}
}
