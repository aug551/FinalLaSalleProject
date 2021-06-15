using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float regenRateSeconds = 3.0f;
    public GameObject colliderDamage;
    public Slider hpSlider;
    public GameObject gameOverPanel;

    public CharacterStats characterStats;


    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        maxHealth = 100;
        currentHealth = 50;
        UpdateHpSlider();
        //InvokeRepeating("RegenHealth", 0.0f, regenRateSeconds); //soruce:https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * characterStats.defence;
        UpdateHpSlider();
        if (currentHealth<=0)
        {
            hpSlider.gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Heal(float amount)
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
        //hpSlider.value = currentHealth;
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
