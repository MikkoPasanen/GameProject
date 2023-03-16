using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PintRush
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator transition;
        [SerializeField] private string levelToLoad;
        [SerializeField] private float transitionWaitTime = 1f;

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
