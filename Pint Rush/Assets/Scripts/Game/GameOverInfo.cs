using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PintRush
{
    public class GameOverInfo : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI beerOne;
        [SerializeField] TextMeshProUGUI beerTwo;
        [SerializeField] TextMeshProUGUI beerThree;
        [SerializeField] TextMeshProUGUI secondsText;
        [SerializeField] GameManagement gm;
        [SerializeField] TextMeshProUGUI totalText; 

        public CustomerSpawnController csc;
        // Start is called before the first frame update
        void Start()
        {
            csc.enabled = false;
            beerOne.text = gm.GetBeerOneScore().ToString();
            beerTwo.text = gm.GetBeerTwoScore().ToString();
            beerThree.text = gm.GetBeerThreeScore().ToString();
            totalText.text = gm.GetTotalPoints().ToString();

            int minutes = Mathf.FloorToInt(CustomerSpawnController.timer / 60);
            int seconds = Mathf.FloorToInt(CustomerSpawnController.timer % 60);

            secondsText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}
