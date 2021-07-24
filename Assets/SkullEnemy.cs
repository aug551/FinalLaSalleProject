using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : MonoBehaviour
{
    public GameObject fireBall;
    public Transform instantiationPos;
    public Quaternion from;
    public Quaternion to;
    public Animator animator;
    public Vector3 rotationBall;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        rotationBall = transform.right;
        //from = transform.rotation;
        //transform.rotation + 359;
        //Invoke("ShootBall",2);
        animator.Play("scream");
        InvokeRepeating("ShootBall", 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //ShootBall();

        //instantiationPos = transform.LookAt(target);
    }

    private void ShootBall()
    {

        GameObject ball =  Instantiate(fireBall, instantiationPos.position, Quaternion.identity);

        ball.transform.parent = null;
        ball.GetComponent<Rigidbody>().velocity=(rotationBall*100*Time.deltaTime);

        Debug.Log(rotationBall);

        //rotationBall = transform.LookAt(target);

        //if (rotationBall == transform.right)
        //{
        //    rotationBall = transform.up;

        //} else if (rotationBall == transform.up)
        //{
        //    rotationBall=transform
        //}

    }

}
