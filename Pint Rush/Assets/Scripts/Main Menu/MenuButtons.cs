using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PintRush
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private GameManagement gameManagement;
        [SerializeField] private GameObject bottle;
        [SerializeField] private GameObject playButton;
        [SerializeField] private Settings settings;

        [SerializeField] private GameObject startingScreen;
        [SerializeField] private GameObject startingScreenLogo;

        private int counter = 0;

        private void Start()
        {
            if(!settings.GetTitleScreenState())
            {
                startingScreen.SetActive(true);
                StartCoroutine(TitleScreenFade());
            }
            else
            {
                startingScreen.SetActive(false);
            }
        }

        IEnumerator TitleScreenFade()
        {
            yield return new WaitForSeconds(1.2f);

            startingScreenLogo.GetComponent<RawImage>().CrossFadeAlpha(0f, 1f, false);
            yield return new WaitForSeconds(1.5f);

            startingScreen.GetComponent<RawImage>().CrossFadeAlpha(0f, 0.7f, false);
            yield return new WaitForSeconds(1f);

            startingScreen.SetActive(false);
            settings.SetTitleScreenPlayed();
        }

        public void OnPlay()
        {
            //Debug.Log("Play clicked!");

            SceneManager.LoadScene(sceneName: "Game"); // Switching to scene GAME.
        }

        public void OnMenu()
        {
            //Debug.Log("Menu clicked!");

            SceneManager.LoadScene(sceneName: "MainMenu"); // Switching to scene MAINMENU.
        }

        public void OnBarrel()
        {
            bottle.SetActive(true); // Setting easter egg bottle ACTIVE.
        }

        public void OnHouse()
        {
            Debug.Log($"House clicked {counter}");
            counter++;
            if(counter >= 10)
            {
                settings.ActivateCodeField();
            }
        }
    }
}
