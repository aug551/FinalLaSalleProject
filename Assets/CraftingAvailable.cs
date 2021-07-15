using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingAvailable : MonoBehaviour
{
    public CraftingUI crafting;
    void Start()
    {
        crafting = GameObject.Find("craftingui").GetComponent<CraftingUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        crafting.canCraft = true;
    }

    private void OnTriggerExit(Collider other)
    {
        crafting.canCraft = false;
    }
}
