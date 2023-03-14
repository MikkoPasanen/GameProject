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
        public Vector2 glassOneSpawn;
        public Vector2 glassTwoSpawn;
        public Vector2 glassThreeSpawn;
        public GameObject glassOnePrefab;
        public GameObject glassTwoPrefab;
        public GameObject glassThreePrefab;

        private void Awake()
        {
            //Debug.Log($"Values fetched from previous scene: \nmuted: " + isMuted+ ", language: " + language);
            //currentGlasses = 0;
            Debug.Log("Current glasses: " + currentGlasses);
        }

        private void Start()
        {
            this.currentGlasses = 0;
        }
        private void Update()
        {
            //Debug.Log("Current glasses: " + currentGlasses);
            //Debug.Log("Current glasses: " + GetCurrentGlasses());
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

        public void RemoveGlass()
        {
            this.currentGlasses -= 1;
            Debug.Log("Current glass amount decreased");
        }

        public void AddGlass()
        {
            this.currentGlasses += 1;
            Debug.Log("Current glass amount increased");
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
