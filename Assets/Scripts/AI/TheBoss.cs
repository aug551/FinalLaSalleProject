using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBoss : MonoBehaviour
{
    IState currentState;
    List<IState> AllStates = new List<IState>();
    IState laserState;
    IState runningAttack;
    public Animator animator;
    public LineRenderer LineRenderer;
    public Transform eyepos;
    public LaserEyes laserEyes;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        LineRenderer = GetComponent<LineRenderer>();
        laserState = new Laser(this);
        runningAttack = new RunningAttack(this);
        AllStates.Add(laserState);
        AllStates.Add(runningAttack);
    }

    void Start()
    {

        currentState = laserState;
        StartCoroutine(currentState.Enter());
    }
    
    void Update()
    {
        //if (currentState.canTransition)
        //{
        //    int i = Random.Range(0, AllStates.Count);
        //    currentState = AllStates[i];
        //    currentState.Enter();
        //}
    }

    public void SwitchStates(IState state)
    {
        currentState.Exit();
        if (currentState.canTransition)
        {
            currentState = state;
            currentState.Enter();
        }
    }  
}
