using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAttack : IState
{
    Animator anim;
    public RunningAttack(TheBoss theBoss1)
    {
        theBoss = theBoss1;
        anim = theBoss1.animator;
    }

    public override IEnumerator Enter()
    {
        Debug.Log("entered run");
        canTransition = false;
        anim.SetBool("Charge", true);
        yield return new WaitForSeconds(2.4f);
        theBoss.agent.SetDestination(theBoss.corner1.transform.position);
        while (!theBoss.agent.pathPending && theBoss.agent.remainingDistance > .55f)
        {
            theBoss.agent.SetDestination(theBoss.corner1.transform.position);
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("Charge", false);
        anim.SetBool("Charge", true);
        theBoss.agent.SetDestination(theBoss.corner2.transform.position);
        while (!theBoss.agent.pathPending && theBoss.agent.remainingDistance > .55f)
        {
            theBoss.agent.SetDestination(theBoss.corner2.transform.position);
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("Charge", false);
        canTransition = true;
        yield break;
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
