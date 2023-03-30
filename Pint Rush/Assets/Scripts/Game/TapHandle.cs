using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class TapHandle : MonoBehaviour
    {
        [SerializeField] private BeerTap beerTapParent;

        private void OnMouseDown()
        {
            beerTapParent.TryPouring();
        }
    }
}
