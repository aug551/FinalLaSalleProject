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

        if (!rmc.IsGrabbing)
        {
            if (closest)
            {
                closest = null;
            }
        }

        HandleSecondaryAttack();
    }

    private void HandleSecondaryAttack()
    {
        // TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO

        if (rmc.GrabbedObj && !rmc.IsGrabbing)
        {
            Rigidbody enemyrigid = rmc.GrabbedObj.GetComponent<Rigidbody>();

            if (Vector3.Distance(enemyrigid.transform.position, this.transform.position) < 1.5f)
            {
                enemyrigid.isKinematic = true;
                enemyrigid.velocity = Vector3.zero;
                rmc.IsGrabbing = false;

                rmc.GrabbedObj.GetComponent<MeshRenderer>().material.color = Color.white;

                rmc.GrabbedObj = null;

            }
            else
            {
                enemyrigid.isKinematic = false;
                enemyrigid.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z));
                if (enemyrigid.transform.position.z >= 0)
                {
                    enemyrigid.velocity += new Vector3(enemyrigid.transform.forward.x, enemyrigid.transform.forward.y, -enemyrigid.velocity.z) * 90f * Time.deltaTime;
                }
                else
                {
                    enemyrigid.velocity += enemyrigid.transform.forward * 90f * Time.deltaTime;
                }
            }
        }
    }
}
