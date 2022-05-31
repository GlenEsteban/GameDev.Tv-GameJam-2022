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
    GameObject resurrectFX;
    ControlsUI controlsUI;
    CharacterManager characterManager;
    public int followerPrefabIndex;

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
        // controlsUI.UpdateResurrect1UI(false);
        // controlsUI.UpdateResurrect2UI(false);

    }

    void IdentifyCorpse(GameObject body)
    {
        if (body.GetComponent<Corpse>() == null) {return;}
        corpse = body;
        followerPrefabIndex = body.GetComponent<Corpse>().GetFollowerPrefabIndex();
        
        // if (characterManager.GetCharacterCount() > 1)
        // {
        //     controlsUI.UpdateResurrect2UI(true);
        // }
        // else
        // {
        //     controlsUI.UpdateResurrect1UI(true);
        // }
    }

    void Resurrect()
    {
        if (followerPrefabIndex != -1)
        {
            Vector3 followerSpawnPosition = followerSpawnPoint.transform.position;
            Instantiate(followerPrefabs[followerPrefabIndex], followerSpawnPosition, followerSpawnPoint.rotation, gameObject.transform);
            Destroy(corpse);
            resurrectFX.SetActive(false);
        }
    }

    IEnumerator ResurrectFX()
    {
        resurrectFX.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        resurrectFX.SetActive(false);
    }
}
