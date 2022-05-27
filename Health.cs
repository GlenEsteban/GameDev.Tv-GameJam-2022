using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField] float startingHealth = 10f;
    [SerializeField] float currentHealth;
    [SerializeField] float deathDecayTime = 1f;
    [SerializeField] bool isDead;
    [SerializeField] bool hasHandledDeath;
    [SerializeField] GameObject corpsePrefab;
    CharacterManager characterManager;
    float timeSinceDeath;

    public bool GetIsDead()
    {
        return isDead;
    }
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (currentHealth == 0)
        {
            isDead = true;
            HandleDeath();
        }
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Projectile")
        {
            float damageTaken = other.gameObject.GetComponent<Projectile>().GetDamage();
            TakeDamage(damageTaken);
        }
    }
        public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
    }

    void HandleDeath()
    {
        HandleFollowerDeath();
        HandleNecromancerDeath();
        HandleDeathPhysics();

        timeSinceDeath += Time.deltaTime;

        if (!hasHandledDeath && timeSinceDeath > deathDecayTime)
        {
            DropCorpse();
            if (tag != "Necromancer")
            {
                Destroy(gameObject);
            }
            hasHandledDeath = true;
        }


    }
    private void HandleFollowerDeath()
    {
        if (!hasHandledDeath && tag == "Follower")
        {
            characterManager.RemoveCharacter(gameObject);
        }
    }
        private void HandleNecromancerDeath()
    {
        if (!hasHandledDeath && tag == "Necromancer")
        {
            // Special Game Over Death Sequence
        }
    }
    void HandleDeathPhysics()
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        transform.Rotate(.5f, 0f, 0f);
    }
    void DropCorpse()
    {
        Instantiate(corpsePrefab, transform.position, transform.rotation);
    }
}
