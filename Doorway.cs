using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] bool isGoal;
    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void OnCollisionEnter(Collision other) {
        if (isGoal)
        {
            if(other.gameObject.tag == "Necromancer")
            {
                sceneLoader.SkipToNextLevel();
            }
        }
    }
}
