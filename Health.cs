using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float deathDecayTime = 1f;
    [SerializeField] float startingHealth = 10f;
    [SerializeField] float currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (currentHealth == 0)
        {
            Destroy(gameObject, deathDecayTime);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Projectile")
        {
            float damageTaken = other.gameObject.GetComponent<Projectile>().GetDamage();
            TakeDamage(damageTaken);
        }
    }
}
