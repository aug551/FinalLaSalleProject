using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float startingAttack;
    public float attack;
    public float startingDefence;
    public float defence;
    public float critChance;
    public float critDamageMultiplier;

    void Start()
    {
        startingAttack = 20;
        attack = startingAttack;
        startingDefence = 1;
        defence = startingDefence;
        critChance = 10.0f;
        critDamageMultiplier = 2.0f;
    }

    void Update()
    {
        
    }

    public void AddAttack(float attackMulti)
    {
        attack = startingAttack * attackMulti;
    }

    public void RemoveAttackStat(float attackMulti)
    {
        attack = startingAttack / attackMulti;
    }

    public void AddDefence(float defenceMulti)
    {
        defence = defence - defenceMulti;
    }

    public void RemoveDefenceStat (float defenceMulti)
    {
        defence = defence + defenceMulti;
    }
}
