using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] bool isStaying = false;
    
    CharacterManager characterManager;
    NavMeshAgent navMeshAgent;
    Transform target;

    public bool GetIsStaying()
    {
        return isStaying;
    }

    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (isStaying) {return;}

        UpdateTarget();
        
        if (gameObject != characterManager.GetCurrentCharacter())
        {
            StartFollowing();
        }
        else
        {
            StopFollowing();
        }
    }

        void OnStay(InputValue value)
    {
        if(value.isPressed)
        {
            isStaying = !isStaying;
        }
    }

    void UpdateTarget()
    {
        if (target != characterManager.GetCurrentCharacter())
        {
            target = characterManager.GetCurrentCharacter().transform;
        }
    }
    void StartFollowing()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.destination = target.position;
    }
    public void StopFollowing()
    {
        navMeshAgent.enabled = false;
    }
}