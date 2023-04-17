using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource beerPouring;
        [SerializeField] private AudioSource upgradeSuccessSound;
        [SerializeField] private AudioSource upgradeFailedSound;
        [SerializeField] private AudioSource pointSound;

        [SerializeField] private GameManagement gameManagement;

        private AudioSource[] allAudio;


        private void Start()
        {
            // Stores all audioSources in an array
            allAudio = gameObject.GetComponents<AudioSource>();
        }

        private void Update()
        {
            // Mute is on mute all sounds
            if ( gameManagement.GetMuteState() )
            {
                foreach ( AudioSource source in allAudio )
                {
                    source.mute = true;
                }
            }
            else
            {
                foreach ( AudioSource source in allAudio )
                {
                    source.mute = false;
                }
            }
        }

        public void PlayBeerPouring()
        {
            if (beerPouring != null && beerPouring.isPlaying)
            {
                beerPouring.Stop();
                Debug.Log($"Stopping: {beerPouring.clip.name}");

            }
            if (beerPouring != null)
            {
                beerPouring.Play();
                Debug.Log($"Playing: {beerPouring.clip.name}");
            }
        }

        public void PlayUpgradeSuccessSound()
        {
            if (upgradeSuccessSound != null)
            {
                upgradeSuccessSound.Play();
            }
        }
        public void PlayUpgradeFailedSound()
        {
            if(upgradeFailedSound != null)
            {
                upgradeFailedSound.Play();
            }
        }

        public void PlayPointSound()
        {
            if(pointSound != null)
            {
                pointSound.Play();
            }
        }

        public bool GetVibrationState()
        {
            return gameManagement.GetVibrationState();
        }
    }
}
