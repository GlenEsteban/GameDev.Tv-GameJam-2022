using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 6f;
    [SerializeField] float timeBetweenAttacks = 2f;
    bool isCurrentCharacter;
    bool isStaying = false;
    bool isAttacking = false;
    float timeSinceLastAttack;
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

        GetComponent<SphereCollider>().radius = chaseDistance;
    }
    void Update()
    {
        CheckIfCurrentCharacter();

        if (!isCurrentCharacter && !isStaying && !isAttacking)
        {
            UpdateTarget();
            StartFollowing();
        }
        else if (isCurrentCharacter || isStaying && !isAttacking)
        {
            StopFollowing();
        }
    }

    private void CheckIfCurrentCharacter()
    {
        if (gameObject == characterManager.GetCurrentCharacter())
        {
            isCurrentCharacter = true;
        }
        else
        {
            isCurrentCharacter = false;
        }
    }

    void OnStay(InputValue value)
    {
        if(value.isPressed)
        {
            isStaying = !isStaying;
        }
    }
    void OnTriggerStay(Collider other) 
    {
        if (isStaying || isCurrentCharacter) return;
        if (other.tag == "Enemy")
        {
            isAttacking = true;
            StartAttacking(other.transform.position);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        isAttacking = false;
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
    void StartAttacking(Vector3 attackTarget)
    {

        navMeshAgent.destination = attackTarget; 
    }
    void StopFollowing()
    {
        navMeshAgent.enabled = false;
    }
}