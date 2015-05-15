/**
 * File : RoadRecycle.cs
 * Author : Balakumar Marimuthu
 * Use : The script triggers the event to recycle the game world objects
 **/

using UnityEngine;
using System.Collections;

public class RoadRecycle : MonoBehaviour {

    private GameObject playerTransform;
    private InfiniteGenerator groundControl;

    void Start()
    {
        playerTransform = GameObject.Find("m01_casualwear_01_h");
        groundControl = this.transform.parent.GetComponent<InfiniteGenerator>();
    }

    public void Update()
    {
        if (playerTransform.transform.position.z >= this.transform.position.z + 10.0f)
        {
            groundControl.ReCycle();
        }
    }
}
