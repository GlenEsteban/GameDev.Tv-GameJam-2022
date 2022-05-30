using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NecromancerAbilities : MonoBehaviour
{
    [SerializeField] GameObject[] followerPrefabs;
    [SerializeField] Transform followerSpawnPoint;

    Health health;
    GameObject corpse;
    public int followerPrefabIndex;

    void Start() 
    {
        health = GetComponent<Health>();    
    }

    void OnAbility()
    {
        // add ability
    }
    void OnSpecialAbility(InputValue value)
    {
        if (health.GetIsDead()) {return;}
        if (value.isPressed)
        {
            // Show charging UI
        }
        
        Resurrect();
    }
    void OnTriggerStay(Collider other) 
    {
        IdentifyCorpse(other.gameObject);
    }

    void OnTriggerExit()
    {
        followerPrefabIndex = -1;
    }

    void IdentifyCorpse(GameObject body)
    {
        if (body.GetComponent<Corpse>() == null) {return;}
        corpse = body;
        followerPrefabIndex = body.GetComponent<Corpse>().GetFollowerPrefabIndex();
    }

    void Resurrect()
    {
        if (followerPrefabIndex != -1)
        {
            Vector3 followerSpawnPosition = followerSpawnPoint.transform.position;
            Instantiate(followerPrefabs[followerPrefabIndex], followerSpawnPosition, followerSpawnPoint.rotation, gameObject.transform);
            Destroy(corpse);
        }
        else
        {
            // Trigger failed spell effects
        }
    }
}
