using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingBehaviour : MonoBehaviour
{
    CharacterManager characterManager;
    NavMeshAgent navMeshAgent;
    Transform target;
    Boolean isFollowing = true;

    public bool GetIsFollowing()
    {
        return isFollowing;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateCurrentCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentCharacter();
        
        if (gameObject != characterManager.GetCurrentCharacter())
        {
            StartFollowing();
        }
        else
        {
            StopFollowing();
        }
    }

    void UpdateCurrentCharacter()
    {
        if (target != characterManager.GetCurrentCharacter())
        {
            target = characterManager.GetCurrentCharacter().transform;
        }
    }
    void StartFollowing()
    {
        isFollowing = true;
        navMeshAgent.enabled = true;
        navMeshAgent.destination = target.position;
    }
    public void StopFollowing()
    {
        isFollowing = false;
        navMeshAgent.enabled = false;
    }
}
