using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator titleTransition;
    [SerializeField] Animator transition;
    [SerializeField] float titleSequenceTime = 2f;
    [SerializeField] float titleTransitionTime = 3f;
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
        print("start game!");
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(titleTransition, "FirstStart", nextLevelIndex, titleTransitionTime));
        StartCoroutine(FadeOut());
    }
    public void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(transition, "Start", currentLevelIndex, transitionTime));
    }
    public void SkipToNextLevel()
    {
        bool isLastLevel = (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex);
        if (!isLastLevel)
        {
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(LoadLevel(transition, "Start", nextLevelIndex, transitionTime));
        }
    }

    IEnumerator LoadLevel(Animator animator, string triggerName, int levelIndex, float waitTime)
    {
        animator.SetTrigger(triggerName);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(titleSequenceTime);
        titleTransition.SetTrigger("Start");
    }
}