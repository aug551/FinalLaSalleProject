using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private GameObject grabbedObj;
    private TeleAttackDetect teleAtk;
    private Controls controls;

    [SerializeField] private float runningSpeed = 1f;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private bool airControl = true;
    [SerializeField] [Range(0,1)] private float jumpInputInfluence = 1.0f;
    [SerializeField] private int jumpAmount = 2;
    private int currentJumpAmount = 0;
    private bool isJumping = false;
    private bool canJump = false;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 currNVel = Vector3.zero;


    private bool isDashing = false;
    private bool canDash = true;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.40f;
    [SerializeField] private float dashStopForce = 3f;
    [SerializeField] private float dashCooldown = 5f;

    private Vector3 controlVelocity = Vector3.zero;

    private GameObject lastWall = null;

    private bool isAttacking = false;
    private bool isGrabbing = false;
    private bool canWalljump = false;
    private bool isControlled = false;

    public bool IsAttacking { get => isAttacking; }
    public bool IsGrabbing { get => isGrabbing; set => isGrabbing = value; }
    public bool CanWalljump { get => canWalljump; set => canWalljump = value; }
    public bool IsControlled { get => isControlled; set => isControlled = value; }
    public GameObject GrabbedObj { get => grabbedObj; set => grabbedObj = value; }
    public bool IsDashing { get => isDashing; set => isDashing = value; }



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("RunSpeed", runningSpeed);
        controller = GetComponent<CharacterController>();
        teleAtk = GetComponentInChildren<TeleAttackDetect>();
        controls = GetComponent<Controls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlled)
        {
            controller.Move(controlVelocity * Time.deltaTime);
            
        }
        else
        {
            // Apply gravity if not grounded
            if (controller.isGrounded)
            {
                anim.SetBool("Jumping", false);
                isJumping = false;
                canJump = true;
                if (playerVelocity.y < 0) playerVelocity.y = 0f;
                currentJumpAmount = jumpAmount - 1;
            }
            else
            {
                playerVelocity.y += gravity * Time.deltaTime;
            }


            // Grab
            StartSecondaryAttack();


            // Move
            Move();


            // Jumping ----------------------------------------------------------------
            if (Input.GetButtonDown("Jump") && (canJump || OnSlope())) StartJumping();

            if (this.isJumping) Jump();
            else anim.applyRootMotion = true;



            // Dashing ---------------------------------------------
            if (Input.GetButtonDown("Dash") && canDash) StartDash();

            if (this.isDashing) Dash();



            // Attacks
            HandlePrimaryAttack();


            CorrectPosition();
        }

    }




    // Functions -------------------------------------------------------------------------------
    public void ControlCharacter(Vector3 velocity, float duration)
    {
        anim.applyRootMotion = false;
        this.controlVelocity = velocity;
        IsControlled = true;
        Invoke("FinishedControl", duration); 
    }
    private void FinishedControl()
    {
        isControlled = false;

    }
    
    private void Move()
    {
        // Move Left-Right or Idle
        if (Input.GetButton("Horizontal")) // && !IsGrabbing
        {
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
    }
    

    // Jumping
    private void StartJumping()
    {
        anim.applyRootMotion = false;
        anim.SetBool("Jumping", true);
        playerVelocity.y = 0f;

        if (isJumping)
        {
            if (canWalljump)
            {
                lastWall = GetComponentInChildren<WallJumpDetection>().Wall;
                //this.transform.eulerAngles = (this.transform.eulerAngles.y == 90) ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);
                anim.SetTrigger("WallJump");
            }
            if (currentJumpAmount > 0)
            {
                currentJumpAmount -= 1;
            }
        }

        playerVelocity.y = Mathf.Sqrt(jumpHeight * -4.0f * gravity);

        currNVel = playerVelocity;
        this.isJumping = true;
    }
    private void Jump()
    {
        // For controlling mid-air
        if (airControl)
        {
            AirControl();
        }
      
        int layerMask = 1 << 7;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.up, out hit, 2.1f, layerMask))
        {
            playerVelocity.y = -0.5f;
        }
        canJump = false;

        canJump = CanWalljump || currentJumpAmount > 0;

        controller.Move(new Vector3(playerVelocity.x, playerVelocity.y, 0) * Time.deltaTime);
    }
    private void AirControl()
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




    // Dashing
    private void StartDash()
    {
        anim.applyRootMotion = false;
        Physics.IgnoreLayerCollision(3, 6, true); //Reference https://docs.unity3d.com/ScriptReference/Physics.IgnoreLayerCollision.html
        DashDamage();
        // Called to stop dashing
        Invoke("FinishedDashing", dashDuration);

        // Called to give cooldowns to the dash
        Invoke("DashCooldown", dashCooldown);

        this.isDashing = true;
        this.canDash = false;
    }
    private void Dash()
    {
        playerVelocity.x = (isJumping) ? (this.transform.forward.x * dashDistance) / 2 : this.transform.forward.x * dashDistance;
        controller.Move(new Vector3(playerVelocity.x, 0, 0) * Time.deltaTime);
    }

    private void FinishedDashing()
    {
        isDashing = false;
        Physics.IgnoreLayerCollision(3, 6, false);
        this.playerVelocity.x = this.playerVelocity.x / dashStopForce;
    }

    private void DashDamage()
    {
        Vector3 dashStart = new Vector3(this.transform.position.x, (this.transform.position.y + 0.75f), this.transform.position.z);
        //float distance = this.transform.rotation.y <= 0 ? distance = -7.3f : distance = 7.3f;
        //Vector3 dashEnd = new Vector3((this.transform.position.x + distance), (this.transform.position.y + 0.75f), this.transform.position.z);
        //Debug.DrawLine(dashStart, dashEnd, Color.blue, 2.5f); 
        Vector3 forward = transform.TransformDirection(Vector3.forward * 7.3f); //Refernce https://docs.unity3d.com/ScriptReference/Transform.TransformDirection.html
        //Debug.DrawRay(dashStart, forward, Color.green, 1.0f);

        RaycastHit[] hits; //Refference https://docs.unity3d.com/ScriptReference/Physics.RaycastAll.html
        hits = Physics.RaycastAll(dashStart, forward, 7.3f);
        string alreadyHit = null;
        for (int i = 0; i < hits.Length; i++)
        {
            
            RaycastHit hit = hits[i];
            Debug.Log(hit.transform.name);
            if(hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth enemy) && alreadyHit!=hit.transform.name)
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(50);
                alreadyHit = hit.transform.name;
            }
        }


        
    }

    private void DashCooldown()
    {
        canDash = true;
    }
    // Attacks
    private void StartSecondaryAttack()
    {
        
        if (Input.GetButtonDown("Attack 2"))
        {
            if(!controls.isCraftingOpen)
            {
                if (!teleAtk.holdingBlock)
                {
                    teleAtk.stopPull = false;
                    isGrabbing = true;
                    teleAtk.SetClosestObject();
                    if (teleAtk.Closest != null && grabbedObj == null)
                    {
                        grabbedObj = teleAtk.Closest;
                    }
                }
                else
                {
                    teleAtk.ThrowBlock(teleAtk.closest.GetComponent<Rigidbody>());
                }
            }
        }

        if (Input.GetButtonUp("Attack 2"))
        {
            teleAtk.stopPull = true;
        }
    }

    private void HandlePrimaryAttack()
    {

        // Attack
        if (!controls.isCraftingOpen)
        {
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

                if (Input.GetButtonDown("Attack 1"))
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetInteger("AttackPhase", 0);
                    isAttacking = true;
                }
            }
        }
    }


    private void CorrectPosition()
    {
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

    

    private void ResetAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void Attacked()
    {
        AttackCollider atk = GetComponentInChildren<AttackCollider>();

        if (atk.Enemy.Count > 0)
        {
            Debug.Log("hit");
            int crit = Random.Range(0, 100);
            if (crit<atk.CharacterStats.critChance)
            {
                foreach (EnemyHealth enemy in atk.Enemy)
                {
                    if (enemy)
                        enemy.TakeDamage(atk.CharacterStats.attack*atk.CharacterStats.critDamageMultiplier);
                }
            }
            else
            {
                foreach (EnemyHealth enemy in atk.Enemy)
                {
                    if (enemy)
                        enemy.TakeDamage(atk.CharacterStats.attack);
                }
            }
        }

        atk.Enemy.Clear();
    }

    public void Turn()
    {
        //this.transform.eulerAngles = (this.transform.eulerAngles.y == 90) ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);
    }
}
