using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PintRush
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        [SerializeField] TextMeshProUGUI timerText;

        private float timer = 0f;
        private int score = 0;
        private bool triggered = false;
        private bool inBucket = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Bottle" && !inputReader.GetTapState())
            {
                triggered = true;
                inBucket = true;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Bottle" && triggered && !inputReader.GetTapState())
            {
                triggered = true;
                inBucket = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Bottle" && triggered)
            {
                triggered = false;
                inBucket = false;
                if(timer > 250)
                {
                    Debug.Log("-5");
                    score-=5;
                }
                else if(timer > 200)
                {
                    Debug.Log("+1");
                    score+=1;
                }
                else if(timer > 175)
                {
                    Debug.Log("+2");
                    score += 2;
                }
                else
                {
                    Debug.Log("-1");
                    score--;
                }
            }
        }

        private void FixedUpdate()
        {
            if(inBucket && timer < 500)
            {
                timer++;
                timerText.text = $"Score: "+score+" Timer: " + timer;
            }
            if(!inBucket)
            {
                timer = 0f;
                timerText.text = $"Score: "+score+" Timer: "+timer;
                timerText.color = Color.cyan;
            }
            if(timer > 175)
            {
                timerText.color = Color.green;
            }
            if(timer > 200)
            {
                timerText.color = Color.yellow;
            }
            if(timer > 250)
            {
                timerText.color = Color.red;
            }
        }
    }
}
