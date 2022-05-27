using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAbilities : MonoBehaviour
{
    [Header("Weapon Configurations")]
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform weaponSpawnPoint;
    Health health;
    void Start() 
    {
        health = GetComponent<Health>();
    }
    public void Ability()
    {
        if (health.GetIsDead()) {return;}
        
        Vector3 weaponSpawnPosition = weaponSpawnPoint.transform.position;
        Instantiate(weaponPrefab, weaponSpawnPosition, transform.rotation);
        GetComponent<Health>().TakeDamage(1f);
    }

    void OnSpecial()
    {
        Ability();
    }
}
