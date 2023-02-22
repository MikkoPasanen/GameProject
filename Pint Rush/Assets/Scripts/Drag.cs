using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PintRush
{
    public class Drag : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private InputReader inputReader;

        private Vector3 dragPos;

        void Update()
        {
            if (inputReader.GetTapState())
            {
                text.text = $"Current: " + inputReader.GetTouchWorldPosition() + "\nStart: " + inputReader.GetTouchStartPosition() + "\nEnd: " + inputReader.GetTouchEndPosition();
                dragPos = inputReader.GetTouchWorldPosition();
                dragPos.z = 0;
                gameObject.transform.position = dragPos;
            }
        }
    }
}
