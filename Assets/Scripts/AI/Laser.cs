using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : IState
{
    LineRenderer laserLineRenderer;
    Transform eyepos;
    public float laserSize = 0.1f;
    Animator animator;
    RaycastHit hit;

    public Laser(TheBoss theBoss)
    {
       animator = theBoss.animator;
       Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
       laserLineRenderer.SetPositions(initLaserPositions);
       laserLineRenderer.endWidth = laserSize;
       laserLineRenderer.startWidth = laserSize;
    }

    public override IEnumerator Enter()
    {
        canTransition = false;
        laserLineRenderer.enabled = true;
        animator.SetBool("laser", true);
        if (Physics.Raycast(eyepos.position, Vector3.forward, out hit))
        {
            laserLineRenderer.SetPosition(0, hit.point);
        }
        yield return new WaitForSeconds(3f);

    }
    public override IEnumerator Exit()
    {
        canTransition = false;
        yield return new WaitForSeconds(3f);
    }
}
