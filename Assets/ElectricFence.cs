using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFence : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("pikachu?");
            other.gameObject.GetComponent<CharacterHealth>().TakeDamage(4);
            other.gameObject.GetComponent<CharacterHealth>().TakeDamage(3);
            other.gameObject.GetComponent<CharacterHealth>().TakeDamage(3);
        }


    }
}
