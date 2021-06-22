using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayKnight : MonoBehaviour
{
    public Animator notreAnimator;
    public Collider frontCollider;
    public Transform player;
    private int times;
    public int attackTimes;
    public Collider swordCollider;


    // Start is called before the first frame update
    void Start()
    {

       times = 0;
        notreAnimator = GetComponent<Animator>();
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //osea q si creo una funcion q inicie la anim en vez q desde el start.
        //notreAnimator.Play("SwordDown");
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            Debug.Log("Ouch");
            
           

                Debug.Log(times);

                //notreAnimator.SetBool("playerVisible", false);

                if (times <= attackTimes)
                {
                    Attack();
                }
        }

    }

    private void Attack()
    {
        notreAnimator.SetTrigger("playerVisible");

        swordCollider.enabled = true;
        
    }

    public void AttackOnce()
    {
        times++;
    }

    public void DamageOn()
    {
        swordCollider.enabled = true;
    }

    public void DamageOff()
    {
        swordCollider.enabled = false;
    }

    
}
