using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTemplate : MonoBehaviour
{
    public Rigidbody rb;
    bool countDownUp = false;
    public int countDownTime;

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public IEnumerator PlatformCountDown()
    {
        Debug.Log("Countdown started");

        yield return new WaitForSeconds(countDownTime);

        countDownUp = true;
    }

}
