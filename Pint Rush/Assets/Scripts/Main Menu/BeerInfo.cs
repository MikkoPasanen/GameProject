using UnityEngine;

namespace PintRush
{
    public class BeerInfo : MonoBehaviour
    {
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject optionsButton;
        [SerializeField] private GameObject infoScreen;
        [SerializeField] private GameObject paleAle;
        [SerializeField] private GameObject stout;
        [SerializeField] private GameObject porter;
        [SerializeField] private GameObject whiteAle;
        [SerializeField] private GameObject brownAle;
        [SerializeField] private GameObject strongGoldenAle;

        public void OnBeerInfo()
        {
            gameObject.SetActive(true);
        }

        public void OnExitBeerInfo()
        {
            gameObject.SetActive(false);
        }

        public void OnInfoScreen()
        {
            infoScreen.SetActive(true);
            paleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            whiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
        }

        public void OnPaleAle()
        {
            infoScreen.SetActive(false);
            paleAle.SetActive(true);
            stout.SetActive(false);
            porter.SetActive(false);
            whiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
        }

        public void OnStout()
        {
            infoScreen.SetActive(false);
            paleAle.SetActive(false);
            stout.SetActive(true);
            porter.SetActive(false);
            whiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
        }

        public void OnPorter()
        {
            infoScreen.SetActive(false);
            paleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(true);
            whiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
        }

        public void OnWhiteAle()
        {
            infoScreen.SetActive(false);
            paleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            whiteAle.SetActive(true);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
        }

        public void OnBrownAle()
        {
            infoScreen.SetActive(false);
            paleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            whiteAle.SetActive(false);
            brownAle.SetActive(true);
            strongGoldenAle.SetActive(false);
        }

        public void OnStrongGoldenAle()
        {
            infoScreen.SetActive(false);
            paleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            whiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(true);
        }
    }
}
