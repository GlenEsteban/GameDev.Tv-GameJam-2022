using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    [Header("Attack State Configurations")]
    [SerializeField] GameObject weapon;
    [SerializeField] Transform weaponSpawnPoint;
    [SerializeField] float chaseDistance = 6f;
    [SerializeField] float timeBetweenAttacks = 1f;
    bool isCurrentCharacter;
    bool isStaying = false;
    bool isAttacking = false;
    float timeSinceLastAttack;
    CharacterManager characterManager;
    NavMeshAgent navMeshAgent;
    Transform targetDestination;

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
        FaceTarget(targetDestination);
    }
    void FaceTarget(Transform target)
    {
        transform.LookAt(target.transform); // Fix bug where it rotates instantly
    }
    void StartAttacking(GameObject target)
    {
        navMeshAgent.destination = target.transform.position;
        FaceTarget(target.transform);

        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            Vector3 weaponSpawnPosition = weaponSpawnPoint.transform.position;
            Instantiate(weapon, weaponSpawnPosition, transform.rotation);

            timeSinceLastAttack = 0;
        }
    }
    void StopFollowing()
    {
        navMeshAgent.enabled = false;
    }
}