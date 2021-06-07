using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // from an older project of mine
    private List<string> lines = new List<string>();
    public GameObject pannelOptions;
    public GameObject pannelDiaologue;
    public Text currentlines;
    int i;
    private string options;


    void nextline()
    {
        if (i == (lines.Count))
        {
            CloseDiologue();
        }
        else
        {
            currentlines.text = lines[i];
        }
    }
    public void StartDiologue(string[] sentences)
    {
        i = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (pannelDiaologue)
        {
            pannelDiaologue.SetActive(true);
        }
        lines = new List<string>();
        foreach (string sentence in sentences)
        {
            lines.Add(sentence);
        }
        currentlines.text = lines[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            triggerNextLine();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            CloseDiologue();
        }
    }

    private void CloseDiologue()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pannelDiaologue.SetActive(false);
    }

    public void triggerNextLine()
    {
        nextline();
        i++;
    }
}
