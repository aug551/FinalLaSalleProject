using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTemplate : MonoBehaviour
{
    public Rigidbody rb;
    bool countDownUp = false;
    public bool canReset;
    public float countDownTime;
    private Vector3 startingPosition;
    public float secForReset;
    //private bool resetTimeFinished=false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;

    }

    // Update is called once per frame


    void Update()
    {

        if(countDownUp == true && canReset == true)
        {
            secForReset= secForReset - Time.deltaTime;

            if(secForReset<=0)
            {
                transform.position = startingPosition;
                rb.isKinematic = true;
                rb.useGravity = false;
                secForReset = 0;
                canReset = false;
            }
        }


        //Debug.Log(countDownTime.ToString());
        //if (countDownUp == true)
        //{


        //    ResetTime();
        //    if (resetTimeFinished == true)
        //    {
        //        ResetPosition();
        //    }

        //}
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
            Debug.Log("Vampire is on the platform");
            //StartCoroutine(PlatformCountDown());

            countDownTime -= Time.deltaTime;
            Debug.Log(countDownTime);

            if (countDownTime <= 0)//( countDownUp==true)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                countDownUp = true;
                //StopCoroutine(PlatformCountDown());


                

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
            if (other.CompareTag("Player")&&countDownUp==true&&canReset==true)
            {

                //StopCoroutine(PlatformCountDown());
                countDownTime = 3;
                //countDownUp = false;

            }
        
       
    }



    //public IEnumerator PlatformCountDown()
    //{
    //    Debug.Log(Time.time);
        
    //    yield return new WaitForSeconds(countDownTime);

    //    countDownUp = true;


    //    //ResetTime();
    //    //if (resetTimeFinished==true)
    //    //{
    //    //    ResetPosition();
    //    //}
    //}

    



//    public IEnumerator ResetTime()
//    {
//        print("time reset started");

//        yield return new WaitForSeconds(secForReset);

//       resetTimeFinished = true;

//}

//    private void ResetPosition()
//    {
//        Debug.Log("reset position started");
//        rb.isKinematic = true;
//        rb.useGravity = false;
//        transform.position = startingPosition;//
//        // = startingPosition;
//    }
}
