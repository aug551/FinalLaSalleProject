using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBlock : MonoBehaviour
{
    public CubeExplode explode;
    public bool fired = false;
    public Vector3 currentForce;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (fired)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyHealth>().TakeDamage(60);
                explode.Explode();
            }
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterHealth>().TakeDamage(20);
                explode.Explode();
            }
            if (other.CompareTag("Wall"))
            {
                explode.Explode();
            }
        }
    }

    private void Start()
    {
        explode = GetComponent<CubeExplode>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void EnemyThrowBlock(Rigidbody cube)
    {
        cube.velocity = Vector3.zero;
        cube.transform.LookAt(new Vector3 (player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z));
        cube.velocity = cube.transform.forward * 800f * Time.deltaTime;
    }
}
