using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleAttackDetect : MonoBehaviour
{
    private RootMotionCharacterController rmc;
    [SerializeField] GameObject blockHolder;


    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject closest = null;
    private float distance = 0f;
    public bool stopPull = false;
    public bool holdingBlock = false;
    public Vector3 mousePos;

    public GameObject Closest { get => closest; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            enemiesInRange.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
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
        HandleSecondaryAttack();
    }

    public void SetClosestObject()
    {
        if (rmc.IsGrabbing)
        {
            if (enemiesInRange.Count > 0)
            {
                foreach (GameObject obj in enemiesInRange)
                {
                    if (obj.TryGetComponent<CubeCooldown>(out CubeCooldown cube))
                    {
                        if (!obj.GetComponent<CubeCooldown>().onCooldown)
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
                    else
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
        }

        if (!rmc.IsGrabbing)
        {
            if (closest)
            {
                closest = null;
            }
        }
    }

    private void HandleSecondaryAttack()
    {
        // TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO

        if (rmc.GrabbedObj && rmc.IsGrabbing)
        {
            Rigidbody enemyrigid = rmc.GrabbedObj.GetComponent<Rigidbody>();
            if (closest.TryGetComponent<CubeCooldown>(out CubeCooldown cube))
            {
                if (!closest.GetComponent<CubeCooldown>().onCooldown)
                {
                    if (Vector3.Distance(enemyrigid.transform.position, new Vector3(rmc.transform.position.x, rmc.transform.position.y + 1.0f, rmc.transform.position.z)) < 1.5f || stopPull)
                    {
                        closest.GetComponent<CubeCooldown>().onCooldown = true;
                        enemyrigid.isKinematic = true;
                        enemyrigid.velocity = Vector3.zero;
                        rmc.IsGrabbing = false;
                        rmc.GrabbedObj = null;
                        closest = null;
                    }
                    else
                    {
                        closest.GetComponent<Renderer>().material.color = Color.red;
                        enemyrigid.isKinematic = false;
                        enemyrigid.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z));
                        if (enemyrigid.transform.position.z > 0.2f || enemyrigid.transform.position.z < 0.2f)
                        {
                            enemyrigid.velocity += enemyrigid.transform.forward * 90f * Time.deltaTime;
                        }
                        else
                        {
                            enemyrigid.velocity += new Vector3(enemyrigid.transform.forward.x, enemyrigid.transform.forward.y, -enemyrigid.velocity.z) * 90f * Time.deltaTime;
                        }
                    }
                }
            }
            else
            {
                enemyrigid.isKinematic = false;
                enemyrigid.transform.LookAt(blockHolder.transform.position);
                enemyrigid.velocity += new Vector3(enemyrigid.transform.forward.x, enemyrigid.transform.forward.y, -enemyrigid.velocity.z) * 90f * Time.deltaTime;
                if (Vector3.Distance(enemyrigid.transform.position, blockHolder.transform.position) < 1.5f)
                {
                    enemyrigid.velocity = Vector3.zero;
                    holdingBlock = true;
                }
            }
        }
    }

    public void ThrowBlock(Rigidbody cube)
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));
        cube.useGravity = false;
        cube.isKinematic = false;
        cube.transform.LookAt(mousePos);
        cube.GetComponent<ThrowableBlock>().fired = true;
        cube.velocity = cube.transform.forward * 5000f * Time.deltaTime;
        holdingBlock = false;
        closest = null;
        rmc.GrabbedObj = null;

    }
}
