using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private bool lifeLost = false;

        private void Update()
        {
            if(lifeLost == true)
            {
                Destroy(gameObject);
            }
        }
    }
}

