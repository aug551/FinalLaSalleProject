using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpToLaser : IState
{
    Animator anim;
    public PlayerUpToLaser(TheBoss theBoss1)
    {
        theBoss = theBoss1;
        anim = theBoss1.animator;
    }

    public override IEnumerator Enter()
    {
        canTransition = false;
        anim.SetBool("Run", true);
        if (!theBoss.isOnCorner1)
        {
            theBoss.isOnCorner1 = true;
            do
            {
                theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, theBoss.corner1.transform.position, Time.fixedDeltaTime * 15);
                yield return new WaitForFixedUpdate();
            } while (Vector3.Distance(theBoss.transform.position, theBoss.corner1.transform.position) > 1f);
        }
        else
        {
            theBoss.isOnCorner1 = false;
            do
            {
                theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, theBoss.corner2.transform.position, Time.fixedDeltaTime * 15);
                yield return new WaitForFixedUpdate();
            } while (Vector3.Distance(theBoss.transform.position, theBoss.corner2.transform.position) > 1f);
        }
        anim.SetBool("Run", false);
        theBoss.playerUpFinished = true;
        canTransition = true;
        yield break;
    }
}
