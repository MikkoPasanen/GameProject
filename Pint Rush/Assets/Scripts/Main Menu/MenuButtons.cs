using UnityEngine;
using UnityEngine.SceneManagement;

namespace PintRush
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private GameManagement gameManagement;
        [SerializeField] private GameObject bottle;
        [SerializeField] private GameObject playButton;
        
        public void OnPlay()
        {
            //Debug.Log("Play clicked!");
            SceneManager.LoadScene(sceneName: "Game");
        }

        public void OnMenu()
        {
            //Debug.Log("Menu clicked!");
            SceneManager.LoadScene(sceneName: "MainMenu");
        }

        public void OnBarrel()
        {
            //Debug.Log("Barrels clicked!");
            bottle.SetActive(true);
        }
    }
}
