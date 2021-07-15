using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hulk : MonoBehaviour, EnemyType
{
    public TypeOfEnemy GetEnemyType()
    {
        return TypeOfEnemy.HULK;
    }
}
