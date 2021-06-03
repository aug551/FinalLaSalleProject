using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTemplate : MonoBehaviour
{
    public Rigidbody rb;
    bool countDownUp = false;
    public int countDownTime;
    private Vector3 startingPosition;
    public int secForReset;

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(countDownTime.ToString());


    }

    //private void OnCollisionEnter(Collider col)
    //{
    //    Debug.Log("vampire is on the platform");

    //    if (col.gameObject.tag =="Player")
    //    {

    //        rb.isKinematic = false;
    //        rb.useGravity = true;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Vampire is alive");
            StartCoroutine(PlatformCountDown());
            
            if( countDownUp==true)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StopCoroutine(PlatformCountDown());
            countDownTime = 1;

        }if(other.CompareTag("Player")&&countDownUp==true)
        {
            ResetTime();
            
        }
    }



    public IEnumerator PlatformCountDown()
    {
        Debug.Log("Countdown started");
        
        yield return new WaitForSeconds(countDownTime);

        countDownUp = true;
    }

    public IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(secForReset);
    }

    private void ResetPosition()
    {
        // = startingPosition;
    }
}
