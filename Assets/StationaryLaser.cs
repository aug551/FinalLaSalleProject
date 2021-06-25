using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryLaser : MonoBehaviour
{
    [SerializeField] bool isHorizontal;
    LineRenderer line;
    public float laserStartSize = 0.1f;

    public LineRenderer Line { get => line; set => line = value; }
    public bool IsHorizontal { get => isHorizontal; set => isHorizontal = value; }

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        PositionLaser();
    }
    void PositionLaser()
    {
        if (isHorizontal)
        {
            line.enabled = false;
            line.endWidth = laserStartSize *5;
            line.startWidth = laserStartSize *5;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, new Vector3(transform.position.x - 20f, transform.position.y, transform.position.z));
        }
        else
        {
            line.enabled = false;
            line.endWidth = laserStartSize;
            line.startWidth = laserStartSize;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, new Vector3(transform.position.x, transform.position.y - 20f, transform.position.z));
        }
    }
    public void IncreaseLaserSize(float maxsize)
    {
        StartCoroutine(IncreaseLaserSizeCoroutine(maxsize));
    }

    public void DecreaseLaserSize()
    {
        StartCoroutine(DecreaseLaserSizeCoroutine());
    }

    IEnumerator IncreaseLaserSizeCoroutine(float maxsize)
    {
        float interT = 0;
        int i = 0;
        float startWidth = line.startWidth;
        float endWidth = line.endWidth;

        while (line.startWidth <= maxsize)
        {
            interT += 6 * Time.deltaTime;
            line.startWidth = Mathf.Lerp(startWidth, maxsize, interT);
            line.endWidth = Mathf.Lerp(endWidth, maxsize, interT);
            i++;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    IEnumerator DecreaseLaserSizeCoroutine()
    {
        float interT = 0;
        int i = 0;
        float startWidth = line.startWidth;
        float endWidth = line.endWidth;

        while (line.startWidth >= laserStartSize)
        {
            interT += 6 * Time.deltaTime;
            line.startWidth = Mathf.Lerp(startWidth, laserStartSize, interT);
            line.endWidth = Mathf.Lerp(endWidth, laserStartSize, interT);
            i++;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}
