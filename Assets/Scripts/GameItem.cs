/**
 * File : GameItem.cs
 * Author : Balakumar Marimuthu
 * Use : The script gives the logic for game items such as coins, obstacles
 **/

using UnityEngine;
using System.Collections;

public class GameItem : MonoBehaviour
{
    public float scoreAdd; //add money if item = coin
    public int decreaseLife; //decrease life if item = obstacle
    public GameObject effectPrefab; // effect when hit item

    private Camera mainCamera;

    [HideInInspector]
    public bool itemActive;

    private GameObject effect;

    public enum TypeItem
    {
        Null, Coin, Obstacle, Obstacle_Roll, LongJump, HealthKit
    }

    public TypeItem typeItem;
    
    public static GameItem instance;

    void Start()
    {
        instance = this;
        mainCamera = Camera.main;
    }
    
    //Set item effect
    public void ItemGet()
    {   
        if (typeItem == TypeItem.Coin)
        {
            HitCoin();
            SoundManager.instance.PlaySound("CoinSound");
        }
        else if (typeItem == TypeItem.Obstacle || typeItem == TypeItem.LongJump)
        {
            if (PlayerControl.instance.isJumping == false)
            {
                HitObstacle();
                SoundManager.instance.PlaySound("HitObstacle");
            }
        }
        else if (typeItem == TypeItem.Obstacle_Roll)
        {
            if (PlayerControl.instance.isRoll == false)
            {
                HitObstacle();
                SoundManager.instance.PlaySound("HitObstacle");
            }
        }
        else if (typeItem == TypeItem.HealthKit)
        {   
            HitHealthKit();
            SoundManager.instance.PlaySound("HitObstacle");
        }
        else 
        {
            if (PlayerControl.instance.isJumping == false)
            {
                HitObstacle();
                SoundManager.instance.PlaySound("HitObstacle");
            }
        }
    }

    //Coin method
    private void HitCoin()
    {
        GameAttribute.gameAttribute.updateCoin((int)this.scoreAdd);

        initEffect(effectPrefab);
        HideCoin();
    }

    //Health Kit
    private void HitHealthKit()
    {
        GameAttribute.gameAttribute.addHealth();
        HideObj();
    }

    //Obstacle method
    private void HitObstacle()
    {
        HideObj();
        GameAttribute.gameAttribute.life -= decreaseLife;
        GameAttribute.gameAttribute.ActiveShakeCamera();
    }

    //Spawn effect method
    private void initEffect(GameObject prefab)
    {
        effect = PoolManager.Spawn(effectPrefab);
        effect.transform.position = PlayerControl.instance.transform.position;
        effect.transform.rotation = Quaternion.identity;
        effect.transform.parent = PlayerControl.instance.transform;
        effect.transform.localPosition = new Vector3(effect.transform.localPosition.x, effect.transform.localPosition.y + 0.5f, effect.transform.localPosition.z);
    }

    public void HideCoin()
    {
        HideObj();
        PoolManager.Despawn(effect);
    }

    public void HideObj()
    {
        this.transform.parent = null;
        this.transform.localPosition = new Vector3(-100, -100, -100);
    }
}
