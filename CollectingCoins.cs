using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingCoins : MonoBehaviour
{
    public int coins;
    public SoundsInGame sound2;                     //zmienna potrzebna do dzwieku

    public void OnTriggerEnter(Collider Col)        //fnkcja kt�ra jest wywo�ywana gdy gracz "zderzy si�" z obiektem
    {
        if(Col.gameObject.tag == "Coin")            //je�li gracz zderzy si� z tagiem Coin to ... 
        {
            coins = coins + 1;                      //do zmiennej dodawny jest jeden coin
            CoinsManager.instance.AddCoins();       //dodawanie monet do CoinsManager
            sound2.coinsSound();                    //dzwiek zebrania coina
            Destroy(Col.gameObject);                //zniszczenie coina
        }
    }

}
