using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damageDealt = 1f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float timeTillDestroy = 3f;
    Vector3 direction;
    public float GetDamage()
    {
        return damageDealt;
    }
    void Start()
    {
        GetComponent<Rigidbody>().velocity += transform.forward * projectileSpeed;
        Destroy(gameObject, timeTillDestroy);
    }
}
