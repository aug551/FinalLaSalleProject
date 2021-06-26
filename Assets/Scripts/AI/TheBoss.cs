using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheBoss : MonoBehaviour
{
    IState currentState;
    List<IState> allBaseStates = new List<IState>();
    IState laserState;
    IState runningAttackState;
    IState groundBreak;
    IState verticalLaserState;

    public Animator animator;
    public AudioSource audioSource;
    public LineRenderer LineRenderer;
    public Transform eyepos;
    public LaserEyes laserEyes;
    public GameObject corner1;
    public GameObject corner2;
    public NavMeshAgent agent;
    public GameObject player;
    public Transform targetRotation;
    public LaserRoom room;
    public bool isOnCorner1;
    public GameObject ground1;
    public GameObject ground2;
    public CinemachineShake cinemachineShake;
    bool alreadyattacked;
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
        allBaseStates.Add(verticalLaserState);
        allBaseStates.Add(groundBreak);
        allBaseStates.Add(laserState);
        allBaseStates.Add(runningAttackState);
    }

    void Start()
    {
        currentState = groundBreak;
        StartCoroutine(currentState.Enter());
    }
    
    void Update()
    {
        if (health.currentHealth <= health.MaxHealth / 2)
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
            if (currentState.canTransition)
            {
                currentState = verticalLaserState;
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
