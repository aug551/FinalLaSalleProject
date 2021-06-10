using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSleep : MonoBehaviour
{
    GameObject player;
    bool canSleep = false;
    bool isAsleep = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (!isAsleep)
            {
                if (canSleep)
                {
                    isAsleep = true;
                    MovePlayer();
                }
            }
            else
            {
                isAsleep = false;
            }
        }
        if (isAsleep)
        {
            player.GetComponent<CharacterHealth>().Heal(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canSleep = true;
    }
    private void OnTriggerExit(Collider other)
    {
        canSleep = false;
    }

    void MovePlayer()
    {
        player.transform.position = this.gameObject.transform.position;
        player.transform.Rotate(0, 90, 0);
    }
}
