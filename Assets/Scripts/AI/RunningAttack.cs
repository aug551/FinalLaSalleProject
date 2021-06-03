using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAttack : IState
{
    public float laserStartSize = 0.1f;
    TheBoss theBoss;
    LaserEyes laserEyes;
    Animator anim;

    public RunningAttack(TheBoss theBoss1)
    {
        theBoss = theBoss1;
        anim = theBoss1.animator;
    }

    public override IEnumerator Enter(TheBoss theBoss1)
    {       
        canTransition = false;
        theBoss.animator.SetBool("Charge", true);
        theBoss.agent.SetDestination(theBoss.corner1.transform.position);
        while (theBoss.agent.pathPending && theBoss.agent.remainingDistance > 1.35f)
        {
            theBoss.agent.SetDestination(theBoss.corner1.transform.position);
            yield return new WaitForFixedUpdate();
        }
        theBoss.animator.SetBool("Charge", false);
        theBoss.animator.SetBool("Charge", true);
        theBoss.agent.SetDestination(theBoss.corner2.transform.position);
        while (theBoss.agent.pathPending && theBoss.agent.remainingDistance > 1.35f)
        {
            theBoss.agent.SetDestination(theBoss.corner2.transform.position);
            yield return new WaitForFixedUpdate();
        }     
    }
    public void Finishedlaser()
    {

    }

    public override IEnumerator Exit()
    {
        canTransition = false;
        yield return new WaitForSeconds(3f);
    }
}
