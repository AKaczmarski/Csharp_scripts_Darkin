using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;

    public Text scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;                                //odniesienie do bie¿¹cego obiektu i przechowanie w zmiennej instance;
    }

    void Start()
    {
        scoreText.text = score.ToString() + " COINS";   //ustawienie scoreText na score, która staje siê Stringiem z dopiskiem COINS

    }

    public void AddCoins()
    {
        score += 1;                                     //dodanie coina
        scoreText.text = score.ToString() + " COINS";   //ustawienie scoreText na score, która staje siê Stringiem z dopiskiem COINS


    }
}
