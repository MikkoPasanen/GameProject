using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PintRush
{
    public class Bottle : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;

        private Vector3 dragPos;

        private void OnMouseDown()
        {
            dragPos = inputReader.GetTouchWorldPosition();
            dragPos.z = 0;
            gameObject.transform.position = dragPos;
        }
    }
}
