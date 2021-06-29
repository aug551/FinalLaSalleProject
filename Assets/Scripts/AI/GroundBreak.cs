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
        theBoss.animator.SetBool("Run", true);
        do
        {
            theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, theBoss.slamPosition.transform.position, Time.fixedDeltaTime * 15);
            yield return new WaitForFixedUpdate();
        } while (Vector3.Distance(theBoss.transform.position, theBoss.slamPosition.transform.position) > 1f);
        theBoss.animator.SetBool("Run", false);
        theBoss.animator.SetTrigger("Slam");
        yield return new WaitForSeconds(1.6f);
        theBoss.cinemachineShake.ShakeCamera(5f, 0.75f);
        theBoss.ground1.SetActive(false);
        theBoss.ground2.SetActive(false);
        theBoss.player.GetComponent<RootMotionCharacterController>().ControlCharacter(new Vector3(theBoss.transform.forward.x, theBoss.transform.forward.y, 0) * 40, 1f);
        theBoss.cinemachineShake.MoveCamera(true);
        yield return new WaitForSeconds(2f);
        canTransition = true;
        yield break;
    }

}
