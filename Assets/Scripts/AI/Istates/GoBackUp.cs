using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackUp : IState
{
    public GoBackUp(TheBoss theBoss1)
    {
        theBoss = theBoss1;
        canTransition = false;
    }

    public override IEnumerator Enter()
    {
        canTransition = false;
        yield return new WaitForSeconds(4f);
        theBoss.PlayerUpCollision.SetActive(true);
        while (!theBoss.IsPlayerUp)
        {
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.25f);
        theBoss.cinemachineShake.MoveCamera(false);
        theBoss.ground1.SetActive(true);
        theBoss.ground2.SetActive(true);
        canTransition = true;
    }
}
