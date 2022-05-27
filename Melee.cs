using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] float damageDealt = 3f;
    [SerializeField] float timeTillDestroy = 1f;

    public float GetDamage()
    {
        return damageDealt;
    }
    void Start()
    {
        tag = "Melee";
        Destroy(gameObject, timeTillDestroy);
    }

    void OnCollisionEnter(Collision other) 
    {
        tag = "Untagged";
    }
}
