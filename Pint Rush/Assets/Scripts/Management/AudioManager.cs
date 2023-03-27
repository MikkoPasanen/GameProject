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
            }
            if (glassBreaking != null)
            {
                glassBreaking.Play();
            }
        }

        public void PlayBeerPouring()
        {
            if (beerPouring.isPlaying)
            {
                beerPouring.Stop();
            }
            if(beerPouring != null)
            {
                beerPouring.Play();
            }
        }
    }
}
