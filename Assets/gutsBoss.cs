using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class gutsBoss : MonoBehaviour
{

    public Animator animator;
    public NavMeshAgent agent;
    private Transform player;
    private float distance;
    public Collider collHit;
    float xPos;
    //public GameObject vampire;



    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        distance = Vector3.Distance(player.position, transform.position);
        xPos= this.transform.position.x;
               

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        Vector3 destination = player.position;
        //agent.transform.Position.x = xPos;

        if (distance<=7)
        {
            Debug.Log("Now I can see the bat");
            FightTheBat();
            agent.destination = player.transform.position;
        }if(distance<=2)
        {
            animator.SetBool("attack", true);


        }
    }

    public void FightTheBat()//Vector3 startPos, float radious
    {
        animator.SetBool("batIsVisible",true);
        

        
    }

  


}
