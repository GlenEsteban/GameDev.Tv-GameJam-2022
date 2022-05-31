using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalWords : MonoBehaviour
{
    [SerializeField] float timeTilTransition = 5f;
    [SerializeField] [TextArea] string finalWords;
    TextMeshProUGUI textMeshPro;
    Animator animator;
    void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        animator = transform.parent.GetComponent<Animator>();
        StartCoroutine(FinalWordsDisplay());
    }

    IEnumerator FinalWordsDisplay()
    {
        yield return new WaitForSeconds(timeTilTransition);
        textMeshPro.text = finalWords;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(timeTilTransition);
        animator.SetTrigger("End");
    }
}
