using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] Item key;
    Inventory inventory;
    Animator anim;
    [SerializeField] Text text;

    private void Awake()
    {
        text.enabled = false;
        anim = GetComponent<Animator>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void OnTriggerStay(Collider other)
    {
        text.enabled = true;
        text.text = "Need Key to enter";
        if (inventory.ContainsItem(key))
        {
            text.text = "Press E to Open";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("open");
                //inventory.RemoveItem(key);
                OpenDoors();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        text.enabled = false;
    }
    void OpenDoors()
    {
        anim.SetBool("Open", true);
    }
}
