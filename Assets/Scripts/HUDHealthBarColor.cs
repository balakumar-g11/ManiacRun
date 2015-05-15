/**
 * File : HUDHealthBarColor.cs
 * Author : SriVardhini Chinnivakkam Suresh
 * Use : The script changes the color of the HUD health Bar based on player health
 **/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class HUDHealthBarColor : MonoBehaviour {

    public Color dangerColor;
    public Color safeColor;
    
    private Color activeColor;
    private bool isDanger;

    public static HUDHealthBarColor instance;

	// Use this for initialization
	void Start () {
        instance = this;
        isDanger = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDanger && HUDHealthSlider.instance.healthVal < 5)
        {
            var fill = this.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            fill.color = Color.Lerp(safeColor, dangerColor, Time.deltaTime);
            isDanger = true;
        }
        if (isDanger && HUDHealthSlider.instance.healthVal > 5)
        {
            var fill = this.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            fill.color = Color.Lerp(dangerColor, safeColor, Time.deltaTime);
            isDanger = false;
        }	    
	}   
}
