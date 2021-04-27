using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = -9.8f;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent <CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
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

            if (Input.GetButtonDown("Jump"))
            {
            }
        }

        Debug.Log(controller.isGrounded);

    }

}
