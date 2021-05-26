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
        projectile.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
        //projectile.transform.Rotate(new Vector3(0, 90, 0));
        //rigid.AddForce((player.transform.position - transform.position) * 0.5f, ForceMode.Impulse);
        rigid.AddForce(projectile.transform.forward, ForceMode.Impulse);
        alreadyShot = false;
    }
}
