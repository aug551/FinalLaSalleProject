using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//************************Santiago Alvarez Cardenas************************ 
public class TimedFireTrap : MonoBehaviour
{
    public ParticleSystem flames;
    public ParticleSystem ignition;
    public ParticleSystem lightP;
    
    public bool canReset;
    public bool on;
    public float seconds;
    private float sec;



    private void Start()
    {
        sec = seconds;
    }


    private void Update()
    {
        Timer();
        Debug.Log("Timer: " + seconds);
    }




    private void TurnOn()
    {
        flames.Play();
        lightP.Play();
        ignition.Play();

    }


    private void TurnOff()
    {
        flames.Stop();
        lightP.Stop();
        ignition.Stop();

    }

    private void Timer()
    {
        seconds = seconds - Time.deltaTime;

        if(seconds<=0)
        {
            if(on==true)
            {
                TurnOff();
                seconds = sec;
                on = false;
                //Timer();
            }else if(on==false)
            {
                TurnOn();
                seconds = sec;
                on = true;
                //Timer();

            }

        }

    }















































    //public bool isOn;
    //private IEnumerator countDownOn;
    //private IEnumerator countDownOff;
    //public float secondsOn;
    //public float secondsOff;
    //public bool timeOnUp = false;
    //public bool timeOffUp = true;
    ////void Start()
    ////{

    //    isOn = true;

    //    countDownOn = StartCountDown(secondsOn);
    //    countDownOff = CountDownToReset(secondsOff);

    //    //TurnOn();
    //    //StartCoroutine(countDownOn);
    //    //TurnOff();
    //    //if (canReset == true)
    //    //{
    //    //    if(timeOnUp==true&&)
    //    //    StartCoroutine(countDownOff);

    //    //}

    //}

    //void Update()
    //{
    //    if (canReset == true)
    //    {

    //        StartCoroutine(countDownOn);
    //    //    if (timeOnUp == false && timeOffUp==true)
    //    //    {
    //    //        Debug.Log("Turning On");
    //    //        TurnOn();
    //    //        StartCoroutine(countDownOn);
    //    //        Debug.Log("ON done");

    //    //        timeOffUp = false;
    //    //    }
    //    //    if (timeOnUp == true && timeOffUp ==false)
    //    //    {
    //    //        Debug.Log("Turning Off");
    //    //        TurnOff();
    //    //        StartCoroutine(countDownOff);
    //    //        Debug.Log("OFF DONE");

    //    //    }if(timeOffUp==true && timeOnUp==true)
    //    //    {
    //    //        timeOnUp = false;
    //    //    }
    //    }

    //}

    //private IEnumerator StartCountDown(float secOn)
    //{
    //    timeOnUp = false;

    //    yield return new WaitForSeconds(secOn);

    //    Debug.Log("secOn=" + secOn);

    //    if (secOn >=secondsOn)
    //    {

    //        StopCoroutine(countDownOn);
    //        timeOnUp = true;


    //        Debug.Log("CountDownFinished");

    //    }
    //}

    //private IEnumerator CountDownToReset(float secOff)
    //{
    //    timeOffUp = false;

    //    yield return new WaitForSeconds(secOff);

    //    //if(secOff)
    //    //timeOffUp = true;
    //}

    //private void TurnOn()
    //{
    //    flames.Play();
    //    lightP.Play();
    //    ignition.Play();

    //}


    //private void TurnOff()
    //{
    //    flames.Stop();
    //    lightP.Stop();
    //    ignition.Stop();

    //}
    /////////////////////////////////////////////////////////////////////////////////////

    //bool countDouwnUp = false;

    ////public float countDownTime;




    //private bool timeOffUp = false;






    //void Update()
    //{
    //    if(isOn==true)
    //    {
    //        TurnOn();
    //        //TurnOff();

    //    }

    //}

    //private void TurnOn ()
    //{
    //    flames.Play();
    //    lightP.Play();
    //    ignition.Play();


    //    if (timeOnUp == false)
    //    {
    //        StartCountDown();
    //    }
    //}

    //private void TurnOff()
    //{
    //    flames.Stop();
    //    lightP.Stop();
    //    ignition.Stop();
    //}

    //private void StartCountDown()
    //{
    //    //secondsOff = secOff;

    //    if(secondsOn>=0&&timeOnUp==false)
    //    {
    //        secondsOn = secondsOn - Time.deltaTime;

    //    }if(secondsOn<=0)
    //    {

    //        Debug.Log("Reached 0 sec ON");
    //        timeOnUp = true;
    //        TurnOff();
    //        //secondsOn = 0;
    //        secondsOn = secOn;
    //        timeOffUp = false;

    //        StartCountToReset();
    //    }

    //}

    //private void StartCountToReset()
    //{
    //    if(secondsOff>=0 && timeOffUp==false)
    //    {
    //        secondsOff = secondsOff - Time.deltaTime;
    //    }else if(secondsOff<=0 && canReset==true)
    //    {
    //        timeOffUp = true;
    //        //secondsOn = secOn;
    //        secondsOff = secOff;

    //        timeOnUp = false;
    //        TurnOn();
    //    }
    //}
}
