using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//***********************Santiago Alvarez Cardenas*********************************
//Reference: https://www.youtube.com/watch?v=9KdY4mafG_E
public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points;
    public int pointnumber = 0;
    private Vector3 currentTarget;
    public float tolerance;
    public float speed;
    public float delayTime;

    private float delayStart;

    public bool automatic;

    void Start()
    {
        if(points.Length>0)
        {
            currentTarget = points[0];
        }
        tolerance = speed * Time.deltaTime;
    }

    
    void Update()
    {
        if(transform.position!=currentTarget)
        {
            MovePlatform();
        }else
        {
            UpdateTarget();
        }
    }

    void MovePlatform()
    {
        Vector3 heading = currentTarget - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if(heading.magnitude<tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    void UpdateTarget()
    {
        if(automatic)
        {
            if(Time.time-delayStart>delayTime)
            {
                NextPlatform();
            }
        }
    }


    public void NextPlatform()
    {
        pointnumber++;
        if(pointnumber>=points.Length)
        {
            pointnumber = 0;
        }
        currentTarget = points[pointnumber];
    }


    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
