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
    [SerializeField] float lingerOnDeathTime = 3f;

    bool isDead;
    bool hasHandledDeath;
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
            print(gameObject.name + " got hurt by " + other.gameObject.name);
            
            float damageTaken = other.gameObject.GetComponent<Projectile>().GetDamage();
            TakeDamage(damageTaken);
        }
        if(other.gameObject.tag == "Melee")
        {
            print(gameObject.name + " got hurt by " + other.gameObject.name);

            float damageTaken = other.gameObject.GetComponent<Melee>().GetDamage();
            TakeDamage(damageTaken);

            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Rigidbody>().velocity -= transform.forward * 100;
            }
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
            if (tag != "Necromancer")
            {
                DropCorpse();
                Destroy(gameObject);
            }
            hasHandledDeath = true;
        }


    }
    void HandleFollowerDeath()
    {
        if (!hasHandledDeath && tag == "Follower")
        {
            if (characterManager.GetCurrentCharacter() == gameObject)
            {
                characterManager.ResetCurrentCharacter();
            }
            characterManager.RemoveCharacter(gameObject);
        }
    }
    void HandleNecromancerDeath()
    {
        if (!hasHandledDeath && tag == "Necromancer")
        {
            characterManager.ResetCurrentCharacter();
            characterManager.UpdateCharacterInControl();
            StartCoroutine(LingerOnDeath());
        }
    }
    IEnumerator LingerOnDeath()
    {
        yield return new WaitForSeconds(lingerOnDeathTime);
        FindObjectOfType<SceneLoader>().RestartLevel();
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