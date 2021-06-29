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
        while (!theBoss.IsPlayerUp)
        {
            yield return new WaitForFixedUpdate();
        }
        theBoss.ground1.SetActive(true);
        theBoss.ground2.SetActive(true);
        canTransition = true;
    }
}
