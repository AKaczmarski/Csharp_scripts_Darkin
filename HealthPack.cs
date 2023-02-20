using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                       //dost�p do UI

public class HealthPack : MonoBehaviour
{
    public GameObject healthBar;
    public float health = 0.3f;
    public SoundsInGame sounds;                             //odwo�anie do skryptu SoundsInGame

    void Start()
    {
        
        healthBar.GetComponent<Slider>().value = health;   //uzyskanie dost�pu do komponentu Slider 
        sounds.forestSound();                               //w��czenie dzwiekow
    }

    //
    public WinningScreenScript WinningScreenScript;        //potrzebne do pojawienia screena z wygranej        
    public LosingScreanScript LosingScreanScript;           //potrzebne do pojawienia screena z przegranej
                                                            

    void forestmusicoff()                                   //funkcja kt�ra wyy��cza dzwieki lasu
    {
        sounds.forestSoundOff();
    }

    void castlemusicoff()                                   //funkcja kt�ra wy��cza dzwieki zamku
    {
        sounds.silence();
    }
    



    public void OnTriggerEnter(Collider Coll)               //fnkcja kt�ra jest wywo�ywana gdy gracz "zderzy si�" z obiektem
    {
        if (Coll.gameObject.tag == "MedKit")                //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag MedKit to ...
        {
            health += 0.2f;                                 //doda 0.2 �ycia
            health = Mathf.Clamp(health, 0, 1);             //maksymalny range poziomu zdrowia od 0 do 1 aby apteczki nie leczy�y gracza do 110 hp z 90 tylko do maks warto�ci 100
            healthBar.GetComponent<Slider>().value = health;    //uzyskanie dost�pu do komponentu slider oraz oddzia�ywane na niego (podniesienie paska zdrowia)
            sounds.medkitSound();                           //dzwiek podniesienia medkita
            Destroy(Coll.gameObject);                       //zniszczenie podniesionego przedmiotu
            
        }

        else if (Coll.gameObject.tag == "Falldmg")            //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Falldmg to ...
        {
            health -= 0.4f;                                   //odejmie 0.4 �ycia
            health = Mathf.Clamp(health, 0, 1);               //maks range paska �ycia
            healthBar.GetComponent<Slider>().value = health;  //odzia�ywanie na pasek zdrowia
            Destroy(Coll.gameObject);                         //zniszczenie podniesionego przedmiotu
        }

        else if (Coll.gameObject.tag == "DMG")                //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag DMG to ...
        {
            health -= 0.3f;
            health = Mathf.Clamp(health, 0, 1);
            sounds.spikeHitSound();                         //dzwiek obrazen od spik�w
            healthBar.GetComponent<Slider>().value = health;
            if (health == 0f)                               //je�li poziom zdrowia jest r�wny 0 to ....
            {
                Time.timeScale = 0;                         //zatrzymuje si� gra
                Cursor.visible = true;                      //kursor jest widoczny
                Cursor.lockState = CursorLockMode.None;     //kursor jest odblokowany
                sounds.deathfromzombieSound();              //dzwiek smierci od zombiego
                LosingScreanScript.Setup();                 //za��czenie si� screena z przegranej
            }
        }

        else if (Coll.gameObject.tag == "Roar")              //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Roar to ...
        {
            sounds.zombieroarSound();                       //dzwiek roar
        }

        else if (Coll.gameObject.tag == "Horde")            //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Horde to ...
        {
            sounds.zombiehordeSound();                      //dzwiek hordy
        }

        else if (Coll.gameObject.tag == "DMG2")             //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Roar to ...
        {
            health -= 0.5f;                                 //-0.5 hp
            health = Mathf.Clamp(health, 0, 1);             //maks range hp
            sounds.zombieBiteSound();                       //dzwiek
            healthBar.GetComponent<Slider>().value = health;//reakcja slidera na dostane obra�enia
            if (health == 0f)                               //je�li hp jest 0
            {
                Time.timeScale = 0;                         //zatrzymanie ekranu
                Cursor.visible = true;                      //widoczny kursor
                Cursor.lockState = CursorLockMode.None;     //odblokowany kursor
                sounds.deathfromzombieSound();              //dzwiek
                LosingScreanScript.Setup();                 //screen przegranej
            }
        }
        else if (Coll.gameObject.tag == "Fall")             //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Fall to ...
        {
            sounds.falldeathSound();                        //dzwiek
        }

        else if (Coll.gameObject.tag == "Death")            //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Death to ...
        {
            health -= 1f;                                   //- ca�e zycie
            health = Mathf.Clamp(health, 0, 1);             //maks range hp
            healthBar.GetComponent<Slider>().value = health;    //reakcja slidera
            Time.timeScale = 0;                             //zatrzymanie gry
            Cursor.visible = true;                          //widzialny kursor
            Cursor.lockState = CursorLockMode.None;         //odblokowany kursor
            LosingScreanScript.Setup();                     //pokazanie screena przegranej
        }

        else if (Coll.gameObject.tag == "CastleSong")       //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag CastleSong to ...
        {
            forestmusicoff();                               //koniec dzwieku lasu
            sounds.castleSound();                           //dzwiek zamku
        }

        else if (Coll.gameObject.tag == "Silence")          ////je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Silence to ...
        {
            castlemusicoff();                               //wy��czenie dzwiek�w zamku
        }

        else if (Coll.gameObject.tag == "Vents")            //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Vents to ...
        {
            sounds.ventsSound();                            //dzwiek zbicia venta
        }

        else if (Coll.gameObject.tag == "Finish")           //je�li obiekt z tym skryptem zderzy si� z obiektem k�trym ma tag Finish to ...
        {
            Time.timeScale = 0;                             //zatryzmanie gry
            Cursor.visible = true;                          //widzialny kursor
            Cursor.lockState = CursorLockMode.None;         //odblokowany kursor
            sounds.winGameSound();                          //dzwiek wygrania gry
            WinningScreenScript.Setup();                    //screen z wygranej gry

        }
    }

}
