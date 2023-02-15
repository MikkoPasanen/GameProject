using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PintRush
{
    public class Buttons : MonoBehaviour
    {
        public GameObject options;
        public GameObject bottle;
        public GameObject playButton;

        public void OnPlay()
        {
            Debug.Log("Play clicked!");
            SceneManager.LoadScene(sceneName: "Game");
        }

        public void OnMenu()
        {
            Debug.Log("Menu clicked!");
            SceneManager.LoadScene(sceneName: "MainMenu");
        }

        // Options menu
        public void OnOptions()
        {
            Debug.Log("Options clicked!");
            options.SetActive(true);
            playButton.SetActive(false);
            
        }
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
        public void OnExitOptions() {
            Debug.Log("Options exited!");
            options.SetActive(false);
            playButton.SetActive(true);
        }

        public void OnBarrel()
        {
            Debug.Log("Barrels clicked!");
            bottle.SetActive(true);
        }
    }
}
