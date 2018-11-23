using UnityEngine;
using System.Collections;

namespace TDGP.Demo
{
    /// <summary>
    /// Demo Script. Spawns weapon at players location on button press.
    /// </summary>
    public class WeaponSpawner : MonoBehaviour
    {
        public Transform Player;
        public GameObject[] WeaponPrefabs;

        private int weaponIndex = 1;
        private int previousIndex = 1;

        void Start()
        {
            SpawnWeapon(weaponIndex);
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.N))
            {
                weaponIndex = (weaponIndex + 1) % WeaponPrefabs.Length;
            }
            else if (Input.GetKeyUp(KeyCode.P))
            {
                weaponIndex--;
                if (weaponIndex < 0)
                    weaponIndex = WeaponPrefabs.Length - 1;
            }

            if (weaponIndex != previousIndex)
            {
                previousIndex = weaponIndex;
                SpawnWeapon(weaponIndex);
            }
        }

        private void SpawnWeapon(int index)
        {
            Instantiate(WeaponPrefabs[index], Player.transform.position, Quaternion.identity);
        }
    }

}
