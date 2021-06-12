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
        SetTargetRotation();
        do
        {
            RotateTowardsPlayer();
            yield return new WaitForFixedUpdate();

        } while (Quaternion.Angle(theBoss.transform.rotation, theBoss.targetRotation.rotation) > 0.1f);
        anim.SetBool("Laser", true);
        yield return new WaitForSeconds(1.633f);//taken from jump anim
        theBoss.player.GetComponent<RootMotionCharacterController>().ControlCharacter(new Vector3(theBoss.transform.forward.x, theBoss.transform.forward.y, 0) * 40, 1f);
        yield return new WaitForSeconds(0.8f);
        yield return new WaitForSeconds(2f);
        theBoss.targetRotation.LookAt(theBoss.player.transform.position);
        Initlaser();
        while ( i < 1.65f / Time.fixedDeltaTime) //3,3f
        {
            RotateTowardsPlayer();
            interT += 6 * Time.deltaTime;
            ActivateTheLasers(interT);
            i++;
            yield return new WaitForFixedUpdate();
        }
        Finishedlaser();
        yield return new WaitForSeconds(3f);
        anim.SetBool("Laser", false);
        anim.SetBool("LaserDown", true);
        Initlaser();
        while (i < 3.3f / Time.fixedDeltaTime) //3,3f
        {
            RotateTowardsPlayer();
            interT += 6 * Time.deltaTime;
            ActivateTheLasers(interT);
            i++;
            yield return new WaitForFixedUpdate();
        }
        Finishedlaser();
        yield return new WaitForSeconds(2f);
        canTransition = true;
        anim.applyRootMotion = true;
        yield break;
    }

    public void Finishedlaser()
    {
        theBoss.audioSource.Stop();
        anim.SetBool("Laser", false);
        anim.SetBool("LaserDown", false);
        theBoss.LineRenderer.enabled = false;
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
