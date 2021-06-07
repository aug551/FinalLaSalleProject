using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : IState
{
    public float laserStartSize = 0.1f;
    Animator anim;

    public Laser(TheBoss theBoss1) //constructor
    {
       theBoss = theBoss1;
       anim = theBoss1.animator;
       theBoss.LineRenderer.enabled = false;
       theBoss.LineRenderer.SetPosition(0,theBoss.laserEyes.transform.position);
       theBoss.LineRenderer.endWidth = laserStartSize;
       theBoss.LineRenderer.startWidth = laserStartSize;
       canTransition = false;
    }

    public override IEnumerator Enter()
    {
        Debug.Log("entered laser");
        canTransition = false;
        int i = 0;
        float interT = 0f;
        do
        {
            RotateTowardsPlayer();
            yield return new WaitForFixedUpdate();

        } while (Quaternion.Angle(theBoss.transform.rotation, theBoss.targetRotation.rotation) > 0.01f);
        anim.SetBool("Laser", true);
        yield return new WaitForSeconds(2f);
        Initlaser();
        while ( i < 3.3f / Time.fixedDeltaTime)
        {
            interT += 6 * Time.deltaTime;
            ActivateTheLasers(interT);
            i++;
            yield return new WaitForFixedUpdate();
        }
        Finishedlaser();
        yield break;
    }
    public void Finishedlaser()
    {
        theBoss.audioSource.Stop();
        anim.SetBool("Laser", false);
        theBoss.LineRenderer.enabled = false;
        canTransition = true;
    }

    public void Initlaser()
    {
        theBoss.LineRenderer.SetPosition(0, theBoss.laserEyes.transform.position);
        theBoss.audioSource.Play();
        theBoss.LineRenderer.enabled = true;
    }

    public void ActivateTheLasers(float interT)
    {
        theBoss.LineRenderer.enabled = true;
        theBoss.LineRenderer.startWidth = Mathf.Lerp(0, 0.3f, interT);
        theBoss.LineRenderer.endWidth = Mathf.Lerp(0, 0.3f, interT);
        theBoss.laserEyes.activateTheLasers(theBoss);
    }
}
