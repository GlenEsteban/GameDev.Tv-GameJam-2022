using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingBehaviour : MonoBehaviour
{
    [SerializeField] bool isFollowing = true;
    
    CharacterManager characterManager;
    NavMeshAgent navMeshAgent;
    Transform target;

    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
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