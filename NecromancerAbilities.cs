using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NecromancerAbilities : MonoBehaviour
{
    [SerializeField] GameObject[] followerPrefabs;
    [SerializeField] Transform followerSpawnPoint;
    [SerializeField] int damageFromResurrection;

    Health health;
    GameObject corpse;
    GameObject resurrectFX;
    ControlsUI controlsUI;
    CharacterManager characterManager;
    int followerPrefabIndex;
    bool isAbleToResurrect;

    public bool GetIsAbleToResurrect()
    {
        return isAbleToResurrect;
    }
    void Start() 
    {
        characterManager = FindObjectOfType<CharacterManager>();
        health = GetComponent<Health>();    
        controlsUI = FindObjectOfType<ControlsUI>();
        resurrectFX = transform.Find("FX").gameObject;
    }

    void OnAbility()
    {
        StartCoroutine(ResurrectFX());
    }
    void OnSpecialAbility(InputValue value)
    {
        if (health.GetIsDead()) {return;}
        
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
        if (body.GetComponent<Corpse>() != null)
        {
            corpse = body;
            followerPrefabIndex = body.GetComponent<Corpse>().GetFollowerPrefabIndex();
            isAbleToResurrect = true;
        }
        else
        {
            isAbleToResurrect = false;
        }
    }

    void Resurrect()
    {
        if (followerPrefabIndex != -1)
        {
            Vector3 followerSpawnPosition = followerSpawnPoint.transform.position;
            Instantiate(followerPrefabs[followerPrefabIndex], followerSpawnPosition, followerSpawnPoint.rotation, gameObject.transform);
            Destroy(corpse);
            resurrectFX.SetActive(false);
            health.TakeDamage(damageFromResurrection);
        }
    }

    IEnumerator ResurrectFX()
    {
        resurrectFX.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        resurrectFX.SetActive(false);
    }
}
