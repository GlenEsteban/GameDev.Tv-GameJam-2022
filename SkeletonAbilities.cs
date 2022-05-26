using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAbilities : MonoBehaviour
{
    [Header("Weapon Configurations")]
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform weaponSpawnPoint;
    public void Ability()
    {
        Vector3 weaponSpawnPosition = weaponSpawnPoint.transform.position;
        Instantiate(weaponPrefab, weaponSpawnPosition, transform.rotation);
    }

    void OnSpecial()
    {
        Ability();
    }
}
