using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Kill", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void Kill()
    {
        Destroy(gameObject);
    }
}
