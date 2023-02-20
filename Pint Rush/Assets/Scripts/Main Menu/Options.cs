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

            gameManagement.SetLanguage("eng"); // Pushing language to ENGLISH to GameManagement.
        }
        public void OnFinnish()
        {
            //Debug.Log("Language changed to Finnish!");

            gameManagement.SetLanguage("fin"); // Pushing language to FINNISH to GameManagement.
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
            Debug.Log("Credits clicked!");
        }
        
        public void OnOptions()
        {
            //Debug.Log("Options clicked!");

            if(gameManagement.GetMuteState()) // Game IS muted!
            {
                // Game IS muted, setting unmute button active.
                unmuteButton.SetActive(true);
                muteButton.SetActive(false);
            }
            if(!gameManagement.GetMuteState()) // Game IS NOT muted!
            {
                unmuteButton.SetActive(false); // Setting unmute button DEACTIVE.
                muteButton.SetActive(true); // Setting mute button ACTIVE.
            }
            gameObject.SetActive(true); // Setting options screen ACTIVE.
        }
        public void OnExitOptions()
        {
            //Debug.Log("Options exited!");

            gameObject.SetActive(false); // Setting options screen DEACTIVE.
        }
    }
}
