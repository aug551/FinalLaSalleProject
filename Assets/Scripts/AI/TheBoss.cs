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
    IState enrageState;
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
    public bool isOnCorner1;
    bool alreadyattacked;
    float attackInterval = 0.75f;


    // state machine idea from https://www.youtube.com/watch?v=G1bd75R10m4&t=949s
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        LineRenderer = GetComponent<LineRenderer>();
        laserState = new Laser(this);
        Debug.Log(animator);
        runningAttackState = new RunningAttack(this);
        Debug.Log(animator);
        enrageState = new Enrage(this);
        allBaseStates.Add(laserState);
        allBaseStates.Add(runningAttackState);
    }

    void Start()
    {
        currentState = runningAttackState;
        StartCoroutine(currentState.Enter());
    }
    
    void Update()
    {
        //if (currentState.canTransition)
        //{
        //    Debug.Log(currentState.canTransition);
        //    int i = Random.Range(0, allBaseStates.Count);
        //    currentState = allBaseStates[i];
        //    StartCoroutine(currentState.Enter());
        //}
        if (currentState.canTransition && currentState != runningAttackState)
        {
            currentState = runningAttackState;
            StartCoroutine(currentState.Enter());
        }
        if (currentState.canTransition && currentState != laserState)
        {
            currentState = laserState;
            StartCoroutine(currentState.Enter());
        }
    }

    public void Attack()
    {
        if (!alreadyattacked)
        {
            alreadyattacked = true;
            animator.SetTrigger("Attack");
            Invoke(nameof(AttackCD), attackInterval);
            animator.SetBool("Attacking", true);
        }
    }
    void AttackCD()
    {
        alreadyattacked = false;
    }
}
