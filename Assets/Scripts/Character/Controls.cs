using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    GameManager instance;
    public GameObject inventoryPanel;
    public GameObject crafitngPanel;
    CanvasGroup group;
    [SerializeField] GameObject pausePanel;

    public bool isCraftingOpen = false;

    private bool isPaused = false;

    public bool isActive = false;

    private void Awake()
    {
        group = inventoryPanel.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if(instance == null)
        {
            instance = GameManager.instance;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            Inventory();
        }
        if (Input.GetButtonDown("Equipment"))
        {
            Equipment();
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                isPaused = true;
            }
        }
    }

    void Inventory()
    {
        Toggle(group);
    }
    void Equipment()
    {
        Toggle(EquipmentManager.Instance.Group);
    }
    public void Toggle(CanvasGroup group)
    {
        if (group.alpha == 0)
        {
            group.alpha = 1;
            group.blocksRaycasts = true;
            group.interactable = true;
            isCraftingOpen = true;
        }
        else
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
            group.interactable = false;
            isCraftingOpen = false;
        }
    }

}
