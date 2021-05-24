using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    Transform player;
    float firingDelay= 2.0f;
    float distance;
    //bool alreadyShot = false;
    bool playerInRange;
    [SerializeField] GameObject arrow;


    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if(distance<=10)
        {
            
            Invoke("Shoot", firingDelay);
        }
    }

    void Shoot()
    {
        //GameObject.Instantiate(arrow, );
    }
}
