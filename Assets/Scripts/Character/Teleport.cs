using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    Teleport homeTele;
    Vector3 lastTele = new Vector3();

    void Start()
    {
        homeTele = GameObject.Find("HomeTeleport").GetComponent<Teleport>();
        player = GameObject.Find("Player");
        lastTele = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           if(this.gameObject == homeTele.gameObject && lastTele == Vector3.zero)
           {
                Debug.Log("cant tele from home");
           }
           else
           {
                if (this.gameObject == homeTele.gameObject)
                {
                    player.gameObject.transform.position = lastTele;
                    Debug.Log("lastTele");
                }
                else
                {
                    homeTele.lastTele = player.transform.position;
                    Debug.Log(player.transform.position);
                    Debug.Log(homeTele.transform.position);
                    player.gameObject.transform.position = homeTele.transform.position;
                    Debug.Log(player.transform.position);
                    Debug.Log(homeTele.transform.position);
                }
           }
        }
    }         
}
