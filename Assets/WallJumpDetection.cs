using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpDetection : MonoBehaviour
{
    RootMotionCharacterController rmcc;
    GameObject wall;

    public GameObject Wall { get => wall; set => wall = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            wall = other.gameObject;
            rmcc.CanWalljump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            rmcc.CanWalljump = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rmcc = GetComponentInParent<RootMotionCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
