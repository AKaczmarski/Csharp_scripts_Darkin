using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                       //dostêp do UI

public class HealthPack : MonoBehaviour
{
    public GameObject healthBar;
    public float health = 0.3f;
    public SoundsInGame sounds;                             //odwo³anie do skryptu SoundsInGame

    void Start()
    {
        
        healthBar.GetComponent<Slider>().value = health;   //uzyskanie dostêpu do komponentu Slider 
        sounds.forestSound();                               //w³¹czenie dzwiekow
    }

    //
    public WinningScreenScript WinningScreenScript;        //potrzebne do pojawienia screena z wygranej        
    public LosingScreanScript LosingScreanScript;           //potrzebne do pojawienia screena z przegranej
                                                            

    void forestmusicoff()                                   //funkcja która wyy³¹cza dzwieki lasu
    {
        sounds.forestSoundOff();
    }

    void castlemusicoff()                                   //funkcja która wy³¹cza dzwieki zamku
    {
        sounds.silence();
    }
    



    public void OnTriggerEnter(Collider Coll)               //fnkcja która jest wywo³ywana gdy gracz "zderzy siê" z obiektem
    {
        if (Coll.gameObject.tag == "MedKit")                //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag MedKit to ...
        {
            health += 0.2f;                                 //doda 0.2 ¿ycia
            health = Mathf.Clamp(health, 0, 1);             //maksymalny range poziomu zdrowia od 0 do 1 aby apteczki nie leczy³y gracza do 110 hp z 90 tylko do maks wartoœci 100
            healthBar.GetComponent<Slider>().value = health;    //uzyskanie dostêpu do komponentu slider oraz oddzia³ywane na niego (podniesienie paska zdrowia)
            sounds.medkitSound();                           //dzwiek podniesienia medkita
            Destroy(Coll.gameObject);                       //zniszczenie podniesionego przedmiotu
            
        }

        else if (Coll.gameObject.tag == "Falldmg")            //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Falldmg to ...
        {
            health -= 0.4f;                                   //odejmie 0.4 ¿ycia
            health = Mathf.Clamp(health, 0, 1);               //maks range paska ¿ycia
            healthBar.GetComponent<Slider>().value = health;  //odzia³ywanie na pasek zdrowia
            Destroy(Coll.gameObject);                         //zniszczenie podniesionego przedmiotu
        }

        else if (Coll.gameObject.tag == "DMG")                //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag DMG to ...
        {
            health -= 0.3f;
            health = Mathf.Clamp(health, 0, 1);
            sounds.spikeHitSound();                         //dzwiek obrazen od spików
            healthBar.GetComponent<Slider>().value = health;
            if (health == 0f)                               //jeœli poziom zdrowia jest równy 0 to ....
            {
                Time.timeScale = 0;                         //zatrzymuje siê gra
                Cursor.visible = true;                      //kursor jest widoczny
                Cursor.lockState = CursorLockMode.None;     //kursor jest odblokowany
                sounds.deathfromzombieSound();              //dzwiek smierci od zombiego
                LosingScreanScript.Setup();                 //za³¹czenie siê screena z przegranej
            }
        }

        else if (Coll.gameObject.tag == "Roar")              //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Roar to ...
        {
            sounds.zombieroarSound();                       //dzwiek roar
        }

        else if (Coll.gameObject.tag == "Horde")            //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Horde to ...
        {
            sounds.zombiehordeSound();                      //dzwiek hordy
        }

        else if (Coll.gameObject.tag == "DMG2")             //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Roar to ...
        {
            health -= 0.5f;                                 //-0.5 hp
            health = Mathf.Clamp(health, 0, 1);             //maks range hp
            sounds.zombieBiteSound();                       //dzwiek
            healthBar.GetComponent<Slider>().value = health;//reakcja slidera na dostane obra¿enia
            if (health == 0f)                               //jeœli hp jest 0
            {
                Time.timeScale = 0;                         //zatrzymanie ekranu
                Cursor.visible = true;                      //widoczny kursor
                Cursor.lockState = CursorLockMode.None;     //odblokowany kursor
                sounds.deathfromzombieSound();              //dzwiek
                LosingScreanScript.Setup();                 //screen przegranej
            }
        }
        else if (Coll.gameObject.tag == "Fall")             //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Fall to ...
        {
            sounds.falldeathSound();                        //dzwiek
        }

        else if (Coll.gameObject.tag == "Death")            //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Death to ...
        {
            health -= 1f;                                   //- ca³e zycie
            health = Mathf.Clamp(health, 0, 1);             //maks range hp
            healthBar.GetComponent<Slider>().value = health;    //reakcja slidera
            Time.timeScale = 0;                             //zatrzymanie gry
            Cursor.visible = true;                          //widzialny kursor
            Cursor.lockState = CursorLockMode.None;         //odblokowany kursor
            LosingScreanScript.Setup();                     //pokazanie screena przegranej
        }

        else if (Coll.gameObject.tag == "CastleSong")       //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag CastleSong to ...
        {
            forestmusicoff();                               //koniec dzwieku lasu
            sounds.castleSound();                           //dzwiek zamku
        }

        else if (Coll.gameObject.tag == "Silence")          ////jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Silence to ...
        {
            castlemusicoff();                               //wy³¹czenie dzwieków zamku
        }

        else if (Coll.gameObject.tag == "Vents")            //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Vents to ...
        {
            sounds.ventsSound();                            //dzwiek zbicia venta
        }

        else if (Coll.gameObject.tag == "Finish")           //jeœli obiekt z tym skryptem zderzy siê z obiektem kótrym ma tag Finish to ...
        {
            Time.timeScale = 0;                             //zatryzmanie gry
            Cursor.visible = true;                          //widzialny kursor
            Cursor.lockState = CursorLockMode.None;         //odblokowany kursor
            sounds.winGameSound();                          //dzwiek wygrania gry
            WinningScreenScript.Setup();                    //screen z wygranej gry

        }
    }

}
