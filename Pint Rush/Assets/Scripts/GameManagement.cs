using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace PintRush
{
    public class GameManagement : MonoBehaviour
    {
        // Mute set by default to false!
        [SerializeField] private static bool isMuted = false;
        [SerializeField] private static string language = "fin";
        private int currentGlasses = 0;
        [SerializeField] private int maxGlasses;
        private bool active = false;

        // Glasses
        public Vector3 glassOneSpawn;
        public Vector3 glassTwoSpawn;
        public Vector3 glassThreeSpawn;
        public GameObject glassOnePrefab;
        public GameObject glassTwoPrefab;
        public GameObject glassThreePrefab;

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

        public void RemoveGlass()
        {
            this.currentGlasses -= 1;
        }

        public void AddGlass()
        {
            this.currentGlasses += 1;
        }

        public void SetCurrentGlasses(int currentGlasses)
        {
            this.currentGlasses = currentGlasses;
        }

        public int GetMaxGlasses()
        {
            return this.maxGlasses;
        }

        public int GetCurrentGlasses()
        {
            return this.currentGlasses;
        }
    }
}
