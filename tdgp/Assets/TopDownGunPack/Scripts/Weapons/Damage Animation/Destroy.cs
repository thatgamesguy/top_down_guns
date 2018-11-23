using UnityEngine;
using System.Collections;

namespace TDGP
{
/// <summary>
/// Destroys gameobject.
/// </summary>
	public class Destroy : MonoBehaviour
	{
		/// <summary>
		/// Executes destroy. Called by animation.
		/// </summary>
		public void Execute ()
		{
			Destroy (this.gameObject);
		}
	}
}
