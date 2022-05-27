using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    [Header("Attack Configurations")]
    [SerializeField] float chaseDistance = 6f;
    [SerializeField] float timeBetweenAttacks = 1f;

    bool isCurrentCharacter;
    bool isStaying = false;
    bool isAttacking = false;
    float timeSinceLastAttack;
    CharacterManager characterManager;
    Health health;
    NavMeshAgent navMeshAgent;
    Transform targetDestination;

    public bool GetIsStaying()
    {
        return isStaying;
    }

    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        health = GetComponent<Health>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        GetComponent<SphereCollider>().radius = chaseDistance;
    }
    void Update()
    {
        if (health.GetIsDead()) {return;}

        CheckIfCurrentCharacter();
        timeSinceLastAttack += Time.deltaTime;

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
            StartAttacking(other.gameObject);
        }
        else
        {
            isAttacking = false;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        isAttacking = false;
    }

    void UpdateTarget()
    {
        if (targetDestination != characterManager.GetCurrentCharacter())
        {
            targetDestination = characterManager.GetCurrentCharacter().transform;
        }
    }

    void StartFollowing()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.destination = targetDestination.position;
        //FaceTarget(targetDestination); // Fix bug where it rotates instantly
    }
    void FaceTarget(Transform target)
    {
        transform.LookAt(target.transform); // Fix bug where it rotates instantly
    }
    void StartAttacking(GameObject target)
    {
        if (navMeshAgent.enabled != false)
        {
            navMeshAgent.destination = target.transform.position;
            FaceTarget(target.transform);
        }

        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            BroadcastMessage("Ability");

            timeSinceLastAttack = 0;
        }
    }
    void StopFollowing()
    {
        navMeshAgent.enabled = false;
    }
}