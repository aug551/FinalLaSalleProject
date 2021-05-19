using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private Attack atk;
    private TeleAttackDetect teleAtk;

    [SerializeField] private float runningSpeed = 1f;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private bool airControl = true;
    [SerializeField] [Range(0,1)] private float jumpInputInfluence = 1.0f;
    private bool isJumping = false;
    private bool canJump = false;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 currNVel = Vector3.zero;


    private bool isDashing = false;
    private bool canDash = true;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.20f;
    [SerializeField] private float dashStopForce = 3f;
    [SerializeField] private float dashCooldown = 5f;

    private bool isAttacking = false;
    private bool isGrabbing = false;

    public bool IsAttacking { get => isAttacking; }
    public bool IsGrabbing { get => isGrabbing; set => isGrabbing = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("RunSpeed", runningSpeed);
        controller = GetComponent<CharacterController>();
        
        atk = GetComponentInChildren<Attack>();
        teleAtk = GetComponentInChildren<TeleAttackDetect>();

    }

    // Update is called once per frame
    void Update()
    {        
        // Apply gravity if not grounded
        if (controller.isGrounded)
        {
            anim.SetBool("Jumping", false);
            isJumping = false;
            canJump = true;
            if (playerVelocity.y < 0) playerVelocity.y = 0f;


            if (Input.GetButton("Attack 2"))
            {
                isGrabbing = true;
            }
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        // Move Left-Right or Idle
        if (Input.GetButton("Horizontal") && !IsGrabbing)
        {
            //anim.SetFloat("RunSpeed", runningSpeed);
            anim.SetBool("Move", true);
            this.transform.eulerAngles =
                (Input.GetAxis("Horizontal") > 0) ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);

            if (!isJumping)
                playerVelocity.x = anim.velocity.x;
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
            currNVel = playerVelocity;
            this.isJumping = true;
            canJump = false;
        }

        // Only use controller for vertical jumping
        if (this.isJumping)
        {
            // For controlling mid-air
            if (airControl)
            {
                if (Input.GetButton("Horizontal"))
                {
                    // If you press the opposite direction
                    if (playerVelocity.x / Mathf.Abs(playerVelocity.x) != -Input.GetAxis("Horizontal"))
                    {
                        playerVelocity.x = -currNVel.x * jumpInputInfluence;
                        currNVel = playerVelocity;
                    }

                }
            }

            controller.Move(new Vector3(playerVelocity.x, playerVelocity.y, 0) * Time.deltaTime);
        }
        else
        {
            anim.applyRootMotion = true;
        }



        // Dash
        if (Input.GetButtonDown("Dash") && canDash)
        {
            anim.applyRootMotion = false;
            Invoke("FinishedDashing", dashDuration);
            Invoke("DashCooldown", dashCooldown);
            this.isDashing = true;
            this.canDash = false;
        }

        if (this.isDashing)
        {
            playerVelocity.x = (isJumping) ? (this.transform.forward.x * dashDistance) / 2 : this.transform.forward.x * dashDistance;
            controller.Move(new Vector3(playerVelocity.x, this.transform.position.y, 0) * Time.deltaTime);
        }


        // Attack
        if (anim.GetCurrentAnimatorClipInfo(1).Length > 0)
        {
            if (Input.GetButtonDown("Attack 1"))
            {
                anim.SetBool("isAttacking", true);
            }

        }
        else
        {
            isAttacking = false;
            if (Input.GetButtonDown("Attack 1"))
            {
                anim.SetTrigger("Attack");
                isAttacking = true;
            }
        }

        // No jitter/bouncing on slopes
        if (anim.velocity.x != 0 && OnSlope())
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        }

        // Reset z position to 0 to avoid falling off
        if (!anim.applyRootMotion)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }

        if (this.IsGrabbing)
        {
            anim.applyRootMotion = false;
            controller.Move(Vector3.zero);

            if (teleAtk.Closest)
            {
                teleAtk.Closest.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }

        //Debug.Log(playerVelocity.x);

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

    private void FinishedDashing()
    {
        isDashing = false;
        this.playerVelocity.x = this.playerVelocity.x / dashStopForce;
    }

    private void DashCooldown()
    {
        canDash = true;
    }

    private void ResetAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
