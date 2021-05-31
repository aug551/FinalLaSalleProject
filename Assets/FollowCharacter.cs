using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    [SerializeField] Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }
}
