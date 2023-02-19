using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class BeerInfo : MonoBehaviour
    {
        public GameObject beerInfo;
        public GameObject playButton;
        public GameObject optionsButton;

        public void OnBeerInfo()
        {
            Debug.Log("Beer info clicked!");
            beerInfo.SetActive(true);
            playButton.SetActive(false);
            optionsButton.SetActive(false);
        }

        public void OnExitBeerInfo()
        {
            Debug.Log("Beer info exited!");
            beerInfo.SetActive(false);
            playButton.SetActive(true);
            optionsButton.SetActive(true);
        }
    }
}
