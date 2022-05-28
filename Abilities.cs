using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [Header("Ablity Configurations")]
    [SerializeField] bool isAbilityProjectile;
    [SerializeField] bool isAbilityMelee;
    [SerializeField] GameObject abilityPrefab;
    [SerializeField] Transform abilitySpawnPoint;
    [SerializeField] bool isSpecialProjectile;
    [SerializeField] bool isSpecialMelee;
    [SerializeField] GameObject specialPrefab;
    [SerializeField] Transform specialSpawnPoint;
    [SerializeField] bool isHealthConsuming;
    [SerializeField] float healthConsumed = 1f;
    [Tooltip("Each simple attack adds a charge.")]
    [SerializeField] int chargeRequiredTillSpecial = 2;
    Health health;
    int specialAttackChargeUp;

    public int GetChargeRequiredTillSpecial()
    {
        return chargeRequiredTillSpecial;
    }

    void Start() 
    {
        health = GetComponent<Health>();
    }
    void OnAbility()
    {

        if (specialAttackChargeUp < chargeRequiredTillSpecial)
        {
            Ability();
            specialAttackChargeUp ++;
        }
        else
        {
            SpecialAbility();
            specialAttackChargeUp = 0;
        }


    }
    public void Ability()
    {
        if (health.GetIsDead()) {return;}
        
        if (isAbilityProjectile)
        {
            Vector3 abilitySpawnPosition = abilitySpawnPoint.transform.position;
            Instantiate(abilityPrefab, abilitySpawnPosition, transform.rotation);
        }
        
        if (isAbilityMelee)
        {
            Vector3 abilitySpawnPosition = abilitySpawnPoint.transform.position;
            Instantiate(abilityPrefab, abilitySpawnPosition, transform.rotation, abilitySpawnPoint);
        }

        if (isHealthConsuming)
        {
            GetComponent<Health>().TakeDamage(healthConsumed);
        }
    }

    public void SpecialAbility()
    {
        if (health.GetIsDead()) {return;}

        if (isSpecialProjectile)
        {
            Vector3 specialSpawnPosition = specialSpawnPoint.transform.position;
            Instantiate(specialPrefab, specialSpawnPosition, transform.rotation);
        }

        if (isSpecialMelee)
        {
            Vector3 specialSpawnPosition = specialSpawnPoint.transform.position;
            Instantiate(specialPrefab, specialSpawnPosition, transform.rotation, specialSpawnPoint);
        }

        if (isHealthConsuming)
        {
            GetComponent<Health>().TakeDamage(healthConsumed);
        }
    }
}