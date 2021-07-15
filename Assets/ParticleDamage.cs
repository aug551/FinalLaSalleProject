using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnParticleCollision(GameObject other)
    {
       
        Debug.Log("Its getting hot");

        if (other.CompareTag("Fire"))
        {
            this.GetComponent<CharacterHealth>().TakeDamage(0.2f);
        }

        if (other.CompareTag("Electricity"))
        {
            this.GetComponent<CharacterHealth>().TakeDamage(1);
        }
            
        
    }
}
