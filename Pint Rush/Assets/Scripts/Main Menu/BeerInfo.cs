using UnityEngine;

namespace PintRush
{
    public class BeerInfo : MonoBehaviour
    {
        [SerializeField] private GameObject infoScreen;
        [SerializeField] private GameObject kajoPaleAle;
        [SerializeField] private GameObject stout;
        [SerializeField] private GameObject porter;
        [SerializeField] private GameObject lentoWhiteAle;
        [SerializeField] private GameObject brownAle;
        [SerializeField] private GameObject strongGoldenAle;
        [SerializeField] private GameObject hallaWhiteAle;
        [SerializeField] private GameObject kiuluPaleAle;

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
            kajoPaleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle?.SetActive(false);
        }

        public void OnKajoPaleAle()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(true);
            stout.SetActive(false);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle?.SetActive(false);
        }

        public void OnStout()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(false);
            stout.SetActive(true);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle?.SetActive(false);
        }

        public void OnPorter()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(true);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle?.SetActive(false);
        }

        public void OnLentoWhiteAle()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(true);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle?.SetActive(false);
        }

        public void OnBrownAle()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(true);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle.SetActive(false);
        }

        public void OnStrongGoldenAle()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(true);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle.SetActive(false);
        }

        public void OnKiuluPaleAle()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(false);
            kiuluPaleAle.SetActive(true);
        }

        public void OnHallaWhiteAle()
        {
            infoScreen.SetActive(false);
            kajoPaleAle.SetActive(false);
            stout.SetActive(false);
            porter.SetActive(false);
            lentoWhiteAle.SetActive(false);
            brownAle.SetActive(false);
            strongGoldenAle.SetActive(false);
            hallaWhiteAle.SetActive(true);
            kiuluPaleAle.SetActive(false);
        }
    }
}
