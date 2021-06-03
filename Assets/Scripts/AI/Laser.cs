using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : IState
{
    public float laserStartSize = 0.1f;
    TheBoss theBoss;
    LaserEyes laserEyes;
    Animator anim;
    AudioSource audio;
    private float interT = 0f;

    public Laser(TheBoss theBoss1)
    {
       theBoss = theBoss1;
       anim = theBoss1.animator;
       audio = theBoss1.audioSource;
       laserEyes = theBoss1.laserEyes;
       theBoss.LineRenderer.enabled = false;
       theBoss.LineRenderer.SetPosition(0,theBoss.laserEyes.transform.position);
       theBoss.LineRenderer.endWidth = laserStartSize;
       theBoss.LineRenderer.startWidth = laserStartSize;
    }

    public override IEnumerator Enter(TheBoss theBoss1)
    {
        int i = 0;
        canTransition = false;
        anim.SetBool("Laser", true);
        yield return new WaitForSeconds(2f);
        //theBoss.LineRenderer.startWidth = .5f; /*Mathf.Lerp(0, 0.1f, 0.2f + Time.deltaTime);*/
        //theBoss.LineRenderer.endWidth = .5f; /*Mathf.Lerp(0, 0.1f, 0.2f + Time.deltaTime);*/
        audio.Play();
        while ( i < 3.5f / Time.fixedDeltaTime)
        {
            interT += 6 * Time.deltaTime;
            theBoss.LineRenderer.enabled = true;
            theBoss.LineRenderer.startWidth = Mathf.Lerp(0, 0.3f, interT);
            theBoss.LineRenderer.endWidth = Mathf.Lerp(0, 0.3f, interT);
            laserEyes.activateTheLasers(theBoss);
            i++;
            yield return new WaitForFixedUpdate();
        }
        audio.Stop();
        anim.SetBool("Laser", false);
        theBoss.LineRenderer.enabled = false;
        yield return null;
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
