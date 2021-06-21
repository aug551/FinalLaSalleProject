using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ButtonHandler : MonoBehaviour
{
    bool SceneGame;
    bool GamePaused;
    public GameObject[] menu = null;
    public GameObject HUD = null;

    private void Awake()
    {
        GamePaused = false;
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            SceneGame = true;
        }
        else
        {
            SceneGame = false;
        }
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private void Update()
    {
        if (SceneGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GamePaused)
                {
                    UnPause();
                }
                else
                {
                    Time.timeScale = 0;
                    for (int i = 0; i < menu.Length; i++)
                    {
                        menu[i].SetActive(true);
                    }
                    HUD.SetActive(false);
                    GamePaused = true;
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
        GetComponent<Animator>().SetBool("Options", true);
        GetComponent<Animator>().SetBool("Level", false);
    }

    public void SelectLevel()
    {
        GetComponent<Animator>().SetBool("Level", true);
        GetComponent<Animator>().SetBool("Options", false);
    }

    public void RetourOption()
    {
        GetComponent<Animator>().SetBool("Options", false);
    }

    public void RetourNiveau()
    {
        GetComponent<Animator>().SetBool("Level", false);
    }

    public void BackToMainMenu()
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

    public void UnPause()
    {
        Time.timeScale = 1;
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].SetActive(false);
        }

        HUD.SetActive(true);
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
