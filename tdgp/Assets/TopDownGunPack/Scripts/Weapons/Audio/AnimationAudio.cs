using UnityEngine;
using System.Collections;

namespace TDGP
{
    /// <summary>
    /// Plays Random audio clip contained in AudioClips. Called my animation.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AnimationAudio : MonoBehaviour
    {
        /// <summary>
        /// The bank of possible audio clips.
        /// </summary>
        public AudioClip[] AudioClips;

        /// <summary>
        /// The volume scale.
        /// </summary>
        public float VolumeScale = 1f;

        /// <summary>
        /// The chance a clip will play. Inverse i.e. a value of 0 will mean the clip will play every time and a value of 1 means 
        /// the clip will never play.
        /// </summary>
        [Range(0, 1f)]
        public float
            InversePlayChance = 0f;

        private bool soundEnabled;

        private AudioSource _audioSource;

        void Start()
        {
            soundEnabled = (AudioClips != null && AudioClips.Length > 0);
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Plays random sound. Called by animation.
        /// </summary>
        public void PlaySound()
        {
            if (_audioSource == null || !_audioSource.enabled)
            {
                return;
            }

            if (soundEnabled && Random.Range(0, 1f) > InversePlayChance)
            {
                _audioSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Length)], VolumeScale);
            }
        }
    }
}
