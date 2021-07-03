using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAttack : IState
{
    Animator anim;
    Transform lookattransform;
   

    public RunningAttack(TheBoss theBoss1)
    {
        theBoss = theBoss1;
        anim = theBoss1.animator;
    }

    public override IEnumerator Enter()
    {
        Debug.Log("entered run");
        canTransition = false;
        SetTargetRotation();
        do
        {
            RotateTowardsPlayer();
            yield return new WaitForFixedUpdate();

        } while (Quaternion.Angle(theBoss.transform.rotation, theBoss.targetRotation.rotation) > 0.01f);
        anim.SetBool("Charge", true);
        yield return new WaitForSeconds(2.9f); // taken from scream animation time
        if (!theBoss.isOnCorner1)
        {
            theBoss.isOnCorner1 = true;
            do
            {
                theBoss.Attack();
                theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, theBoss.corner1.transform.position, Time.fixedDeltaTime * 15);
                yield return new WaitForFixedUpdate();
            } while (Vector3.Distance(theBoss.transform.position, theBoss.corner1.transform.position) > 1f);
        }
        else
        {
            theBoss.isOnCorner1 = false;
            do
            {
                theBoss.Attack();
                theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, theBoss.corner2.transform.position, Time.fixedDeltaTime * 15);
                yield return new WaitForFixedUpdate();
            } while (Vector3.Distance(theBoss.transform.position, theBoss.corner2.transform.position) > 1f);
        }
        anim.SetBool("Charge", false);
        anim.SetBool("Attacking", false);
        theBoss.cinemachineShake.ShakeCamera(5f, 1f);
        anim.SetTrigger("HitWall"); //arrived at wall
        yield return new WaitForSeconds(2.5f);// taken from fall animation time
        yield return new WaitForSeconds(4.967f);// taken from fall animation time
        SetTargetRotation();
        do
        {
            RotateTowardsPlayer();
            yield return new WaitForFixedUpdate();

        } while (Quaternion.Angle(theBoss.transform.rotation, theBoss.targetRotation.rotation) > 0.01f);
        canTransition = true;
        yield break;
    }
}
