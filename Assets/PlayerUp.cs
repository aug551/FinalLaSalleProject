using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUp : MonoBehaviour
{
    [SerializeField] TheBoss boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.IsPlayerUp = true;
            Debug.Log("up");
        }
    }
}
