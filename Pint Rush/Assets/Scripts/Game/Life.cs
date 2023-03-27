using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;

        private Animator animator;
        public bool animationEnded = false;
        public bool playSound = false;
        private bool soundPlayed = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(playSound && !soundPlayed)
            {
                soundPlayed = true;
                audioManager.PlayGlassBreaking();
            }

            if (animationEnded)
            {
                Destroy(gameObject);
            }
        }

        public void LifeLost()
        {
            animationEnded = false;
            animator.SetBool("PlayLifeLostAnimation", true);
        }
    }
}

