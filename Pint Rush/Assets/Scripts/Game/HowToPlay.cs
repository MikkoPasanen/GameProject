using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class HowToPlay : MonoBehaviour
    {

        public void OnHowToPlay()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void OnExitHowToPlay()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
