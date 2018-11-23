using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDGP.Demo
{
/// <summary>
/// Reloads scene if enemy touches player in demo scene.
/// </summary>
	public class LoadSceneOnCollision : MonoBehaviour
	{
		public int SceneNumber = 0;

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.CompareTag ("Enemy"))
				SceneManager.LoadScene (SceneNumber);
		}
	}
}

