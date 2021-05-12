using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float regenRateSeconds = 3.0f;
    public GameObject colliderDamage;
    public Slider hpSLider;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        InvokeRepeating("RegenHealth", 0.0f, regenRateSeconds); //soruce:https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
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
        UpdateHpSlider();
        if (currentHealth<=0)
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

    public void UpdateHpSlider()
    {
        hpSLider.value = currentHealth;
    }

    public void RegenHealth()
    {
        if(currentHealth<maxHealth)
        {
            currentHealth += 1;
            UpdateHpSlider();
        }
    }
}
