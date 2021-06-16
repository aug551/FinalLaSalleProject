using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

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
}
