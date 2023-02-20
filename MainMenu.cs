using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                                              //potrzebne do prze��czanie si� miedzy scenami

public class MainMenu : MonoBehaviour
{
    public SoundsInGame sounds2;                                                //potrzebne do dzwi�ku
    public void Start()
    {
        sounds2.menuSound();                                                    //dzwiek
        Time.timeScale = 1.0f;                                                  //odblokwanie czasu w grze
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //�adowanie nast�pnej sceny w kompilacji przy u�yciu klasy SceneMenager
                                                                                //GetActiveScene s�u�y do pobieraniaa aktywnej sceny / buildIndex pobiera indeks sceny     
                                                                                //LoadScene �aduje scene poprzez dodanie 1;
    }

    public void Quit()
    {
        Application.Quit();                                                     //Wyjscie z gry
        Debug.Log("Quit");                                                      //Sprawdzenie czy gra poprawnie si� zamyka w edytorze => efekt w konsoli napis Quit
    }
}
