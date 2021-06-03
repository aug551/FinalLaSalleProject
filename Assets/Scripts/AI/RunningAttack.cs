using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAttack : IState
{
    public float laserStartSize = 0.1f;
    TheBoss theBoss;
    LaserEyes laserEyes;

    public RunningAttack(TheBoss theBoss1)
    {
        theBoss = theBoss1;
        laserEyes = theBoss1.laserEyes;
        theBoss.LineRenderer.enabled = false;
        theBoss.LineRenderer.SetPosition(0, theBoss.laserEyes.transform.position);
        theBoss.LineRenderer.endWidth = laserStartSize;
        theBoss.LineRenderer.startWidth = laserStartSize;
    }

    public override IEnumerator Enter()
    {
        int i = 0;
        canTransition = false;
        theBoss.animator.SetBool("Charge", true);
        yield return new WaitForSeconds(2f);
        theBoss.LineRenderer.startWidth = .5f; /*Mathf.Lerp(0, 0.1f, 0.2f + Time.deltaTime);*/
        theBoss.LineRenderer.endWidth = .5f; /*Mathf.Lerp(0, 0.1f, 0.2f + Time.deltaTime);*/
        while (i < 1000)
        {
            theBoss.LineRenderer.enabled = true;
            laserEyes.activateTheLasers(theBoss);
            i++;
            yield return new WaitForFixedUpdate();
        }
        theBoss.LineRenderer.enabled = false;
        yield return new WaitForSeconds(3f);
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
