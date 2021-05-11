using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] float deltaX = -60;
    [SerializeField] float deltaY = 30;
    [SerializeField] GameObject panel;
    public bool hovered;

    private void Awake()
    {
        panel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (hovered)
        {
            panel.gameObject.SetActive(true);
            panel.transform.position = new Vector2(Input.mousePosition.x + deltaX , Input.mousePosition.y + deltaY);
        }
        else panel.gameObject.SetActive(false);
    }
}
