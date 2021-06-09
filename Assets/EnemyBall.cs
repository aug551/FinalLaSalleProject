using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    [SerializeField] int speed; //{ get; set; }
    private Vector3 direction,leftS,rightS;
    private Rigidbody rigidBody;
    public enum DirectionSelection { left, right };
    public DirectionSelection directionSelection;

    // Start is called before the first frame update
    void Start()
    {
        if (directionSelection == DirectionSelection.left)
        {
            direction = new Vector3(0, 0, 1);
        }
        else if (directionSelection == DirectionSelection.right)
        {

            direction = new Vector3(0, 0, -1);

        }
        else direction = Vector3.up;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(1, 0, 0));

        transform.Translate(direction * speed * Time.deltaTime);
        
    }

    //enum directionEnum
    //{
    //    left,
    //    right,


    //}
}
