using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private GameObject grabbedObj;
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

    private GameObject lastWall = null;

    private bool isAttacking = false;
    private bool isGrabbing = false;
    private bool canWalljump = false;

    public bool IsAttacking { get => isAttacking; }
    public bool IsGrabbing { get => isGrabbing; set => isGrabbing = value; }
    public bool CanWalljump { get => canWalljump; set => canWalljump = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("RunSpeed", runningSpeed);
        controller = GetComponent<CharacterController>();
        
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

        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }


        // Grab
        if (Input.GetButton("Attack 2"))
        {
            isGrabbing = true;
            if (teleAtk.Closest != null && grabbedObj == null)
                grabbedObj = teleAtk.Closest;
        }

        if (Input.GetButtonUp("Attack 2"))
        {
            isGrabbing = false;
        }



        // Move Left-Right or Idle
        if (Input.GetButton("Horizontal") ) // && !IsGrabbing
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

            if (canWalljump && isJumping)
            {
                lastWall = GetComponentInChildren<WallJumpDetection>().Wall;
                this.transform.eulerAngles = (this.transform.eulerAngles.y == 90) ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);
                anim.SetTrigger("WallJump");
            }

            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);


            currNVel = playerVelocity;
            this.isJumping = true;
        }

        // Only use controller for vertical jumping
        if (this.isJumping)
        {
            // For controlling mid-air
            if (airControl)
            {
                if (Input.GetButton("Horizontal"))
                {

                    if (playerVelocity.x == 0)
                    {

                        playerVelocity.x = -Input.GetAxis("Horizontal") * 3f * runningSpeed;
                    }
                    else if (playerVelocity.x / Mathf.Abs(playerVelocity.x) != -Input.GetAxis("Horizontal"))
                    {
                        playerVelocity.x = -currNVel.x * jumpInputInfluence;
                        currNVel = playerVelocity;
                    }

                }
            }

            canJump = false;


            if (canWalljump && lastWall != GetComponentInChildren<WallJumpDetection>().Wall)
            {
                canJump = true;
            }

            controller.Move(new Vector3(playerVelocity.x, playerVelocity.y, 0) * Time.deltaTime);
        }
        else
        {
            lastWall = null;
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
            int atkPhase = -1;

            if (Input.GetButtonDown("Attack 1"))
            {
                if (anim.GetCurrentAnimatorClipInfo(1)[0].clip.name == "First Hit")
                {
                    atkPhase = 1;
                }
                else if (anim.GetCurrentAnimatorClipInfo(1)[0].clip.name == "Second Hit")
                {
                    atkPhase = 2;
                }

                anim.SetBool("isAttacking", true);
                anim.SetInteger("AttackPhase", atkPhase);
            }


        }
        else
        {
            isAttacking = false;
            anim.SetInteger("AttackPhase", -1);

            if (Input.GetButtonDown("Attack 1") && !IsGrabbing)
            {
                anim.SetBool("isAttacking", true);
                anim.SetInteger("AttackPhase", 0);
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

        // TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO

        if (grabbedObj && !isGrabbing)
        {
            Rigidbody enemyrigid = grabbedObj.GetComponent<Rigidbody>();
            grabbedObj.GetComponent<MeshRenderer>().material.color = Color.red;
        
            if (Vector3.Distance(enemyrigid.transform.position, this.transform.position) < 2f)
            {
                enemyrigid.isKinematic = true;
                enemyrigid.velocity = Vector3.zero;
                IsGrabbing = false;

                grabbedObj.GetComponent<MeshRenderer>().material.color = Color.white;

                grabbedObj = null;

            }
            else
            {
                enemyrigid.isKinematic = false;
                enemyrigid.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z));
                enemyrigid.velocity += enemyrigid.transform.forward * 90f * Time.deltaTime;
            }
        }

        Debug.Log(IsGrabbing);


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

    public void Attacked()
    {
        AttackCollider atk = GetComponentInChildren<AttackCollider>();

        if (atk.Enemy.Count > 0)
        {
            foreach (EnemyHealth enemy in atk.Enemy)
            {
                if(enemy)
                enemy.TakeDamage(atk.CharacterStats.attack);
            }
        }

        atk.Enemy.Clear();
    }
}
