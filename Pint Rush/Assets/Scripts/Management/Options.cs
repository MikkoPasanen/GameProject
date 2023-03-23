using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

namespace PintRush
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private GameManagement gameManagement;
        [SerializeField] private GameObject muteButton;
        [SerializeField] private GameObject unmuteButton;
        [SerializeField] private GameObject credits;
        private bool active = false;
        private bool optionsActive = false;

        public void OnEnglish()
        {
            //Debug.Log("Language changed to English!");

            gameManagement.SetLanguage("eng"); // Pushing language to ENGLISH to GameManagement.
        }
        public void OnFinnish()
        {
            //Debug.Log("Language changed to Finnish!");

            gameManagement.SetLanguage("fin"); // Pushing language to FINNISH to GameManagement.
        }

        //Change the locale aka language and make it that it is not called more than once
        public void ChangeLocal(int localeID)
        {
            if (active == true)
            {
                return;
            }

            StartCoroutine(SetLocale(localeID));
        }

        //Select the locale aka language
        IEnumerator SetLocale(int _localeID)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
            active = false;
        }
        public void OnMute()
        {
            //Debug.Log("Mute clicked!");

            gameManagement.SetMuteState(true); // Pushing mute state TRUE to GameManagement.
            unmuteButton.SetActive(true); // Setting unmute button ACTIVE.
            muteButton.SetActive(false); // Setting mute button DEACTIVE.
        }
        public void OnUnmute()
        {
            //Debug.Log("Unmute clicked!");

            gameManagement.SetMuteState(false); // Pushing mute state FALSE to GameManagement.
            unmuteButton.SetActive(false); // Setting unmute button DEACTIVE.
            muteButton.SetActive(true); // Setting mute button ACTIVE.
        }
        public void OnCredits()
        {
            credits.SetActive(true);
        }
        
        public void OnOptions()
        {
            optionsActive = !optionsActive;

            if (optionsActive)
            {
                Time.timeScale = 0;
                if (gameManagement.GetMuteState()) // Game IS muted!
                {
                    // Game IS muted, setting unmute button active.
                    unmuteButton.SetActive(true);
                    muteButton.SetActive(false);
                }
                if (!gameManagement.GetMuteState()) // Game IS NOT muted!
                {
                    unmuteButton.SetActive(false); // Setting unmute button DEACTIVE.
                    muteButton.SetActive(true); // Setting mute button ACTIVE.
                }
            }
            else
            {
                Time.timeScale = 1;
            }
            gameObject.SetActive(optionsActive);
        }

        public void OnBackToMenu()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }

        public void OnExitOptions()
        {
            //Debug.Log("Options exited!");

            gameObject.SetActive(false); // Setting options screen DEACTIVE.
            Time.timeScale = 1;
        }
    }
}
