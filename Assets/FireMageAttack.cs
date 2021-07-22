using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMageAttack : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject FireStone;
    GameObject spike;

    float distanceToAttack = 20f;
    float distanceToPlayer;
    bool attackStarted = false;
    Vector3 nextLocation;
    int amountOfCasts = 0;
    int amountOfCastsAllowed = 15;

    void Start()
    {
        player = GameObject.Find("Player");
        nextLocation = this.transform.position;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        if (distanceToPlayer <= distanceToAttack && player.transform.position.y + 0.5 >= this.transform.position.y)
        {
            if (!attackStarted)
            {
                SpikeAttack();
            }
        }
        if(spike != null)
        {
            if (spike.GetComponent<Spike>().hitPlayer)
            {
                CancelInvoke();
                nextLocation = this.transform.position;
                attackStarted = false;
            }
        }
        if(amountOfCasts>=amountOfCastsAllowed)
        {
            amountOfCasts = 0;
            CancelInvoke();
            nextLocation = this.transform.position;
            attackStarted = false;
        }
    }

    void SpikeAttack()
    {
        attackStarted = true;

        if (player.transform.position.x > this.transform.position.x)
        {
            InvokeRepeating("InstantiateNextStone", 0.25f, 0.25f);
        }
        else
        {
            InvokeRepeating("InstantiateNextStoneOtherSide", 0.25f, 0.25f);
        }
    }

    void InstantiateNextStone()
    {
        nextLocation.x = nextLocation.x + 1;
        spike = Instantiate(FireStone, nextLocation, Quaternion.identity);
        amountOfCasts++;
    }

    void InstantiateNextStoneOtherSide()
    {
        nextLocation.x = nextLocation.x - 1;
        spike = Instantiate(FireStone, nextLocation, Quaternion.identity);
        amountOfCasts++;
    }
}
