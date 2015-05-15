/**
 * File : MainMenuStart.cs
 * Author : SriVardhini Chinnivakkam Suresh
 * Use : The script is used for the controls in game start screen
 **/

using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour
{

    public void OnClickPlay()
    {
        Application.LoadLevel("Game");
    }

    public void OnClickHighScore()
    {
        Application.LoadLevel("HighScore");
    }

    public void onClickBack()
    {
        Application.LoadLevel("Start");
    }

    public void onExitButton()
    {
        Application.Quit();
    }
}
