using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterController : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("Move", true);
            this.transform.eulerAngles = 
                (Input.GetAxis("Horizontal") > 0) ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);

        }
        else
        {
            anim.SetBool("Move", false);
        }
    }
}
