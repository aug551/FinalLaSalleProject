using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrapActivator : MonoBehaviour
{
    public Rigidbody rb;
    bool countDownUp = false;
    public bool canReset;
    public float countDownTime;
    private Vector3 startingPosition;
    public float secForReset;
    public ParticleSystem flamesParticles;
    public ParticleSystem ignitionParticles;
    public ParticleSystem lightParticles;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;

    }


    void Update()
    {

        if (countDownUp == true && canReset == true)
        {
            secForReset = secForReset - Time.deltaTime;

            if (secForReset <= 0)
            {
                transform.position = startingPosition;
                rb.isKinematic = true;
                rb.useGravity = false;
                secForReset = 0;
                canReset = false;
            }
        }

        if(rb.isKinematic==false)
        {
            FireOn();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
         
            

            countDownTime -= Time.deltaTime;
            //Debug.Log(countDownTime);

            if (countDownTime <= 0)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                countDownUp = true;
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") && countDownUp == true && canReset == true)
        {
            countDownTime = 3;   
        }


    }

    private void FireOn()
    {
        flamesParticles.Play();
        ignitionParticles.Play();
        lightParticles.Play();
    }


   
}

