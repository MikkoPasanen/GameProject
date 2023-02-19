using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PintRush
{
    public class MenuButtons : MonoBehaviour
    {
        public GameObject bottle;
        
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

        public void OnBarrel()
        {
            Debug.Log("Barrels clicked!");
            bottle.SetActive(true);
        }
    }
}
