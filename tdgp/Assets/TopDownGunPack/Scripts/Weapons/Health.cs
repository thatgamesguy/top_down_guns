using UnityEngine;
using System.Collections;
using TDGP.Demo;

namespace TDGP
{
    /// <summary>
    /// Enemy health script.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The starting health of enemies in the demo scene.
        /// </summary>
        public float MaxHealth = 10f;

        /// <summary>
        /// Sound pool of possible sounds to play when hit.
        /// </summary>
        public AudioClip[] OnHitSounds;

        /// <summary>
        /// Animation to play when enemy killed.
        /// </summary>
        public GameObject OnDeadAnimation;

        /// <summary>
        /// Pool of sprites to place when enemy killed.
        /// </summary>
        public GameObject[] OnDeadSprites;

        private float? dpsAmount = null;

        // For demo purposes. Used to limit number of enemies on screen at once.
        private EnemySpawner spawner;

        private AudioSource _audioSource;

        void Awake()
        {
            if (OnDeadSprites == null || OnDeadSprites.Length == 0)
            {
                Debug.LogError("Please set sprites to be shown when zombie dies");
            }

            var spawnObj = GameObject.FindGameObjectWithTag("Spawner");

            if (spawnObj)
            {
                spawner = spawnObj.GetComponent<EnemySpawner>();
            }

            _audioSource = Camera.main.GetComponent<AudioSource>();

        }

        private void PlayHitSound()
        {
            _audioSource.PlayOneShot(OnHitSounds[Random.Range(0, OnHitSounds.Length)]);
        }

        /// <summary>
        /// Reduces health, plays hit sound, and kills enemy if health less than or equal to zero.
        /// </summary>
        /// <param name="damageAmount">Damage amount.</param>
        public void OnHit(float damageAmount)
        {
            PlayHitSound();
            MaxHealth -= damageAmount;

            if (MaxHealth <= 0f)
            {
                OnDead();
            }
        }

        /// <summary>
        /// Applies damage per second.
        /// </summary>
        /// <param name="dps">Damage per second.</param>
        /// <param name="time">Time.</param>
        public void ApplyDPS(float dps, float time)
        {
            PlayHitSound();
            this.dpsAmount = dps;
            Invoke("DisableDPS", time - Time.deltaTime);
        }

        private void DisableDPS()
        {
            dpsAmount = null;
        }

        void Update()
        {
            if (dpsAmount.HasValue)
            {

                MaxHealth -= dpsAmount.Value * Time.deltaTime;

                if (MaxHealth <= 0f)
                {
                    OnDead();
                }
            }
        }

        private void OnDead()
        {
            if (OnDeadAnimation)
            {
                Instantiate(OnDeadAnimation, transform.position, Quaternion.identity);
            }

            Instantiate(OnDeadSprites[Random.Range(0, OnDeadSprites.Length)], transform.position, Quaternion.identity);

            spawner.EnemyRemoved();

            Destroy(gameObject);

        }
    }
}

