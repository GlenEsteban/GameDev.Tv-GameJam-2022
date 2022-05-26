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
    [SerializeField] bool isRemoved;
    [SerializeField] GameObject corpsePrefab;
    CharacterManager characterManager;
    public int characterIndex = -1;
    float timeSinceDeath;

    public bool GetIsDead()
    {
        return isDead;
    }
        public void SetCharacterIndex(int index)
    {
        characterIndex = index;
    }
        public int GetCharacterIndex()
    {
        return characterIndex;
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
        if (!isRemoved && tag == "Player")
        {
            print("removed");
            characterManager.RemoveCharacter(gameObject);
            isRemoved = true;
        }

        HandleDeathPhysics();
        timeSinceDeath += Time.deltaTime;

        if (timeSinceDeath > deathDecayTime)
        {
            DropCorpse();
            Destroy(gameObject);
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
