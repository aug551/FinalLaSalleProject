using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    float firingDelay= 2.0f;
    float distance;
    bool alreadyShot = false;
    bool playerInRange;
    [SerializeField] GameObject arrow;
    [SerializeField] private Transform player;

    private LineRenderer lr;
    private float interT = 0f;
    private Vector3 projectileDirection = Vector3.zero;
    private Ray projectileRay;


    void Start()
    {
        if(!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, this.transform.position);
        lr.startWidth = 0;
        lr.endWidth = 0;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance<=25f && !alreadyShot)
        {
            interT += 0.33f * Time.deltaTime;
            Aim(interT);

            if (interT >= 1)
            {
                Shoot();
                Debug.Log("Shoot");
                alreadyShot = true;
            }

        }
        else
        {
            ResetAim();
        }
    }

    private void ResetAim()
    {
        interT = 0f;
        lr.startWidth = 0f;
        lr.endWidth = 0f;
        lr.SetPosition(1, this.transform.position);
    }

    void Aim(float interp)
    {
        Vector3 direction = (player.position - new Vector3(0.4f, 1.5f, 0)) - this.transform.position;

        RaycastHit hit;
        projectileRay = new Ray(transform.position + new Vector3(0.4f, 1.5f, 0), Vector3.Normalize(direction) * 10f);

        if (!alreadyShot && Physics.Raycast(projectileRay, out hit, 25f))
        {
            lr.SetPosition(0, transform.position + new Vector3(0.4f, 1.5f, 0));
            lr.SetPosition(1, hit.point);
            projectileDirection = Vector3.Normalize(hit.point - this.transform.position);
        }

        lr.startWidth = Mathf.Lerp(0, 0.1f, interp);
        lr.endWidth = Mathf.Lerp(0, 0.1f, interp);
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, projectileDirection, out hit, 25f))
        {
                lr.SetPosition(0, transform.position + new Vector3(0.4f, 1.5f, 0));
                lr.SetPosition(1, hit.point);
        }
        else
        {
            lr.SetPosition(0, transform.position + new Vector3(0.4f, 1.5f, 0));
            lr.SetPosition(1, projectileDirection * 25f);
        }

        Invoke("SummonProjectile", 0.2f);
        Invoke("ShootCooldown", firingDelay);
    }

    private void SummonProjectile()
    {
        lr.startWidth = 0f;
        lr.endWidth = 0f;
        interT = 0f;

        GameObject projectile = Instantiate(arrow, transform.position + new Vector3(0.4f, 1.5f, 0), Quaternion.identity);
        Rigidbody rigid = projectile.GetComponent<Rigidbody>();
        projectile.transform.LookAt(player.transform.position);
        rigid.AddForce(projectile.transform.forward * 10, ForceMode.Impulse);


    }

    private void ShootCooldown()
    {
        alreadyShot = false;
    }

}
