using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damageDealt = 1f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float timeTillDestroy = 3f;
    int ricochetCount;
    public float GetDamage()
    {
        return damageDealt;
    }
    void Start()
    {
        tag = "Projectile";
        GetComponent<Rigidbody>().velocity += transform.forward * projectileSpeed;
        Destroy(gameObject, timeTillDestroy);
    }

    void OnCollisionEnter(Collision other) 
    {
        ricochetCount ++;
        if (ricochetCount == 2)
        {
            tag = "Untagged";
        }
    }
}
