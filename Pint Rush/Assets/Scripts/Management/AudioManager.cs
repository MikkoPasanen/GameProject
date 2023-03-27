using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource glassBreaking;
        [SerializeField] private AudioSource beerPouring;

        public void PlayGlassBreaking()
        {
            if (glassBreaking.isPlaying)
            {
                glassBreaking.Stop();
                Debug.Log($"Stopping: {glassBreaking.clip.name}");
            }
            if (glassBreaking != null)
            {
                glassBreaking.Play();
                Debug.Log($"Playing: {glassBreaking.clip.name}");
            }
        }

        public void PlayBeerPouring()
        {
            if (beerPouring.isPlaying)
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
    }
}
