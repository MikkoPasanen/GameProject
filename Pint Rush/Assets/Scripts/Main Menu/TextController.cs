using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PintRush
{
    public class TextController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private InputReader inputReader;

        void Update()
        {
            //text.text = $""+inputReader.GetTouchInput();
        }
    }
}
