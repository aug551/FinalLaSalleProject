using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Santiago Alvarez Cardenas
public class ParticleDamage : MonoBehaviour
{
    public Animator anim;

   

    // Start is called before the first frame update
    void Start()
    {
        //isGettingBurnt = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetBool("onFire", false);
    }


    private void OnParticleCollision(GameObject other)
    {
       
        

        if (other.CompareTag("Fire"))
        {

            Debug.Log("Its getting hot");

            anim.SetBool("onFire", true);
            this.GetComponent<CharacterHealth>().TakeDamage(0.2f);
            

        } 

        if (other.CompareTag("Electricity"))
        {
            this.GetComponent<CharacterHealth>().TakeDamage(1);
        }
        if (other == null)
        {
            anim.SetBool("onFire", false);
        }
    }
}

