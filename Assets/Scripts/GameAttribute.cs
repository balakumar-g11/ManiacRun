/**
 * File : GameAttribute.cs
 * Author : Balakumar Marimuthu
 * Use : The script maintains the game attributes such as speed, distance, no of coins. etc.
 **/

using UnityEngine;
using System.Collections;

public class GameAttribute : MonoBehaviour {

    public float starterSpeed = 10.0f; //Speed Character
    public float starterLife = 1; //Life character

    public float speed = 10.0f;

    [HideInInspector]
    public float distance;
    [HideInInspector]
    public int coin;
    [HideInInspector]
    public float life = 3;
    [HideInInspector]
    public bool pause = false;
    [HideInInspector]
    public bool isPlaying;
    [HideInInspector]
    public float multiplyValue;
    [HideInInspector]
    public int score;
    [HideInInspector]
    public float healthVal;
    [HideInInspector]
    public bool isVolumeMute;
    
    public static GameAttribute gameAttribute;

    void Start() {
        gameAttribute = this;
		DontDestroyOnLoad(this);
        speed = starterSpeed;
        pause = false;
        isPlaying = true;
        life = 3;
        multiplyValue = 1;
        healthVal = 20.0f;
        score = 0;
        coin = 0;

        StartCoroutine("monitorHealth");
        StartCoroutine("maintainLife");
    }

    IEnumerator monitorHealth()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (!isPlaying)
                continue;

            healthVal -= Time.deltaTime;

            HUDHealthSlider.instance.updateHealthVal(healthVal);

            if (healthVal == 0)
                break;
        }
    }

    IEnumerator maintainLife()
    {
        while (true)
        {
            yield return new WaitForSeconds(30.0f);

            if (life < 3)
                life += 1;
        }
    }
    
    public void ActiveShakeCamera()
    {
        CameraControl.instance.ActiveShake();
    }

    public void addHealth()
    {
        if (healthVal < 15)
        {
            healthVal = 15;
            HUDHealthSlider.instance.updateHealthVal(healthVal);
        }
    }

    public void updateCoin(int scoreAdd)
    {
       scoreAdd += scoreAdd;
       HUDScore.instance.updateScore(score);

       coin += 1;
       HUDCoin.instance.updateCoin(coin);
    }
    
    public void updateScore(float distance)
    {
        score += (int)(distance * multiplyValue);

        HUDScore.instance.updateScore(score);

        if(multiplyValue < 3.0f)
            multiplyValue += score * 0.01f;

        if(speed < 15.0f)
        {
            speed += 0.005f;
        }
    }

}
