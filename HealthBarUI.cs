using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{
    Health health;
    Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpHealthBarUI();
    }
    void Update()
    {
        float healthPercent = (health.GetCurrentHealth() / health.GetStartingHealth());
        image.fillAmount = healthPercent;
    }

    void SetUpHealthBarUI()
    {
        Transform character = transform.parent.transform.parent;
        if (character.tag == "Follower")
        {
            print("Follower");
            transform.parent.Find("Warm Heart").gameObject.SetActive(false);
        }
        else if (character.tag == "Enemy")
        {
            print("Enemy");
            transform.parent.Find("Cold Heart").gameObject.SetActive(false);
        }
        
        health = transform.parent.transform.parent.GetComponent<Health>();
        image = GetComponent<Image>();
    }
}    
