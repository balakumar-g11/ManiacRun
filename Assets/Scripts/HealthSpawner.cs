/**
 * File : HealthSpawner.cs
 * Author : Balakumar Marimuthu
 * Use : The script gives the logic to spawn health boosters at regular interval
 **/

using UnityEngine;
using System.Collections;

public class HealthSpawner : MonoBehaviour {

    private float timeSinceLastHealthSpawn = 0.0f;

    private float spawnInterval;

    private GameObject healthKit;

    [HideInInspector]
    public static HealthSpawner instance;

	// Use this for initialization
	void Start () {
        timeSinceLastHealthSpawn = 0.0f;
        spawnInterval = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
        
        timeSinceLastHealthSpawn += Time.deltaTime;

        if (timeSinceLastHealthSpawn >= spawnInterval)
        {
            healthKit = PoolManager.Spawn("HealthKit");
            int randX = Random.Range(-1, 1);
            healthKit.transform.position = new Vector3(randX * 1.8f, 2.5f, (PlayerControl.instance.transform.position.z + 30));

            timeSinceLastHealthSpawn = 0.0f;
        }

        if (this.healthKit != null && this.healthKit.transform.position.z < PlayerControl.instance.transform.position.z)
        {
            PoolManager.Despawn(healthKit);
        }
	}
}
