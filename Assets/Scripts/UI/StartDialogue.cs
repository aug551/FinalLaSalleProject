using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public string npcName;
    [TextArea(3, 10)]
    public string[] Phrases;
    [TextArea(3, 10)]
    public string Options;

}
