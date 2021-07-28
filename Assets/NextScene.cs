using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    private GameManager manager;

    void Start()
    {
        if (!manager) manager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if(other.CompareTag("Player"))
        {
            manager.LoadNextLevel();
        }
    }
}
