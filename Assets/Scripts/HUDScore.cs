/**
 * File : GameScore.cs
 * Author : SriVardhini Chinnivakkam Suresh
 * Use : The script updates the game score in the HUD
 **/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScore : MonoBehaviour {

    private int currentScore = 0;

    public static HUDScore instance;

	// Use this for initialization
	void Start () {
        instance = this;
        currentScore = 0;
	}

    void OnGUI()
    {
        this.GetComponent<Text>().text = currentScore.ToString();
    }

    public void updateScore(int score)
    {
        currentScore = (int)Mathf.Lerp((float)currentScore, (float)score, Time.deltaTime);
    }	
}
