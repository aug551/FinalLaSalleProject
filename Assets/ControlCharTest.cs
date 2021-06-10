using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCharTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<RootMotionCharacterController>().ControlCharacter(Vector3.right * 100f, 1);

            Debug.Log(other.tag);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
