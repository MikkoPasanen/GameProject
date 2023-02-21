using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace PintRush
{
    public class GameManagement : MonoBehaviour
    {
        // Mute set by default to false!
        [SerializeField] private static bool isMuted = false;
        // Language set by default to finnish!
        [SerializeField] private static string language = "fin";

        private bool active = false;

        private void Awake()
        {
            Debug.Log($"Values fetched from previous scene: \nmuted: " + isMuted+ ", language: " + language);
        }

        public bool GetMuteState()
        {
            return isMuted;
        }
        public string GetLanguage()
        {
            return language;
        }

        public void SetMuteState(bool newIsMuted)
        {
            isMuted = newIsMuted;
        }
        public void SetLanguage(string newLanguage)
        {
            language = newLanguage;
        }

        //Change the locale aka language and make it that it is not called more than once
        public void ChangeLocal(int localeID)
        {
            if(active == true)
            {
                return;
            }

            StartCoroutine(SetLocale(localeID));
        }

        //Select the locale aka language
        IEnumerator SetLocale(int _localeID)
        {
            active= true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
            active= false;
        }
    }
}
