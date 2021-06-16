using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScript : MonoBehaviour
{
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject createPanel;

    private void Start()
    {
        LoginPlayer();
    }

    public void LoginPlayer()
    {
        loginPanel.SetActive(true);
        createPanel.SetActive(false);
    }

    public void CreatePlayer()
    {
        loginPanel.SetActive(false);
        createPanel.SetActive(true);
    }

}
