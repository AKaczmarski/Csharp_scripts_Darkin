using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                                              //potrzebne do prze³¹czanie siê miedzy scenami

public class MainMenu : MonoBehaviour
{
    public SoundsInGame sounds2;                                                //potrzebne do dzwiêku
    public void Start()
    {
        sounds2.menuSound();                                                    //dzwiek
        Time.timeScale = 1.0f;                                                  //odblokwanie czasu w grze
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //³adowanie nastêpnej sceny w kompilacji przy u¿yciu klasy SceneMenager
                                                                                //GetActiveScene s³u¿y do pobieraniaa aktywnej sceny / buildIndex pobiera indeks sceny     
                                                                                //LoadScene ³aduje scene poprzez dodanie 1;
    }

    public void Quit()
    {
        Application.Quit();                                                     //Wyjscie z gry
        Debug.Log("Quit");                                                      //Sprawdzenie czy gra poprawnie siê zamyka w edytorze => efekt w konsoli napis Quit
    }
}
