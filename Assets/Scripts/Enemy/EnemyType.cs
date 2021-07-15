using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfEnemy { HULK, ZOMBIE, ARCHER }

public interface EnemyType
{
    public TypeOfEnemy GetEnemyType();

}