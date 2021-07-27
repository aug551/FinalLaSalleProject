using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Santiago Alvarez Cardenas
public class ElectricFence : MonoBehaviour
{
    public ParticleSystem electricParticles1;
    public ParticleSystem electricParticles2;
    public ParticleSystem electricParticles3;

    public ParticleSystem sparkExplosion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            Debug.Log("pikachu?");

            electricParticles1.Play();
            electricParticles2.Play();
            electricParticles3.Play();

            //other.gameObject.GetComponent<CharacterHealth>().TakeDamage(4);
            //other.gameObject.GetComponent<CharacterHealth>().TakeDamage(3);
            //other.gameObject.GetComponent<CharacterHealth>().TakeDamage(3);
        }


    }



    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {


            Instantiate(sparkExplosion, transform.position, Quaternion.identity);
            sparkExplosion.Play();
            Destroy(gameObject);

        }
    }
}
