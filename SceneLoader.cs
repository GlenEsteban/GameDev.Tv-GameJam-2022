using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator titleTransition;
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 1f;
    void OnRestartLevel()
    {
        RestartLevel();
    }
    void OnSkipLevel()
    {
        SkipToNextLevel();
    }

    public void StartGame()
    {
        titleTransition.SetTrigger("Start");
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(nextLevelIndex, transitionTime));
    }
    public void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(currentLevelIndex, transitionTime));
    }
        public void SkipToNextLevel()
    {
        bool isLastLevel = (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex);
        if (!isLastLevel)
        {
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(LoadLevel(nextLevelIndex, transitionTime));
        }
    }

    IEnumerator LoadLevel(int levelIndex, float waitTime)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelIndex);
    }
}