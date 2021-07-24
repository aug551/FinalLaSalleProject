using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Santiago Alvarez Cardenas
public class EnemyCube : MonoBehaviour
{
    [SerializeField] int speed;
   /* [SerializeField] */ private Vector3 distanceV;
    [SerializeField] float distance;
    public enum DirectionSelection { left,right,up,down};
    public DirectionSelection directionSelection;
    private Vector3 direction;
    private Vector3 startingPosition;
    private bool distanceCompleted = false;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

        distanceV *= distance;

        if (directionSelection == DirectionSelection.left)
        {
            direction = new Vector3(0, 0, 1);
        }
        else if (directionSelection == DirectionSelection.right)
        {

            direction = new Vector3(0, 0, -1);

        }else if(directionSelection==DirectionSelection.up)
        {
            direction = new Vector3(0, 1, 0);
        }else if(directionSelection==DirectionSelection.down)
        {
            direction = new Vector3(0, -1, 0);
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.Distance(startingPosition, transform.position);

        if(Vector3.Distance(startingPosition, transform.position)<=distance)
        {
            Move();


            

            //if (distanceCompleted == true)
            //{
            //    MoveBack();
            //    distanceCompleted = false;
            //}

        }else {

            distanceCompleted = true;

            this.gameObject.transform.Rotate(0, 90,0);
            transform.Translate((direction * speed) * Time.deltaTime);

        }
        //if (distanceCompleted == true)
        //{
        //    transform.Translate((-direction * speed) * Time.deltaTime);
        //}



    }

    private void Move()
    {
        transform.Translate((direction * speed)*Time.deltaTime);
        
    }

    private void MoveBack()
    {
        transform.Translate((direction * speed) * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Vampire Spotted!");
            //transform.Translate(other.transform.position);
            other.gameObject.GetComponent<CharacterHealth>().TakeDamage(5);
        }
    }

}
