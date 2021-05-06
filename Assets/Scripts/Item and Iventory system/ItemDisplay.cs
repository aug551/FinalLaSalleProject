using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] float deltaX = -60;
    [SerializeField] float deltaY = 30;
    [SerializeField] GameObject panel;
    public bool hovered;
    // Start is called before the first frame update
    private void Awake()
    {
        panel.gameObject.SetActive(false);
    }
    // Update is called once per frame
    private void Update()
    {
        if (hovered)
        {
            panel.gameObject.SetActive(true);
            panel.transform.position = new Vector2(Input.mousePosition.x + deltaX, Input.mousePosition.y + deltaY);
        }
        else panel.gameObject.SetActive(false);
    }
}
