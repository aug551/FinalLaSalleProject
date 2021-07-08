using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    private List<GameObject> listOfEnemies = new List<GameObject>();

    public Player Player { get => player; set => player = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;

        DontDestroyOnLoad(this);
    }

    internal void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        this.player.hp = player.GetComponent<CharacterHealth>().currentHealth;

        GetComponentInChildren<SocketClient>().SaveGame();
    }

    //internal void LoadGame()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    player.GetComponent<CharacterHealth>().currentHealth = this.player.hp;
    //}

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GetEnemies()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name.StartsWith("Level"))
        {
            Debug.Log("islevel");
            listOfEnemies.Clear();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                listOfEnemies.Add(enemy);
            }
        }
        Debug.Log("Not a level.");
    }

}
