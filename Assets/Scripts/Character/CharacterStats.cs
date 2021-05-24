using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float startingAttack;
    public float attack;
    public float startingDefence;
    public float defence;
    //public float critChance;
    //public float critDamageMultiplier;

    void Start()
    {
        startingAttack = 20;
        attack = startingAttack;
        startingDefence = 1;
        defence = startingDefence;
    }

    void Update()
    {
        
    }

    void AddAttack(float attackMulti)
    {
        attack = startingAttack * attackMulti;
    }

    void RemoveAttackStat(float attackMulti)
    {
        attack = startingAttack / attackMulti;
    }

    void AddDefence(float defenceMulti)
    {
        defence = defence - defenceMulti;
    }

    void RemoveDefenceStat (float defenceMulti)
    {
        defence = defence + defenceMulti;
    }
}
