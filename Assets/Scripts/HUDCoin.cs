/**
 * File : GameScore.cs
 * Author : SriVardhini Chinnivakkam Suresh
 * Use : The script updates the coins collected in the HUD
 **/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDCoin : MonoBehaviour
{

    [HideInInspector]
    public string coinCount;

    public static HUDCoin instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
        coinCount = "0";
    }

    void OnGUI()
    {
        this.GetComponent<Text>().text = coinCount;
    }

    public void updateCoin(int iCoinCount)
    {
        coinCount = iCoinCount.ToString();
    }


}
