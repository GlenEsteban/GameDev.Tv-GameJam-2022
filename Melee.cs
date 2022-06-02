using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] float damageDealt = 3f;
    [SerializeField] float timeTillDestroy = 1f;
    GameObject weaponUser;

    public float GetDamage()
    {
        return damageDealt;
    }
    void Start()
    {
        weaponUser = transform.parent.gameObject;
        transform.parent = null;
        tag = "Melee";
        Destroy(gameObject, timeTillDestroy);
    }
    
    private void Update() {
        transform.position = weaponUser.transform.position;
        transform.rotation = weaponUser.transform.rotation;
    }
    void OnCollisionEnter(Collision other) 
    {
        tag = "Untagged";
    }
}