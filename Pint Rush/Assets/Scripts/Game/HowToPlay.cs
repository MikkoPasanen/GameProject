using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class HowToPlay : MonoBehaviour
    {
        [SerializeField] private CustomerSpawnController csc;
        [SerializeField] private GameObject htp1;
        [SerializeField] private GameObject htp2;
        private bool gameStarted;

        private void Awake()
        {
            gameStarted = false;
        }

        public void OnHowToPlay()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void OnExitHowToPlay()
        {
            if(!gameStarted)
            {
                csc.StartSpawn();
                gameStarted = true;
            }
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void OnHtp1()
        {
            gameObject.SetActive(false);
            htp1.gameObject.SetActive(true);
        }
        public void OnHtp2()
        {
            gameObject.SetActive(false);
            htp2.gameObject.SetActive(true);
        }
    }
}
