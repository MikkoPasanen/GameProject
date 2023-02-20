using UnityEngine;

namespace PintRush
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private GameManagement gameManagement;
        [SerializeField] private GameObject muteButton;
        [SerializeField] private GameObject unmuteButton;

        public void OnEnglish()
        {
            //Debug.Log("Language changed to English!");
            gameManagement.SetLanguage("eng");
        }
        public void OnFinnish()
        {
            //Debug.Log("Language changed to Finnish!");
            gameManagement.SetLanguage("fin");
        }
        public void OnMute()
        {
            //Debug.Log("Mute clicked!");
            gameManagement.SetMuteState(true);
            unmuteButton.SetActive(true);
            muteButton.SetActive(false);
        }
        public void OnUnmute()
        {
            //Debug.Log("Unmute clicked!");
            gameManagement.SetMuteState(false);
            unmuteButton.SetActive(false);
            muteButton.SetActive(true);
        }
        public void OnCredits()
        {
            Debug.Log("Credits clicked!");
        }
        public void OnExitOptions()
        {
            //Debug.Log("Options exited!");
            gameObject.SetActive(false);
        }
        public void OnOptions()
        {
            //Debug.Log("Options clicked!");
            if(gameManagement.GetMuteState())
            {
                unmuteButton.SetActive(true);
                muteButton.SetActive(false);
            }
            if(!gameManagement.GetMuteState())
            {
                unmuteButton.SetActive(false);
                muteButton.SetActive(true);
            }
            gameObject.SetActive(true);

        }
    }
}
