/**
 * File : GameScore.cs
 * Author : SriVardhini Chinnivakkam Suresh
 * Use : The script updates the game score in the game over screen
 **/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour
{

    private bool scoreShown;

    private int gameScore;

    private int highScore;
    private int totalCoins;

    [HideInInspector]
    public static GameScore instance;

    void Start()
    {
        scoreShown = false;
        gameScore = 0;

        highScore = PlayerPrefs.GetInt("High Score");
        totalCoins = PlayerPrefs.GetInt("Coins");

        totalCoins += GameAttribute.gameAttribute.coin;
        PlayerPrefs.SetInt("Coins", totalCoins);
    }

    void OnGUI()
    {
        this.GetComponent<Text>().text = GameAttribute.gameAttribute.score.ToString();

        if(highScore < GameAttribute.gameAttribute.score)
            PlayerPrefs.SetInt("High Score", GameAttribute.gameAttribute.score);
    }

}
