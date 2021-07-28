using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//************************Santiago Alvarez Cardenas****************************
public class KnightAttack : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Vampire slice");
            
            other.gameObject.GetComponent<CharacterHealth>().TakeDamage(10);

            Debug.Log(other.gameObject.GetComponent<CharacterHealth>().currentHealth);
        }
    }
}
