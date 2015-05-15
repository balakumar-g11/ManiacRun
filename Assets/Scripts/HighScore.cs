/**
 * File : GameScore.cs
 * Author : SriVardhini Chinnivakkam Suresh
 * Use : The script updates the game score in the game over screen
 **/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScore : MonoBehaviour
{
    void OnGUI()
    {
        string showText;

        if (this.name == "HighScoreStat")
            showText = PlayerPrefs.GetInt("High Score").ToString();
        else if (this.name == "CoinStat")
            showText = PlayerPrefs.GetInt("Coins").ToString();
        else
            showText = "---";

        this.GetComponent<Text>().text = showText;
    }

}
