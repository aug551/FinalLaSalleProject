using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreak : IState
{
    public GroundBreak(TheBoss Boss) //constructor
    {
        theBoss = Boss;
        canTransition = false;
    }

    public override IEnumerator Enter()
    {
        //17 lasers total
        canTransition = false;

        theBoss.animator.SetBool("SLAM", true);
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(theBoss.ground1);
        GameObject.Destroy(theBoss.ground2);
        theBoss.player.GetComponent<RootMotionCharacterController>().ControlCharacter(new Vector3(theBoss.transform.forward.x, theBoss.transform.forward.y, 0) * 40, 1f);
        yield return new WaitForSeconds(2f);

        canTransition = true;
        yield break;
    }

}
