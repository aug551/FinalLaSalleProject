using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotate : MonoBehaviour
{
    [SerializeField] Transform character;

    void Update()
    {
        transform.RotateAround(character.position, Vector3.up, 20 * Time.deltaTime);
        transform.LookAt(character.position);
    }
}
