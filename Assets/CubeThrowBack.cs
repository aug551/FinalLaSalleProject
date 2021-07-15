using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeThrowBack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ThrowableBlock>(out ThrowableBlock cube))
        {
            other.GetComponent<ThrowableBlock>().EnemyThrowBlock(other.GetComponent<Rigidbody>());
        }
    }
}
