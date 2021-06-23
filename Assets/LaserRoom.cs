using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRoom : MonoBehaviour
{
    IState currentState;
    List<IState> allBaseStates = new List<IState>();
    IState VerticalLaserState;
    IState HorizontalLaserState;

    private void Awake()
    {
        //laserState = new Laser(this);
        //Debug.Log(animator);
        //runningAttackState = new RunningAttack(this);
        //Debug.Log(animator);
        //enrageState = new Enrage(this);
        //allBaseStates.Add(laserState);
        //allBaseStates.Add(runningAttackState);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
