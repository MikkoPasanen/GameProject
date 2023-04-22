using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class Life : MonoBehaviour
    {
        private int currentLives;
        private bool allLivesLost;

        [SerializeField] private SpriteRenderer lifeOne;
        [SerializeField] private SpriteRenderer lifeTwo;
        [SerializeField] private SpriteRenderer lifeThree;

        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Settings settings;


        private void Start()
        {
            currentLives = 3;
            allLivesLost = false;

            lifeOne.color = Color.red;
            lifeTwo.color = Color.red;
            lifeThree.color = Color.red;
        }

        public void LifeLost()
        {
            if(settings.GetVibrationState())
            {
                Handheld.Vibrate();
            }
            currentLives--;

            switch (currentLives)
            {
                case 2:
                    lifeOne.color = Color.black;
                    break;
                case 1:
                    lifeTwo.color = Color.black;
                    break;
                case 0: 
                    lifeThree.color = Color.black;
                    allLivesLost = true;
                    break;
                default:
                    Debug.Log("tomato");
                    break;
            }
            audioManager.PlayUpgradeFailedSound();
            
        }
        public bool GetALife() 
        {
            return this.allLivesLost;
        }
    }
}

