using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;


public class HUDHealthSlider : MonoBehaviour {

    [HideInInspector]
    public float healthVal;

    public Color dangerColor;
    public Color safeColor;

    private Color activeColor;
    private bool isDanger;

    public static HUDHealthSlider instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
        healthVal = 20;
        isDanger = false;
    }

    void OnGUI()
    {
        this.GetComponent<Slider>().value = healthVal;
    }

    public void updateHealthVal(float iHealthVal)
    {
        if (iHealthVal > 15)
            iHealthVal = 15;
        
        healthVal = iHealthVal;

        if (!isDanger && healthVal < 5)
        {
            var fill = this.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            fill.color = dangerColor;
            isDanger = true;
        }
        if (isDanger && healthVal > 5)
        {
            var fill = this.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            fill.color = safeColor;
            isDanger = false;
        }	  
    }
}
