using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheBoss : MonoBehaviour
{
    IState currentState;
    List<IState> AllStates = new List<IState>();
    IState laserState;
    IState runningAttack;
    public Animator animator;
    public AudioSource audioSource;
    public LineRenderer LineRenderer;
    public Transform eyepos;
    public LaserEyes laserEyes;
    public GameObject corner1;
    public GameObject corner2;
    public NavMeshAgent agent;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        LineRenderer = GetComponent<LineRenderer>();
        laserState = new Laser(this);
        runningAttack = new RunningAttack(this);
        AllStates.Add(laserState);
        AllStates.Add(runningAttack);
    }

    void Start()
    {
        currentState = laserState;
        StartCoroutine(currentState.Enter(this));
    }
    
    void Update()
    {

    }

    //public void SwitchStates(IState state)
    //{
    //    currentState.Exit();
    //    if (currentState.canTransition)
    //    {
    //        currentState = state;
    //        currentState.Enter();
    //    }
    //}  
}
