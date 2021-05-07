using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject colliderDamage;
    public Slider hpSLider;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("DamageBox"))
        {
            TakeDamage(24);
            UpdateHpSlider();
            colliderDamage.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth<=0)
        {
            Debug.Log("You suck");
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHpSlider();
    }

    void UpdateHpSlider()
    {
        hpSLider.value = currentHealth;
    }
}
