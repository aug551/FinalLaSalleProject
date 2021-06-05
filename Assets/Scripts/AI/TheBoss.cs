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

        if (currentState.canTransition)
        {
            Debug.Log(currentState.canTransition);
            int i = Random.Range(0, allBaseStates.Count);
            currentState = allBaseStates[i];
            StartCoroutine(currentState.Enter());
        }
        //if(//boss health lower than 50% && onlyOnce)
        //{
        //    if (currentState.canTransition)
        //    {
        //        currentState = enrageState;
        //        StartCoroutine(currentState.Enter(this));
        //    }
        //}
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
