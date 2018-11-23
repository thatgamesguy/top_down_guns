using UnityEngine;
using System.Collections;

namespace TDGP.Demo
{
/// <summary>
/// Spawns enemies for the example scene.
/// </summary>
	public class EnemySpawner : MonoBehaviour
	{
		/// <summary>
		/// The enemy prefab.
		/// </summary>
		public GameObject Enemy;

		/// <summary>
		/// Time between spawns.
		/// </summary>
		public float SpawnTime = 0.8f;

		/// <summary>
		/// The maximum number of enemies on screen.
		/// </summary>
		public int MaxEnemiesOnScreen = 10;

		/// <summary>
		/// Reference to the kill text.
		/// </summary>
		public KillCount KillCount;
	
		private int currentEnemyCount = 0;

		void Start ()
		{
			InvokeRepeating ("SpawnEnemy", 0f, SpawnTime);
		}

		/// <summary>
		/// Updates kill count.
		/// </summary>
		public void EnemyRemoved ()
		{
			currentEnemyCount--;
			KillCount.EnemyKilled ();
		}

		private void SpawnEnemy ()
		{
			if (currentEnemyCount >= MaxEnemiesOnScreen)
				return;
			
			currentEnemyCount++;
			var position = new Vector2 (Random.Range (1, 11), Random.Range (4, 11));
			Instantiate (Enemy, position, Quaternion.identity);
		}
	}
}
