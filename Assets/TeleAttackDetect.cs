using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleAttackDetect : MonoBehaviour
{
    public List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject closest = null;
    private float distance = 0f;

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

    void Update()
    {
        if (Input.GetButton("Attack 2"))
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

            Debug.Log(closest);
        }        
    }
}
