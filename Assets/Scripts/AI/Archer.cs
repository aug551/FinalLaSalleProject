using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    float distance;
    bool alreadyShot = false;
    private Animator anim;
    private FindPlayer orbit;

    [SerializeField] private float timeToShoot = 1f;
    [SerializeField] [Range(1f, 30f)] private float projectileSpeed = 10f;
    [SerializeField] [Range(1f, 10f)] private float firingDelay = 2.0f;

    [SerializeField] GameObject arrow;
    [SerializeField] private Transform player;

    private LineRenderer lr;
    private float interT = 0f;
    private Vector3 projectileDirection = Vector3.zero;
    private Ray projectileRay;

    public Transform Player { get => player;}

    void Start()
    {
        // In the inspector, change this to the "Neck" of the player
        if(!player) player = GameObject.Find("PlayerNeck").transform;

        orbit = GetComponentInChildren<FindPlayer>();

        anim = GetComponent<Animator>();
        anim.SetFloat("AnimationSpeed", 1/timeToShoot);

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
            interT += 1/timeToShoot * Time.deltaTime;

            Aim(interT);

            if (interT >= 1)
            {
                Shoot();
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
        anim.SetBool("isAttacking", false);
        interT = 0f;
        lr.startWidth = 0f;
        lr.endWidth = 0f;
        lr.SetPosition(1, this.transform.position);
    }

    void Aim(float interp)
    {
        anim.SetBool("isAttacking", true);

        Vector3 direction = (player.position) - orbit.GetLocalPos().position;
        RaycastHit hit;
        projectileRay = new Ray(orbit.GetLocalPos().position, Vector3.Normalize(direction) * 10f);

        if (!alreadyShot && Physics.Raycast(projectileRay, out hit, 25f))
        {
            lr.SetPosition(0, orbit.GetLocalPos().position);
            lr.SetPosition(1, hit.point);
            projectileDirection = Vector3.Normalize(hit.point - orbit.GetLocalPos().position);
            Debug.Log(hit.transform.tag);
        }

        lr.startWidth = Mathf.Lerp(0, 0.1f, interp);
        lr.endWidth = Mathf.Lerp(0, 0.1f, interp);
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(orbit.GetLocalPos().position, projectileDirection, out hit, 25f))
        {
                lr.SetPosition(0, orbit.GetLocalPos().position);
                lr.SetPosition(1, hit.point);
        }
        else
        {
            lr.SetPosition(0, orbit.GetLocalPos().position);
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

        GameObject projectile = Instantiate(arrow, orbit.GetLocalPos().position, Quaternion.identity);
        Rigidbody rigid = projectile.GetComponent<Rigidbody>();
        projectile.transform.LookAt(player.transform.position);
        rigid.AddForce(projectile.transform.forward * this.projectileSpeed, ForceMode.Impulse);


    }

    private void ShootCooldown()
    {
        alreadyShot = false;
    }

}
