using UnityEngine;
using System.Collections;

namespace TDGP.Demo
{
/// <summary>
/// Updates player sprite based on currently held weapon.
/// </summary>
	[RequireComponent (typeof(SpriteRenderer))]
	public class PlayerBodyController : MonoBehaviour
	{
		public Sprite OneHandedWeaponBody;
		public Sprite TwoHandedWeaponBody;
		public Sprite DualWieldWeaponBody;

		private SpriteRenderer spriteRenderer;

		void Start ()
		{
			spriteRenderer = GetComponent<SpriteRenderer> ();
		}

		public void PickedUpOneHanded ()
		{
			spriteRenderer.sprite = OneHandedWeaponBody;
		}

		public void PickedUpTwoHandedWeapon ()
		{
			spriteRenderer.sprite = TwoHandedWeaponBody;
		}

		public void PickedUpDualWieldWeapon ()
		{
			spriteRenderer.sprite = DualWieldWeaponBody;
		}
	}
}
