using UnityEngine;
using System.Collections;

namespace TDGP
{
    /// <summary>
    /// Weapon pickup script, also allows for weapon switching. Used for the demo scene but can be adapted for use by your player.
    /// </summary>
    public class Holster : MonoBehaviour
    {
        private static readonly int WEAPON_CAPICITY = 1;

        private Transform[] weapons;
        private Gun[] gunScripts;
        private int equippedWeapons = 0;
        private int currentWeapon = -1;

        void Awake()
        {
            weapons = new Transform[WEAPON_CAPICITY];
            gunScripts = new Gun[WEAPON_CAPICITY];
        }

        /// <summary>
        /// Called when a weapon is picked up. Attaches weapon to player in a hoster slot. 
        /// If the holster is full it destroys the weapon in the current slot.
        /// </summary>
        /// <param name="weapon">Weapon.</param>
        public void OnWeaponPickup(Transform weapon)
        {
            if (IsHolsterFull())
            {
                DestroyWeaponInCurrentSlot();
            }
            else
            {
                if (currentWeapon != -1)
                {
                    DisableWeapon(currentWeapon);
                }

                currentWeapon++;
            }


            EquipWeaponInCurrentSlot(weapon);
        }

        private void RemoveWeaponInCurrentSlot()
        {
            weapons[currentWeapon].transform.rotation = Quaternion.identity;
            gunScripts[currentWeapon].OnDrop();
            weapons[currentWeapon].SetParent(null, true);
            weapons[currentWeapon] = null;
            equippedWeapons--;
        }

        private void DestroyWeaponInCurrentSlot()
        {
            Destroy(weapons[currentWeapon].gameObject);
            equippedWeapons--;
        }

        private void EquipWeaponInCurrentSlot(Transform weapon)
        {
            weapons[currentWeapon] = weapon;
            weapons[currentWeapon].SetParent(transform);
            weapons[currentWeapon].localPosition = Vector2.zero;
            weapons[currentWeapon].rotation = transform.rotation;

            var gun = weapon.GetComponent<Gun>();
            if (!gun)
            {
                Debug.LogError("Attempted to pickup weapon that does not have Gun script. Please attach the Gun script to the weapon");
            }
            else
            {
                gunScripts[currentWeapon] = gun;
                UpdateBodySprite(gun.GunType);
                gun.OnPickup();
            }

            equippedWeapons++;
        }

        private void SwitchToWeapon(int desiredWeapon)
        {
            var disabledWeapon = (desiredWeapon + 1) % WEAPON_CAPICITY;

            DisableWeapon(disabledWeapon);
            EnableWeapon(desiredWeapon);
            UpdateBodySprite(gunScripts[desiredWeapon].GunType);
        }

        private bool IsHolsterFull()
        {
            return equippedWeapons == weapons.Length;
        }

        private void DisableWeapon(int weapon)
        {
            weapons[weapon].gameObject.SetActive(false);
        }

        private void EnableWeapon(int weapon)
        {
            weapons[weapon].gameObject.SetActive(true);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Weapon"))
            {
                OnWeaponPickup(other.transform);
            }
        }

        void Update()
        {
            if (equippedWeapons < 2)
            {
                return;
            }

            var mouseInput = Input.GetAxis("MouseScrollWheel");

            if ((mouseInput < 0 || Input.GetKeyUp(KeyCode.Alpha1)) && currentWeapon != 0)
            {
                currentWeapon = 0;
                SwitchToWeapon(currentWeapon);
            }
            else if ((mouseInput > 0 || Input.GetKeyUp(KeyCode.Alpha2) && currentWeapon != 1))
            {
                currentWeapon = 1;
                SwitchToWeapon(currentWeapon);
            }


        }

        private void UpdateBodySprite(Gun_Type type)
        {
            switch (type)
            {
                case Gun_Type.ONE_HANDED:
                    SendMessage("PickedUpOneHanded");
                    break;
                case Gun_Type.TWO_HANDED:
                    SendMessage("PickedUpTwoHandedWeapon");
                    break;
                case Gun_Type.DUAL_WIELD:
                    SendMessage("PickedUpDualWieldWeapon");
                    break;
            }
        }
    }

}

