using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAttack : IState
{
    Animator anim;
    private Transform lookattransform;

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
        RotateTowardsPlayer();
        theBoss.transform.rotation = new Quaternion(0, theBoss.transform.rotation.y, 0, theBoss.transform.rotation.w);
        yield return new WaitForSeconds(4f);
        anim.SetBool("Charge", false);
        yield return new WaitForSeconds(8.65f);
        do
        {
            RotateTowardsPlayer();
            yield return new WaitForFixedUpdate();

        } while (Quaternion.Angle(theBoss.transform.rotation, theBoss.targetRotation.rotation) > 0.01f);
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
