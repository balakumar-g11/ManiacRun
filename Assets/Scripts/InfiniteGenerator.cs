/**
 * File : InfiniteGenerator.cs
 * Author : Balakumar Marimuthu
 * Use : The script generates the infinite world and obstacles
 **/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteGenerator : MonoBehaviour {

    private int loadLevel = 30;

    [HideInInspector]
    public bool isLoadingComplete;

    private List<GameObject> gameItems;
    
    private LinkedList<GameObject> roadList;
    private LinkedList<GameObject> leftBuildingList;
    private LinkedList<GameObject> rightBuildingList;
    
    public static InfiniteGenerator instance;

   	// Use this for initialization
	void Start () {
        instance = this;

        gameItems = new List<GameObject>();
        roadList = new LinkedList<GameObject>();
        leftBuildingList = new LinkedList<GameObject>();
        rightBuildingList = new LinkedList<GameObject>();

        PoolManager.Instance.InitializePrefabPools();

        isLoadingComplete = false;
        
        for (int i = 0; i < loadLevel; i++)
        {
            int randRoad = Random.Range(1, 2);

            GameObject tRoad = PoolManager.Spawn("SamplePrefab");
            tRoad.transform.position = new Vector3(0, 0, (i * tRoad.transform.localScale.z));
            tRoad.transform.parent = this.transform;
            roadList.AddLast(tRoad);

            if (i > 2)
            {
                createGameItems(tRoad.transform.position.z);
            }

            int randBuild = Random.Range(1, 4);
            GameObject leftBuild = PoolManager.Spawn("Building" + randBuild);
            float x = tRoad.transform.localScale.x / 2 + leftBuild.transform.localScale.x / 2 + 0.8f;
            leftBuild.transform.position = new Vector3(x, leftBuild.transform.localScale.y / 2, (i * tRoad.transform.localScale.z));
            leftBuild.transform.parent = this.transform;
            leftBuildingList.AddLast(leftBuild);

            randBuild = Random.Range(1, 4);
            GameObject rightBuild = PoolManager.Spawn("Building" + randBuild);
            x = tRoad.transform.localScale.x / 2 + rightBuild.transform.localScale.x / 2 + 0.8f;
            rightBuild.transform.position = new Vector3((-x), leftBuild.transform.localScale.y / 2, (i * tRoad.transform.localScale.z));
            rightBuild.transform.parent = this.transform;
            rightBuildingList.AddLast(rightBuild);
        }

        isLoadingComplete = true;
	}

    IEnumerable wait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void ReCycle()
    {
        GameObject road = roadList.First.Value;
        GameObject last = roadList.Last.Value;

        roadList.Remove(road);
        PoolManager.Despawn(road);

        GameObject newRoad = PoolManager.Spawn("SamplePrefab");
        newRoad.transform.parent = this.transform;
        roadList.AddLast(newRoad);

        float z = (last.transform.position.z + last.transform.localScale.z);

        newRoad.transform.position = new Vector3(last.transform.position.x, last.transform.position.y, z);

        createGameItems(z);
        
        GameObject leftBuild = leftBuildingList.First.Value;
        leftBuild.transform.parent = this.transform;

        leftBuildingList.Remove(leftBuild);
        PoolManager.Despawn(leftBuild);

        int randBuild = Random.Range(1, 4);
        GameObject newLeftBuild = PoolManager.Spawn("Building" + randBuild);
        float x = newRoad.transform.localScale.x / 2 + newLeftBuild.transform.localScale.x / 2 + 0.8f;
        newLeftBuild.transform.position = new Vector3(x, newLeftBuild.transform.position.y, z);
        leftBuildingList.AddLast(newLeftBuild);

        GameObject rightBuild = rightBuildingList.First.Value;
        rightBuild.transform.parent = this.transform;

        rightBuildingList.Remove(rightBuild);
        PoolManager.Despawn(rightBuild);

        randBuild = Random.Range(1, 4);
        GameObject newrightBuild = PoolManager.Spawn("Building" + randBuild);
        x = newRoad.transform.localScale.x / 2 + newrightBuild.transform.localScale.x / 2 + 0.8f;
        newrightBuild.transform.position = new Vector3((-x), newrightBuild.transform.position.y, z);
        rightBuildingList.AddLast(newrightBuild);

        ReCycleGameItems();
    }

    private void createGameItems(float zLocation)
    {
        int randCoinSet = Random.Range(1, 4);
        int randFixedObstacle = Random.Range(1, 8);
        int randLane = Random.Range(-1, 2);

        for (int i = 1; i <= 2; i++) { 
            if (randFixedObstacle < 4)
            {
                GameObject gObstacle = PoolManager.Spawn("CrossBoard");
                gObstacle.transform.position = new Vector3(randLane * 1.8f, (gObstacle.transform.localScale.y / 2.0f), zLocation + 10.0f / i);
                gameItems.Add(gObstacle);
            }
            else if (randFixedObstacle >= 4 && randFixedObstacle < 7)
            {
                GameObject gObstacle1 = PoolManager.Spawn("CrossBoard");
                gObstacle1.transform.position = new Vector3(randLane * 1.8f, (gObstacle1.transform.localScale.y / 2.0f), zLocation + 10.0f / i);
                gameItems.Add(gObstacle1);

                if (randLane == 0)
                {
                    randLane = Random.Range(0, 1);
                    if (randLane == 0)
                        randLane = 1;
                    else
                        randLane = 1;
                }
                else if (randLane == 1)
                    randLane = 0;
                else if (randLane == -1)
                    randLane = 0;

                GameObject gObstacle2 = PoolManager.Spawn("CrossBoard");
                gObstacle2.transform.position = new Vector3(randLane * 1.8f, (gObstacle2.transform.localScale.y / 2.0f), zLocation + 10.0f / i);
                gameItems.Add(gObstacle2);
            }
        }

        if (randCoinSet < 2)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject gCoin = PoolManager.Spawn("Coin");
                gCoin.transform.position = new Vector3(randLane * 1.8f, (gCoin.transform.localScale.y / 2.0f), (zLocation + (i * 1)));
                gameItems.Add(gCoin);
            }
        }
    }

    private void ReCycleGameItems()
    {
        gameItems.RemoveAll(isVisible);
    }

    private static bool isVisible(GameObject go)
    {
        bool bVisible = false;

        if (go.transform.position.z < Camera.main.transform.position.z)
        {
            bVisible = true;
            PoolManager.Despawn(go);
        }

        return bVisible;
    }
}
