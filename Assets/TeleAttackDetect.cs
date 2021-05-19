using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleAttackDetect : MonoBehaviour
{
    private RootMotionCharacterController rmc;

    public List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject closest = null;
    private float distance = 0f;

    public GameObject Closest { get => closest;  }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private void Start()
    {
        rmc = GetComponentInParent<RootMotionCharacterController>();
    }

    void Update()
    {
        if (rmc.IsGrabbing)
        {
            if (enemiesInRange.Count > 0)
            {
                foreach (GameObject obj in enemiesInRange)
                {
                    if (closest == null)
                    {
                        closest = obj;
                    }
                    else
                    {
                        distance = Vector3.Distance(this.transform.parent.position, obj.transform.position);
                        if (distance < Vector3.Distance(this.transform.parent.position, closest.transform.position))
                        {
                            closest = obj;
                        }
                    }
                }
            }
        }

        if (Input.GetButtonUp("Attack 2"))
        {
            if (closest)
            {
                closest.GetComponent<MeshRenderer>().material.color = Color.white;
                closest = null;
                rmc.IsGrabbing = false;
            }
        }
    }
}
