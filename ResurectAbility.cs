using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResurectAbility : MonoBehaviour
{
    [SerializeField] GameObject[] followerPrefabs;
    [SerializeField] Transform followerSpawnPoint;

    GameObject corpse;
    public int followerPrefabIndex;

    void OnSpecial(InputValue value)
    {
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
        print(body);
        corpse = body;
        followerPrefabIndex = body.GetComponent<Corpse>().GetFollowerPrefabIndex();
    }

    void Resurrect()
    {
        if (followerPrefabIndex != -1)
        {
            print("Special");
            Vector3 followerSpawnPosition = followerSpawnPoint.transform.position;
            Instantiate(followerPrefabs[followerPrefabIndex], followerSpawnPosition, followerSpawnPoint.rotation);
            Destroy(corpse);
        }
        else
        {
            print("Failed Spell");
            // Trigger failed spell effects
        }
    }
}
