using UnityEngine;

namespace PintRush
{
    public class BeerInfo : MonoBehaviour
    {
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject optionsButton;

        public void OnBeerInfo()
        {
            //Debug.Log("Beer info clicked!");

            gameObject.SetActive(true);
        }

        public void OnExitBeerInfo()
        {
            //Debug.Log("Beer info exited!");

            gameObject.SetActive(false);
        }
    }
}
