using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Santiago Alvarez Cardenas
public class Projectile : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public GameObject explosionEffect;
    public ParticleSystem fire;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //rb.velocity = transform.right * speed;
        fire.Play();
    }

    
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("Burn, Vampire!");
            other.GetComponent<CharacterHealth>().TakeDamage(10);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }

}
