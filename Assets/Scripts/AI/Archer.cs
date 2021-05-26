using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    GameObject player;
    float firingDelay= 2.0f;
    float distance;
    bool alreadyShot = false;
    bool playerInRange;
    [SerializeField] GameObject arrow;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance<=10)
        {
            if (!alreadyShot)
            {
                alreadyShot = true;
                Invoke("Shoot", firingDelay);
            }
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(arrow, transform.position + new Vector3(0.4f,1.5f,0), Quaternion.identity);
        Rigidbody rigid = projectile.GetComponent<Rigidbody>();
        projectile.transform.LookAt(player.transform);
        rigid.AddForce(player.transform.position - transform.position, ForceMode.Impulse);
        alreadyShot = false;
    }
}
