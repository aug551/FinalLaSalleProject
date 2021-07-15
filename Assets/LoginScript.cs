using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScript : MonoBehaviour
{
    GameManager instance = GameManager.instance;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject createPanel;
    [SerializeField] private GameObject offlinePanel;

    private void Start()
    {
        if (instance == null) { instance = GameManager.instance; }
        if (instance.gameObject.GetComponent<SocketClient>().IsOffline)
        {
            NoConnection();
        }
        else
        {
            LoginPlayer();
        }
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

    public void NoConnection()
    {
        loginPanel.SetActive(false);
        createPanel.SetActive(false);
        offlinePanel.SetActive(true);
    }

}
