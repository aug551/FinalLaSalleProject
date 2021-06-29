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
    bool laserRoomFinished;
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
        verticalLaserState = new VerticalLaserState(room);
        horizontalLaserState = new HorizontalLaserState(room);
        playerUpState = new GoBackUp(this);
        PlayerUpCollision.SetActive(false);
    }

    void Start()
    {
        currentState = groundBreak;
        StartCoroutine(currentState.Enter());
    }
    
    void Update()
    {
        if (health.currentHealth >= health.MaxHealth / 2)
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
        }
        else
        {
            if (!laserRoomFinished)
            {
                if (currentState.canTransition && onlyOnce)
                {
                    onlyOnce = false;
                    currentState = groundBreak;
                    StartCoroutine(currentState.Enter());
                }
                if (currentState.canTransition && currentState != verticalLaserState)
                {
                    currentState = verticalLaserState;
                    StartCoroutine(currentState.Enter());
                }
                if (currentState.canTransition)
                {
                    currentState = horizontalLaserState;
                    StartCoroutine(currentState.Enter());
                    laserRoomFinished = true;
                }
            }
            else
            {
                PlayerUpCollision.SetActive(true);
                currentState = playerUpState;
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
