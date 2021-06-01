using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBoss : MonoBehaviour
{
    IState currentState;
    List<IState> AllStates = new List<IState>();
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        IState laserState = new Laser(this);
        IState DialogueState = new Laser(this);
        IState LastState = new Laser(this);
        animator = GetComponent<Animator>();
        currentState = DialogueState;
        StartCoroutine(currentState.Enter());
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentState.canTransition)
        {
            int i = Random.Range(0, AllStates.Count);
            currentState = AllStates[i];
            currentState.Enter();
        }
    }

    void SwitchStates(IState state)
    {
        currentState.Exit();
        if (currentState.canTransition)
        {
            currentState = state;
            currentState.Enter();
        }
    }  
}
