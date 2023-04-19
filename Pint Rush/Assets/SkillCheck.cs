using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class SkillCheck : MonoBehaviour
    {
        int timer = 0;
        [SerializeField] int timeLimit;
        [SerializeField] float rotationSpeed;

        [SerializeField] bool rotatingRight = false;

        //RectTransform transform;
        private void Start()
        {
            //transform = GetComponent<RectTransform>();
            rotatingRight = true;
        }

        private void FixedUpdate()
        {
            timer++;

            if(rotatingRight)
            {
                transform.Rotate(new Vector3(0, 0, -rotationSpeed));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, rotationSpeed));
            }

            Debug.Log(transform.localRotation.eulerAngles.z);

            if(rotatingRight && timer >= timeLimit)
            {
                timer = 0;
                rotatingRight = false;
            }
            if(!rotatingRight && timer >= timeLimit)
            {
                timer = 0;
                rotatingRight = true;
            }

        }
    }
}
