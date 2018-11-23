using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TDGP
{
/// <summary>
/// Special damage types.
/// </summary>
	public enum SPECIAL_DAMAGE_TYPE
	{
		ICE_CASE,
		CHAIN_LIGHTNING
	}

/// <summary>
/// Controls animation and damage for special projectiles (currently ice and chain lightning).
/// </summary>
	[RequireComponent (typeof(Health))]
	[RequireComponent (typeof(EnemyMovement))]
	public class DamageAnimationController : MonoBehaviour
	{
		/// <summary>
		/// Ice case animation prefab.
		/// </summary>
		public GameObject IceCaseAnimation;

		/// <summary>
		/// Lightning animation prefab.
		/// </summary>
		public GameObject LightningAnimation;

		/// <summary>
		/// The chain lightning range. Enemies within this proximity will also be struck with chain ligtning.
		/// </summary>
		public float ChainLightningRange = 3f;

		/// <summary>
		/// The maximum number of enemies that can be hit by chain ligtning.
		/// </summary>
		public int MaxChainLightningEnemiesHit = 6;

		private EnemyMovement movement;
		private Health health;
		private delegate void ApplyAnimation (float dps,float time);
		private Dictionary<SPECIAL_DAMAGE_TYPE, ApplyAnimation> damageMethodLookUp;
		private bool encasedInIce = false;

		void Awake ()
		{
			if (!IceCaseAnimation) {
				Debug.Log ("Ice case animation has not been set");
			}

			if (!LightningAnimation) {
				Debug.Log ("Lightning animation has not been set");
			}
	
			damageMethodLookUp = new Dictionary<SPECIAL_DAMAGE_TYPE, ApplyAnimation> ();
			damageMethodLookUp.Add (SPECIAL_DAMAGE_TYPE.ICE_CASE, ApplyIceCase);
			damageMethodLookUp.Add (SPECIAL_DAMAGE_TYPE.CHAIN_LIGHTNING, ApplyLightningChain);
		
			movement = GetComponent<EnemyMovement> ();
			health = GetComponent<Health> ();
		}

		/// <summary>
		/// Applies damage of type.
		/// </summary>
		/// <param name="damageType">Damage type.</param>
		/// <param name="dps">Damage per second.</param>
		/// <param name="time">Seconds damage occurs.</param>
		public void ApplyDamage (SPECIAL_DAMAGE_TYPE damageType, float dps, float time)
		{
			damageMethodLookUp [damageType] (dps, time);
		}
	
		private void ApplyIceCase (float dps, float time)
		{
			if (!encasedInIce) {
	
				encasedInIce = true;
				var ice = (GameObject)Instantiate (IceCaseAnimation, transform.position, Quaternion.identity);
				ice.transform.SetParent (transform);
				health.ApplyDPS (dps, time);
				movement.CanMove = false;
				Invoke ("DisableIceCase", 2.5f);
			}
		}

		private void DisableIceCase ()
		{
			encasedInIce = false;
		}

		private void ApplyLightningChain (float dps, float time)
		{

			health.ApplyDPS (dps, time);
			var otherEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			var i = 0;
			foreach (var enemy in otherEnemies) {
				if (enemy.GetInstanceID () == gameObject.GetInstanceID ())
					continue;

				if (i++ > MaxChainLightningEnemiesHit)
					break; 

				var to = (enemy.transform.position - transform.position).magnitude;

				if (to < ChainLightningRange) {
					var dir = enemy.transform.position - transform.position;

					var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
				
					// 512 = pixels of bolt sprite, 100f = pixels per units.
					var objectWidthSize = 512f / 100f;
				 
					var lightning = (GameObject)Instantiate (LightningAnimation, transform.position, Quaternion.AngleAxis (angle, Vector3.forward));
					lightning.transform.localScale = new Vector3 (dir.magnitude / objectWidthSize, dir.magnitude / objectWidthSize, lightning.transform.localScale.z);

					var otherHealth = enemy.GetComponent<Health> ();

					if (otherHealth) {
						otherHealth.ApplyDPS (dps, 1f);
					}
				}
			}
	
		}
	}	
}
