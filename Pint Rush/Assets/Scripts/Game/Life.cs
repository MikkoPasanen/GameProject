using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;

        private Animator animator;
        private bool soundPlayed;

        private Rigidbody2D rb2d;

        // !Updated in the Animator!
        public bool animationEnded;
        public bool playSound;

        private bool destroy;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.simulated = false;
            soundPlayed = false;
            playSound = false;
            animationEnded = false;
        }

        private void Update()
        {
            // playSound updated in animator
            if(destroy && !soundPlayed)
            {
                soundPlayed = true;
                audioManager.PlayGlassBreaking();
            }

            // animationEnded updated in animator
            if (animationEnded)
            {
                Destroy(gameObject);
                StartCoroutine(LifeLost(1f));
            }
        }

        public IEnumerator LifeLost(float waitTime)
        {
            rb2d.simulated = true;
            animationEnded = false;

            yield return new WaitForSeconds(waitTime);
            animator.SetBool("PlayLifeLostAnimation", true);
        }

        public void SetDestroy()
        {
            this.destroy = true;
        }
    }
}

