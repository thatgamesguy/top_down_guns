using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TDGP
{
/// <summary>
/// Responsiblities: act as a object pool for the guns projectiles, provide projectiles when requested by the guns barrel, 
/// limit the maximum number of bullets fired by the gun, and reload bullets when requested by player.
/// </summary>
	public class GunClip : GunComponent
	{	
		/// <summary>
		/// The bullets in a clip.
		/// </summary>
		public int BulletsInClip = 15;

		/// <summary>
		/// If true, this weapon can be reloaded.
		/// </summary>
		public bool SupportReload = false;

		/// <summary>
		/// The time it takes to reload.
		/// </summary>
		public float ReloadSpeed = 0.2f;
	
		private bool reloading = false;
		private int currentBulletsInClip;
		private GameObject projectile;

		/// <summary>
		/// The pooled objects currently available.
		/// </summary>
		private List<GameObject> pooledBullets;

		private static readonly int INITIAL_POOL_MAX = 20;

		void Awake ()
		{
			foreach (Transform sibling in transform) {
				if (sibling.CompareTag ("GunProjectile")) {
					projectile = sibling.gameObject;
					break;
				}
			}
		
			var poolAmount = (BulletsInClip > INITIAL_POOL_MAX) ? INITIAL_POOL_MAX : BulletsInClip;
		
			if (!projectile) {
				Debug.LogError ("Gun clip requires child bullet object with tag 'GunProjectile'");
			} else {
	
				pooledBullets = new List<GameObject> ();
				
				for (int n = 0; n < poolAmount; n++) {
					GameObject newObj = (GameObject)Instantiate (projectile);
					SetOwner (newObj);
					PoolObject (newObj);
				}
			}

		}
	
		void Update ()
		{
			if (!SupportReload)
				return;
	
			if (Input.GetButtonUp ("Reload") && OkToReload ()) {
				StartCoroutine (Reload ());
			}
		}

		/// <summary>
		/// Pools the object specified.  Will not be pooled if there is no prefab of that type.
		/// </summary>
		/// <param name='obj'>
		/// Object to be pooled.
		/// </param>
		public void PoolObject (GameObject obj)
		{
			obj.SetActive (false);
			obj.transform.SetParent (transform);
			pooledBullets.Add (obj);
		}

		/// <summary>
		/// Request a bullet. Returns a buller from the pool if present totherwise instantiates and returns new bullet.
		/// </summary>
		/// <returns>The bullet.</returns>
		public GameObject GetBullet ()
		{
			if (pooledBullets == null)
				return null;
		
			if (pooledBullets.Count > 0) {
				GameObject pooledObject = pooledBullets [0];
			
				if (pooledObject) {
					pooledBullets.RemoveAt (0);
					pooledObject.transform.SetParent (null, false);
					pooledObject.SetActive (true);
				} 
			
				return pooledObject;
			} else {
				var newObj = (GameObject)Instantiate (projectile);
				newObj.SetActive (true);
				SetOwner (newObj);
				return newObj;
			}
		
		
		}

		/// <summary>
		/// Requests a bullet. A bullet is only returned if: there are currently bullets in the clip, and the weapon is not currently being reloaded.
		/// </summary>
		/// <returns>The bullet.</returns>
		public GameObject RequestBullet ()
		{
			if (reloading)
				return null;
		
			if (currentBulletsInClip > 0) {
				currentBulletsInClip--;
				var bullet = GetBullet ();
				return bullet;
			}
		
			return null;
		}

		/// <summary>
		/// Called by gun component on parent. Resets current bullets in clip.
		/// </summary>
		public override void OnPickup ()
		{
			currentBulletsInClip = BulletsInClip;
		}

		/// <summary>
		/// Called by gun component on parent.
		/// </summary>
		public override void OnDrop ()
		{
		
		}
	
		private bool OkToReload ()
		{
			return currentBulletsInClip != BulletsInClip && !reloading;
		}
	
		private IEnumerator Reload ()
		{
			reloading = true;
			Debug.Log ("Reloading");
			yield return new WaitForSeconds (ReloadSpeed);
		
			currentBulletsInClip = BulletsInClip;
			reloading = false;
		}

		private void SetOwner (GameObject bullet)
		{
			var projectile = bullet.GetComponent<GunProjectile> ();

			if (!projectile) {
				Debug.LogError ("Bullet should have Projectile script attached");
			} else {
				projectile.Owner = this;
			}
		}
	}
}
