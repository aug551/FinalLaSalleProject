using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool gameScene;
    bool isPaused;
    public GameObject[] menu = null;
    public GameObject HUD = null;

    private void Awake()
    {
        isPaused = false;
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            gameScene = true;
        }
        else
        {
            gameScene = false;
        }
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private void Update()
    {
        if (gameScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Continue();
                }
                else
                {
                    Time.timeScale = 0;
                    for (int i = 0; i < menu.Length; i++)
                    {
                        menu[i].SetActive(true);
                    }
                    HUD.SetActive(false);
                    isPaused = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        GetComponent<Animator>().SetBool("Option", true);
        GetComponent<Animator>().SetBool("Niveau", false);
    }

    public void OptionsOut()
    {
        GetComponent<Animator>().SetBool("Option", false);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        Time.timeScale = 1;
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].SetActive(false);
        }
        HUD.SetActive(true);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
