using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);                 //w��czenie pauseMenu
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))       //je�li wci�niesz escape w��czy si� panel pauzy
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);                            //w�aczenie pauzy
        Time.timeScale = 0f;                                  //zatrzymanie czasu w grze
        Cursor.visible = true;                                //widzialny kursor
        Cursor.lockState = CursorLockMode.None;               //odblokowanie kursora
        isPaused = true;                                      //pauza
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);                            //wy��czanie pauzy
        Time.timeScale = 1f;                                   //odblokanie czasu w grze
        Cursor.visible = false;                                //widzlany kursor
        isPaused = false;                                      //koniec pauzy
    }

    

    public void ExitButton()
    {
        Time.timeScale = 1f;                                    //odblokwanie czasu w grze
        SceneManager.LoadScene("MainMenu");                     //za�adowanie sceny Menu
    }
}
