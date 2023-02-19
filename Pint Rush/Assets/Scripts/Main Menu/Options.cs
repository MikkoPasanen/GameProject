using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class Options : MonoBehaviour
    {
        public GameObject options;
        public GameObject playButton;

        public void OnEnglish()
        {
            Debug.Log("Language changed to English!");
        }
        public void OnFinnish()
        {
            Debug.Log("Language changed to Finnish!");
        }
        public void OnMute()
        {
            Debug.Log("Mute clicked!");
        }
        public void OnCredits()
        {
            Debug.Log("Credits clicked!");
        }
        public void OnExitOptions()
        {
            Debug.Log("Options exited!");
            options.SetActive(false);
            playButton.SetActive(true);
        }
        public void OnOptions()
        {
            Debug.Log("Options clicked!");
            options.SetActive(true);
            playButton.SetActive(false);

        }
    }
}
