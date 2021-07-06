using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refference : https://www.youtube.com/watch?v=s_v9JnTDCCY
public class CubeExplode : MonoBehaviour
{
    float cubeSize = 0.2f;
    float cubesInRow = 4;

    float cubePivotDistance;
    Vector3 cubesPivot;
    float explosionRadius = 5;
    float explosionForce = 150;

    void Start()
    {
        cubePivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubePivotDistance, cubePivotDistance, cubePivotDistance);
    }

    
    void Update()
    {
        
    }

    public void Explode()
    {
        gameObject.SetActive(false);
        for (int x = 0; x<cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    CreatePiece(x, y, z);
                }
            }
        }
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 5);
            }
        }

    }

    void CreatePiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position + new Vector3(cubeSize*x, cubeSize*y, cubeSize*z)- cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        piece.AddComponent<DestroyAfter2>();
    }
}
