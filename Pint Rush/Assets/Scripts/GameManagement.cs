using UnityEngine;

namespace PintRush
{
    public class GameManagement : MonoBehaviour
    {
        // Mute set by default to false!
        [SerializeField] private static bool isMuted = false;
        // Language set by default to finnish!
        [SerializeField] private static string language = "fin";

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
    }
}
