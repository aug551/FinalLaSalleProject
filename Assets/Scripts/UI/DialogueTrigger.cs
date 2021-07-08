using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject text;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Convo started");
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueController>().StartDialogue(dialogue);

    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Hit i to interact");

            text.SetActive(true);


            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Convo started");
                TriggerDialogue();

                
            }
            ////



        }
        
    }


    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}


