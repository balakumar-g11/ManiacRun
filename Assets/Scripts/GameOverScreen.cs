/**
 * File : GameOverScreen.cs
 * Author : SriVardhini Chinnivakkam Suresh
 * Use : The script is used for the controls in game over screen
 **/

using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

    public void OnClickPlay()
    {
        Application.LoadLevel("Game");
    }

    public void onExitButton()
    {
        Application.Quit();
    }
}
