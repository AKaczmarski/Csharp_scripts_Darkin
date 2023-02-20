using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingCoins : MonoBehaviour
{
    public int coins;
    public SoundsInGame sound2;                     //zmienna potrzebna do dzwieku

    public void OnTriggerEnter(Collider Col)        //fnkcja która jest wywo³ywana gdy gracz "zderzy siê" z obiektem
    {
        if(Col.gameObject.tag == "Coin")            //jeœli gracz zderzy siê z tagiem Coin to ... 
        {
            coins = coins + 1;                      //do zmiennej dodawny jest jeden coin
            CoinsManager.instance.AddCoins();       //dodawanie monet do CoinsManager
            sound2.coinsSound();                    //dzwiek zebrania coina
            Destroy(Col.gameObject);                //zniszczenie coina
        }
    }

}
