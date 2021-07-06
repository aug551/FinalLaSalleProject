using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = GameManager.instance;
        }

        instance.GetEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
