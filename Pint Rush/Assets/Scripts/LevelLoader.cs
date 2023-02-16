using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PintRush
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;
        public string levelToLoad;
        public float transitionWaitTime = 1f;

        public void LoadLevel()
        {
            StartCoroutine(Transition(levelToLoad));
        }

        IEnumerator Transition(string levelToLoad)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionWaitTime);
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
