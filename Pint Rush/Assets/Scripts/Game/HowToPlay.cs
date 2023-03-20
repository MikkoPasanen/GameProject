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

        }

        public void OnExitHowToPlay()
        {
            gameObject.SetActive(false);
        }
    }
}
