using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private Attack atk;

    [SerializeField] GameObject whip;

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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("RunSpeed", runningSpeed);
        controller = GetComponent<CharacterController>();
        atk = GetComponentInChildren<Attack>();
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
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        // Move Left-Right or Idle
        if (Input.GetButton("Horizontal"))
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
        if (Input.GetButtonDown("Attack 1"))
        {
            anim.SetTrigger("Attack");
        }





        // No jitter/bouncing on slopes
        if (anim.velocity.x != 0 && OnSlope())
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
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

    private void Attack()
    {
        whip.SetActive(true);

        List<GameObject> toRemove = new List<GameObject>();
        Debug.Log(atk.objectsInRange.Count);
        foreach (GameObject inRange in atk.objectsInRange)
        {
            if (inRange.CompareTag("Enemy"))
            {
                Destroy(inRange);
                toRemove.Add(inRange);
            }
        }

        foreach (GameObject obj in toRemove)
        {
            atk.objectsInRange.Remove(obj);
        }
    }

    private void FinishedAttack()
    {
        whip.SetActive(false);
    }

}
