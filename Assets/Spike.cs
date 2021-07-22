using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public BoxCollider spike;
    public bool hitPlayer = false;
    void Start()
    {
        spike = this.gameObject.GetComponent<BoxCollider>();
        Invoke("DestroySpike", 0.3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            hitPlayer = true;
        }
    }

    void DestroySpike()
    {
        Destroy(this.gameObject);
    }
}
