using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheBoss : MonoBehaviour
{
    IState currentState;
    IState laserState;
    IState runningAttackState;
    IState groundBreak;
    IState verticalLaserState;
    IState horizontalLaserState;
    IState playerUpState;
    IState PlayerUpToLaser;

    public Animator animator;
    public AudioSource audioSource;
    public LineRenderer LineRenderer;
    public Transform eyepos;
    public LaserEyes laserEyes;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject slamPosition;
    public NavMeshAgent agent;
    public GameObject player;
    public Transform targetRotation;
    public LaserRoom room;
    public bool isOnCorner1;
    public GameObject ground1;
    public GameObject ground2;
    public GameObject PlayerUpCollision;
    public CinemachineShake cinemachineShake;
    public bool IsPlayerUp;
    bool alreadyattacked;
    bool onlyOnce;
    bool onlyOnce2;
    public bool laserRoomFinished;
    public bool playerUpFinished;
    EnemyHealth health;

    float attackInterval = 0.75f;



    // state machine idea from https://www.youtube.com/watch?v=G1bd75R10m4&t=949s
    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        LineRenderer = GetComponent<LineRenderer>();
        laserState = new Laser(this);
        runningAttackState = new RunningAttack(this);
        groundBreak = new GroundBreak(this);
        verticalLaserState = new VerticalLaserState(room, this);
        horizontalLaserState = new HorizontalLaserState(room);
        playerUpState = new GoBackUp(this);
        PlayerUpToLaser = new PlayerUpToLaser(this);
        PlayerUpCollision.SetActive(false);
    }

    void Start()
    {
        currentState = laserState;
        StartCoroutine(currentState.Enter());
    }

    void Update()
    {
        //mini state machine

        //Default loop (laser and run)
        if (currentState.canTransition && health.currentHealth >= (health.MaxHealth / 2) && playerUpFinished)
        {
            if (currentState.canTransition && currentState != laserState)
            {
                currentState = laserState;
                StartCoroutine(currentState.Enter());
            }
            if (currentState.canTransition && currentState != runningAttackState)
            {
                currentState = runningAttackState;
                StartCoroutine(currentState.Enter());
            }
            return;
        }
        //Laser room loop (After boss falls below 50% hp)
        if (currentState.canTransition && !laserRoomFinished && !playerUpFinished)
        {
            if (currentState.canTransition && !onlyOnce)
            {
                onlyOnce = true;
                currentState = groundBreak;
                StartCoroutine(currentState.Enter());
            }
            if (currentState.canTransition)
            {
                currentState = verticalLaserState;
                StartCoroutine(currentState.Enter());
            }
        }
        //PlayerUp loop (After boss falls below 50% hp and player is finished laser room)
        if (currentState.canTransition && laserRoomFinished && !playerUpFinished)
        {
            if (currentState.canTransition && !onlyOnce2)
            {
                onlyOnce2 = true;
                currentState = playerUpState;
                StartCoroutine(currentState.Enter());
            }
            if (currentState.canTransition)
            {
                currentState = PlayerUpToLaser;
                StartCoroutine(currentState.Enter());
            }
        }
    }

    public void Attack()
    {
        if (!alreadyattacked)
        {
            alreadyattacked = true;
            animator = GetComponent<Animator>();
            animator.SetTrigger("Attacking");
            Invoke(nameof(AttackCD), attackInterval);
        }
    }

    void AttackCD()
    {
        alreadyattacked = false;
    }
    //public float DistanceFromPlayer()
    //{
    //   return Vector3.Distance(player.transform.position, transform.position);
    //}
    void ResetPosition()
    {
        animator = GetComponent<Animator>(); //because of weird bug
        animator.applyRootMotion = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
