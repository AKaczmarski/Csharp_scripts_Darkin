using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinningScreenScript : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);                                         //w³¹czenie gameObjectu
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;                                                //odblokowanie czasu w grze
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //ponowne za³adowanie bie¿¹cej sceny
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");                                 //za³adowanie sceny MainMenu
    }
}
