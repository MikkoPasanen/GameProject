using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using TMPro;

namespace PintRush
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject settingsCanvas;

        [SerializeField] private GameObject muteButton;
        [SerializeField] private GameObject unmuteButton;
        [SerializeField] private GameObject vibrateOnButton;
        [SerializeField] private GameObject vibrateOffButton;
        [SerializeField] private GameObject credits;

        [SerializeField] private GameObject codeInput;
        private const string developerCode = "!SALAPATO!";
        private static bool developerMode = false;

        private bool active = false;
        private bool optionsActive = false;

        private static int audioOn;
        private const string audioKey = "AUDIO_ON";
        private static int vibrationOn;
        private const string vibrationKey = "VIBRATION_ON";
        private static int language;
        private const string languageKey = "LANGUAGE";

        private static bool titleScreenPlayed = false;

        private void Start()
        {
            language = PlayerPrefs.GetInt(languageKey, 0);
            ChangeLocal(language);

            audioOn = PlayerPrefs.GetInt(audioKey, 1);
            ChangeAudio(audioOn);

            vibrationOn = PlayerPrefs.GetInt(vibrationKey, 1);
            ChangeVibration(vibrationOn);

            if(SceneManager.GetActiveScene().name == "MainMenu")
            {
                codeInput.SetActive(false);

                var input = codeInput.GetComponent<TMP_InputField>();
                var submit = new TMP_InputField.SubmitEvent();
                submit.AddListener(Submit);
                input.onEndEdit = submit;
            }
        }
        private void Submit(string str)
        {
            if(str == developerCode)
            {
                Debug.Log("SUCCESS! Developer mode activated!");
                developerMode = true;
            }
        }

        // SETTINGS opened
        public void OnSettings()
        {
            optionsActive = !optionsActive;

            if (optionsActive)
            {
                if(SceneManager.GetActiveScene().name == "Game")
                {
                    Time.timeScale = 0;
                }
            }
            else
            {
                Time.timeScale = 1;
            }
            settingsCanvas.SetActive(optionsActive);
        }

        // SETTINGS closed
        public void OnExitSettings()
        {
            optionsActive = false;
            settingsCanvas.SetActive(false);
            Time.timeScale = 1;
        }

        // LANGUAGE settings
        public void ChangeLocal(int localeID)
        {
            //Change the locale aka language and make it that it is not called more than once
            if (active == true)
            {
                return;
            }
            StartCoroutine(SetLocale(localeID));
        }
        IEnumerator SetLocale(int _localeID)
        {
            active = true;

            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
            
            SetPlayerPrefs(languageKey, _localeID);
            Debug.Log($"PlayerPrefs languageKey {PlayerPrefs.GetInt(languageKey)}");
            
            active = false;
        }
        
        // AUDIO settings
        public void ChangeAudio(int i)
        {
            audioOn = i;
            SetPlayerPrefs(audioKey, i);
            Debug.Log($"PlayerPrefs audioKey {PlayerPrefs.GetInt(audioKey)}");
            if (i == 0)
            {
                unmuteButton.SetActive(false);
                muteButton.SetActive(true);
            }
            else
            {
                unmuteButton.SetActive(true);
                muteButton.SetActive(false);
            }
        }
        public bool GetAudioState()
        {
            if(audioOn == 0)
            {
                return false;
            }
            return true;
        }

        // VIBRATION settings
        public void ChangeVibration(int i)
        {
            vibrationOn = i;
            SetPlayerPrefs(vibrationKey, i);
            Debug.Log($"PlayerPrefs vibrationKey {PlayerPrefs.GetInt(vibrationKey)}");
            if (i == 0)
            {
                vibrateOnButton.SetActive(true);
                vibrateOffButton.SetActive(false);
            }
            else
            {
                vibrateOnButton.SetActive(false);
                vibrateOffButton.SetActive(true);
            }
        }
        public bool GetVibrationState()
        {
            if(vibrationOn == 0)
            {
                return false;
            }
            return true;
        }

        // CREDITS
        public void OnCredits()
        {
            credits.SetActive(true);
        }
        
        // MENU
        public void OnBackToMenu()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }

        private void SetPlayerPrefs(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public void ActivateCodeField()
        {
            codeInput.SetActive(true);
        }

        public bool CheckDeveloperMode()
        {
            return developerMode;
        }

        public void SetTitleScreenPlayed()
        {
            titleScreenPlayed = true;
        }

        public bool GetTitleScreenState()
        {
            return titleScreenPlayed;
        }

    }
}
