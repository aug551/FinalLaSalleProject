using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public int i = 0;
    public Text text;

    private void Update()
    {
        ModifyInstructions();
    }

    public void ModifyInstructions()
    {
        if (i == 0)
        {
            text.text = "Pick up items";
        }
        else if (i == 1)
        {
            text.text = "Go to crafting station and craft a key (Press i)";
        }
        else if (i == 2)
        {
            text.text = "Get out of the room";
        }
        else if (i == 3)
        {
            text.text = "Double jump over the wall";
        }
        else if (i == 4)
        {
            text.text = "Climb the walls using space bar";
        }
        else if (i == 5)
        {
            text.text = "Kill zombie";
        }
        else if (i == 6)
        {
            text.text = "Right click to make blocks come towards you. Use this to make it to the hole in the ceiling";
        }

    }
}
