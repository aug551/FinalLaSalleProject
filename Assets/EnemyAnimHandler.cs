using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
public class EnemyAnimHandler : MonoBehaviour
{
    protected Animator anim;
    protected Enemy enemy;
    protected bool isPatrolling;
    protected bool isChasing;
    protected bool isAttacking;

    public void SyncVariables()
    {
        isAttacking = enemy.IsAttacking;
        isPatrolling = enemy.IsPatrolling;
        isChasing = enemy.IsChasing;
    }



}
