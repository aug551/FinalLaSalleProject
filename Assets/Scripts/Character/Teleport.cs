using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    GameObject homeTele;
    Vector3 lastTele = new Vector3();
    public bool alreadyTele = false;
    public string compare;
    public string actual;

    void Start()
    {
        //homeTele = GameObject.Find("HomeTeleport").gameObject;
        player = GameObject.Find("Player").gameObject;
        lastTele = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Teleport"))
        {
           if(other.gameObject == homeTele.gameObject && lastTele == Vector3.zero)
           {
                Debug.Log("cant tele from home");
           }
           else
           {
                compare = other.name;
                if(!alreadyTele)
                {
                    if (other.gameObject == homeTele.gameObject)
                    {
                        player.gameObject.transform.position = lastTele;
                        alreadyTele = true;
                        actual = other.name;
                    }
                    else
                    {
                        lastTele = player.transform.position;
                        player.gameObject.transform.position = homeTele.transform.position;
                        alreadyTele = true;
                        actual = other.name;
                    }
                }
           }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(actual != compare)
        {
            alreadyTele = false;
        }
    }
}
