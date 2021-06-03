using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // player = GetComponentInParent<Archer>().Player.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponentInParent<Archer>().Player.transform;
        this.transform.position = player.transform.position;
        this.transform.localPosition = this.transform.localPosition.normalized / 100;
    }

    public Transform GetLocalPos() { return this.transform; }
}
