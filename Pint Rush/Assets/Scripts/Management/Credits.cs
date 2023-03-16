using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class Credits : MonoBehaviour
    {
        public void OnCredits()
        {
            gameObject.SetActive(true);
        }

        public void OnExitCredits()
        {
            gameObject.SetActive(false);
        }
    }
} 
