using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryLaser : MonoBehaviour
{
    RaycastHit hit;
    LineRenderer line;
    public float laserStartSize = 0.1f;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        PositionLaser();
    }
    void PositionLaser()
    {
        line.endWidth = laserStartSize;
        line.startWidth = laserStartSize;
        line.SetPosition(0, transform.position);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            line.SetPosition(1, hit.point);
        }
    }
    public void IncreaseLaserSize(float maxsize)
    {
        StartCoroutine(IncreaseLaserSizeCoroutine(maxsize));
    }

    IEnumerator IncreaseLaserSizeCoroutine(float maxsize)
    {
        float interT = 0;
        int i = 0;
        while (line.startWidth >= maxsize)
        {
            interT += 6 * Time.deltaTime;
            line.startWidth = Mathf.Lerp(0, 0.3f, interT);
            line.endWidth = Mathf.Lerp(0, 0.3f, interT);
            i++;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}
