using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUp : MonoBehaviour
{
    [SerializeField] TheBoss boss;
    private void OnTriggerEnter(Collider other)
    {
        boss.IsPlayerUp = true;
    }
}
