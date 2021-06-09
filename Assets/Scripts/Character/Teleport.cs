using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public GameObject homeTele;
    public Transform lastTele;
    public bool alreadyTeleported;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           if(this.gameObject.transform.position==homeTele.transform.position && lastTele == null)
            {
                Debug.Log("cant tele from home");
            }
           else
            {
                if (homeTele.transform.position==this.gameObject.transform.position)
                {
                    //lastTele.position = this.gameObject.transform.position;
                    player.transform.localPosition = lastTele.position;
                }
                else
                {
                    lastTele.position = player.transform.position;
                    player.transform.position = homeTele.transform.position;
                }
            }
        }
    }         
}
