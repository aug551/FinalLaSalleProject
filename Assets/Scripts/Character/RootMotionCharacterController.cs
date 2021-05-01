using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.0f;
    private bool isJumping = false;
    private bool canJump = false;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private Vector3 playerVelocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity if not grounded
        if (controller.isGrounded)
        {
            anim.applyRootMotion = true;
            anim.SetBool("Jumping", false);
            isJumping = false;
            canJump = true;
            if (playerVelocity.y < 0) playerVelocity.y = 0f;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }





        // Move Left-Right or Idle
        if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("Move", true);
            this.transform.eulerAngles =
                (Input.GetAxis("Horizontal") > 0) ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);

            if (!isJumping)
            {
                playerVelocity.x = anim.velocity.x;
            }
        }
        else
        {
            anim.SetBool("Move", false);
            if (!isJumping)
                playerVelocity.x = 0;
        }

        
        // Jumping
        if (Input.GetButtonDown("Jump") && (canJump || OnSlope()))
        {
            anim.applyRootMotion = false;
            anim.SetBool("Jumping", true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            this.isJumping = true;
            canJump = false;

        }

        // Only use controller for vertical jumping
        if (this.isJumping)
        {
            controller.Move(new Vector3(playerVelocity.x, playerVelocity.y, 0) * Time.deltaTime);

        }




        // No jitter/bouncing on slopes
        if (anim.velocity.x != 0 && OnSlope())
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        }

        Debug.Log(playerVelocity.x);

    }

    // return true if on slope
    private bool OnSlope()
    {
        if (isJumping) return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false;

    }

}
